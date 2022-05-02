using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;

public class Evaluation : MonoBehaviour {

	public Image background_1;
	public Image background_2;
	public Text title;
	public Button _action_bt;
	public Button _x_bt;
	public Button _right_bt;
	public Button _left_bt;
	public TEXDraw _right_text;
	public TEXDraw _left_text;
	public Image question_image;
	public GameObject _options;

	public GameObject _backgroundPref;
	private GameObject _background;

	public GameObject _scorePref;
	private GameObject _scoreMsg;

	public int statementIndex = 0;
	private int statementLength = 0;
	public int questionIndex = -1;
	private int questionLength = 0;
	public int[] orderOptions;

	private int bgInitialIndex = 0;

	private float questionsScore = 0;
	private float MaxQuestionsScore = 0;
	public float totalScore = 0;
	public string visibleScore = "";

	public GameObject _reportPrefab;
	public GameObject _report;

	public GameObject _reportQuestionsPrefab;
	public GameObject _reportQuestions;
	public GameObject _extraPagePrefab;

	public GameObject _reportComplementaryPrefab;
	public GameObject _reportComplementary;

	public List<reportImgElement> imgElements;
	public List<reportExtraPageStructure> extraPages;

	private int type;
	private string isRTL = "false";

	// Use this for initialization
	void Start () {
		XmlNode languageNode = Manager.Instance.globalLanguages.SelectSingleNode("/data/language[@code='" + Manager.Instance.globalLanguage + "']");
		isRTL = languageNode.Attributes["isRTL"].Value;

		title.font = (Font)Resources.Load("Fonts/ArialBold28");
		_action_bt.GetComponentInChildren<Text>().font = (Font)Resources.Load("Fonts/ArialBold34");
		title.text = TextUtility.SetText(Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='evaluation']").InnerText);
		_action_bt.GetComponentInChildren<Text> ().text = TextUtility.SetText(Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='finish']").InnerText);

		_action_bt.gameObject.SetActive (false);
		_left_bt.gameObject.SetActive (false);
		_left_text.gameObject.SetActive (false);
		_options.SetActive (false);

		_x_bt.onClick.AddListener (closeWindow);
		_right_bt.onClick.AddListener (rightAction);
		_left_bt.onClick.AddListener (leftAction);
		_action_bt.onClick.AddListener (terminateAction);

		if (isRTL == "true")
		{
			title.alignment = TextAnchor.MiddleRight;
		}
	}

