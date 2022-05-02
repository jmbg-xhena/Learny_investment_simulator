using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Xml;

public class NotePad : MonoBehaviour {

	public InputField pageText;
	public Text _titleText;
	public Text _indicator;
	public Button _leftBt;
	public Button _rightBt;
	public Button _xBt;
	public Button _trashBt;
	public Button _addBt;
	public GameObject _bgPrefab;
	private GameObject _background;

	public List<PageElement> data = new List<PageElement>();

	private int currentIndex = -1;
	private float yPosition = 0;

	private int bgInitialIndex;

	private bool noQuestions = false;

	// Use this for initialization
	void Start () {
		XmlNode languageNode = Manager.Instance.globalLanguages.SelectSingleNode("/data/language[@code='" + Manager.Instance.globalLanguage + "']");
		string isRTL = languageNode.Attributes["isRTL"].Value;

		_leftBt.onClick.AddListener (leftAction);
		_rightBt.onClick.AddListener (rightAction);
		_xBt.onClick.AddListener (xAction);
		_trashBt.onClick.AddListener (trashAction);
		_addBt.onClick.AddListener (addAction);

		_trashBt.interactable = false;

		_titleText.fontSize = 31;
        _titleText.font = (Font)Resources.Load("Fonts/ArialBold31");

		if (noQuestions == false) {
			Debug.Log ("normal");
			yPosition = pageText.transform.localPosition.y;
			Debug.Log ("yPosition: " + yPosition.ToString());
		} else {
			Debug.Log ("start notepad no yPosition");
		}

		pageText.placeholder.GetComponent<Text> ().fontSize = 32;
		pageText.placeholder.GetComponent<Text> ().lineSpacing = 0.96f;
		pageText.placeholder.GetComponent<Text>().text = TextUtility.SetText(Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='notepadPlaceholder']").InnerText);

		if (isRTL == "true")
		{
			_titleText.alignment = TextAnchor.UpperRight;
			pageText.placeholder.GetComponent<Text>().alignment = TextAnchor.UpperRight;
			pageText.textComponent.alignment = TextAnchor.UpperRight;
			pageText.onValueChanged.AddListener(delegate { TextInputChanged(); });
		}
	}

	public void TextInputChanged()
	{
		pageText.caretPosition = 0;
	}

	public void addPage(string initialContent, bool _erasable = true, string _title = "", bool _noQuestions = false){
		data.Add (new PageElement (){ content = initialContent, erasable = _erasable, title = _title });
		if (currentIndex == -1) {
			currentIndex = 0;
		} else {
			PageElement _data = data [currentIndex];
			_data.content = pageText.text;
			data [currentIndex] = _data;

			currentIndex++;
		}
			
		pageText.text = TextUtility.SetText(initialContent);
		_titleText.text = TextUtility.SetText(_title);

		if (_noQuestions == true) {
			noQuestions = true;
			Debug.Log ("no questions page");
			_titleText.gameObject.SetActive (false);
			RectTransform _rect = pageText.GetComponent<RectTransform> ();
			_rect.sizeDelta = new Vector2 (_rect.rect.width, 475);
			yPosition = _rect.localPosition.y;
			Debug.Log ("yPosition: " + yPosition.ToString());
			_rect.localPosition = new Vector3 (_rect.localPosition.x, _rect.localPosition.y + 105, _rect.localPosition.z);
			_addBt.interactable = true;
		} else {
			if (_title == "") {
				configurePage ();
			}
		}
	}

	public void removePage(int _index){
		if (data [_index].erasable == true) {

			if (currentIndex > 0) {
				currentIndex--;
				pageText.text = TextUtility.SetText(data [currentIndex].content);
				_titleText.text = TextUtility.SetText(data [currentIndex].title);
			} else {
				if (data.Count > 1) {
					pageText.text = TextUtility.SetText(data [currentIndex+1].content);
				} else {
					addPage ("", true);
					currentIndex = 0;
					pageText.text = "";
				}
			}

			data.RemoveAt (_index);
			configurePage ();
		}
	}

