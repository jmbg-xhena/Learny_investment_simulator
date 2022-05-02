using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Xml;
using System;

public class Alert : MonoBehaviour {

	public Image background_1;
	public Image background_2;
	public Image background_3;
	public Image background_4;
    public Image background_5;

    public Button bt1;
	public Button bt2;
	public Button bt3;

	public Text title_label;
	public Text subtitle_label;
	public GameObject text_area;
	public TEXDraw text_equations;
	public GameObject scrollView;

	public Button x_alert;
	public Button button_1;
	public Button button_2;

	public delegate void Bt1Action();
	public delegate void Bt2Action();
	public delegate void Bt3Action();

	public Bt1Action bt_1_action;
	public Bt2Action bt_2_action;
	public Bt3Action bt_3_action;

	public GameObject _backgroundPref;

	private GameObject _background;

	// Use this for initialization
	private Vector3 _position;
	private int bgInitialIndex;

	private string[] _messages;
	private string _subtitleGlobal;

	private string isRTL = "false";

	void Start () {

		background_1.enabled = false;
		background_2.enabled = false;
		background_3.enabled = false;
		background_4.enabled = false;
        background_5.enabled = false;

        bt1.gameObject.SetActive(false);
		bt2.gameObject.SetActive(false);
		bt3.gameObject.SetActive(false);
		x_alert.gameObject.SetActive(false);
		button_1.gameObject.SetActive(false);
		button_2.gameObject.SetActive(false);

		title_label.enabled = false;
		subtitle_label.enabled = false;
		text_area.SetActive(false);
		text_equations.enabled = false;

		title_label.font = (Font)Resources.Load("Fonts/ArialBold28");
		bt1.GetComponentInChildren<Text>().font = (Font)Resources.Load("Fonts/ArialBold30");
		bt2.GetComponentInChildren<Text>().font = (Font)Resources.Load("Fonts/ArialBold30");
		bt3.GetComponentInChildren<Text>().font = (Font)Resources.Load("Fonts/ArialBold30");
		button_1.GetComponentInChildren<Text>().font = (Font)Resources.Load("Fonts/ArialBold34");
		button_2.GetComponentInChildren<Text>().font = (Font)Resources.Load("Fonts/ArialBold34");
		subtitle_label.font = (Font)Resources.Load("Fonts/ArialBold42");

		text_area.GetComponent<TextMeshProUGUI>().font = (TMP_FontAsset)Resources.Load("Fonts/Arial SDF");

		XmlNode languageNode = Manager.Instance.globalLanguages.SelectSingleNode("/data/language[@code='" + Manager.Instance.globalLanguage + "']");
		isRTL = languageNode.Attributes["isRTL"].Value;

		if (isRTL == "true")
		{
			text_area.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Right;
			text_area.GetComponent<TextMeshProUGUI>().fontSize = 45;
			text_equations.alignment = new Vector2(1, 1);
		}
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void showAlert(int _type, string _header, string _subtitle, string _message, string text_bt_1 = "", string text_bt_2 = "", Bt1Action listener_bt_1 = null, Bt2Action listener_bt_2 = null, Bt3Action listener_bt_x = null){
		/*
		kind of alerts: 
		1: small window with 1 button
		2: small two buttons window
		3: instructions or situation start no buttons
		4: help window no buttons
		5: situation menu
		6: small window without buttons, text centered
		7: Situation menu with only 2 options
		8: Small window with X button and subtitle       
		*/

		switch(_type){
		case 1:
			text_area.GetComponent<TextMeshProUGUI>().fontSize = 35;
			text_area.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
			text_area.GetComponent<TextMeshProUGUI>().text = TextUtility.SetText(_message, false, true);
			_position = new Vector3 (-700, -540, 0);
			button_1.GetComponentInChildren<Text> ().text = TextUtility.SetText(text_bt_1);
			button_1.transform.localPosition = new Vector3 (710, 260, 0);
			StartCoroutine (ActivateType1());
			if (listener_bt_1 != null) {
				button_1.onClick.AddListener ( () => listener_bt_1.Invoke());
			} else {
				button_1.onClick.AddListener (closeAlert);
			}
			break;
		case 2:
			text_area.GetComponent<TextMeshProUGUI>().fontSize = 35;
			text_area.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
			text_area.GetComponent<TextMeshProUGUI>().text = TextUtility.SetText(_message, false, true);
			_position = new Vector3 (-700, -540, 0);
			button_1.GetComponentInChildren<Text> ().text = TextUtility.SetText(text_bt_1);
			button_1.transform.localPosition = new Vector3 (510, 260, 0);
			button_2.GetComponentInChildren<Text> ().text = TextUtility.SetText(text_bt_2);
			button_2.transform.localPosition = new Vector3 (910, 260, 0);
			StartCoroutine (ActivateType2());
			if (listener_bt_1 != null) {
				button_1.onClick.AddListener ( () => listener_bt_1.Invoke());
			} else {
				button_1.onClick.AddListener (closeAlert);
			}
			if (listener_bt_2 != null) {
				button_2.onClick.AddListener ( () => listener_bt_2.Invoke());
			} else {
				button_2.onClick.AddListener (closeAlert);
			}
			break;
		case 3:
			text_area.GetComponent<TextMeshProUGUI> ().fontSize = 35;
			if (isRTL == "true")
			{
				text_area.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Right;
				title_label.alignment = TextAnchor.MiddleRight;
			}
			else
			{
				text_area.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Justified;
			}
			text_area.GetComponent<TextMeshProUGUI> ().text = TextUtility.SetText(_message, false, true);
			title_label.text = TextUtility.SetText(_header);
			if (_subtitle != "") {
				subtitle_label.text = TextUtility.SetText(_subtitle);
				_position = new Vector3 (-700, -420, 0);
				scrollView.GetComponent<RectTransform> ().SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, 400f);
				scrollView.transform.localPosition = new Vector3 (700, 350, 0);
			} else {
				subtitle_label.text = "";
				_position = new Vector3 (-700, -420, 0);
				scrollView.GetComponent<RectTransform> ().SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, 500f);
				scrollView.transform.localPosition = new Vector3 (700, 400, 0);
			}

			if (listener_bt_x != null) {
				x_alert.onClick.AddListener (() => listener_bt_x.Invoke ());
			} else {
				x_alert.onClick.AddListener (closeAlert);
			}
				
			StartCoroutine (ActivateType3 ());
			break;
		case 4:
			text_area.GetComponent<TextMeshProUGUI>().fontSize = 35;
			if (isRTL == "true")
            {
                text_area.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Right;
                title_label.alignment = TextAnchor.MiddleRight;
                title_label.transform.localPosition = new Vector3(520, title_label.transform.localPosition.y, 0);
            }
            else
            {
                text_area.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Justified;
                title_label.transform.localPosition = new Vector3(570, title_label.transform.localPosition.y, 0);
            }
			text_area.GetComponent<TextMeshProUGUI>().text = TextUtility.SetText( _message, false, true);
			title_label.text = TextUtility.SetText(_header);
			title_label.transform.localPosition = new Vector3 (570, title_label.transform.localPosition.y,0);
			_position = new Vector3 (-700, -420, 0);
			scrollView.GetComponent<RectTransform> ().SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, 500f);
			scrollView.transform.localPosition = new Vector3 (700,400,0);
			StartCoroutine (ActivateType4 ());
			x_alert.onClick.AddListener (closeAlert);
			break;
		case 5:
			string[] _titles = _header.Split('|');
			_messages = _message.Split ('|');
			_subtitleGlobal = _subtitle;
			subtitle_label.text = TextUtility.SetText(_subtitle);
			text_area.GetComponent<TextMeshProUGUI>().fontSize = 35;
			if (isRTL == "true")
            {
                text_area.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Right;
            }
            else
            {
                text_area.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Justified;
            }
			text_area.GetComponent<TextMeshProUGUI>().text = TextUtility.SetText(_messages[0], false, true);
			bt1.GetComponentInChildren<Text> ().text = TextUtility.SetText(_titles[0]);
			bt2.GetComponentInChildren<Text> ().text = TextUtility.SetText(_titles[1]);
			bt3.GetComponentInChildren<Text> ().text = TextUtility.SetText(_titles[2]);
			_position = new Vector3 (-700, -420, 0);
			scrollView.GetComponent<RectTransform> ().SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, 400f);
			scrollView.transform.localPosition = new Vector3 (700, 350, 0);
			StartCoroutine (ActivateType5 ());
			x_alert.onClick.AddListener (closeAlert);
			bt1.onClick.AddListener (Menu1Action);
			bt2.onClick.AddListener (Menu2Action);
			bt3.onClick.AddListener (Menu3Action);
			break;
		case 6:
			text_area.GetComponent<TextMeshProUGUI>().fontSize = 35;
			text_area.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
			text_area.GetComponent<TextMeshProUGUI>().text = TextUtility.SetText(_message, false, true);
			_position = new Vector3 (-700, -540, 0);
			button_1.GetComponentInChildren<Text> ().text = TextUtility.SetText(text_bt_1);
			button_1.transform.localPosition = new Vector3 (710, 260, 0);
			StartCoroutine (ActivateType6());
			break;
        case 7:
            string[] _titles7 = _header.Split('|');
            _messages = _message.Split('|');
            _subtitleGlobal = _subtitle;
            subtitle_label.text = _subtitle;
            text_area.GetComponent<TextMeshProUGUI>().fontSize = 35;
            if (isRTL == "true")
            {
                text_area.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Right;
            }
            else
            {
                text_area.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Justified;
            }
            text_area.GetComponent<TextMeshProUGUI>().text = TextUtility.SetText(_messages[0], false, true);
            bt1.GetComponentInChildren<Text>().text = TextUtility.SetText(_titles7[0]);
            bt2.GetComponentInChildren<Text>().text = TextUtility.SetText(_titles7[1]);
            _position = new Vector3(-700, -420, 0);
            scrollView.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 400f);
            scrollView.transform.localPosition = new Vector3(700, 350, 0);
            StartCoroutine(ActivateType7());
            x_alert.onClick.AddListener(closeAlert);
            bt1.onClick.AddListener(Menu1Action);
            bt2.onClick.AddListener(Menu2Action);
            break;
        case 8:
            text_area.GetComponent<TextMeshProUGUI>().fontSize = 35;
            text_area.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
            text_area.GetComponent<TextMeshProUGUI>().text = TextUtility.SetText(_message, false, true);
            _position = new Vector3(-700, -540, 0);

            if (_subtitle != "")
            {
                subtitle_label.alignment = TextAnchor.MiddleCenter;
                subtitle_label.text = TextUtility.SetText(_subtitle);
                subtitle_label.fontSize = 58;
                _position = new Vector3(-700, -420, 0);
                scrollView.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 200f);
                scrollView.transform.localPosition = new Vector3(700, 450, 0);
            }
            else
            {
                subtitle_label.text = "";
                _position = new Vector3(-700, -420, 0);
                scrollView.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 300f);
                scrollView.transform.localPosition = new Vector3(700, 500, 0);
            }

            if (listener_bt_x != null)
            {
                x_alert.onClick.AddListener(() => listener_bt_x.Invoke());
            }
            else
            {
                x_alert.onClick.AddListener(closeAlert);
            }

            StartCoroutine(ActivateType8());
            break;
            default:
			break;
		}
		/*	

		*/
	}

	IEnumerator ActivateType1(){
		GameObject bg = GameObject.Find ("TransversalBg");
		if (bg) {
			Destroy (bg);
		}

		_background = Instantiate(_backgroundPref, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
		_background.name = "TransversalBg";
		_background.transform.SetParent (GameObject.Find("Canvas").transform, false);
		_background.transform.localPosition = new Vector3(0,0,0);
		_background.transform.SetAsLastSibling ();

		gameObject.transform.SetAsLastSibling ();

		GameObject _msgScore = GameObject.Find ("_scoreMsg");
		if (_msgScore) {
			//_msgScore.transform.SetSiblingIndex (gameObject.transform.GetSiblingIndex () - 2);
		}

		yield return new WaitForSeconds ((float)0.1);

		background_2.enabled = true;
		text_area.gameObject.SetActive(true);

		button_1.gameObject.SetActive(true);


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
	}

	IEnumerator ActivateType2(){
		GameObject bg = GameObject.Find ("TransversalBg");
		if (bg) {
			Destroy (bg);
		}

		_background = Instantiate(_backgroundPref, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
		_background.name = "TransversalBg";
		_background.transform.SetParent (GameObject.Find("Canvas").transform, false);
		_background.transform.localPosition = new Vector3(0,0,0);
		_background.transform.SetSiblingIndex (gameObject.transform.GetSiblingIndex () - 1);

		_background.transform.SetAsLastSibling ();

		gameObject.transform.SetAsLastSibling ();

		yield return new WaitForSeconds ((float)0.1);

		background_2.enabled = true;
		text_area.gameObject.SetActive(true);

		button_1.gameObject.SetActive(true);
		button_2.gameObject.SetActive(true);


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
	}

	IEnumerator ActivateType3(){
		GameObject bg = GameObject.Find ("TransversalBg");
		if (bg) {
			Destroy (bg);
		}

		_background = Instantiate(_backgroundPref, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
		_background.name = "TransversalBg";
		_background.transform.SetParent (GameObject.Find("Canvas").transform, false);
		_background.transform.localPosition = new Vector3(0,0,0);
		_background.transform.SetSiblingIndex (gameObject.transform.GetSiblingIndex () - 1);

		_background.transform.SetAsLastSibling ();

		gameObject.transform.SetAsLastSibling ();

		yield return new WaitForSeconds ((float)0.1);

		background_1.enabled = true;
		text_area.gameObject.SetActive(true);
		title_label.enabled = true;
		subtitle_label.enabled = true;

		x_alert.gameObject.SetActive (true);

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
	}

	IEnumerator ActivateType4(){
		GameObject bg = GameObject.Find ("TransversalBg");
		if (bg) {
			Destroy (bg);
		}

		_background = Instantiate(_backgroundPref, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
		_background.name = "TransversalBg";
		_background.transform.SetParent (GameObject.Find("Canvas").transform, false);
		_background.transform.localPosition = new Vector3(0,0,0);
		_background.transform.SetSiblingIndex (gameObject.transform.GetSiblingIndex () - 1);

		_background.transform.SetAsLastSibling ();
		gameObject.transform.SetAsLastSibling ();

		yield return new WaitForSeconds ((float)0.1);

		background_3.enabled = true;
		text_area.gameObject.SetActive(true);
		title_label.enabled = true;

		x_alert.gameObject.SetActive (true);

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
	}

	IEnumerator ActivateType5(){
		GameObject bg = GameObject.Find ("TransversalBg");
		if (bg) {
			Destroy (bg);
		}

		_background = Instantiate(_backgroundPref, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
		_background.name = "TransversalBg";
		_background.transform.SetParent (GameObject.Find("Canvas").transform, false);
		_background.transform.localPosition = new Vector3(0,0,0);
		_background.transform.SetSiblingIndex (gameObject.transform.GetSiblingIndex () - 1);

		_background.transform.SetAsLastSibling ();
		gameObject.transform.SetAsLastSibling ();

		yield return new WaitForSeconds ((float)0.1);

		background_4.enabled = true;
		background_4.transform.localPosition = new Vector3 (background_4.transform.localPosition.x, background_4.transform.localPosition.y-2, background_4.transform.localPosition.z);
		text_area.gameObject.SetActive(true);
		subtitle_label.enabled = true;

		bt1.gameObject.SetActive (true);
		bt2.gameObject.SetActive (true);
		bt3.gameObject.SetActive (true);

		bt1.transform.localPosition = new Vector3 (bt1.transform.localPosition.x, bt1.transform.localPosition.y-2, bt1.transform.localPosition.z);
		bt2.transform.localPosition = new Vector3 (bt2.transform.localPosition.x, bt2.transform.localPosition.y-2, bt2.transform.localPosition.z);
		bt3.transform.localPosition = new Vector3 (bt3.transform.localPosition.x, bt3.transform.localPosition.y-2, bt3.transform.localPosition.z);

		bt2.GetComponent<Image> ().enabled = false;
		bt3.GetComponent<Image> ().enabled = false;

		x_alert.gameObject.SetActive (true);

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
	}

	IEnumerator ActivateType6(){
		GameObject bg = GameObject.Find ("TransversalBg");
		if (bg) {
			Destroy (bg);
		}

		_background = Instantiate(_backgroundPref, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
		_background.name = "TransversalBg";
		_background.transform.SetParent (GameObject.Find("Canvas").transform, false);
		_background.transform.localPosition = new Vector3(0,0,0);
		_background.transform.SetSiblingIndex (gameObject.transform.GetSiblingIndex () - 1);

		_background.transform.SetAsLastSibling ();
		gameObject.transform.SetAsLastSibling ();

		GameObject _msgScore = GameObject.Find ("_scoreMsg");
		if (_msgScore) {
			//_msgScore.transform.SetSiblingIndex (gameObject.transform.GetSiblingIndex () - 2);
		}

		yield return new WaitForSeconds ((float)0.1);

		background_2.enabled = true;
		text_area.gameObject.SetActive(true);

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
	}

    IEnumerator ActivateType7()
    {
        GameObject bg = GameObject.Find("TransversalBg");
        if (bg)
        {
            Destroy(bg);
        }

        _background = Instantiate(_backgroundPref, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        _background.name = "TransversalBg";
        _background.transform.SetParent(GameObject.Find("Canvas").transform, false);
        _background.transform.localPosition = new Vector3(0, 0, 0);
        _background.transform.SetSiblingIndex(gameObject.transform.GetSiblingIndex() - 1);

        _background.transform.SetAsLastSibling();
        gameObject.transform.SetAsLastSibling();

        yield return new WaitForSeconds((float)0.1);

        background_5.enabled = true;
        background_5.transform.localPosition = new Vector3(background_5.transform.localPosition.x, background_5.transform.localPosition.y - 2, background_5.transform.localPosition.z);
        text_area.gameObject.SetActive(true);
        subtitle_label.enabled = true;

        bt1.gameObject.SetActive(true);
        bt2.gameObject.SetActive(true);

        bt1.transform.localPosition = new Vector3(bt1.transform.localPosition.x, bt1.transform.localPosition.y - 2, bt1.transform.localPosition.z);
        bt2.transform.localPosition = new Vector3(bt2.transform.localPosition.x, bt2.transform.localPosition.y - 2, bt2.transform.localPosition.z);

        bt2.GetComponent<Image>().enabled = false;
        bt3.GetComponent<Image>().enabled = false;

        x_alert.gameObject.SetActive(true);

        iTween.MoveTo(
            gameObject,
            iTween.Hash(
                "position", _position,
                "looktarget", Camera.main,
                "easeType", iTween.EaseType.easeOutExpo,
                "time", 1f,
                "islocal", true
            )
        );
    }

    IEnumerator ActivateType8()
    {
        GameObject bg = GameObject.Find("TransversalBg");
        if (bg)
        {
            Destroy(bg);
        }

        _background = Instantiate(_backgroundPref, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        _background.name = "TransversalBg";
        _background.transform.SetParent(GameObject.Find("Canvas").transform, false);
        _background.transform.localPosition = new Vector3(0, 0, 0);
        _background.transform.SetSiblingIndex(gameObject.transform.GetSiblingIndex() - 1);

        _background.transform.SetAsLastSibling();
        gameObject.transform.SetAsLastSibling();

        yield return new WaitForSeconds((float)0.1);

        background_2.enabled = true;
        text_area.gameObject.SetActive(true);

        if(subtitle_label.text != "")
        {
            subtitle_label.enabled = true;
        }

        x_alert.gameObject.SetActive(true);

        iTween.MoveTo(
            gameObject,
            iTween.Hash(
                "position", _position,
                "looktarget", Camera.main,
                "easeType", iTween.EaseType.easeOutExpo,
                "time", 1f,
                "islocal", true
            )
        );
    }

    public void closeAlert(){
		GameObject bg = GameObject.Find ("TransversalBg");

		if (bg != null) {
			if (GameObject.FindObjectsOfType<Login>().Length > 0) {
				Login _login = GameObject.FindObjectOfType<Login> ();
				bg.transform.SetSiblingIndex (_login.transform.GetSiblingIndex()-1);
			} else {
				Image _bg = bg.GetComponent<Image>();
				_bg.CrossFadeAlpha (0f, 1f, false);
				Destroy(bg, 1.2f);
			}
		}
			
		_position = new Vector3 (-700, 700, 0);

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

		GameObject btnRegister = GameObject.Find("Register_Button");
		if (btnRegister)
		{
			/*if (!btnRegister.GetComponent<DialogButtonManager>().isDialogVisible)
			{
				GameObject objectBlocker = GameObject.Find("ObjBlocker");
				if (objectBlocker) objectBlocker.GetComponent<ObjectBlocker>().coll.enabled = false;
			}*/
		}
		else
		{
			GameObject objectBlocker = GameObject.Find("ObjBlocker");
			if (objectBlocker) objectBlocker.GetComponent<ObjectBlocker>().coll.enabled = false;
		}
		GameObject bluePrint = GameObject.Find("WorkTable/BlueprintScreen");
		if (bluePrint) bluePrint.GetComponent<MeshCollider>().enabled = true;

		GameObject bg_Msg = GameObject.Find("blackoutWarning");
		if(bg_Msg) Destroy(bg_Msg, 1.2f);

		Destroy(gameObject, 1.1f);
	}

	public void closeLoadingAlert(){

		if (Manager.Instance.globalAula != true) {
			GameObject bg = GameObject.Find ("TransversalBg");

			if (bg != null) {
				if (GameObject.FindObjectsOfType<Login>().Length > 0) {
					Login _login = GameObject.FindObjectOfType<Login> ();
					bg.transform.SetSiblingIndex (_login.transform.GetSiblingIndex()-1);
				} else {
					Image _bg = bg.GetComponent<Image>();
					_bg.CrossFadeAlpha (0f, 1f, false);
					Destroy(bg, 1.2f);
				}
			}

			GameObject btnRegister = GameObject.Find("Register_Button");
			if (btnRegister)
			{
				/*if (!btnRegister.GetComponent<DialogButtonManager>().isDialogVisible)
				{
					GameObject objectBlocker = GameObject.Find("ObjBlocker");
					if (objectBlocker) objectBlocker.GetComponent<ObjectBlocker>().coll.enabled = false;
				}*/
			}
			else
			{
				GameObject objectBlocker = GameObject.Find("ObjBlocker");
				if (objectBlocker) objectBlocker.GetComponent<ObjectBlocker>().coll.enabled = false;
			}
			GameObject bluePrint = GameObject.Find("WorkTable/BlueprintScreen");
			if (bluePrint) bluePrint.GetComponent<MeshCollider>().enabled = true;

			GameObject bg_Msg = GameObject.Find("blackoutWarning");
			if(bg_Msg) Destroy(bg_Msg, 1.2f);
		}

		_position = new Vector3 (-700, 700, 0);

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

		Destroy(gameObject, 1.1f);

	}

	public void Menu1Action(){
		Debug.Log ("menu1");
		bt1.GetComponent<Image> ().enabled = true;
		bt2.GetComponent<Image> ().enabled = false;
		bt3.GetComponent<Image> ().enabled = false;

		scrollView.GetComponent<RectTransform> ().SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, 400f);
		scrollView.transform.localPosition = new Vector3 (700, 350, 0);
		text_area.GetComponent<TextMeshProUGUI>().text = TextUtility.SetText(_messages[0], false, true);

		text_area.gameObject.SetActive(true);
		text_equations.enabled = false;

		subtitle_label.enabled = true;
		subtitle_label.text = TextUtility.SetText(_subtitleGlobal);
		//text_area.alignment = TextAnchor.UpperLeft;
	}

	public void Menu2Action(){
		Debug.Log ("menu2");
		bt1.GetComponent<Image> ().enabled = false;
		bt2.GetComponent<Image> ().enabled = true;
		bt3.GetComponent<Image> ().enabled = false;
		scrollView.GetComponent<RectTransform> ().SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, 500f);
		scrollView.transform.localPosition = new Vector3 (700, 400, 0);
		text_area.GetComponent<TextMeshProUGUI>().text = TextUtility.SetText(_messages[1], false, true);

		text_area.gameObject.SetActive(true);
		text_equations.enabled = false;

		subtitle_label.enabled = false;
		subtitle_label.text = "";
		//text_area.alignment = TextAnchor.UpperLeft;
	}

	public void Menu3Action(){
		Debug.Log ("menu3");
		bt1.GetComponent<Image> ().enabled = false;
		bt2.GetComponent<Image> ().enabled = false;
		bt3.GetComponent<Image> ().enabled = true;
		scrollView.GetComponent<RectTransform> ().SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, 500f);
		scrollView.transform.localPosition = new Vector3 (700, 400, 0);

		text_area.GetComponent<TextMeshProUGUI>().text = "";

		text_area.gameObject.SetActive(false);
		text_equations.enabled = true;

		string _message3 = "";
		if (_messages.Length > 3) {
			for (int i = 2; i < (_messages.Length); i++) {
				if(i == 2){
					_message3 = _messages [i];
				}
				else {
					_message3 = _message3 + "|" + _messages [i];
				}

			}
		} else {
			_message3 = _messages [2];
		}

		text_equations.text = TextUtility.SetText(_message3, true);

		subtitle_label.enabled = false;
		subtitle_label.text = "";
	}

}
