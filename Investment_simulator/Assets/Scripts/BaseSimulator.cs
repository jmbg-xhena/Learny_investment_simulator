using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;

using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Xml.Linq;
using System;
using System.Linq;
using System.Runtime.InteropServices;
#if !UNITY_WEBGL
using ValidacionMenu;
#endif
#if PLATFORM_ANDROID
using UnityEngine.Android;
#endif
public class BaseSimulator : MonoBehaviour {

	public enum TypeScoreList { NUMERIC, ALPHABETIC };
	public enum LanguageList {Spanish, English, Portuguese};
	public enum ConfigureLanguageList {Spanish_English, Spanish_Portuguese, English_Portuguese};
    public enum ComplexModes {U,K12}

	public bool SecurityModule = false;
	public bool LanguageEnabled = false;
	public bool LanguageFirstTime = false;
	public bool Aula = false;
	public bool MonoUser = false;
    public bool ltiAula = false;

	public string Language = "Spanish";
	public string AllowedUrl = "ielicenseserver.herokuapp.com";

    //public string BundleId = "air.com.cloudlabs.mru.v2";
    public string BundleId = "com.cloudlabs.triangulos";
    public string UrlServer = "https://ielicenseserver.herokuapp.com/validacion/verificacion_licencia";
	public string UrlScore = "https://ielicenseserver.herokuapp.com/lti_launcher/setScore";
	public string UrlAula = "";
    public string WebGLUrl = "";
    public string menuName = "menu_scene";

	public GameObject _alertPrefab;
	public GameObject _loginPanelPrefab;
	public GameObject _languagePanelPrefab;

	public GameObject _alert;
	public GameObject _loginPanel;
	public GameObject _languagePanel;

    public ComplexModes complexMode;
	public TypeScoreList typeScore;
	public int[] NumericScore = new int[2]{0,5};
	public QualificationElement[] AlphabeticalScore = new QualificationElement[5]
														{
															new QualificationElement(){name = "F", maxValue=0.6f},
															new QualificationElement(){name = "D", maxValue=0.7f},
															new QualificationElement(){name = "C", maxValue=0.8f},
															new QualificationElement(){name = "B", maxValue=0.9f},
															new QualificationElement(){name = "A", maxValue=1.0f}
														};
	public float MinimalSuccessScore = 0.6f;
    public float maxScore = 5f;

    private XmlDocument xmlTexts;
	private XmlDocument xmlInfo;
	private XmlDocument xmlQuestions;

	private string _urlTexts;
	private string _urlInfo;
	private string _urlAula;

    //seguridad nueva datos
    private string[] cmd;
    private string correoMono;
    private string nombreMono;
    private string instituMono;
    private string mono_password;
    private bool isLTI = false;

#if UNITY_WEBGL
    [DllImport("__Internal")]
    private static extern bool isMobile();
#endif
    public string webGLCode = "";

    public delegate void LoginActionDelegate();
    public event LoginActionDelegate LoginActionEvent;

    private bool requirePermissions = false;

