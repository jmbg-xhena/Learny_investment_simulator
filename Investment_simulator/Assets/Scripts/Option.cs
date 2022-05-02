using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;

public class Option : MonoBehaviour {

	public Sprite onElement;
	public Sprite offElement;
	public Button btElement;
	public TEXDraw textElement;

	public bool selected = false;
	public int index = 0;
	// Use this for initialization
	void Start () {
		btElement.GetComponent<Image> ().sprite = offElement;
		btElement.onClick.AddListener (setActiveState);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setText(string _text, float _size = 36){
		textElement.text = TextUtility.SetText(_text);
		textElement.size = _size;
	}

	public void setState(bool _state){

		GameObject _evaluation = GameObject.Find("SitEvaluation");
		int _statementIndex = _evaluation.GetComponent<Evaluation> ().statementIndex;
		int _questionIndex = _evaluation.GetComponent<Evaluation> ().questionIndex;
		int[] _orderOptions = _evaluation.GetComponent<Evaluation> ().orderOptions;

		if (_state == true) {
			btElement.GetComponent<Image> ().sprite = onElement;
			selected = true;
		} else {
			btElement.GetComponent<Image> ().sprite = offElement;
			selected = false;
		}

		XmlElement _attribute = Manager.Instance.globalQuestions.Item (_statementIndex).ChildNodes.Item (_questionIndex).ChildNodes.Item (_orderOptions [index-1]) as XmlElement;
		_attribute.SetAttribute ("selected", selected.ToString());
	}

	private void setActiveState(){
		setState (true);

		switch (index) {
		case 1:
			GameObject.Find ("EvalOption2").GetComponent<Option> ().setState (false);
			GameObject.Find ("EvalOption3").GetComponent<Option> ().setState (false);
			GameObject.Find ("EvalOption4").GetComponent<Option> ().setState (false);
			break;
		case 2:
			GameObject.Find ("EvalOption1").GetComponent<Option> ().setState (false);
			GameObject.Find ("EvalOption3").GetComponent<Option> ().setState (false);
			GameObject.Find ("EvalOption4").GetComponent<Option> ().setState (false);
			break;
		case 3:
			GameObject.Find ("EvalOption1").GetComponent<Option> ().setState (false);
			GameObject.Find ("EvalOption2").GetComponent<Option> ().setState (false);
			GameObject.Find ("EvalOption4").GetComponent<Option> ().setState (false);
			break;
		case 4:
			GameObject.Find ("EvalOption1").GetComponent<Option> ().setState (false);
			GameObject.Find ("EvalOption2").GetComponent<Option> ().setState (false);
			GameObject.Find ("EvalOption3").GetComponent<Option> ().setState (false);
			break;
		}

	}
}