	public void startEvaluation(int _type, float _qt, int[] _orderOptions, float _questionsQtMax = 0f, List<reportImgElement> _imgElements = null, List<reportExtraPageStructure> _extraPages = null){
		/*type options:
		 * 1: questions, score and report
		 * 2: score and report
		 * 3: report
		 * */
		type = _type;
		totalScore = _qt;
		orderOptions = _orderOptions;
		MaxQuestionsScore = _questionsQtMax;
		imgElements = _imgElements;
		extraPages = _extraPages;

		GameObject bg = GameObject.Find ("backgroundEval");

		switch (_type) {
		case 1:
			if (bg) {
				bgInitialIndex = bg.transform.GetSiblingIndex ();
				bg.transform.SetSiblingIndex (gameObject.transform.GetSiblingIndex ()-1);
				Debug.Log ("alert check");
			} else {
				_background = Instantiate(_backgroundPref, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
				_background.name = "backgroundEval";
				_background.transform.SetParent (GameObject.Find("Canvas").transform, false);
				_background.transform.localPosition = new Vector3(0,0,0);
				_background.transform.SetSiblingIndex (gameObject.transform.GetSiblingIndex () - 1);
			}
			doQuestions ();
			break;

		case 2:
			if (bg) {
				bgInitialIndex = bg.transform.GetSiblingIndex ();
				bg.transform.SetSiblingIndex (gameObject.transform.GetSiblingIndex ()-1);
				Debug.Log ("alert check");
			} else {
				_background = Instantiate(_backgroundPref, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
				_background.name = "backgroundEval";
				_background.transform.SetParent (GameObject.Find("Canvas").transform, false);
				_background.transform.localPosition = new Vector3(0,0,0);
				_background.transform.SetSiblingIndex (gameObject.transform.GetSiblingIndex () - 1);
			}
			doScore ();
			break;

		case 3:
			visibleScore = Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='na']").InnerText;
			StartCoroutine(doReport (false, false));
			break;
		}
			
	}

	private void doQuestions(){

		statementLength = Manager.Instance.globalQuestions.Count;
		questionLength = Manager.Instance.globalQuestions [statementIndex].ChildNodes.Count;

		question_image.sprite = Resources.Load<Sprite>( Manager.Instance.globalQuestions.Item(statementIndex).Attributes.GetNamedItem("image").InnerText );
		question_image.preserveAspect = true;

		_right_text.size = float.Parse(Manager.Instance.globalQuestions.Item (statementIndex).Attributes.GetNamedItem ("size").InnerText);
		_right_text.text = TextUtility.SetText(Manager.Instance.globalQuestions.Item (statementIndex).Attributes.GetNamedItem ("text").InnerText);

		iTween.MoveTo(
			gameObject,
			iTween.Hash(
				"position", new Vector3(0,0,0),
				"looktarget", Camera.main,
				"easeType", iTween.EaseType.easeOutExpo,
				"time", 1f,
				"islocal",true
			)
		);
	}

	private void doScore(){
		int countQuestions = 0;
		int countCorrectAnswers = 0;
		float _total = 0;
		float _totalScaled = 0;

		for (int i = 0; i < statementLength; i++) {
			for(int j = 0; j < Manager.Instance.globalQuestions [i].ChildNodes.Count; j++){
				countQuestions++;
				for (int k = 0; k < 4; k++) {
					if(Manager.Instance.globalQuestions.Item (i).ChildNodes.Item (j).ChildNodes.Item (k).Attributes.GetNamedItem ("correct").InnerText.ToLower() == "true"){
						if (Manager.Instance.globalQuestions.Item (i).ChildNodes.Item (j).ChildNodes.Item (k).Attributes.GetNamedItem ("selected").InnerText.ToLower() == "true") {
							countCorrectAnswers++;
						}
					}
				}
			}
		}

		if (countQuestions > 0) {
			questionsScore = (float)countCorrectAnswers / countQuestions;
			questionsScore = questionsScore * MaxQuestionsScore;
			_total = totalScore + questionsScore;
		} else {
			_total = totalScore;
		}

		totalScore = _total;
			
		if (Manager.Instance.globalTypeScore == BaseSimulator.TypeScoreList.NUMERIC) {
			float _min = Manager.Instance.globalNumericScore [0];
			float _max = Manager.Instance.globalNumericScore [1];

			_totalScaled = _total * (_max - _min) + _min;
			_totalScaled = (Mathf.Round (_totalScaled * 10)) / 10;
			visibleScore = _totalScaled.ToString ();
		} else {
			for (int l = 0; l < Manager.Instance.globalAlphabeticalScore.Length; l++) {
				if (_total < Manager.Instance.globalAlphabeticalScore [l].maxValue) {
					visibleScore = Manager.Instance.globalAlphabeticalScore [l].name;
					break;
				}
			}
		}

		_scoreMsg = Instantiate (_scorePref, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
		_scoreMsg.transform.SetParent (GameObject.Find ("Canvas").transform, false);
		_scoreMsg.transform.localPosition = new Vector3 (-15, 1200, 0);
		_scoreMsg.name = "_scoreMsg";
		StartCoroutine(_scoreMsg.GetComponent<ScoreMsg> ().ShowScore (visibleScore, _total, (float)countCorrectAnswers / (float)countQuestions, this));
	}

	public IEnumerator doReport(bool _hasAttempts = true, bool _hasDelay = false, List<reportImgElement> scoreCaptures = null){
		if (_hasDelay == true) {
			yield return new WaitForSeconds (1.5f);
		}
		_report = Instantiate(_reportPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
		_report.name = "reportPractice";
		_report.transform.SetParent (GameObject.Find("CanvasReport").transform, false);
		_report.transform.localPosition = new Vector3(-850,1100,0);
		GameObject.Find ("CanvasReport").GetComponent<Canvas> ().enabled = true;
		StartCoroutine(_report.GetComponent<Report> ().generateReport (visibleScore, _hasAttempts, imgElements, type, totalScore, extraPages, scoreCaptures));

		yield return null;
	}

	private void rightAction(){
		if (questionIndex == -1) {
			questionIndex = 0;

			question_image.gameObject.SetActive (false);
			_left_text.gameObject.SetActive (true);
			_left_bt.gameObject.SetActive (true);
			_options.SetActive (true);
			StartCoroutine(setQuestionOptions ());

			_right_text.text = "";

			_left_text.text = TextUtility.SetText(Manager.Instance.globalQuestions.Item (statementIndex).ChildNodes.Item (questionIndex).Attributes.GetNamedItem ("text").InnerText);
			_left_text.size = float.Parse (Manager.Instance.globalQuestions.Item (statementIndex).ChildNodes.Item (questionIndex).Attributes.GetNamedItem ("size").InnerText);

		} else {
			if (questionIndex < (questionLength - 1)) {
				questionIndex++;

				question_image.gameObject.SetActive (false);
				_left_text.gameObject.SetActive (true);
				_left_bt.gameObject.SetActive (true);
				_options.SetActive (true);
				StartCoroutine(setQuestionOptions());

				_right_text.text = "";

				_left_text.text = TextUtility.SetText(Manager.Instance.globalQuestions.Item (statementIndex).ChildNodes.Item (questionIndex).Attributes.GetNamedItem ("text").InnerText);
				_left_text.size = float.Parse (Manager.Instance.globalQuestions.Item (statementIndex).ChildNodes.Item (questionIndex).Attributes.GetNamedItem ("size").InnerText);

				if(statementIndex == (statementLength - 1) && (questionIndex == (questionLength - 1))){
					_action_bt.gameObject.SetActive (true);
					_right_bt.gameObject.SetActive (false);
				}

			} else {
				if (statementIndex < (statementLength - 1)) {
					statementIndex++;
					questionIndex = -1;

					question_image.gameObject.SetActive (true);
					_left_text.gameObject.SetActive (false);
					_options.SetActive (false);

					question_image.sprite = Resources.Load<Sprite>( Manager.Instance.globalQuestions.Item(statementIndex).Attributes.GetNamedItem("image").InnerText );
					question_image.preserveAspect = true;

					_right_text.size = float.Parse(Manager.Instance.globalQuestions.Item (statementIndex).Attributes.GetNamedItem ("size").InnerText);
					_right_text.text = TextUtility.SetText(Manager.Instance.globalQuestions.Item (statementIndex).Attributes.GetNamedItem ("text").InnerText);

				} else {
					//END OF SEQUENCE	
				}
			}
		}
	}

	private void leftAction(){

		_action_bt.gameObject.SetActive (false);
		_right_bt.gameObject.SetActive (true);

		if (questionIndex > 0) {
			questionIndex--;

			question_image.gameObject.SetActive (false);
			_left_text.gameObject.SetActive (true);
			_left_bt.gameObject.SetActive (true);
			_options.SetActive (true);
			StartCoroutine(setQuestionOptions());

			_right_text.text = "";

			_left_text.text = TextUtility.SetText(Manager.Instance.globalQuestions.Item (statementIndex).ChildNodes.Item (questionIndex).Attributes.GetNamedItem ("text").InnerText);
			_left_text.size = float.Parse (Manager.Instance.globalQuestions.Item (statementIndex).ChildNodes.Item (questionIndex).Attributes.GetNamedItem ("size").InnerText);
		} else if (questionIndex == 0) {
			questionIndex = -1;

			question_image.gameObject.SetActive (true);
			_left_text.gameObject.SetActive (false);
			_options.SetActive (false);

			question_image.sprite = Resources.Load<Sprite> (Manager.Instance.globalQuestions.Item (statementIndex).Attributes.GetNamedItem ("image").InnerText);
			question_image.preserveAspect = true;

			_right_text.size = float.Parse (Manager.Instance.globalQuestions.Item (statementIndex).Attributes.GetNamedItem ("size").InnerText);
			_right_text.text = TextUtility.SetText(Manager.Instance.globalQuestions.Item (statementIndex).Attributes.GetNamedItem ("text").InnerText);

			if (statementIndex == 0) {
				_left_bt.gameObject.SetActive (false);
			}

		} else {
			statementIndex--;
			questionIndex = Manager.Instance.globalQuestions.Item (statementIndex).ChildNodes.Count - 1;

			question_image.gameObject.SetActive (false);
			_left_text.gameObject.SetActive (true);
			_left_bt.gameObject.SetActive (true);
			_options.SetActive (true);
			setQuestionOptions ();

			_right_text.text = "";

			_left_text.text = TextUtility.SetText(Manager.Instance.globalQuestions.Item (statementIndex).ChildNodes.Item (questionIndex).Attributes.GetNamedItem ("text").InnerText);
			_left_text.size = float.Parse (Manager.Instance.globalQuestions.Item (statementIndex).ChildNodes.Item (questionIndex).Attributes.GetNamedItem ("size").InnerText);

		}
	}

	private IEnumerator setQuestionOptions(){
		GameObject _option1 = GameObject.Find ("EvalOption1");
		GameObject _option2 = GameObject.Find ("EvalOption2");
		GameObject _option3 = GameObject.Find ("EvalOption3");
		GameObject _option4 = GameObject.Find ("EvalOption4");

		yield return new WaitForSeconds(0.01f);

		_option1.GetComponent<Option> ().setText (Manager.Instance.globalQuestions.Item (statementIndex).ChildNodes.Item (questionIndex).ChildNodes.Item (orderOptions[0]).InnerText, float.Parse (Manager.Instance.globalQuestions.Item (statementIndex).ChildNodes.Item (questionIndex).ChildNodes.Item (orderOptions[0]).Attributes.GetNamedItem ("size").InnerText));
		_option2.GetComponent<Option> ().setText (Manager.Instance.globalQuestions.Item (statementIndex).ChildNodes.Item (questionIndex).ChildNodes.Item (orderOptions[1]).InnerText, float.Parse (Manager.Instance.globalQuestions.Item (statementIndex).ChildNodes.Item (questionIndex).ChildNodes.Item (orderOptions[1]).Attributes.GetNamedItem ("size").InnerText));
		_option3.GetComponent<Option> ().setText (Manager.Instance.globalQuestions.Item (statementIndex).ChildNodes.Item (questionIndex).ChildNodes.Item (orderOptions[2]).InnerText, float.Parse (Manager.Instance.globalQuestions.Item (statementIndex).ChildNodes.Item (questionIndex).ChildNodes.Item (orderOptions[2]).Attributes.GetNamedItem ("size").InnerText));
		_option4.GetComponent<Option> ().setText (Manager.Instance.globalQuestions.Item (statementIndex).ChildNodes.Item (questionIndex).ChildNodes.Item (orderOptions[3]).InnerText, float.Parse (Manager.Instance.globalQuestions.Item (statementIndex).ChildNodes.Item (questionIndex).ChildNodes.Item (orderOptions[3]).Attributes.GetNamedItem ("size").InnerText));

		if (Manager.Instance.globalQuestions.Item (statementIndex).ChildNodes.Item (questionIndex).ChildNodes.Item (orderOptions [0]).Attributes.GetNamedItem ("selected") != null) {
			if (Manager.Instance.globalQuestions.Item (statementIndex).ChildNodes.Item (questionIndex).ChildNodes.Item (orderOptions [0]).Attributes.GetNamedItem ("selected").InnerText == "True") {
				_option1.GetComponent<Option> ().setState (true);
			} else {
				_option1.GetComponent<Option> ().setState (false);
			}
		} else {
			XmlElement _attribute = Manager.Instance.globalQuestions.Item (statementIndex).ChildNodes.Item (questionIndex).ChildNodes.Item (orderOptions [0]) as XmlElement;
			_attribute.SetAttribute ("selected", "False");
			_option1.GetComponent<Option> ().setState (false);
		}

		if (Manager.Instance.globalQuestions.Item (statementIndex).ChildNodes.Item (questionIndex).ChildNodes.Item (orderOptions [1]).Attributes.GetNamedItem ("selected") != null) {
			if (Manager.Instance.globalQuestions.Item (statementIndex).ChildNodes.Item (questionIndex).ChildNodes.Item (orderOptions [1]).Attributes.GetNamedItem ("selected").InnerText == "True") {
				_option2.GetComponent<Option> ().setState (true);
			} else {
				_option2.GetComponent<Option> ().setState (false);
			}
		} else {
			XmlElement _attribute = Manager.Instance.globalQuestions.Item (statementIndex).ChildNodes.Item (questionIndex).ChildNodes.Item (orderOptions [1]) as XmlElement;
			_attribute.SetAttribute ("selected", "False");
			_option2.GetComponent<Option> ().setState (false);
		}

		if (Manager.Instance.globalQuestions.Item (statementIndex).ChildNodes.Item (questionIndex).ChildNodes.Item (orderOptions [2]).Attributes.GetNamedItem ("selected") != null) {
			if (Manager.Instance.globalQuestions.Item (statementIndex).ChildNodes.Item (questionIndex).ChildNodes.Item (orderOptions [2]).Attributes.GetNamedItem ("selected").InnerText == "True") {
				_option3.GetComponent<Option> ().setState (true);
			} else {
				_option3.GetComponent<Option> ().setState (false);
			}
		} else {
			XmlElement _attribute = Manager.Instance.globalQuestions.Item (statementIndex).ChildNodes.Item (questionIndex).ChildNodes.Item (orderOptions [2]) as XmlElement;
			_attribute.SetAttribute ("selected", "False");
			_option3.GetComponent<Option> ().setState (false);
		}

		if (Manager.Instance.globalQuestions.Item (statementIndex).ChildNodes.Item (questionIndex).ChildNodes.Item (orderOptions [3]).Attributes.GetNamedItem ("selected") != null) {
			if (Manager.Instance.globalQuestions.Item (statementIndex).ChildNodes.Item (questionIndex).ChildNodes.Item (orderOptions [3]).Attributes.GetNamedItem ("selected").InnerText == "True") {
				_option4.GetComponent<Option> ().setState (true);
			} else {
				_option4.GetComponent<Option> ().setState (false);
			}
		} else {
			XmlElement _attribute = Manager.Instance.globalQuestions.Item (statementIndex).ChildNodes.Item (questionIndex).ChildNodes.Item (orderOptions [3]) as XmlElement;
			_attribute.SetAttribute ("selected", "False");
			_option4.GetComponent<Option> ().setState (false);
		}

    }

	private void terminateAction(){

		iTween.MoveTo(
			gameObject,
			iTween.Hash(
				"position", new Vector3(0,1200,0),
				"looktarget", Camera.main,
				"easeType", iTween.EaseType.easeOutExpo,
				"time", 1f,
				"islocal",true
			)
		);
		Invoke ("doScore", 1.5f);
	}

	private void closeWindow(){
		iTween.MoveTo(
			gameObject,
			iTween.Hash(
				"position", new Vector3(0,1200,0),
				"looktarget", Camera.main,
				"easeType", iTween.EaseType.easeOutExpo,
				"time", 1f,
				"islocal",true
			)
		);
			
		Image _bg = GameObject.Find ("backgroundEval").GetComponent<Image>();
		_bg.CrossFadeAlpha (0f, 1f, false);
		Destroy(_background, 1.2f);

		Destroy(gameObject, 2.0f);
	}

	// Update is called once per frame
	void FixedUpdate () {
        if (_options.activeSelf == true)
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(_options.GetComponent<RectTransform>());
        }
    }
}
