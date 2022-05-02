using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class ReportComplementary : MonoBehaviour {

	public Text _title;
	public Text _contentTitle;
	public Text _contentText;
    public TEXDraw _richText;

    public GameObject container;

    public IEnumerator loadData(){
		XmlNode languageNode = Manager.Instance.globalLanguages.SelectSingleNode("/data/language[@code='" + Manager.Instance.globalLanguage + "']");
		string isRTL = languageNode.Attributes["isRTL"].Value;

		_title.fontSize = 48;
		_title.font = (Font)Resources.Load("Fonts/ArialBold48");
		_contentTitle.font = (Font)Resources.Load("Fonts/ArialBold48");
		_title.text = TextUtility.SetText(Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='simulator_title']").InnerText);
		_contentTitle.text = TextUtility.SetText(Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='complementary_questions']").InnerText);

		GameObject _notePad = GameObject.Find ("notePad");

		string _content = "";
		if (_notePad != null) {
			if (_notePad.GetComponent<NotePad> ().data.Count > 0) {
				for (int i = 0; i < _notePad.GetComponent<NotePad> ().data.Count; i++) {
					if (_notePad.GetComponent<NotePad> ().data [i].title != "") {
						if (isRTL == "true")
						{
							_content = _content + "\\meta[align=r]";
						}
						_content = _content + "\\opens[b]{" + TextUtility.SetText(_notePad.GetComponent<NotePad> ().data [i].title) + "}";
						_content = _content + "\n\n";
					}

					if (_notePad.GetComponent<NotePad> ().data [i].content != "") {
						if (isRTL == "true")
						{
							_content = _content + "\\meta[align=r]";
						}
						_content = _content + "\\opens{" + TextUtility.SetText(_notePad.GetComponent<NotePad> ().data [i].content) + "}";
						_content = _content + "\n\n\n";
					}
				}

                _richText.text = _content;
				//yield return new WaitForSeconds (0.5f);
			}
		}
        LayoutRebuilder.ForceRebuildLayoutImmediate(container.GetComponent<RectTransform>());
        //Destroy (gameObject, 2.0f);

        yield return null;
    }
}
