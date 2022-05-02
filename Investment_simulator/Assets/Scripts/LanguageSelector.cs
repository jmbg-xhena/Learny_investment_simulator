using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;

public class LanguageSelector : MonoBehaviour {

	public Text _title;
	public Text _message;
	public Button _buttonAction;
	public Dropdown _selector;
	public GameObject _backgroundPref;

	private GameObject _background;

	private XmlNodeList xmlList;
	
	void Start () {
		_title.font = (Font)Resources.Load("Fonts/ArialBold28");
		_message.font = (Font)Resources.Load("Fonts/ArialBold28");
		_buttonAction.GetComponentInChildren<Text>().font = (Font)Resources.Load("Fonts/ArialBold28");

		_background = Instantiate(_backgroundPref, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
		_background.name = "background";
		_background.transform.SetParent (GameObject.Find("Canvas").transform, false);
		_background.transform.localPosition = new Vector3(0, 0, 0);
		_background.transform.SetSiblingIndex(gameObject.transform.GetSiblingIndex() - 1);

		_title.text = TextUtility.SetText(Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='language_title']").InnerText);
		_message.text = TextUtility.SetText(Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='language_selector']").InnerText);
		_buttonAction.GetComponentInChildren<Text>().text = TextUtility.SetText(Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='accept']").InnerText);

		List<Dropdown.OptionData> listItems = new List<Dropdown.OptionData>();
		xmlList = Manager.Instance.globalLanguages.SelectNodes("/data/language");
		int index = 0;
		int selectedIndex = 0;
		foreach(XmlNode item in xmlList)
        {
			listItems.Add(new Dropdown.OptionData(item.Attributes["name"].Value));
            if (Manager.Instance.globalLanguage == item.Attributes["code"].Value)
            {
				selectedIndex = index;
			}
			index++;
		}
		_selector.options = listItems;
		_selector.value = selectedIndex;
	}

	public void LoadLanguage()
    {
		XmlNode itemSelected = xmlList[_selector.value];
		string languageSelected = itemSelected.Attributes["code"].Value;

		PlayerPrefs.SetString("language", languageSelected);

		Manager.Instance.globalLanguage = languageSelected;

		GameObject menuObj = GameObject.Find(Manager.Instance.globalMenuName);
		if (menuObj != null)
		{
			menu _menu = menuObj.GetComponent<menu>();
			_menu.Language = languageSelected;
			if (_menu.LanguageFirstTime == true)
			{
				PlayerPrefs.SetString("languageFirstTime", "true");
			}

			StartCoroutine(_menu.loadLanguageXML(true, false));
		}
		else
		{
            Invoke("RestartScene", 1f);
		}

		iTween.MoveTo(
			gameObject,
			iTween.Hash(
				"position", new Vector3(0, 1200, 0),
				"looktarget", Camera.main,
				"easeType", iTween.EaseType.easeOutExpo,
				"time", 1f,
				"islocal", true
			)
		);

		Destroy(gameObject, 2);
	}

    private void RestartScene()
    {
		BaseSituation situation = GameObject.FindObjectOfType<BaseSituation>();
		situation.RestartScene();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
