using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dataRegistrySit1 : MonoBehaviour {

	public GameObject _situation1;
	public Text titleElement;
	public Text title1;
	public Text title2;
	public Text title3;
	public Text indicator1;
	public Text indicator2;
	public Text indicator3;
	public InputField answer1;
	public InputField answer2;
    public Dropdown answer3;
	public Button x_element;
	public Button verifyBt;
	public Button reportBt;
	public Image xInd1;
	public Image xInd2;
	public Image xInd3;

	public Texture2D _screenPict1;
	public Texture2D _screenPict2;

	public List<reportImgElement> _list;

	public GameObject _situation;

	public GameObject _alertPref;
	public GameObject _bgPref;

	private GameObject _bg;
	private GameObject _alert;

	private float _machineScore = 0f;
	private float _coordinatesScore = 0f;
	private float _attemptsScore = 0f;

	// Use this for initialization
	void Start () {
		
		verifyBt.GetComponentInChildren<Text>().text = Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='verify']").InnerText;
		reportBt.GetComponentInChildren<Text> ().text = Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='reporte']").InnerText;

		verifyBt.GetComponentInChildren<Text>().font = (Font)Resources.Load("Fonts/ArialBold28");
		reportBt.GetComponentInChildren<Text> ().font = (Font)Resources.Load("Fonts/ArialBold28");
	}
		
	// Update is called once per frame
	void Update () {
		
	}

	public void verifyAction(){

		if( answer1.text == "" || answer2.text == "" || answer3.value == 0 ){
			_alert = Instantiate(_alertPref, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
			_alert.transform.SetParent(GameObject.Find("Canvas").transform, false);
			_alert.transform.localPosition = new Vector3(-700, 700, 0);
			_alert.GetComponent<Alert>().showAlert(1, "", "", Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='data_empty_sit']").InnerText, Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='accept']").InnerText);
			//_situation1.GetComponent<situation_1> ()._upperIndicator.GetComponent<UpperIndicator> ().addAttempt ();
		}
		else {
			if (situationCorrect() == false) {
				//BAD
				_alert = Instantiate(_alertPref, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
				_alert.transform.SetParent(GameObject.Find("Canvas").transform, false);
				_alert.transform.localPosition = new Vector3(-700, 700, 0);
				_alert.GetComponent<Alert>().showAlert(1, "", "", Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='verify_bad']").InnerText, Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='accept']").InnerText);
				//_situation1.GetComponent<situation_1> ()._upperIndicator.GetComponent<UpperIndicator> ().addAttempt ();
			} else {
				//GOOD
				_alert = Instantiate(_alertPref, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
				_alert.transform.SetParent(GameObject.Find("Canvas").transform, false);
				_alert.transform.localPosition = new Vector3(-700, 700, 0);
				_alert.GetComponent<Alert>().showAlert(1, "", "", Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='verify_success']").InnerText, Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='accept']").InnerText);
			}

			exitAction();
		}
	}

	public void reportAction(){

		if (answer1.text == "" || answer2.text == "" || answer3.value == 0) {
			_alert = Instantiate (_alertPref, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
			_alert.transform.SetParent (GameObject.Find ("Canvas").transform, false);
			_alert.transform.localPosition = new Vector3 (-700, 700, 0);
			_alert.GetComponent<Alert> ().showAlert (1, "", "", Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='data_empty_sit']").InnerText, Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='accept']").InnerText);
			//_situation1.GetComponent<situation_1> ()._upperIndicator.GetComponent<UpperIndicator> ().addAttempt ();
		} else {
			if (situationCorrect() == false) {
				//BAD
				_alert = Instantiate(_alertPref, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
				_alert.transform.SetParent(GameObject.Find("Canvas").transform, false);
				_alert.transform.localPosition = new Vector3(-700, 700, 0);
				_alert.GetComponent<Alert>().showAlert(2, "", "", Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='report_bad']").InnerText, Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='accept']").InnerText, Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='cancel']").InnerText, gotoReport );
				//_situation1.GetComponent<situation_1> ()._upperIndicator.GetComponent<UpperIndicator> ().addAttempt ();
			} else {
				//GOOD
				_alert = Instantiate(_alertPref, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
				_alert.transform.SetParent(GameObject.Find("Canvas").transform, false);
				_alert.transform.localPosition = new Vector3(-700, 700, 0);
				_alert.GetComponent<Alert>().showAlert(2, "", "", Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='report_success']").InnerText, Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='accept']").InnerText, Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='cancel']").InnerText, gotoReport );
			}

			exitAction();
		}
	}

	private bool situationCorrect(){
		bool _correct = false;

		return _correct;
	}

	public void gotoReport(){

		_list = new List<reportImgElement>();

		reportImgElement _img1 = new reportImgElement ();
		
		_img1.type = 1;
		_img1.title1 = Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='scenario_report']").InnerText;
		_img1.height1 = 800;
		_img1.image1 = _screenPict1;
		_list.Add (_img1);

        //Example of extra pages
        List<reportExtraPageStructure> _listExtraPages = new List<reportExtraPageStructure>();

        //extra page created
        reportExtraPageStructure pageA = new reportExtraPageStructure();

        //Items inside page
        List<reportImgElement> itemsPageA = new List<reportImgElement>();


        //item 1 image with title
        reportImgElement _imgExtraA = new reportImgElement();
        _imgExtraA.type = 1;
        _imgExtraA.title1 = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='scenario_report']").InnerText;
        _imgExtraA.height1 = 300;
        _imgExtraA.image1 = _screenPict1;
        itemsPageA.Add(_imgExtraA);

        //item 2 images with title
        reportImgElement _imgExtraB = new reportImgElement();
        _imgExtraB.type = 2;
        _imgExtraB.title1 = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='scenario_report']").InnerText;
        _imgExtraB.height1 = 300;
        _imgExtraB.image1 = _screenPict1;

        _imgExtraB.title2 = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='scenario_report']").InnerText;
        _imgExtraB.height2 = 300;
        _imgExtraB.image2 = _screenPict1;
        itemsPageA.Add(_imgExtraB);

        //item 3 images without title
        reportImgElement _imgExtraC = new reportImgElement();
        _imgExtraC.type = 2;
        _imgExtraC.title1 = "";
        _imgExtraC.height1 = 300;
        _imgExtraC.image1 = _screenPict1;

        _imgExtraC.title2 = "";
        _imgExtraC.height2 = 300;
        _imgExtraC.image2 = _screenPict1;
        itemsPageA.Add(_imgExtraC);

        //item 4 image without title
        reportImgElement _imgExtraD = new reportImgElement();
        _imgExtraD.type = 1;
        _imgExtraD.title1 = "";
        _imgExtraD.height1 = 300;
        _imgExtraD.image1 = _screenPict1;
        itemsPageA.Add(_imgExtraD);


        //assigned items to page
        pageA._listElements = itemsPageA;

        //page added
        _listExtraPages.Add(pageA);



        //Attempts score. Use if the evaluation have a component from attempts
        if (_situation1.GetComponent<situation_1>().getAttempts() <= 11)
        {
            _attemptsScore = 0.1f * (10f - ((float)_situation1.GetComponent<situation_1>().getAttempts() - 1f));
        }
        else
        {
            _attemptsScore = 0f;
        }

        float scoreValue = float.Parse(answer1.text) * 0.8f + _attemptsScore * 0.1f; //Add others required points required in evaluation

		_alert.GetComponent<Alert>().closeAlert ();
		_situation1.GetComponent<situation_1>()._evaluation = Instantiate(_situation1.GetComponent<situation_1>()._evaluationPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
		_situation1.GetComponent<situation_1>()._evaluation.transform.SetParent(GameObject.Find("Canvas").transform, false);
		_situation1.GetComponent<situation_1>()._evaluation.transform.localPosition = new Vector3(0, 1200, 0);
		_situation1.GetComponent<situation_1>()._evaluation.name = "SitEvaluation";
        _situation1.GetComponent<situation_1>()._evaluation.GetComponent<Evaluation>().startEvaluation(1, scoreValue, _situation1.GetComponent<situation_1>().orderOptions, 0.1f, _list, _listExtraPages);

    }

	public void exitAction(){
		hideBg ();
		iTween.MoveTo(
			gameObject,
			iTween.Hash(
				"position", new Vector3(0f,1400f,0f),
				"looktarget", Camera.main,
				"easeType", iTween.EaseType.easeOutExpo,
				"time", 1f,
				"islocal",true
			)
		);
	}

	public void showBg(){
		GameObject bg = GameObject.Find ("dataRegistryBg");
		if (bg) {
			bg.transform.SetSiblingIndex (gameObject.transform.GetSiblingIndex ()-1);
		} else {
			_bg = Instantiate(_bgPref, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
			_bg.name = "dataRegistryBg";
			_bg.transform.SetParent (GameObject.Find("Canvas").transform, false);
			_bg.transform.localPosition = new Vector3(0,0,0);
			_bg.transform.SetSiblingIndex (gameObject.transform.GetSiblingIndex () - 1);
		}
	}

	public void hideBg(){
		GameObject bg = GameObject.Find ("dataRegistryBg");

		if (bg != null) {
			Image _bg = bg.GetComponent<Image>();
			_bg.CrossFadeAlpha (0f, 1f, false);
			Destroy(bg, 1.2f);
		}
	}

}
