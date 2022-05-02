using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class ScoreMsg : MonoBehaviour {

	public Text _title;
	public Button _acceptBt;
	public Text _timeText;
	public Text _scoreText;
	public Text _attemptsLabel;
	public GameObject _attemptsLevel;
	public Text _conceptsLabel;
	public GameObject _conceptsLevel;
	public Text _skillsLabel;
	public GameObject _skillsBaseIcons;
	public Image _skillsDone1;
	public Image _skillsDone2;
	public Image _skillsDone3;
	public Image _skillsDone4;
	public Image _skillsDone5;
	public Text _timeLabel;
	public Text _scoreLabel;
	public Text _levelLabel;
	public Text _levelText;
	public Image _levelImg;
	public Sprite _levelA;
	public Sprite _levelA2;
	public Sprite _levelB;
	public Sprite _levelC;
	public Sprite _levelD;
	public Sprite _levelE;
	public Sprite _levelF;

	public Image bg1;
	public Image bg2;
	public Image bg3;
	public Image bg4;
	public Image bg5;

	private Evaluation evalObj;
	private Texture2D capture;


	// Use this for initialization
	void Start () {
		XmlNode languageNode = Manager.Instance.globalLanguages.SelectSingleNode("/data/language[@code='" + Manager.Instance.globalLanguage + "']");
		string isRTL = languageNode.Attributes["isRTL"].Value;

		_title.font = (Font)Resources.Load("Fonts/ArialBold28");
		_acceptBt.GetComponentInChildren<Text>().font = (Font)Resources.Load("Fonts/ArialBold34");
		_scoreText.font = (Font)Resources.Load("Fonts/ArialBold72");

		_title.text = TextUtility.SetText(Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='score']").InnerText);
		_acceptBt.GetComponentInChildren<Text>().text = TextUtility.SetText(Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='accept']").InnerText);

		_attemptsLabel.text = TextUtility.SetText(Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='score_attempts']").InnerText);
		_conceptsLabel.text = TextUtility.SetText(Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='score_concepts']").InnerText);
		_skillsLabel.text = TextUtility.SetText(Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='score_skills']").InnerText);
		_timeLabel.text = TextUtility.SetText(Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='score_time']").InnerText);
		_scoreLabel.text = TextUtility.SetText(Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='score_score']").InnerText);
		_levelLabel.text = TextUtility.SetText(Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='score_level']").InnerText);

		if (isRTL == "true")
		{
			_title.alignment = TextAnchor.MiddleRight;
		}
	}

	public IEnumerator ShowScore(string _score, float _total, float _scoreQuestions, Evaluation evaluationObj) {

		evalObj = evaluationObj;

		iTween.MoveTo(
			gameObject,
			iTween.Hash(
				"position", new Vector3(-15, 0, 0),
				"looktarget", Camera.main,
				"easeType", iTween.EaseType.easeOutExpo,
				"time", 1f,
				"islocal", true
			)
		);

		yield return new WaitForSeconds(1f);

		_attemptsLabel.gameObject.SetActive(true);
		bg1.gameObject.SetActive(true);
		_attemptsLevel.SetActive(true);

		//attempts score
		situation_1 situation = FindObjectOfType<situation_1>();

		if (situation != null)
        {
			int attempts = situation.getAttempts();

			_attemptsLabel.text = _attemptsLabel.text + ": <b>" + attempts.ToString() + "</b>";

			float attemptsScore = 0;

			if (attempts <= 11)
			{
				attemptsScore = 0.1f * (10f - ((float)attempts - 1f));
			}

			iTween.ValueTo(
				gameObject,
				iTween.Hash(
					"from", 0f,
					"to", 366f * attemptsScore,
					"time", 1f,
					"easeType", iTween.EaseType.easeOutSine,
					"onupdate", "onUpdateAttemptsLevel"
				)
			);

			yield return new WaitForSeconds(1f);
		}

		//time
		_timeLabel.gameObject.SetActive(true);
		_timeText.gameObject.SetActive(true);
		bg4.gameObject.SetActive(true);

		_timeText.text = "00:00";

		int timeVal = 0;
		if (situation != null)
		{
			timeVal = situation.getTime();
			iTween.ValueTo(
				gameObject,
				iTween.Hash(
					"from", 0,
					"to", timeVal,
					"time", 1f,
					"easeType", iTween.EaseType.easeOutSine,
					"onupdate", "onUpdateTime"
				)
			);
		}

		//time used

		yield return new WaitForSeconds(1f);

		//concepts
		_conceptsLabel.gameObject.SetActive(true);
		_conceptsLevel.SetActive(true);
		bg2.gameObject.SetActive(true);

		iTween.ValueTo(
			gameObject,
			iTween.Hash(
				"from", 0,
				"to", 366f * _scoreQuestions,
				"time", 1f,
				"easeType", iTween.EaseType.easeOutSine,
				"onupdate", "onUpdateConcepts"
			)
		);

		yield return new WaitForSeconds(1f);


		//score
		_scoreLabel.gameObject.SetActive(true);
		_scoreText.gameObject.SetActive(true);
		bg5.gameObject.SetActive(true);

		if(Manager.Instance.globalTypeScore == BaseSimulator.TypeScoreList.ALPHABETIC || _score.Contains("N/A"))
        {
			_scoreText.text = _score;
		} else
        {
			iTween.ValueTo(
				gameObject,
				iTween.Hash(
					"from", 0,
					"to", float.Parse(_score),
					"time", 1f,
					"easeType", iTween.EaseType.easeOutSine,
					"onupdate", "onUpdateScore"
				)
			);

			yield return new WaitForSeconds(1f);
		}

		//skills
		_skillsLabel.gameObject.SetActive(true);
		bg3.gameObject.SetActive(true);
		_skillsBaseIcons.SetActive(true);

		if (situation != null)
		{
			int skillsLevel = situation.GetSkillLevel();

			_skillsLabel.text = _skillsLabel.text + ": <b>" + skillsLevel.ToString() + "/5</b>";

			float transitionTime = 0.25f;
			switch (skillsLevel)
            {
				case 1:
					_skillsDone1.gameObject.SetActive(true);
					ActivateLevel(_skillsDone1.gameObject);
					yield return new WaitForSeconds(transitionTime);
					break;
				case 2:
					_skillsDone1.gameObject.SetActive(true);
					ActivateLevel(_skillsDone1.gameObject);
					yield return new WaitForSeconds(transitionTime);
					_skillsDone2.gameObject.SetActive(true);
					ActivateLevel(_skillsDone2.gameObject);
					yield return new WaitForSeconds(transitionTime);
					break;
				case 3:
					_skillsDone1.gameObject.SetActive(true);
					ActivateLevel(_skillsDone1.gameObject);
					yield return new WaitForSeconds(transitionTime);
					_skillsDone2.gameObject.SetActive(true);
					ActivateLevel(_skillsDone2.gameObject);
					yield return new WaitForSeconds(transitionTime);
					_skillsDone3.gameObject.SetActive(true);
					ActivateLevel(_skillsDone3.gameObject);
					yield return new WaitForSeconds(transitionTime);
					break;
				case 4:
					_skillsDone1.gameObject.SetActive(true);
					ActivateLevel(_skillsDone1.gameObject);
					yield return new WaitForSeconds(transitionTime);
					_skillsDone2.gameObject.SetActive(true);
					ActivateLevel(_skillsDone2.gameObject);
					yield return new WaitForSeconds(transitionTime);
					_skillsDone3.gameObject.SetActive(true);
					ActivateLevel(_skillsDone3.gameObject);
					yield return new WaitForSeconds(transitionTime);
					_skillsDone4.gameObject.SetActive(true);
					ActivateLevel(_skillsDone4.gameObject);
					yield return new WaitForSeconds(transitionTime);
					break;
				case 5:
					_skillsDone1.gameObject.SetActive(true);
					ActivateLevel(_skillsDone1.gameObject);
					yield return new WaitForSeconds(transitionTime);
					_skillsDone2.gameObject.SetActive(true);
					ActivateLevel(_skillsDone2.gameObject);
					yield return new WaitForSeconds(transitionTime);
					_skillsDone3.gameObject.SetActive(true);
					ActivateLevel(_skillsDone3.gameObject);
					yield return new WaitForSeconds(transitionTime);
					_skillsDone4.gameObject.SetActive(true);
					ActivateLevel(_skillsDone4.gameObject);
					yield return new WaitForSeconds(transitionTime);
					_skillsDone5.gameObject.SetActive(true);
					ActivateLevel(_skillsDone5.gameObject);
					yield return new WaitForSeconds(transitionTime);
					break;
				default:
					break;
            }

			yield return new WaitForSeconds(0.5f);
		}


		//level
		_levelLabel.gameObject.SetActive(true);
		_levelText.gameObject.SetActive(true);
		_levelImg.gameObject.SetActive(true);

		//SET LEVEL
		if (_total <= 0.2f )
        {
			//amateur
			_levelText.text = TextUtility.SetText(Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='score_level_a']").InnerText);
			_levelImg.sprite = _levelA;
		}
		else if (_total <= 0.4f)
		{
			//Aprendiz
			_levelText.text = TextUtility.SetText(Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='score_level_a2']").InnerText);
			_levelImg.sprite = _levelA2;
		}
		else if(_total <= 0.6f)
		{
			//practicante
			_levelText.text = TextUtility.SetText(Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='score_level_b']").InnerText);
			_levelImg.sprite = _levelB;
		}
		else if (_total <= 0.7f)
        {
			//profesional
			_levelText.text = TextUtility.SetText(Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='score_level_c']").InnerText);
			_levelImg.sprite = _levelC;
		}
		else if(_total >= 0.999f && timeVal < 900)
        {
			//leyenda
			_levelText.text = TextUtility.SetText(Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='score_level_f']").InnerText);
			_levelImg.sprite = _levelF;
		}
		else if (_total >= 0.9f && timeVal < 1200)
		{
			//maestro
			_levelText.text = TextUtility.SetText(Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='score_level_e']").InnerText);
			_levelImg.sprite = _levelE;
		}
		else
        {
			//experto
			_levelText.text = TextUtility.SetText(Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='score_level_d']").InnerText);
			_levelImg.sprite = _levelD;
		}

		iTween.ScaleTo(_levelImg.gameObject, iTween.Hash(
				"scale", new Vector3(0.5f, 0.5f, 0.5f),
				"looktarget", Camera.main,
				"easeType", iTween.EaseType.easeOutExpo,
				"time", 0.5f
			)
		);

		yield return new WaitForSeconds(0.5f);

		//accept
		_acceptBt.gameObject.SetActive(true);
		_acceptBt.gameObject.GetComponent<RectTransform>().localScale = new Vector3(0,0,0);
		iTween.ScaleTo(
			_acceptBt.gameObject,
			iTween.Hash(
				"scale", new Vector3(1,1,1),
				"time", 0.5f,
				"easetype", iTween.EaseType.easeOutBack
			)
		);

		StartCoroutine (loadListener());

	}

	private void onUpdateAttemptsLevel(float newValue)
    {
		_attemptsLevel.GetComponent<RectTransform>().sizeDelta = new Vector2(newValue, 17f);
	}

	private void onUpdateTime(float newValue)
    {
        int minutes = Mathf.FloorToInt(newValue / 60f);
        int seconds = (int)newValue - minutes * 60;

        _timeText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
	}

	private void onUpdateConcepts(float newValue)
	{
		_conceptsLevel.GetComponent<RectTransform>().sizeDelta = new Vector2(newValue, 17f);
	}

	private void onUpdateScore(float newValue)
    {
		if (Manager.Instance.globalAula)
		{
			float score_number = (newValue / Manager.Instance.globalMaxLimit) * 100;
			_scoreText.text = score_number.ToString("0.#") + " / " + 100.ToString();
		}
		else
		{
			_scoreText.text = newValue.ToString("0.#") + " / " + Manager.Instance.globalMaxLimit.ToString();
		}
	}

	private void ActivateLevel(GameObject level)
	{
		iTween.ScaleTo(level, iTween.Hash(
				"scale", new Vector3(0.5f, 0.5f, 0.5f),
				"looktarget", Camera.main,
				"easeType", iTween.EaseType.easeOutExpo,
				"time", 0.5f
			)
		);
	}

	IEnumerator loadListener(){
		if (Application.platform == RuntimePlatform.Android) {
			yield return new WaitForSeconds (4f);
		} else {
			yield return new WaitForSeconds (2f);
		}
		_acceptBt.onClick.AddListener (closeAction);
	}

	private void closeAction(){

		_acceptBt.gameObject.SetActive(false);
		capture = screenCapture.captureImage(new Rect(560, 300, 790, 600), "Main Camera");

		GameObject _background = GameObject.Find ("backgroundEval");
		if (_background != null) {
			Image _bg = GameObject.Find ("backgroundEval").GetComponent<Image>();
			_bg.CrossFadeAlpha (0f, 1f, false);
			Destroy(_background, 1.2f);
		}
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
        GameObject bg = GameObject.Find("TransversalBg");
		if (bg != null) {
			Image _bg1 = bg.GetComponent<Image>();
			if (_bg1)
			{
				_bg1.CrossFadeAlpha(0f, 1f, false);
				Destroy(bg, 1.2f);
			}
		}

        GameObject bg_Msg = GameObject.Find("blackoutWarning");
        if(bg_Msg) Destroy(bg_Msg, 1.2f);

		Destroy(gameObject, 1.3f);
		Invoke("CallReport", 1.21f);


	}

	private void CallReport()
    {
		List<reportImgElement> _list = new List<reportImgElement>();
		reportImgElement _img1 = new reportImgElement();

		_img1.type = 1;
		_img1.title1 = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='score_report']").InnerText;
		_img1.height1 = 1000;
		_img1.image1 = capture;
		_list.Add(_img1);

		StartCoroutine(evalObj.doReport(true, false, _list));
	}

	// Update is called once per frame
	void Update () {
		
	}
}