	void leftAction(){
		if(currentIndex > 0){
			PageElement _data = data [currentIndex];
			_data.content = pageText.text;
			data [currentIndex] = _data;
			currentIndex--;
			pageText.text = TextUtility.SetText(data [currentIndex].content);
			_titleText.text = TextUtility.SetText(data [currentIndex].title);

			configurePage ();
				
		}
	}

	void rightAction(){
		if(currentIndex < (data.Count-1)){
			PageElement _data = data [currentIndex];
			_data.content = pageText.text;
			data [currentIndex] = _data;
			currentIndex++;
			pageText.text = TextUtility.SetText(data [currentIndex].content);
			_titleText.text = TextUtility.SetText(data [currentIndex].title);

			configurePage ();

		}
	}

	void xAction(){

		PageElement _data = data [currentIndex];
		_data.content = pageText.text;
		data [currentIndex] = _data;

		GameObject bg = GameObject.Find ("NotepadBg");

		Image _bg = bg.GetComponent<Image>();
		_bg.CrossFadeAlpha (0f, 1f, false);
		Destroy(bg, 1.2f);

        GameObject objectBlocker = GameObject.Find("ObjBlocker");
       // if (objectBlocker) objectBlocker.GetComponent<ObjectBlocker>().coll.enabled = false;

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
	}

	void trashAction(){
		removePage (currentIndex);
	}

	void addAction(){
		addPage ("", true,"");
	}

	public void goToFirstPage(){
		currentIndex=0;
		pageText.text = TextUtility.SetText(data [currentIndex].content);
		_titleText.text = TextUtility.SetText(data [currentIndex].title);

		if (currentIndex == (data.Count-1)) {
			_addBt.interactable = true;
		} else {
			_addBt.interactable = false;
		}
	}

	public void configurePage(){
		if (data [currentIndex].title == "") {
			_titleText.gameObject.SetActive(false);
			RectTransform _rect = pageText.GetComponent<RectTransform> ();
			_rect.sizeDelta = new Vector2 (_rect.rect.width, 475);
			_rect.localPosition = new Vector3 (pageText.transform.localPosition.x, yPosition + 105, pageText.transform.localPosition.z);
		} else {
			_titleText.gameObject.SetActive(true);
			RectTransform _rect = pageText.GetComponent<RectTransform> ();
			_rect.sizeDelta = new Vector2 (_rect.rect.width, 370);
			_rect.localPosition = new Vector3 (pageText.transform.localPosition.x, yPosition, pageText.transform.localPosition.z);
		}
		if (data [currentIndex].erasable == true) {
			_trashBt.interactable = true;
		} else {
			_trashBt.interactable = false;
		}

		if (currentIndex == (data.Count-1)) {
			_addBt.interactable = true;
		} else {
			_addBt.interactable = false;
		}

	}

	public void showNotePad(){

		gameObject.transform.SetAsLastSibling ();

		GameObject bg = GameObject.Find ("NotepadBg");
		if (bg) {
			bgInitialIndex = bg.transform.GetSiblingIndex ();
			bg.transform.SetSiblingIndex (gameObject.transform.GetSiblingIndex ()-1);
		} else {
			_background = Instantiate(_bgPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
			_background.name = "NotepadBg";
			_background.transform.SetParent (GameObject.Find("Canvas").transform, false);
			_background.transform.localPosition = new Vector3(0,0,0);
			_background.transform.SetSiblingIndex (gameObject.transform.GetSiblingIndex () - 1);
		}

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

	// Update is called once per frame
	void Update () {
		if (currentIndex != -1) {
			_indicator.text = TextUtility.SetText(Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='page']").InnerText + " " + (currentIndex + 1).ToString ());
		} else {
			_indicator.text = "";
		}
	}
}
