using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System;
using SimpleJSON;
using UnityEngine.Networking;
using System.Xml;

public class Login : MonoBehaviour {

	//labels
	public Text titleLabelElement;
	public Text UserLabelElement;
	public Text CourseLabelElement;
	public Text CourseIdLabelElement;
	public Text InstitutionLabelElement;
	//inputfields
	public InputField UserInputElement;
	public InputField CourseInputElement;
	public InputField IdCourseInputElement;
	public InputField InstitutionInputElement;

	//buttons
	public Button loginBt;

	//backgrounds
	public Image background1;
	public Image background2;

	//elements
	public GameObject _alert;
	public GameObject _alertPrefab;

	public GameObject _backgroundPref;
	private GameObject _background;

    public BaseSimulator _baseSim;

	private string isRTL = "false";

	void Start () {

		GameObject bg = GameObject.Find ("TransversalBg");
		if (!bg) {
			_background = Instantiate(_backgroundPref, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
			_background.name = "TransversalBg";
			_background.transform.SetParent (GameObject.Find("Canvas").transform, false);
			_background.transform.localPosition = new Vector3(0,0,0);
			_background.transform.SetSiblingIndex (gameObject.transform.GetSiblingIndex () - 1);
		}

		XmlNode languageNode = Manager.Instance.globalLanguages.SelectSingleNode("/data/language[@code='" + Manager.Instance.globalLanguage + "']");
		string isRTL = languageNode.Attributes["isRTL"].Value;

		if (isRTL == "true")
		{
			UserInputElement.onValueChanged.AddListener(delegate { UserInputChanged(); });
			CourseInputElement.onValueChanged.AddListener(delegate { CourseInputChanged(); });
			IdCourseInputElement.onValueChanged.AddListener(delegate { IdCourseInputChanged(); });
			InstitutionInputElement.onValueChanged.AddListener(delegate { InstitutionInputChanged(); });
		}

		configureElement();
	}

	public void UserInputChanged()
	{
		UserInputElement.caretPosition = 0;
	}

	public void CourseInputChanged()
	{
		CourseInputElement.caretPosition = 0;
	}

	public void IdCourseInputChanged()
	{
		IdCourseInputElement.caretPosition = 0;
	}

	public void InstitutionInputChanged()
	{
		InstitutionInputElement.caretPosition = 0;
	}

	void configureElement(){

		titleLabelElement.font = (Font)Resources.Load("Fonts/ArialBold28");
		UserLabelElement.font = (Font)Resources.Load("Fonts/ArialBold31");
		CourseLabelElement.font = (Font)Resources.Load("Fonts/ArialBold31");
		CourseIdLabelElement.font = (Font)Resources.Load("Fonts/ArialBold31");
		InstitutionLabelElement.font = (Font)Resources.Load("Fonts/ArialBold31");
		loginBt.GetComponentInChildren<Text>().font = (Font)Resources.Load("Fonts/ArialBold34");

		if (isRTL == "true")
		{
			titleLabelElement.alignment = TextAnchor.MiddleRight;
		}

		if (Manager.Instance.globalAula == true) {
			background1.enabled = false;
			CourseIdLabelElement.enabled = false;
			InstitutionLabelElement.enabled = false;
			CourseInputElement.inputType = InputField.InputType.Password;
			IdCourseInputElement.gameObject.SetActive (false);
			InstitutionInputElement.gameObject.SetActive (false);
			loginBt.transform.localPosition = new Vector3 (13.6f,-164f,0f);

			titleLabelElement.text = TextUtility.SetText(Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='login']").InnerText);
			UserLabelElement.text = TextUtility.SetText(Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='user']").InnerText);
			CourseLabelElement.text = TextUtility.SetText(Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='password']").InnerText);

			UserInputElement.placeholder.GetComponent<Text>().text = TextUtility.SetText(Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='user']").InnerText + "...");
			CourseInputElement.placeholder.GetComponent<Text>().text = TextUtility.SetText(Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='password']").InnerText + "...");

			if (Manager.Instance.globalMonoUser == true)
            {
                UserInputElement.text = TextUtility.SetText(Manager.Instance.globalMonoUserEmail);
                UserInputElement.enabled = false;//core
                CourseInputElement.inputType = InputField.InputType.Password;
                CourseInputElement.text = TextUtility.SetText(Manager.Instance.globalMonoPassword);// Md5Sum( Manager.Instance.parametro_3)globalMonoPassword
                CourseInputElement.enabled = false;//core
            } else {
				UserInputElement.text = "";
				CourseInputElement.text = "";
			}

			loginBt.GetComponentInChildren<Text>().text = TextUtility.SetText(Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='login_bt']").InnerText);
			Button btn1 = loginBt.GetComponent<Button>();
			btn1.onClick.RemoveAllListeners();
			btn1.onClick.AddListener(LoginAulaClick);

#if UNITY_WEBGL
            UserInputElement.enabled = false;
            CourseInputElement.enabled = false;

            string urlWebGL = Manager.Instance.globalWebGLUrl;
            string webGLCode = Manager.Instance.globalWebGLCode;

            Debug.Log(urlWebGL);
            Debug.Log(webGLCode);

            WWWForm form = new WWWForm();
            form.AddField("codigo", webGLCode);

            WWW wwwAulaWebGL = new WWW(urlWebGL, form);
            StartCoroutine(WaitForRequestAulaWebGL(wwwAulaWebGL));
#endif

        }
        else {
			background2.enabled = false;
			background1.enabled = true;
			CourseIdLabelElement.enabled = true;
			InstitutionLabelElement.enabled = true;
			CourseInputElement.inputType = InputField.InputType.Standard;
			IdCourseInputElement.gameObject.SetActive (true);
			InstitutionInputElement.gameObject.SetActive (true);
			loginBt.transform.localPosition = new Vector3 (13.6f,-347f,0f);

			titleLabelElement.text = TextUtility.SetText(Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='login']").InnerText);
			UserLabelElement.text = TextUtility.SetText(Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='user']").InnerText);
			CourseLabelElement.text = TextUtility.SetText(Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='course']").InnerText);
			CourseIdLabelElement.text = TextUtility.SetText(Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='idcourse']").InnerText);
			InstitutionLabelElement.text = TextUtility.SetText(Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='institution']").InnerText);

			UserInputElement.placeholder.GetComponent<Text>().text = TextUtility.SetText(Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='user']").InnerText + "...");
			CourseInputElement.placeholder.GetComponent<Text>().text = TextUtility.SetText(Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='course']").InnerText + "...");
			IdCourseInputElement.placeholder.GetComponent<Text>().text = TextUtility.SetText(Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='idcourse']").InnerText + "...");
			InstitutionInputElement.placeholder.GetComponent<Text>().text = TextUtility.SetText(Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='institution']").InnerText + "...");

			if (Manager.Instance.globalUser != "") {
				UserInputElement.text = TextUtility.SetText(Manager.Instance.globalUser);
				UserInputElement.enabled = false;
			} else {
				UserInputElement.text = TextUtility.SetText(Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='user_input']").InnerText);	
			}

			if (Manager.Instance.globalCourse != "") {
				CourseInputElement.text = TextUtility.SetText(Manager.Instance.globalCourse);
				CourseInputElement.enabled = false;
			} else {
				CourseInputElement.text = TextUtility.SetText(Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='course_input']").InnerText);
			}

			if (Manager.Instance.globalCourseId != "") {
				IdCourseInputElement.text = TextUtility.SetText(Manager.Instance.globalCourseId);
				IdCourseInputElement.enabled = false;
			} else {
				IdCourseInputElement.text = TextUtility.SetText(Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='idcourse_input']").InnerText);
			}

			if (Manager.Instance.globalInstitution != "") {
				InstitutionInputElement.text = TextUtility.SetText(Manager.Instance.globalInstitution);
				InstitutionInputElement.enabled = false;
			} else {
				InstitutionInputElement.text = TextUtility.SetText(Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='institution_input']").InnerText);
			}

			if (Manager.Instance.globalMonoUser == true) {
				UserInputElement.text = TextUtility.SetText(Manager.Instance.globalMonoUserName);
				UserInputElement.enabled = false;
				InstitutionInputElement.text = TextUtility.SetText(Manager.Instance.globalMonoUserInstitution);
				InstitutionInputElement.enabled = false;
			}

			loginBt.GetComponentInChildren<Text>().text = TextUtility.SetText(Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='login_bt']").InnerText);
			Button btn1 = loginBt.GetComponent<Button>();
			btn1.onClick.RemoveAllListeners();
			btn1.onClick.AddListener(LoginClick);	
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void LoginClick()
	{
		//Output this to console when the Button is clicked
		Debug.Log("You have clicked the button!");
		if (UserInputElement.text == "" || CourseInputElement.text == "" || IdCourseInputElement.text == "" || InstitutionInputElement.text == "") {
			_alert = Instantiate(_alertPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
			_alert.transform.SetParent (GameObject.Find("Canvas").transform, false);
			_alert.transform.localPosition = new Vector3(-700,700,0);
			_alert.GetComponent<Alert> ().showAlert (1, "", "", Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='login_empty']").InnerText, Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='accept']").InnerText);	
		} else {
			Manager.Instance.logged = true;
			Manager.Instance.globalUser = UserInputElement.text;
			Manager.Instance.globalCourse = CourseInputElement.text;
			Manager.Instance.globalCourseId = IdCourseInputElement.text;
			Manager.Instance.globalInstitution = InstitutionInputElement.text;

			Vector3 _position = new Vector3 (0, 1200, 0);

			iTween.MoveTo(
				gameObject,
				iTween.Hash(
					"position", _position,
					"looktarget", Camera.main,
					"easeType", iTween.EaseType.easeOutExpo,
					"time", 1f,
					"islocal",true
				)
			);

			Image _bg = GameObject.Find ("TransversalBg").GetComponent<Image>();
			_bg.CrossFadeAlpha (0f, 1f, false);

            _baseSim.LoginActionHanlder();

			Destroy(GameObject.Find ("TransversalBg"), 1.4f);

			Destroy(gameObject, 1.5f);
		}

	}

	void LoginAulaClick(){
		if (UserInputElement.text == "" || CourseInputElement.text == "") {
			_alert = Instantiate (_alertPrefab, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
			_alert.transform.SetParent (GameObject.Find ("Canvas").transform, false);
			_alert.transform.localPosition = new Vector3 (-700, 700, 0);
			_alert.GetComponent<Alert> ().showAlert (1, "", "", Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='login_empty']").InnerText, Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='accept']").InnerText);	
		} else {
			Manager.Instance.globalUserAula = UserInputElement.text;
            string _params;
            //string _params = "{\"user\":\"" + UserInputElement.text + "\",\"pass\":\"" + Md5Sum(CourseInputElement.text) + "\"}";
#if UNITY_WEBGL
            _params = "{\"user\":\"" + UserInputElement.text + "\",\"pass\":\"" + CourseInputElement.text + "\"}";

#elif UNITY_ANDROID

            if (Manager.Instance.globalMonoUser == true)
            {
                _params = "{\"user\":\"" + UserInputElement.text + "\",\"pass\":\"" + CourseInputElement.text + "\"}";
            }
            else
            {
                _params = "{\"user\":\"" + UserInputElement.text + "\",\"pass\":\"" + Md5Sum(CourseInputElement.text) + "\"}";
            }
#else
            _params = "{\"user\":\"" + UserInputElement.text + "\",\"pass\":\"" + Md5Sum(CourseInputElement.text) + "\"}";
#endif

            Debug.Log("Parametros sin codificar: " + _params);
            byte[] bytesToEncode = Encoding.UTF8.GetBytes(_params);
            string encodedText = Convert.ToBase64String(bytesToEncode);

            string url_get_aula = "";

            url_get_aula = Manager.Instance.globalUrlAula + "/externals/login?data=" + encodedText;


            //url_get_aula = Manager.Instance.globalUrlAula + "/externals/login?data=" + encodedText;
            Debug.Log("antes de: " + url_get_aula);
            //url_get_aula = "https://apiclassroom.cloudlabs.us/externals/login?data=eyJ1c2VyIjoiZHZhbGxlam9AaW5ub3ZhdGl2ZWNvbG9tYmlhLmNvbSIsInBhc3MiOiI5YmQ0NGE1ZmRmMjkyNmU4ZmM3NWNlZGE1MThmYTg0ZiJ9";

            //Debug.Log(url_get_aula);
            Manager.Instance.url_entera = url_get_aula;

            UnityWebRequest wwwAula = UnityWebRequest.Get(url_get_aula);

            //decode
            /*byte[] decodedBytes = Convert.FromBase64String(wwwAula.text);
            string decodedText = Encoding.UTF8.GetString(decodedBytes);
            Debug.Log(decodedText);
            Manager.Instance.url_entera = Manager.Instance.url_entera + " -decode- " + decodedText;*/

            //StartCoroutine(GetText());

            StartCoroutine(WaitForRequestAula(wwwAula));


            loginBt.enabled = false;

        }
    }

    //test get data
    IEnumerator GetText()
    {
        UnityWebRequest www = UnityWebRequest.Get("https://apiclassroom.cloudlabs.us/externals/login?data=eyJ1c2VyIjoiZHZhbGxlam9AaW5ub3ZhdGl2ZWNvbG9tYmlhLmNvbSIsInBhc3MiOiI5YmQ0NGE1ZmRmMjkyNmU4ZmM3NWNlZGE1MThmYTg0ZiJ9");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            Debug.Log(www.downloadHandler.text);
            Manager.Instance.url_entera += www.downloadHandler.text;

            // Or retrieve results as binary data
            byte[] results = www.downloadHandler.data;
        }
    }

