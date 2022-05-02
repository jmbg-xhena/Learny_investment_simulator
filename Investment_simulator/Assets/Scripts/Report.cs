using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Text;
using Crosstales.FB;
using SimpleJSON;
using System.Xml;
using UnityEngine.Networking;
using System.Runtime.InteropServices;

public class Report : MonoBehaviour {

	public Text _titleReport;
	public Text _userLabel;
	public Text _userText;
	public Text _courseIDLabel;
	public Text _courseIDText;
	public Text _institutionLabel;
	public Text _institutionText;
	public Text _startDateLabel;
	public Text _startDateText;

	public GameObject _alertPrefab;
	private GameObject _alert;

	public GameObject _oneColumnPref;
	public GameObject _twoColumnPref;

	public RectTransform _reportBg;
	public Texture2D tex;

	private string path;
	private string androidDir = "/storage/emulated/0/";
	Document doc = new Document();
	iTextSharp.text.Image pic1;

	private byte[] fileBytesAula;

	private string page1WebGL = "";
	private string page2WebGL = "";
	private string page3WebGL = "";
	private List<string> pagesWebGL; 

	private float numericScore = 0;

    private bool waitingWWW = false;
    private float countWaiting = 0;

    private int extraPageCount = 0;
	private List<reportExtraPageStructure> extraPages;

	private string attempts = "";
	private bool hasAttempts = true;
	private string score = "";
	private string timeText = "";

#if UNITY_IOS
	[DllImport("__Internal")]
	internal static extern bool OpenDocumentOnIOS(string path);
#endif
	// Use this for initialization
	void Start () {
		
	}

