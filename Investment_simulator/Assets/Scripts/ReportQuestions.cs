using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class ReportQuestions : MonoBehaviour {

	public Text _title;
	public Image _statementImage;
	public TEXDraw _statementText;
	public Text _answersTitle;
	public Text _correctAnswersTitle;
	public TEXDraw _answersText;
	public TEXDraw _correctAnswersText;
	public Text _titleSection;

    public GameObject container;

	public IEnumerator loadData(){
		XmlNode languageNode = Manager.Instance.globalLanguages.SelectSingleNode("/data/language[@code='" + Manager.Instance.globalLanguage + "']");
		string isRTL = languageNode.Attributes["isRTL"].Value;

		_title.fontSize = 48;
		_title.font = (Font)Resources.Load("Fonts/ArialBold48");
		_titleSection.font = (Font)Resources.Load("Fonts/ArialBold48");
		_answersTitle.font = (Font)Resources.Load("Fonts/ArialBold42");
		_correctAnswersTitle.font = (Font)Resources.Load("Fonts/ArialBold42");

		_title.text = TextUtility.SetText(Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='simulator_title']").InnerText);
		_titleSection.text = TextUtility.SetText(Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='evaluationReport']").InnerText);

		_statementImage.sprite = Resources.Load<Sprite>( Manager.Instance.globalQuestions.Item(0).Attributes.GetNamedItem("image").InnerText );
		_statementImage.preserveAspect = true;

		if (isRTL == "true")
		{
			_title.alignment = TextAnchor.MiddleRight;
			_answersTitle.alignment = TextAnchor.UpperRight;
			_correctAnswersTitle.alignment = TextAnchor.UpperRight;
		}

		string _statement = Manager.Instance.globalQuestions.Item (0).Attributes.GetNamedItem ("text").InnerText;
		string _answers = "\n";
		string _correct_answers = "\n";
		for (int i = 0; i < Manager.Instance.globalQuestions.Item (0).ChildNodes.Count; i++) {
			_statement = _statement + "\n\n";
			_statement = _statement + (i + 1).ToString () + ". ";
			_statement = _statement + Manager.Instance.globalQuestions.Item (0).ChildNodes.Item (i).Attributes.GetNamedItem ("text").InnerText;
		
		
			for (int j = 0; j < Manager.Instance.globalQuestions.Item (0).ChildNodes.Item (i).ChildNodes.Count; j++) {

				if(Manager.Instance.globalQuestions.Item (0).ChildNodes.Item (i).ChildNodes.Item(j).Attributes.GetNamedItem("selected").InnerText == "True"){
					_answers = _answers + (i + 1).ToString () + ". ";
					_answers = _answers + Manager.Instance.globalQuestions.Item (0).ChildNodes.Item (i).ChildNodes.Item(j).InnerText;
					_answers = _answers + "\n\n\n";
				}

				if(Manager.Instance.globalQuestions.Item (0).ChildNodes.Item (i).ChildNodes.Item(j).Attributes.GetNamedItem("correct").InnerText == "True"){
					_correct_answers = _correct_answers + (i + 1).ToString () + ". ";
					_correct_answers = _correct_answers + Manager.Instance.globalQuestions.Item (0).ChildNodes.Item (i).ChildNodes.Item(j).InnerText;
					_correct_answers = _correct_answers + "\n\n\n";
				}
			}
		}

		_statementText.text = TextUtility.SetText(_statement);

		_statementText.text = _statementText.text + " ";

		_answersTitle.text = TextUtility.SetText(Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='answers']").InnerText);
		_correctAnswersTitle.text = TextUtility.SetText(Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='correct_answers']").InnerText);

		_answersText.text = TextUtility.SetText(_answers);

		_correctAnswersText.text = TextUtility.SetText(_correct_answers);

		if (isRTL == "true")
		{
			_answersText.text = "\\meta[align=r]\n" + _answersText.text;
			_correctAnswersText.text = "\\meta[align=r]\n" + _correctAnswersText.text;
		}

		LayoutRebuilder.ForceRebuildLayoutImmediate(container.GetComponent<RectTransform>());

        yield return null;
    }
}