    IEnumerator WaitForRequestAula(UnityWebRequest www)
	{
        www.certificateHandler = new BypassCertificate();
        yield return www.SendWebRequest();

        byte[] decodedBytesB = Convert.FromBase64String(www.downloadHandler.text);
        string decodedTextB = Encoding.UTF8.GetString(decodedBytesB);
        Debug.Log(decodedTextB);
        Manager.Instance.url_entera = Manager.Instance.url_entera + " -Dec " + decodedTextB;

        if (www.error == null)
		{
            byte[] decodedBytes = Convert.FromBase64String(www.downloadHandler.text);
            string decodedText = Encoding.UTF8.GetString(decodedBytes);
            Debug.Log(decodedText);
            Manager.Instance.url_entera = Manager.Instance.url_entera + " - " + decodedText;

            var _data = JSON.Parse(decodedText);

            if (_data["state"] != null)
			{
				if (_data["state"].Value == "true")
				{
					if (_data["res_code"] != null)
					{
						switch (_data["res_code"].Value)
						{
						case "INVALID_USER_PASS":
							loginBt.enabled = true;
							_alert = Instantiate (_alertPrefab, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
							_alert.transform.SetParent (GameObject.Find ("Canvas").transform, false);
							_alert.transform.localPosition = new Vector3 (-700, 700, 0);
							_alert.GetComponent<Alert> ().showAlert (1, "", "", Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='invalid_user_pass']").InnerText, Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='accept']").InnerText);
							break;
						case "LAB_NOT_ASSIGNED":
							loginBt.enabled = true;
							_alert = Instantiate (_alertPrefab, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
							_alert.transform.SetParent (GameObject.Find ("Canvas").transform, false);
							_alert.transform.localPosition = new Vector3 (-700, 700, 0);
							_alert.GetComponent<Alert> ().showAlert (1, "", "", Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='lab_not_assigned']").InnerText, Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='accept']").InnerText);
							break;
						case "LOGIN_OK":
							Manager.Instance.logged = true;
							Manager.Instance.globalUser = _data["name"].Value + " " + _data["last_name"].Value;
							Manager.Instance.globalCourse = _data["class_group"].Value;
							Manager.Instance.globalCourseId = Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='idcourse_input']").InnerText;
							Manager.Instance.globalInstitution = _data["school_name"].Value;

							Vector3 _position = new Vector3 (0, 1200, 0);

							iTween.MoveTo(
								gameObject,
								iTween.Hash(
									"position", _position,
									"looktarget", Camera.main,
									"easeType", iTween.EaseType.easeOutExpo,
									"time", 1f,
									"islocal",true
								)
							);

							Image _bg = GameObject.Find ("TransversalBg").GetComponent<Image>();
							_bg.CrossFadeAlpha (0f, 1f, false);

                            _baseSim.LoginActionHanlder();
                            
                            Destroy(GameObject.Find ("TransversalBg"), 1.4f);
							Destroy(gameObject, 1.5f);
							break;
						case "DB_EXCEPTION":
							loginBt.enabled = true;
							_alert = Instantiate (_alertPrefab, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
							_alert.transform.SetParent (GameObject.Find ("Canvas").transform, false);
							_alert.transform.localPosition = new Vector3 (-700, 700, 0);
							_alert.GetComponent<Alert> ().showAlert (1, "", "", Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='db_exception']").InnerText, Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='accept']").InnerText);
							break;
						case "LICENSE_EXPIRED":
							loginBt.enabled = true;
							_alert = Instantiate (_alertPrefab, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
							_alert.transform.SetParent (GameObject.Find ("Canvas").transform, false);
							_alert.transform.localPosition = new Vector3 (-700, 700, 0);
							_alert.GetComponent<Alert> ().showAlert (1, "", "", Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='license_expired_offline']").InnerText, Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='accept']").InnerText);
							break;
						}
						Debug.Log("check + :" + _data["res_code"].Value);
					}
					else
					{
						loginBt.enabled = true;
						_alert = Instantiate (_alertPrefab, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
						_alert.transform.SetParent (GameObject.Find ("Canvas").transform, false);
						_alert.transform.localPosition = new Vector3 (-700, 700, 0);
						_alert.GetComponent<Alert> ().showAlert (1, "", "", Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='invalid_response']").InnerText, Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='accept']").InnerText);
					}
				}
				else
				{
					loginBt.enabled = true;
					_alert = Instantiate (_alertPrefab, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
					_alert.transform.SetParent (GameObject.Find ("Canvas").transform, false);
					_alert.transform.localPosition = new Vector3 (-700, 700, 0);
					_alert.GetComponent<Alert> ().showAlert (1, "", "", Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='invalid_response']").InnerText, Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='accept']").InnerText);
				}
			}
			else
			{
				loginBt.enabled = true;
				_alert = Instantiate (_alertPrefab, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
				_alert.transform.SetParent (GameObject.Find ("Canvas").transform, false);
				_alert.transform.localPosition = new Vector3 (-700, 700, 0);
				_alert.GetComponent<Alert> ().showAlert (1, "", "", Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='invalid_response']").InnerText, Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='accept']").InnerText);
			}

		}
		else
		{
			//two buttons
			loginBt.enabled = true;
			_alert = Instantiate (_alertPrefab, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
			_alert.transform.SetParent (GameObject.Find ("Canvas").transform, false);
			_alert.transform.localPosition = new Vector3 (-700, 700, 0);
#if UNITY_IPHONE
            _alert.GetComponent<Alert> ().showAlert (1, "", "", Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='no_internet_aula_ios']").InnerText, Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='try_again']").InnerText,"", tryAgainAction);
