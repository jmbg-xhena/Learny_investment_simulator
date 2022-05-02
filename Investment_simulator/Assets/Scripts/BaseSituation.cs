using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#if !UNITY_WEBGL
using ValidacionMenu;
#endif

public class BaseSituation : MonoBehaviour {

	public string situationTag;
	public int[] orderOptions;
	public bool developerMode = false;

	public GameObject _alert;
	public GameObject _alertPrefab;

	public GameObject _evaluation;
	public GameObject _evaluationPrefab;

	public GameObject _upperIndicator;
	public GameObject _upperIndicatorPrefab;

	public GameObject skillsProgress;
	public GameObject skillsProgressPrefab;

	public GameObject skillsAlert;
	public GameObject skillsAlertPrefab;

	public GameObject _notePad;
	public GameObject _notePadPrefab;

	public GameObject _languagePanel;
	public GameObject _languagePanelPrefab;

	public Camera _cam1;
	public Camera _cam2;

	public Canvas _reportCanvas;

	public int skillsAlertTime = 3;

	// Use this for initialization
	public void StartBaseSituation () {

		TextAsset _xmlTexts;
		TextAsset _xmlInfo;
		XmlDocument xmlTexts;
		XmlDocument xmlInfo;

		string _urlInfo = "";
		string _urlQuestions = "";
		string _urlTexts = "";

		if (developerMode == true) {
			_xmlTexts = Resources.Load("Texts/spanish/texts") as TextAsset;
            switch (Manager.Instance.globalComplexMode)
            {
                case BaseSimulator.ComplexModes.K12:
                    _urlInfo = "Texts/spanish/K12/info";
                    break;
                case BaseSimulator.ComplexModes.U:
                    _urlInfo = "Texts/spanish/U/info";
                    break;
                default:
                    _urlInfo = "Texts/spanish/U/info";
                    break;
            }
            _xmlInfo = Resources.Load(_urlInfo) as TextAsset;

            xmlTexts = new XmlDocument();
            xmlTexts.LoadXml(_xmlTexts.text);
            Manager.Instance.globalTexts = xmlTexts;

            xmlInfo = new XmlDocument();
            xmlInfo.LoadXml(_xmlInfo.text);
            Manager.Instance.globalInfo = xmlInfo;
        }

		XmlNode languageNode = Manager.Instance.globalLanguages.SelectSingleNode("/data/language[@code='" + Manager.Instance.globalLanguage + "']");
		_urlTexts = languageNode.Attributes["folder"].Value + "/texts";

		switch (Manager.Instance.globalComplexMode)
        {
            case BaseSimulator.ComplexModes.K12:
				_urlQuestions = languageNode.Attributes["folder"].Value + "/K12/questions";
				_urlInfo = languageNode.Attributes["folder"].Value + "/K12/info";
				break;
            case BaseSimulator.ComplexModes.U:
                _urlQuestions = languageNode.Attributes["folder"].Value + "/U/questions";
				_urlInfo = languageNode.Attributes["folder"].Value + "/U/info";
				break;
            default:
                _urlQuestions = languageNode.Attributes["folder"].Value + "/U/questions";
				_urlInfo = languageNode.Attributes["folder"].Value + "/U/info";
				break;
        }

        TextAsset _xmlQuestions = Resources.Load(_urlQuestions) as TextAsset;

		XmlDocument xmlQuestions = new XmlDocument();
		xmlQuestions.LoadXml(_xmlQuestions.text);

		_xmlTexts = Resources.Load(_urlTexts) as TextAsset;
		_xmlInfo = Resources.Load(_urlInfo) as TextAsset;

		xmlTexts = new XmlDocument();
		xmlTexts.LoadXml(_xmlTexts.text);
		Manager.Instance.globalTexts = xmlTexts;

		xmlInfo = new XmlDocument();
		xmlInfo.LoadXml(_xmlInfo.text);
		Manager.Instance.globalInfo = xmlInfo;

		Manager.Instance.globalQuestions = xmlQuestions.SelectNodes ("/data/" + situationTag + "/statement");
		Manager.Instance.globalComplementaryQuestions = xmlQuestions.SelectNodes ("/data/" + situationTag + "/complementaries/question");

        Manager.Instance.currentSituationName = Manager.Instance.globalInfo.SelectSingleNode ("/data/" + situationTag + "/name_practice_").InnerText;
		Manager.Instance.currentAulaCode = Manager.Instance.globalInfo.SelectSingleNode ("/data/" + situationTag + "/aula_code").InnerText;

		orderOptions = new int[4];
		orderOptions [0] = Mathf.RoundToInt(Random.Range(0.0f,3.0f));
		int _tempValue = Mathf.RoundToInt (Random.Range(0.0f,3.0f));
		while(_tempValue == orderOptions [0]){
			_tempValue = Mathf.RoundToInt (Random.Range(0.0f,3.0f));
		}
		orderOptions [1] = _tempValue;
		while(_tempValue == orderOptions [0] || _tempValue == orderOptions [1]){
			_tempValue = Mathf.RoundToInt (Random.Range(0.0f,3.0f));
		}
		orderOptions [2] = _tempValue;

		while(_tempValue == orderOptions [0] || _tempValue == orderOptions [1] || _tempValue == orderOptions [2]){
			_tempValue = Mathf.RoundToInt (Random.Range(0.0f,3.0f));
		}
		orderOptions [3] = _tempValue;

		skillsProgress = Instantiate(skillsProgressPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
		skillsProgress.transform.SetParent(GameObject.Find("Canvas").transform, false);
		skillsProgress.GetComponent<RectTransform>().anchoredPosition = new Vector2(-20, -95);
		skillsProgress.name = "skillsProgress";
		skillsProgress.transform.SetSiblingIndex(1);

		_upperIndicator = Instantiate (_upperIndicatorPrefab, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
		_upperIndicator.transform.SetParent (GameObject.Find ("Canvas").transform, false);
		_upperIndicator.GetComponent<RectTransform>().anchoredPosition = new Vector2 (-12, 15);
		_upperIndicator.name = "upperIndicator";
		_upperIndicator.transform.SetSiblingIndex (2);
		_upperIndicator.GetComponent<UpperIndicator> ().startTimer ();


		_notePad = Instantiate (_notePadPrefab, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
		_notePad.transform.SetParent (GameObject.Find ("Canvas").transform, false);
		_notePad.transform.localPosition = new Vector3 (0, 1200, 0);
		_notePad.name = "notePad";

		string _complementary = "";
		for(int i = 0; i < Manager.Instance.globalComplementaryQuestions.Count; i++){
			if(Manager.Instance.globalComplementaryQuestions [i].InnerText == ""){
				_notePad.GetComponent<NotePad> ().addPage ("", false, Manager.Instance.globalComplementaryQuestions [i].InnerText, true);
			}
			else {
				_notePad.GetComponent<NotePad> ().addPage ("", false, Manager.Instance.globalComplementaryQuestions [i].InnerText);
			}
		}
		_notePad.GetComponent<NotePad> ().goToFirstPage ();

		_reportCanvas.enabled = false;

		GameObject langGameObject = GameObject.Find("lang_bt");
        if (langGameObject != null)
        {
			if (Manager.Instance.globalLanguageEnabled == true)
			{
				langGameObject.GetComponent<Button>().onClick.AddListener(LanguageChangeHandler);
			}
			else
			{
				langGameObject.SetActive(false);
			}
		}

        //edit weight items inputs
#if UNITY_WEBGL
        InputField[] inputsFields = FindObjectsOfType<InputField>();
        for (int i = 0; i < inputsFields.Length; i++)
        {
            inputsFields[i].caretWidth = 2;
        }
#endif
    }

	private void LanguageChangeHandler()
    {
		_languagePanel = Instantiate(_languagePanelPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
		_languagePanel.transform.SetParent(GameObject.Find("Canvas").transform, false);
		_languagePanel.transform.localPosition = new Vector3(0, 1400, 0);

		iTween.MoveTo(
				_languagePanel,
				iTween.Hash(
					"position", new Vector3(0, 0, 0),
					"looktarget", Camera.main,
					"easeType", iTween.EaseType.easeOutExpo,
					"time", 1f,
					"islocal", true
				)
			);
	}

	public void RestartScene()
    {
		Scene scene = SceneManager.GetActiveScene();
		SceneManager.LoadScene(scene.name);
	}

    // Update is called once per frame
    void Update () {
		
	}
}