    // Use this for initialization
    public void startSimulator(){
        //
        // -- Tasks to do:
        // -- Load XMLs.
        // -- Check security.
        // -- Check language.
        // -- Login.
#if UNITY_IPHONE
        Aula = true;
#endif
        if (Manager.Instance.logged == false) {
			Manager.Instance.globalMenuName = menuName;
			Manager.Instance.globalAula = Aula;
			Manager.Instance.globalMonoUser = MonoUser;
			Manager.Instance.globalUrlAula = UrlAula;
			Manager.Instance.globalTypeScore = typeScore;
			Manager.Instance.globalNumericScore = NumericScore;
			Manager.Instance.globalAlphabeticalScore = AlphabeticalScore;
			Manager.Instance.globalMinimalSuccessScore = MinimalSuccessScore;
			Manager.Instance.globalUrlScore = UrlScore;
            Manager.Instance.globalMaxLimit = maxScore;
            Manager.Instance.globalComplexMode = complexMode;
            Manager.Instance.globalLanguageEnabled = LanguageEnabled;
            if (Manager.Instance.globalMonoUser == true)
            {
                Manager.Instance.globalAula = true;
            }
            Manager.Instance.globalLtiAula = ltiAula;
            //PARAMETERS FOR LTI
#if UNITY_WEBGL

            Manager.Instance.globalWebGLUrl = WebGLUrl;

            string[] _parametrosLTI = Application.absoluteURL.Split('?');

            if (_parametrosLTI.Length > 1)
            {
                string[] arrayParametros = _parametrosLTI[1].Split('&');

                Manager.Instance.globalUser = WWW.UnEscapeURL(arrayParametros[0]);
                Manager.Instance.globalCourse = WWW.UnEscapeURL(arrayParametros[1]);
                Manager.Instance.globalCourseId = WWW.UnEscapeURL(arrayParametros[2]);
                Manager.Instance.globalInstitution = WWW.UnEscapeURL(arrayParametros[3]);
                Manager.Instance.globalLTIParameters = WWW.UnEscapeURL(arrayParametros[4]);
                if(ltiAula == true){
                    Manager.Instance.globalUserId = UnityWebRequest.UnEscapeURL(arrayParametros[5]);
                }
                isLTI = true;
            }

            try
            {
                Manager.Instance.globalWebGLMobile = isMobile();
            }
            catch (Exception _error)
            {

            }
#endif

#if PLATFORM_ANDROID


            if (Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
            {
                // The user authorized use of the microphone.
                requirePermissions = false;
            }
            else
            {
                requirePermissions = true;
                Permission.RequestUserPermission(Permission.ExternalStorageWrite);
            }
#endif

            //EXTERNAL PARAMETERS
            StartCoroutine(onAppInvoke());
        }
	}

    public static string safeCallStringMethod(AndroidJavaObject javaObject, string methodName, params object[] args)
    {

        if (args == null) args = new object[] { null };
        IntPtr methodID = AndroidJNIHelper.GetMethodID<string>(javaObject.GetRawClass(), methodName, args, false);
        jvalue[] jniArgs = AndroidJNIHelper.CreateJNIArgArray(args);
        try
        {
            IntPtr returnValue = AndroidJNI.CallObjectMethod(javaObject.GetRawObject(), methodID, jniArgs);
            if (IntPtr.Zero != returnValue)
            {
                var val = AndroidJNI.GetStringUTFChars(returnValue);
                AndroidJNI.DeleteLocalRef(returnValue);
                return val;
            }
        }
        finally
        {
            AndroidJNIHelper.DeleteJNIArgArray(args, jniArgs);
        }

        return null;

    }

	private IEnumerator onAppInvoke() {

        if (requirePermissions == true)
        {
            yield return new WaitForSeconds(1f);
        }
        else
        {
            yield return null;
        }

#if UNITY_ANDROID
        try
        {
            AndroidJavaClass UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            if (currentActivity != null)
            {
                AndroidJavaObject intent = currentActivity.Call<AndroidJavaObject>("getIntent");
                if (intent != null)
                {
                    if (safeCallStringMethod(intent, "getStringExtra", "LTI") != null)
                    {
                        isLTI = true;
                        Manager.Instance.globalUser = safeCallStringMethod(intent, "getStringExtra", "login_user");
                        Manager.Instance.globalCourse = safeCallStringMethod(intent, "getStringExtra", "login_course");
                        Manager.Instance.globalCourseId = safeCallStringMethod(intent, "getStringExtra", "login_id_curso");
                        Manager.Instance.globalInstitution = safeCallStringMethod(intent, "getStringExtra", "login_institute");
                        Manager.Instance.globalLTIParameters = safeCallStringMethod(intent, "getStringExtra", "lti_params");
                        if (ltiAula == true)
                        {
                            Manager.Instance.globalUserId = safeCallStringMethod(intent, "getStringExtra", "login_id_user");
                        }
                    }
                    else if (MonoUser == true)
                    {
                        /*nombreMono = safeCallStringMethod(intent, "getStringExtra", "nombre");
                        instituMono = safeCallStringMethod(intent, "getStringExtra", "institucion");
                        correoMOno = safeCallStringMethod(intent, "getStringExtra", "correo");*/
                        Manager.Instance.globalMonoUserName = safeCallStringMethod(intent, "getStringExtra", "nombre");
                        Manager.Instance.globalMonoUserInstitution = safeCallStringMethod(intent, "getStringExtra", "institucion");
                        Manager.Instance.globalMonoUserEmail = safeCallStringMethod(intent, "getStringExtra", "correo");
                        Manager.Instance.globalMonoPassword = safeCallStringMethod(intent, "getStringExtra", "password");
                    }
                }

            }
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
#elif (UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN || UNITY_IOS)
        //string[] args = Environment.GetCommandLineArgs();
        //if (args.Length > 1)
        //{
        if (MonoUser == true)
        {
            

            //Nueva seguridad traida de datos
            cmd = System.Environment.CommandLine.Split(',');
            try
            {
                if (cmd.Length > 5)
                {
                    //Datos del LTI
                    Manager.Instance.globalMonoUserName = nombreMono = cmd[3];
                    Manager.Instance.globalMonoUserInstitution = instituMono = cmd[4];
                    Manager.Instance.globalMonoUserEmail = correoMono = cmd[6];
                    Manager.Instance.globalMonoPassword = mono_password = cmd[7];
                }

            }
            catch
            {


            }

        }
        else
        {
            //license_number = args[1];
        }

        //}
#endif

        _urlAula = "";
#if UNITY_IPHONE
        _urlAula = Application.persistentDataPath + "/" + "aula_conf.xml"; 
        loadAulaXML();
#elif UNITY_ANDROID
        string[] potentialDirectories = new string[]
        {
            "/storage/emulated/0/",
            "/mnt/sdcard/",
            "/storage/sdcard/",
            "/storage/sdcard0/",
            "/storage/sdcard1/",
            "/storage/",
            "/sdcard/",
        };

        for (int i = 0; i < potentialDirectories.Length; i++)
        {
            try
            {
                //... (code inside actual for sentence)
                if (Directory.Exists(potentialDirectories[i]))
                {
                    Debug.Log(potentialDirectories[i]);
                    Manager.Instance.globalAndroidUrl = potentialDirectories[i];
                    DirectoryInfo currentDirectory = new DirectoryInfo(potentialDirectories[i]);
                    _urlAula = ProcessDirectory(currentDirectory);
                    break;
                }
            }
            catch (Exception error)
            {
                Debug.Log(error);
                // no go directly to loadAulaXML
            }
        }

        if (_urlAula != "")
        {
            loadAulaXML();
        }
        else
        {
            //Manager.Instance.globalAndroidUrl = "not found";
            StartCoroutine(loadLanguageXML(true, true));
        }
#elif UNITY_EDITOR
        _urlAula = Application.dataPath + "/" + "../../aula_conf.xml";
        loadAulaXML();
#elif UNITY_STANDALONE_OSX
        _urlAula = Application.dataPath + "/" + "../../aula_conf.xml"; 
        loadAulaXML();
#elif UNITY_STANDALONE_WIN
        _urlAula = Application.dataPath + "/" + "../../../../aula_conf.xml";
        loadAulaXML();
#elif UNITY_WEBGL
        loadAulaXML();
#endif
    }

    public void loadAulaXML()
    {
#if UNITY_WEBGL
        Debug.Log("WebGL mode");
        StartCoroutine(loadLanguageXML(true, true));
        return;
#else
        try
        {
            XmlDocument newXml = new XmlDocument();
            newXml.Load(_urlAula);
            XmlNodeList _list = newXml.ChildNodes[0].ChildNodes;

            //LOAD SCORE CONFIG FROM XML
            Manager.Instance.globalAulaDoc = newXml;
            //---
#if UNITY_IPHONE
            StartCoroutine(loadLanguageXML(true, true));
            return;
#endif
            for (int i = 0; i < _list.Count; i++)
            {
                if (_list[i].Name.Equals("url_aula"))
                {
                    UrlAula = _list[i].InnerText;
                    _urlAula = _urlAula + " - " + UrlAula;
                    Manager.Instance.globalUrlAula = UrlAula;
                }
                if (_list[i].Name.Equals("aula"))
                {
                    Debug.Log(_list[i].InnerText);
                    if (_list[i].InnerText.Equals("true"))
                    {
                        Aula = true;
                        Manager.Instance.globalAula = Aula;
                    }
                    else
                    {
                        Aula = false;
                        Manager.Instance.globalAula = Aula;
                        //condición para dejar siempre el aula en monousuario
                        if (Manager.Instance.globalMonoUser == true)
                        {
                            Manager.Instance.globalAula = true;
                        }
                    }
                    _urlAula = _urlAula + " - " + Aula.ToString();
                }
            }
            Debug.Log("Hello - Sim-xml true");
            StartCoroutine(loadLanguageXML(true, true));
        }
        catch (Exception e)
        {
            Debug.Log("file not exist" + e);
            Debug.Log("Hello - Sim-xml catch" + e);
            StartCoroutine(loadLanguageXML(true, true));
        }
#endif
        }


    public void scoreConfig()
    {
        if (Manager.Instance.globalAulaDoc.SelectSingleNode("/data/general/score") != null)
        {
            XmlNode scoreObj = Manager.Instance.globalAulaDoc.SelectSingleNode("/data/general/score");

            MinimalSuccessScore = float.Parse(scoreObj.Attributes["minimalSuccessScore"].Value);
            UrlScore = scoreObj.Attributes["urlScore"].Value;

            //numeric data
            XmlNode numericObj = scoreObj.SelectSingleNode("numeric");
            XmlNode alphaObj = scoreObj.SelectSingleNode("alphabetical");

            if (numericObj.Attributes["selected"].Value == "true")
            {
                typeScore = TypeScoreList.NUMERIC;
            }
            if (alphaObj.Attributes["selected"].Value == "true")
            {
                typeScore = TypeScoreList.ALPHABETIC;
            }

            NumericScore = new int[numericObj.ChildNodes.Count];
            for (int i = 0; i < numericObj.ChildNodes.Count; i++)
            {
                NumericScore[i] = int.Parse(numericObj.ChildNodes[i].InnerText);
            }

            Debug.Log(NumericScore[0] + " - " + NumericScore[1]);

            AlphabeticalScore = new QualificationElement[alphaObj.ChildNodes.Count];
            for (int i = 0; i < alphaObj.ChildNodes.Count; i++)
            {
                AlphabeticalScore[i].name = alphaObj.ChildNodes[i].InnerText;
                AlphabeticalScore[i].maxValue = float.Parse(alphaObj.ChildNodes[i].Attributes["max"].Value);
            }

            Manager.Instance.globalTypeScore = typeScore;
            Manager.Instance.globalNumericScore = NumericScore;
            Manager.Instance.globalAlphabeticalScore = AlphabeticalScore;
            Manager.Instance.globalMinimalSuccessScore = MinimalSuccessScore;
            Manager.Instance.globalMaxLimit = NumericScore[1];
            Manager.Instance.globalUrlScore = UrlScore;
            //end of score configuration
        }
    }

    public IEnumerator loadLanguageXML(Boolean _firstTime = false, Boolean _toSecurityModule = false){
        //false, false -- to menu
        //false, true -- not implemented
        //true, false -- to login / welcome
        //true, true -- to check security
        TextAsset _xmlConfName = Resources.Load("languageConf") as TextAsset;
        XmlDocument xmlLanguages = new XmlDocument();
        xmlLanguages.LoadXml(_xmlConfName.text);
        Manager.Instance.globalLanguages = xmlLanguages;

        if (_firstTime == true) {
			if (PlayerPrefs.HasKey ("language")) {
				string _language = PlayerPrefs.GetString("language");
                Language = _language;
                Manager.Instance.globalLanguage = Language;
			}
            else {
                Manager.Instance.globalLanguage = Language;
            }
		}
        else {
            Manager.Instance.globalLanguage = Language;
        }

		PlayerPrefs.SetString("language", Language);

        XmlNode languageNode = Manager.Instance.globalLanguages.SelectSingleNode("/data/language[@code='" + Language + "']");

        _urlTexts = languageNode.Attributes["folder"].Value + "/texts";

        switch (complexMode)
        {
            case ComplexModes.K12:
                _urlInfo = languageNode.Attributes["folder"].Value + "/K12/info";
                break;
            case ComplexModes.U:
                _urlInfo = languageNode.Attributes["folder"].Value + "/U/info";
                break;
            default:
                _urlInfo = languageNode.Attributes["folder"].Value + "/U/info";
                break;
        }

		TextAsset _xmlTexts = Resources.Load(_urlTexts) as TextAsset;
		TextAsset _xmlInfo = Resources.Load(_urlInfo) as TextAsset;

        xmlTexts = new XmlDocument ();
		xmlTexts.LoadXml (_xmlTexts.text);
		Manager.Instance.globalTexts = xmlTexts;

		xmlInfo = new XmlDocument();
		xmlInfo.LoadXml(_xmlInfo.text);
		Manager.Instance.globalInfo = xmlInfo;

        //start of score configuration
        if (Manager.Instance.globalAulaDoc != null)
        {
            scoreConfig();
        }

        if (_firstTime == true) {
			if(_toSecurityModule == true){
				/*if(SecurityModule == true){
					checkLicense();
				}
				else {
					StartCoroutine(checkLanguage());
				}*/
                if(SecurityModule == true){
                    //checkLicense();
#if !UNITY_WEBGL
                    
                    if (Validacion.Validar() || isLTI == true)
                    {
                        //TODO: Security module
                        StartCoroutine(checkLanguage());
                    }
                    else
                    {
                        loadAlertClose();
                    }
#endif
                }else{
                    yield return new WaitForSeconds (0.01f);
                    StartCoroutine(checkLanguage());
                }
			}
			else {
                //TODO - Si no va seguridad-webGL-LTI
                yield return new WaitForSeconds(0.5f);
                loadLogin(true);
				//yield return new WaitForSeconds (0.01f);
				//loadWelcome();
			}
		}
		else {
			if(_toSecurityModule == true){
				//TODO: Not implemented
				//return;
				Debug.Log ("in step 1");
			}
			else {
				//TODO: to menu
				Debug.Log ("in step 2");
			}
		}
	}

	private void checkLicense(){

	}

	IEnumerator checkLanguage(){
		Debug.Log ("in step 3");
		if (LanguageEnabled == true) {
			if (LanguageFirstTime == true) {
				if (PlayerPrefs.HasKey ("languageFirstTime")) {
					string _language = PlayerPrefs.GetString("language");
                    Language = _language;
                    Manager.Instance.globalLanguage = _language;
                    loadLogin();
                    yield return new WaitForSeconds(0.01f);

				} else {
					_languagePanel = Instantiate(_languagePanelPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
					_languagePanel.transform.SetParent (GameObject.Find("Canvas").transform, false);
					_languagePanel.transform.localPosition = new Vector3(30,0,0);
				}
			} else {
				_languagePanel = Instantiate(_languagePanelPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
				_languagePanel.transform.SetParent (GameObject.Find("Canvas").transform, false);
				_languagePanel.transform.localPosition = new Vector3(30,0,0);
			}
		} else {
			loadLogin ();
			yield return new WaitForSeconds (0.01f);
			
		}
	}

	private void loadLogin(bool animate = false){
        float yPosition = -10;
        _loginPanel = Instantiate(_loginPanelPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
		_loginPanel.transform.SetParent (GameObject.Find("Canvas").transform, false);

        if (animate == true)
        {
            yPosition = 1400;
        }
        _loginPanel.transform.localPosition = new Vector3(0, yPosition, 0);
		_loginPanel.transform.SetSiblingIndex (_loginPanel.transform.GetSiblingIndex());
        _loginPanel.GetComponent<Login>()._baseSim = this;

        if (animate == true)
        {
            iTween.MoveTo(
                _loginPanel,
                iTween.Hash(
                    "position", new Vector3(0,-10, 0),
                    "looktarget", Camera.main,
                    "easeType", iTween.EaseType.easeOutExpo,
                    "time", 1f,
                    "islocal", true
                )
            );
        }
    }

    public void LoginActionHanlder()
    {
        try
        {
            LoginActionEvent();
        } catch(Exception e)
        {
            Debug.Log(e);
        }
    }

    private void loadWelcome(){
		_alert = Instantiate(_alertPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
		_alert.transform.SetParent (GameObject.Find("Canvas").transform, false);
		_alert.transform.localPosition = new Vector3(-700,700,0);
		_alert.GetComponent<Alert> ().showAlert (1, "", "", Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='welcome']").InnerText, Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='accept']").InnerText);
		return;
	}
        private void loadAlertClose()
    {
        _alert = Instantiate(_alertPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        _alert.transform.SetParent(GameObject.Find("Canvas").transform, false);
        _alert.transform.localPosition = new Vector3(-700, 700, 0);
        _alert.GetComponent<Alert>().showAlert(1, "", "", Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='securityclose']").InnerText, Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='accept']").InnerText, "",closeApp);
        return;
    }

        private void closeApp(){
        Application.Quit();
        }



#if UNITY_ANDROID
	string ProcessDirectory(DirectoryInfo aDir){
        string inner_url = "";
    	var files = aDir.GetFiles().Where(f => f.Extension == ".xml").ToArray();
        foreach (var _fileName in files)
        {
            if (_fileName.Name.Equals("aula_conf.xml"))
            {
                inner_url = _fileName.FullName;
                break;
            }
        }

        return inner_url;
    }
#endif
}