#else
			_alert.GetComponent<Alert>().showAlert(2, "", "", Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='no_internet_aula']").InnerText, Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='try_again']").InnerText, Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='offline']").InnerText, tryAgainAction, offLineAction);
#endif
		}
	}

	void tryAgainAction(){
		_alert.GetComponent<Alert> ().closeAlert ();
		LoginAulaClick ();
	}

	void offLineAction(){
		Manager.Instance.globalAula = false;
		configureElement ();
		_alert.GetComponent<Alert> ().closeAlert ();
	}

	public string Md5Sum(string strToEncrypt)
	{
		UTF8Encoding ue = new System.Text.UTF8Encoding();
		byte[] bytes = ue.GetBytes(strToEncrypt);

		// encrypt bytes
		System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
		byte[] hashBytes = md5.ComputeHash(bytes);

		// Convert the encrypted bytes back to a string (base 16)
		string hashString = "";

		for (int i = 0; i < hashBytes.Length; i++)
		{
			hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
		}

		return hashString.PadLeft(32, '0');
    }

    private IEnumerator WaitForRequestAulaWebGL(WWW www)
    {
        yield return www;
        if (www.error == null)
        {
            try
            {
                Debug.Log(www.text);
                var dataReceived = JSON.Parse(www.text);
                if (dataReceived["RESULT"] != null)
                {
                    UserInputElement.text = dataReceived["RESULT"]["email"].Value;
                    CourseInputElement.text = dataReceived["RESULT"]["password"].Value;
                }
            }
            catch (Exception error)
            {
                Debug.Log(error);
            }
        }
    }

    public class BypassCertificate : CertificateHandler
    {
        protected override bool ValidateCertificate(byte[] certificateData)
        {
            //Simply return true no matter what
            return true;
        }
    }
}