	public IEnumerator generateReport(string _score = "", bool _hasAttempts = true, List<reportImgElement> _imgElements = null, int _type = 1, float _numericScore = 0, List<reportExtraPageStructure> _extraPages = null, List<reportImgElement> scoreCaptures = null)
	{
		XmlNode languageNode = Manager.Instance.globalLanguages.SelectSingleNode("/data/language[@code='" + Manager.Instance.globalLanguage + "']");
		string isRTL = languageNode.Attributes["isRTL"].Value;

		_titleReport.font = (UnityEngine.Font)Resources.Load("Fonts/ArialBold48");
		_userLabel.font = (UnityEngine.Font)Resources.Load("Fonts/ArialBold42");
		_courseIDLabel.font = (UnityEngine.Font)Resources.Load("Fonts/ArialBold42");
		_institutionLabel.font = (UnityEngine.Font)Resources.Load("Fonts/ArialBold42");
		_startDateLabel.font = (UnityEngine.Font)Resources.Load("Fonts/ArialBold42");

		numericScore = _numericScore;

        _alert = Instantiate(_alertPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        _alert.transform.SetParent(GameObject.Find("Canvas").transform, false);
        _alert.transform.localPosition = new Vector3(-700, 700, 0);

        extraPages = _extraPages;

		pagesWebGL = new List<string> ();

        if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
        {
            _alert.GetComponent<Alert>().showAlert(6, "", "", Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='generatingReportNoAndroid']").InnerText, Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='accept']").InnerText);

            string extensions = "pdf";

            path = FileBrowser.SaveFile(Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='reportDialog']").InnerText, "", Manager.Instance.currentPDFName, extensions);

            Debug.Log("Save file: " + path);

            if (path == "")
            {
                _alert.GetComponent<Alert>().closeLoadingAlert();
                GameObject eval = GameObject.Find("SitEvaluation");
                if (eval)
                {
                    Destroy(eval);
                }
                Destroy(gameObject, 1);
                yield break;
            }

        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            _alert.GetComponent<Alert>().showAlert(6, "", "", Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='generatingReport']").InnerText, Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='accept']").InnerText);

            if (!Directory.Exists(Manager.Instance.globalAndroidUrl + "CloudLabs"))
            {
                System.IO.Directory.CreateDirectory(Manager.Instance.globalAndroidUrl + "CloudLabs");
            }
            path = Manager.Instance.globalAndroidUrl + "CloudLabs/" + Manager.Instance.currentPDFName + ".pdf";

        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            path = Application.persistentDataPath + "/" + Manager.Instance.currentPDFName + ".pdf";
			_alert.GetComponent<Alert>().showAlert(6, "", "", Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='generatingReport']").InnerText, Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='accept']").InnerText);
		}
        else if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            _alert.GetComponent<Alert>().showAlert(6, "", "", Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='generatingReportNoAndroid']").InnerText, Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='accept']").InnerText);
        }

        yield return new WaitForSeconds(1.3f);

        var _rectangle = new Rectangle(1700, 2200);

		if (Application.platform != RuntimePlatform.WebGLPlayer) {
			doc = new Document (_rectangle);
			PdfWriter.GetInstance(doc, new FileStream(path, FileMode.Create));
			doc.Open();
			doc.SetMargins (0, 0, 0, 0);
		}
			
		_reportBg = GameObject.Find("reportPractice").GetComponent<RectTransform>();

		if (isRTL == "true")
		{
			_titleReport.alignment = TextAnchor.MiddleRight;
		}

		_titleReport.fontSize = 48;
		_titleReport.text = TextUtility.SetText(Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='simulator_title']").InnerText);
		_userLabel.text = TextUtility.SetText(Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='user']").InnerText);
		_courseIDLabel.text = TextUtility.SetText(Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='institution']").InnerText);
		_institutionLabel.text = TextUtility.SetText(Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='situation']").InnerText);
		_startDateLabel.text = TextUtility.SetText(Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='report_date']").InnerText);
		_userText.text = TextUtility.SetText(Manager.Instance.globalUser);
		_courseIDText.text = TextUtility.SetText(Manager.Instance.globalInstitution);
		_institutionText.text = TextUtility.SetText(Manager.Instance.currentSituationName);

		DateTime dt = DateTime.Now;
		_startDateText.text = dt.ToString("dd/MM/yyyy");

		hasAttempts = _hasAttempts;
		score = _score;

		GameObject _upper = GameObject.Find ("upperIndicator");
		if (_upper != null) {
			timeText = _upper.GetComponent<UpperIndicator>()._time;
			if (_hasAttempts == true) {
				attempts = _upper.GetComponent<UpperIndicator> ()._attempts.ToString();
			} else {
				attempts = TextUtility.SetText(Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='na']").InnerText);
			}

		}

		if (scoreCaptures != null)
        {
			for (int imgIndex = 0; imgIndex < scoreCaptures.Count; imgIndex++)
			{
				GameObject _oneColumn = Instantiate(_oneColumnPref, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
				_oneColumn.transform.SetParent(GameObject.Find("bodyElement").transform, false);
				_oneColumn.transform.localPosition = new Vector3(0, 0, 0);
				_oneColumn.GetComponent<OneColumnReport>().setText(scoreCaptures[imgIndex].title1);
				_oneColumn.GetComponent<OneColumnReport>().setImage(scoreCaptures[imgIndex].image1, scoreCaptures[imgIndex].height1);
			}
		}

		/*
        if (_imgElements != null) {
			for (int imgIndex = 0; imgIndex < _imgElements.Count; imgIndex++) {
				if (_imgElements [imgIndex].type == 1) {
					GameObject _oneColumn = Instantiate (_oneColumnPref, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
					_oneColumn.transform.SetParent (GameObject.Find ("bodyElement").transform, false);
					_oneColumn.transform.localPosition = new Vector3 (0, 0, 0);
					_oneColumn.GetComponent<OneColumnReport>().setText(_imgElements [imgIndex].title1);
					_oneColumn.GetComponent<OneColumnReport> ().setImage (_imgElements [imgIndex].image1, _imgElements [imgIndex].height1);
				} else {
					GameObject _twoColumn = Instantiate (_twoColumnPref, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
					_twoColumn.transform.SetParent (GameObject.Find ("bodyElement").transform, false);
					_twoColumn.transform.localPosition = new Vector3 (0, 0, 0);
					_twoColumn.GetComponent<TwoColumnReport>().setText1(_imgElements [imgIndex].title1);
					_twoColumn.GetComponent<TwoColumnReport>().setText2(_imgElements [imgIndex].title2);
					_twoColumn.GetComponent<TwoColumnReport> ().setImage1 (_imgElements [imgIndex].image1, _imgElements [imgIndex].height1);
					_twoColumn.GetComponent<TwoColumnReport> ().setImage2 (_imgElements [imgIndex].image2, _imgElements [imgIndex].height2);
				}
			}
		}
		*/

        yield return new WaitForSeconds(0.1f);

        //capture screen
        if (tex != null) {
			Destroy (tex);
		}
        Rect _area = new Rect (0,0,1700,2200);
		tex = screenCapture.captureImage (_area, "Camera1", true);

		byte[] bytes = tex.EncodeToPNG();
		if (Application.platform == RuntimePlatform.WebGLPlayer) {
            // Enviar la textura para se codificada como JPG y luego enviarla al html para ser descargada con jsPDF 
            StartCoroutine(saveImagetoWebGLMultiple(tex, false));
        } else {
			//Debug.Log (Application.persistentDataPath);
			File.WriteAllBytes(Application.persistentDataPath + "/report.png", bytes);

			pic1 = iTextSharp.text.Image.GetInstance(Application.persistentDataPath + "/report.png", true);
			pic1.SetAbsolutePosition (0, 0);
			doc.Add(pic1);
		}
		Destroy (tex);

		StartCoroutine(generatePage1A(_imgElements, _type));
	}

	IEnumerator generatePage1A(List<reportImgElement> _imgElements, int _type)
	{
		GameObject _questionsPref = GameObject.Find("SitEvaluation");

		GameObject _reportExtraPage = Instantiate(_questionsPref.GetComponent<Evaluation>()._extraPagePrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
		_reportExtraPage.transform.SetParent(GameObject.Find("CanvasReport").transform, false);
		_reportExtraPage.transform.localPosition = new Vector3(0, 0, 0);
		StartCoroutine(_reportExtraPage.GetComponent<ReportExtraPage>().loadData(_imgElements));

		yield return new WaitForSeconds(0.2f);

		Rect _area1 = new Rect(0, 0, 1700, 2200);

		tex = screenCapture.captureImage(_area1, "Camera1", true);

		if (Application.platform == RuntimePlatform.WebGLPlayer)
		{
			// Enviar la textura para se codificada como JPG y luego enviarla al html para ser descargada con jsPDF 
		}
		else
		{
			byte[] bytes1 = tex.EncodeToPNG();
			//Debug.Log (Application.persistentDataPath);
			File.WriteAllBytes(Application.persistentDataPath + "/page_1A_" + extraPageCount.ToString() + ".png", bytes1);
			doc.NewPage();
			iTextSharp.text.Image pic2 = iTextSharp.text.Image.GetInstance(Application.persistentDataPath + "/page_1A_" + extraPageCount.ToString() + ".png", true);
			pic2.SetAbsolutePosition(0, 0);
			doc.Add(pic2);
		}

		Destroy(_reportExtraPage);

		if (_type == 1)
		{
			//page questions
			StartCoroutine(generatePage2());
		}
		else
		{
			StartCoroutine(generatePage3());
		}

		yield return null;
	}

	IEnumerator generatePage2(){
		GameObject _questionsPref = GameObject.Find ("SitEvaluation");

		GameObject _reportQuestions = Instantiate (_questionsPref.GetComponent<Evaluation> ()._reportQuestionsPrefab, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
		_reportQuestions.transform.SetParent (GameObject.Find ("CanvasReport").transform, false);
		_reportQuestions.transform.localPosition = new Vector3 (0, 0, 0);
		StartCoroutine(_reportQuestions.GetComponent<ReportQuestions> ().loadData ());

        Rect _area1 = new Rect (0, 0, 1700, 2200);
        yield return new WaitForSeconds(0.2f);
        tex = screenCapture.captureImage (_area1, "Camera1", true);

		if (Application.platform == RuntimePlatform.WebGLPlayer) {
			// Enviar la textura para se codificada como JPG y luego enviarla al html para ser descargada con jsPDF 
		} else {
			byte[] bytes1 = tex.EncodeToPNG ();
			//Debug.Log (Application.persistentDataPath);
			File.WriteAllBytes (Application.persistentDataPath + "/report1.png", bytes1);
			doc.NewPage ();
			iTextSharp.text.Image pic2 = iTextSharp.text.Image.GetInstance (Application.persistentDataPath + "/report1.png", true);
			pic2.SetAbsolutePosition (0, 0);
			doc.Add (pic2);
		}

        Destroy(_reportQuestions);

        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            StartCoroutine(saveImagetoWebGLMultiple(tex, false));
        }
		StartCoroutine(generatePage3());

        yield return null;
    }

	IEnumerator generatePage3(){
		GameObject _questionsPref = GameObject.Find ("SitEvaluation");

		GameObject _reportQuestions = Instantiate (_questionsPref.GetComponent<Evaluation> ()._reportComplementaryPrefab, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
		_reportQuestions.transform.SetParent (GameObject.Find ("CanvasReport").transform, false);
		_reportQuestions.transform.localPosition = new Vector3 (0, 0, 0);
		StartCoroutine(_reportQuestions.GetComponent<ReportComplementary> ().loadData ());

        Rect _area1 = new Rect (0, 0, 1700, 2200);
        yield return new WaitForSeconds(0.2f);
        tex = screenCapture.captureImage (_area1, "Camera1", true);

		if (Application.platform == RuntimePlatform.WebGLPlayer) {
			// Enviar la textura para se codificada como JPG y luego enviarla al html para ser descargada con jsPDF 
		} else {
			byte[] bytes1 = tex.EncodeToPNG ();
			Debug.Log (Application.persistentDataPath);
			File.WriteAllBytes (Application.persistentDataPath + "/report2.png", bytes1);
			doc.NewPage ();
			iTextSharp.text.Image pic3 = iTextSharp.text.Image.GetInstance (Application.persistentDataPath + "/report2.png", true);
			pic3.SetAbsolutePosition (0, 0);
			doc.Add (pic3);
		}

        Destroy(_reportQuestions);

        if (extraPages != null) {
			if (Application.platform == RuntimePlatform.WebGLPlayer)
            {
                StartCoroutine(saveImagetoWebGLMultiple(tex, false));
            }
			StartCoroutine (generateExtraPage ());
		} else {
			if (Application.platform == RuntimePlatform.WebGLPlayer)
            {
                StartCoroutine(saveImagetoWebGLMultiple(tex, true));
            }
            else
            {
                closeFile();
            }
		}

        yield return null;
    }

	IEnumerator generateExtraPage(){
		GameObject _questionsPref = GameObject.Find ("SitEvaluation");

		GameObject _reportExtraPage = Instantiate (_questionsPref.GetComponent<Evaluation> ()._extraPagePrefab, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
		_reportExtraPage.transform.SetParent (GameObject.Find ("CanvasReport").transform, false);
		_reportExtraPage.transform.localPosition = new Vector3 (0, 0, 0);
		StartCoroutine (_reportExtraPage.GetComponent<ReportExtraPage> ().loadData (extraPages [extraPageCount]._listElements));

        yield return new WaitForSeconds(0.2f);

        Rect _area1 = new Rect (0, 0, 1700, 2200);
		
        tex = screenCapture.captureImage (_area1, "Camera1", true);

		if (Application.platform == RuntimePlatform.WebGLPlayer) {
			// Enviar la textura para se codificada como JPG y luego enviarla al html para ser descargada con jsPDF 
		} else {
			byte[] bytes1 = tex.EncodeToPNG ();
			//Debug.Log (Application.persistentDataPath);
			File.WriteAllBytes (Application.persistentDataPath + "/report_extra_" + extraPageCount.ToString() + ".png", bytes1);
			doc.NewPage ();
			iTextSharp.text.Image pic2 = iTextSharp.text.Image.GetInstance (Application.persistentDataPath + "/report_extra_" + extraPageCount.ToString () + ".png", true);
			pic2.SetAbsolutePosition (0, 0);
			doc.Add (pic2);
		}

        Destroy(_reportExtraPage);

		if (extraPageCount < (extraPages.Count - 1)) {
			extraPageCount++;
			if (Application.platform == RuntimePlatform.WebGLPlayer) {
				StartCoroutine (saveImagetoWebGLMultiple (tex, false));
			}
			StartCoroutine (generateExtraPage ());
		} else {
			if (Application.platform == RuntimePlatform.WebGLPlayer) {
				StartCoroutine (saveImagetoWebGLMultiple (tex, true));
			} else {
				closeFile ();
			}
		}

        yield return null;
    }

	void closeFile(){
		if (Application.platform != RuntimePlatform.WebGLPlayer) {
			doc.Close();
            _alert.GetComponent<Alert>().closeLoadingAlert();

			if(Application.platform == RuntimePlatform.IPhonePlayer){
				openPDF ();
			}
		}

		Debug.Log ("Manager.Instance.globalAula: " + Manager.Instance.globalAula);

		if (Manager.Instance.globalAula == true) {
			FileStream stream = File.OpenRead(path);
			Debug.Log(stream.Length);
			fileBytesAula = new byte[stream.Length];
			stream.Read(fileBytesAula, 0, fileBytesAula.Length);
			stream.Close();
			Debug.Log(fileBytesAula.Length);
			//Debug.Log(fileBytesAula);
			readAulaScore();
		}
		else if (Application.platform == RuntimePlatform.Android && Manager.Instance.globalLTIParameters != "")
		{
			//For LTI
			string _url = Manager.Instance.globalUrlScore + "?score=" + numericScore.ToString() + "&lti_params=" + Manager.Instance.globalLTIParameters;
			Debug.Log(_url);
			UnityWebRequest wwwLTI = UnityWebRequest.Get(_url);
			StartCoroutine(WaitForRequestAulaLTI(wwwLTI));
		}
        else
        {
            GameObject eval = GameObject.Find("SitEvaluation");
            if (eval)
            {
                Destroy(eval);
            }
            Destroy (gameObject);
        }

    }

	private IEnumerator saveImagetoWebGLMultiple(Texture2D reportImage, bool lastPage)
	{
		var jpgEncoder = new JPGEncoder(reportImage, 100);
		float totalTime = 0;

		while (!jpgEncoder.isDone && totalTime < 3) {
			yield return null;
			totalTime += Time.deltaTime;
		}

		if (jpgEncoder.isDone)
		{
			byte[] imgJPG = jpgEncoder.GetBytes();

			pagesWebGL.Add ("data:image/jpeg;base64," + Convert.ToBase64String (imgJPG));

			if (lastPage == true) {
                _alert.GetComponent<Alert>().closeLoadingAlert();

                if (Manager.Instance.globalAula == true)
                {
                    Application.ExternalCall("pdfReporteMultiple", pagesWebGL, Manager.Instance.currentPDFName, "true");
                }
                else
                {
                    Application.ExternalCall("pdfReporteMultiple", pagesWebGL, Manager.Instance.currentPDFName, "false");

                    string _url = Manager.Instance.globalUrlScore + "?score=" + numericScore.ToString() + "&lti_params=" + Manager.Instance.globalLTIParameters;
                    Debug.Log(_url);
					UnityWebRequest wwwLTI = UnityWebRequest.Get(_url);
					StartCoroutine(WaitForRequestAulaLTI(wwwLTI));
                }
			}
		}

	}

	private IEnumerator saveImagetoWebGL(Texture2D reporte, int pageNumber, bool lastPage)
	{
		var jpgEncoder = new JPGEncoder(reporte, 100);
		float tiempoTotal = 0;

		while (!jpgEncoder.isDone && tiempoTotal < 3)
		{
			yield return null;
			tiempoTotal += Time.deltaTime;
		}

		if (jpgEncoder.isDone)
		{
			byte[] imgJPG = jpgEncoder.GetBytes();

			if (pageNumber == 1)
			{
				page1WebGL = "data:image/jpeg;base64," + Convert.ToBase64String(imgJPG);
			}
			else if(pageNumber == 2)
			{
				page2WebGL = "data:image/jpeg;base64," + Convert.ToBase64String(imgJPG);
			}
			else if(pageNumber == 3)
			{
				page3WebGL = "data:image/jpeg;base64," + Convert.ToBase64String(imgJPG);
			}

            if (lastPage == true)
            {
                _alert.GetComponent<Alert>().closeLoadingAlert();

                if (Manager.Instance.globalAula == true)
                {
                    Application.ExternalCall("pdfReporte", page1WebGL, page2WebGL, page3WebGL, Manager.Instance.currentPDFName, "true");
                }
                else
                {
                    Application.ExternalCall("pdfReporte", page1WebGL, page2WebGL, page3WebGL, Manager.Instance.currentPDFName, "false");

                    string _url = Manager.Instance.globalUrlScore + "?score=" + numericScore.ToString() + "&lti_params=" + Manager.Instance.globalLTIParameters;
                    Debug.Log(_url);
                    UnityWebRequest wwwLTI = UnityWebRequest.Get(_url);
                    StartCoroutine(WaitForRequestAulaLTI(wwwLTI));
                }
            }
		}
	}

    public void GetDataFromWeb(string reportData)
    {
        Debug.Log("In GetDataFromWeb");
        //Debug.Log(reportData.Length);

        Debug.Log("Convert to bytes");
        fileBytesAula = Convert.FromBase64String(reportData);

        Debug.Log("Convert to bytes");
        //Debug.Log(fileBytesAula.Length);
        //Debug.Log(fileBytesAula.ToString());

        readAulaScore();

        //Destroy(gameObject, 2.0f);
        //Debug.Log(reportData);
    }

	public void GetDataLTIFromWeb(string reportData)
    {
		fileBytesAula = Convert.FromBase64String(reportData);
		set_aula_score(true);
	}

    IEnumerator WaitForRequestAulaLTI(UnityWebRequest www)
	{
		yield return www.SendWebRequest();

		if (Manager.Instance.globalLtiAula == true)
		{
			if (Application.platform == RuntimePlatform.WebGLPlayer)
			{
				Application.ExternalCall("PdfDataMultiple", pagesWebGL);
			}
			else
			{
				//For AULA
				FileStream stream = File.OpenRead(path);
				fileBytesAula = new byte[stream.Length];
				stream.Read(fileBytesAula, 0, fileBytesAula.Length);
				stream.Close();

				set_aula_score(true);
			}
		} else
        {
			GameObject eval = GameObject.Find("SitEvaluation");
			if (eval)
			{
				Destroy(eval);
			}

			Destroy(gameObject, 1.0f);
		}
	}

	private void readAulaScore()
	{
		Debug.Log (Manager.Instance.globalUserAula);
		Debug.Log (Manager.Instance.currentAulaCode);
		string _data = "{\"user\":\"" + Manager.Instance.globalUserAula + "\",\"labCode\":\"" + Manager.Instance.currentAulaCode + "\"}";
		byte[] bytesToEncode = Encoding.UTF8.GetBytes(_data);
		string encodedText = Convert.ToBase64String(bytesToEncode);
		string _url = Manager.Instance.globalUrlAula + "/externals/get_lab?data=" + encodedText;
		Debug.Log(_url);
		UnityWebRequest wwwAulaSc = UnityWebRequest.Get(_url);
		StartCoroutine(WaitForRequestAulaGet(wwwAulaSc));
	}

	IEnumerator WaitForRequestAulaGet(UnityWebRequest www)
	{
        bool correctGet = false;

        www.certificateHandler = new BypassCertificate();
        yield return www.SendWebRequest();

		if (www.error == null)
		{
			byte[] decodedBytes = Convert.FromBase64String(www.downloadHandler.text);
			string decodedText = Encoding.UTF8.GetString(decodedBytes);
			Debug.Log(decodedText);
			var _data = JSON.Parse(decodedText);

			if (_data["state"] != null)
			{
				if (_data["state"].Value == "true")
				{
					if (_data["res_code"] != null)
					{
						switch (_data["res_code"].Value)
						{
						case "USER_NOT_FOUND":
							_alert = Instantiate (_alertPrefab, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
							_alert.transform.SetParent (GameObject.Find ("Canvas").transform, false);
							_alert.transform.localPosition = new Vector3 (-700, 700, 0);
							_alert.GetComponent<Alert> ().showAlert (1, "", "", Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='user_not_found']").InnerText, Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='accept']").InnerText);
							break;

						case "LAB_NOT_ASSIGNED":
							_alert = Instantiate (_alertPrefab, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
							_alert.transform.SetParent (GameObject.Find ("Canvas").transform, false);
							_alert.transform.localPosition = new Vector3 (-700, 700, 0);
							_alert.GetComponent<Alert> ().showAlert (1, "", "", Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='lab_not_assigned']").InnerText, Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='accept']").InnerText);
							break;

						case "LAB_CODE_NOT_FOUND":
							_alert = Instantiate (_alertPrefab, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
							_alert.transform.SetParent (GameObject.Find ("Canvas").transform, false);
							_alert.transform.localPosition = new Vector3 (-700, 700, 0);
							_alert.GetComponent<Alert> ().showAlert (1, "", "", Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='lab_not_found']").InnerText, Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='accept']").InnerText);
							break;
						case "STATUS_OK":
							if (_data["lab_state"].Value == "0")
							{
								Debug.Log("OK");
                                correctGet = true;
                                set_aula_score();
                            }
							else
							{
								_alert = Instantiate (_alertPrefab, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
								_alert.transform.SetParent (GameObject.Find ("Canvas").transform, false);
								_alert.transform.localPosition = new Vector3 (-700, 700, 0);
								_alert.GetComponent<Alert> ().showAlert (1, "", "", Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='lab_delivered_prev']").InnerText, Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='accept']").InnerText);
							}
							break;

						case "DB_EXCEPTION":
							_alert = Instantiate (_alertPrefab, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
							_alert.transform.SetParent (GameObject.Find ("Canvas").transform, false);
							_alert.transform.localPosition = new Vector3 (-700, 700, 0);
							_alert.GetComponent<Alert> ().showAlert (1, "", "", Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='db_exception']").InnerText, Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='accept']").InnerText);
							break;
						}
						Debug.Log("checkReport-01 + :" + _data["res_code"].Value);//--report
					}
					else
					{
						_alert = Instantiate (_alertPrefab, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
						_alert.transform.SetParent (GameObject.Find ("Canvas").transform, false);
						_alert.transform.localPosition = new Vector3 (-700, 700, 0);
						_alert.GetComponent<Alert> ().showAlert (1, "", "", Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='invalid_response']").InnerText, Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='accept']").InnerText);

					}
				}
				else
				{
					_alert = Instantiate (_alertPrefab, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
					_alert.transform.SetParent (GameObject.Find ("Canvas").transform, false);
					_alert.transform.localPosition = new Vector3 (-700, 700, 0);
					_alert.GetComponent<Alert> ().showAlert (1, "", "", Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='invalid_response']").InnerText, Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='accept']").InnerText);
				}
			}
			else
			{
				_alert = Instantiate (_alertPrefab, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
				_alert.transform.SetParent (GameObject.Find ("Canvas").transform, false);
				_alert.transform.localPosition = new Vector3 (-700, 700, 0);
				_alert.GetComponent<Alert> ().showAlert (1, "", "", Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='invalid_response']").InnerText, Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='accept']").InnerText);

			}

		}
		else
		{
			_alert = Instantiate (_alertPrefab, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
			_alert.transform.SetParent (GameObject.Find ("Canvas").transform, false);
			_alert.transform.localPosition = new Vector3 (-700, 700, 0);
			_alert.GetComponent<Alert> ().showAlert (1, "", "", Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='lab_send_error']").InnerText, Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='accept']").InnerText);
		}

        if(correctGet == false)
        {
            GameObject eval = GameObject.Find("SitEvaluation");
            if (eval)
            {
                Destroy(eval);
            }
            Destroy(gameObject);
        }

    }

	private void set_aula_score(bool fromLTI = false)
	{
		string _url = Manager.Instance.globalUrlAula + "/externals/put_lab";
		string _date = DateTime.Now.ToString("yyyy/MM/dd");
		WWWForm form = new WWWForm();
        string _scoreAula = "";
		string _attemptsAula = "";
        if (score.Contains("N/A"))
        {
            _scoreAula = "0";
        }
        else
        {
            Debug.Log("_numericScore: " + numericScore);//0.09 to 1
            Debug.Log("Cálculo a 100%: " + (numericScore * 100));
            _scoreAula = (numericScore * 100).ToString();
            Debug.Log("_scoreAula: " + _scoreAula);
        }

        if (hasAttempts == false)
        {
            _attemptsAula = "0";
        }
        else
        {
            _attemptsAula = attempts;
        }
		string aulaUser = Manager.Instance.globalUserAula;

		if (fromLTI == true)
		{
			aulaUser = Manager.Instance.globalUserId;
		}

		string _params = "{\"user\":\"" + aulaUser + "\",\"labCode\":\"" + Manager.Instance.currentAulaCode + "\",\"attempts\":" + _attemptsAula + ",\"delivery_date\":\"" + _date + "\",\"delivery_time\":\"" + timeText + "\",\"app_score\":" + _scoreAula + "}";
        Debug.Log(_params);
        form.AddField("data", _params);
        form.AddBinaryData("report_file", fileBytesAula, Manager.Instance.currentPDFName + ".pdf", "application/pdf");
        //WWW wwwAulaPut = new WWW(_url, form);
        UnityWebRequest wwAulaPut_mono = UnityWebRequest.Post(_url, form);
        StartCoroutine(WaitForRequestAulaPut(wwAulaPut_mono));
        Debug.Log("end set aula");
	}

	IEnumerator WaitForRequestAulaPut(UnityWebRequest www)
	{
        Debug.Log("aula put 0 previous load");
        waitingWWW = true;

        www.certificateHandler = new BypassCertificate();
        yield return www.SendWebRequest();

        Debug.Log("aula put post load");
        waitingWWW = false;

        if (www.error == null)
		{
			byte[] decodedBytes = Convert.FromBase64String(www.downloadHandler.text);
			string decodedText = Encoding.UTF8.GetString(decodedBytes);
			Debug.Log(decodedText);
			var _data = JSON.Parse(decodedText);

			if (_data["state"] != null)
			{
				if (_data["state"].Value == "true")
				{
					if (_data["res_code"] != null)
					{
						switch (_data["res_code"].Value)
						{
						case "LAB_INSERTED":
							_alert = Instantiate (_alertPrefab, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
							_alert.transform.SetParent (GameObject.Find ("Canvas").transform, false);
							_alert.transform.localPosition = new Vector3 (-700, 700, 0);
							_alert.GetComponent<Alert> ().showAlert (1, "", "", Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='lab_inserted']").InnerText, Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='accept']").InnerText);
							break;
						case "LAB_DELIVERED":
							_alert = Instantiate (_alertPrefab, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
							_alert.transform.SetParent (GameObject.Find ("Canvas").transform, false);
							_alert.transform.localPosition = new Vector3 (-700, 700, 0);
							_alert.GetComponent<Alert> ().showAlert (1, "", "", Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='lab_inserted']").InnerText, Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='accept']").InnerText);
							break;
						case "LAB_NOT_ASSIGNED":
							_alert = Instantiate (_alertPrefab, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
							_alert.transform.SetParent (GameObject.Find ("Canvas").transform, false);
							_alert.transform.localPosition = new Vector3 (-700, 700, 0);
							_alert.GetComponent<Alert> ().showAlert (1, "", "", Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='lab_not_assigned']").InnerText, Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='accept']").InnerText);

							break;

						case "LAB_CODE_NOT_FOUND":
							_alert = Instantiate (_alertPrefab, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
							_alert.transform.SetParent (GameObject.Find ("Canvas").transform, false);
							_alert.transform.localPosition = new Vector3 (-700, 700, 0);
							_alert.GetComponent<Alert> ().showAlert (1, "", "", Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='lab_not_found']").InnerText, Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='accept']").InnerText);

							break;

						case "LAB_UPDATED":
							_alert = Instantiate (_alertPrefab, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
							_alert.transform.SetParent (GameObject.Find ("Canvas").transform, false);
							_alert.transform.localPosition = new Vector3 (-700, 700, 0);
							_alert.GetComponent<Alert> ().showAlert (1, "", "", Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='lab_updated']").InnerText, Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='accept']").InnerText);

							break;

						case "DB_EXCEPTION":
							_alert = Instantiate (_alertPrefab, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
							_alert.transform.SetParent (GameObject.Find ("Canvas").transform, false);
							_alert.transform.localPosition = new Vector3 (-700, 700, 0);
							_alert.GetComponent<Alert> ().showAlert (1, "", "", Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='db_exception']").InnerText, Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='accept']").InnerText);

							break;

						case "LICENSE_EXPIRED":
							_alert = Instantiate (_alertPrefab, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
							_alert.transform.SetParent (GameObject.Find ("Canvas").transform, false);
							_alert.transform.localPosition = new Vector3 (-700, 700, 0);
							_alert.GetComponent<Alert> ().showAlert (1, "", "", Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='license_expired']").InnerText, Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='accept']").InnerText);

							break;
						}
						Debug.Log("checkReport-02 + :" + _data["res_code"].Value);//--report
					}
					else
					{
						_alert = Instantiate (_alertPrefab, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
						_alert.transform.SetParent (GameObject.Find ("Canvas").transform, false);
						_alert.transform.localPosition = new Vector3 (-700, 700, 0);
						_alert.GetComponent<Alert> ().showAlert (1, "", "", Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='invalid_response']").InnerText, Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='accept']").InnerText);

					}
				}
				else
				{
					_alert = Instantiate (_alertPrefab, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
					_alert.transform.SetParent (GameObject.Find ("Canvas").transform, false);
					_alert.transform.localPosition = new Vector3 (-700, 700, 0);
					_alert.GetComponent<Alert> ().showAlert (1, "", "", Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='invalid_response']").InnerText, Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='accept']").InnerText);

				}
			}
			else
			{
				_alert = Instantiate (_alertPrefab, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
				_alert.transform.SetParent (GameObject.Find ("Canvas").transform, false);
				_alert.transform.localPosition = new Vector3 (-700, 700, 0);
				_alert.GetComponent<Alert> ().showAlert (1, "", "", Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='invalid_response']").InnerText, Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='accept']").InnerText);

			}
		}
		else
		{
			_alert = Instantiate (_alertPrefab, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
			_alert.transform.SetParent (GameObject.Find ("Canvas").transform, false);
			_alert.transform.localPosition = new Vector3 (-700, 700, 0);
			_alert.GetComponent<Alert> ().showAlert (1, "", "", Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='lab_send_error']").InnerText, Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='accept']").InnerText);

		}

        GameObject eval = GameObject.Find("SitEvaluation");
        if (eval)
        {
            Destroy(eval);
        }
        Destroy(gameObject);
    }

	void openPDF()
	{
		if (Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXEditor)
		{
			string _pdf = "file:///" + path;
			_pdf = Uri.EscapeUriString(_pdf);
			Debug.Log (_pdf);
			Application.OpenURL(_pdf);
		}
		else if(Application.platform == RuntimePlatform.Android){
			Application.OpenURL(path);
		} else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
			Debug.Log("opening path...");
				Debug.Log(path);
#if UNITY_IOS
			OpenDocumentOnIOS(path);
#endif
		}
	}

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (GameObject.Find("headerElement"))
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(GameObject.Find("headerElement").GetComponent<RectTransform>());
        }
        if (GameObject.Find("bodyElement"))
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(GameObject.Find("bodyElement").GetComponent<RectTransform>());
        }
    }

    void Update () {
        if (waitingWWW == true)
        {
            countWaiting += Time.deltaTime;
            int seconds = (int)(countWaiting % 60);

            if (seconds == 15)
            {
                waitingWWW = false;
                _alert = Instantiate(_alertPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                _alert.transform.SetParent(GameObject.Find("Canvas").transform, false);
                _alert.transform.localPosition = new Vector3(-700, 700, 0);
                _alert.GetComponent<Alert>().showAlert(1, "", "", Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='lab_send_error_no_response']").InnerText, Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='accept']").InnerText);

                GameObject eval = GameObject.Find("SitEvaluation");
                if (eval)
                {
                    Destroy(eval);
                }
                Destroy(gameObject, 1.0f);
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
