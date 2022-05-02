using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TwoColumnReport : MonoBehaviour {

	// Use this for initialization
	public GameObject header1;
	public Text headerText1;
	public Image contentImage1;
	public GameObject header2;
	public Text headerText2;
	public Image contentImage2;

	public void setText1(string _text){
		if (_text != "") {
			headerText1.font = (Font)Resources.Load("Fonts/ArialBold48");
			header1.SetActive (true);
			headerText1.text = TextUtility.SetText(_text);
		} else {
			header1.SetActive (false);
		}
	}

	public void setText2(string _text){
		if (_text != "") {
			headerText2.font = (Font)Resources.Load("Fonts/ArialBold48");
			header2.SetActive (true);
			headerText2.text = TextUtility.SetText(_text);
		} else {
			header2.SetActive (false);
		}
	}

	public void setImage1(Texture2D _image, float _height){
		Sprite _sprite;
		if (_image) {
			_sprite = Sprite.Create (_image, new Rect (0.0f, 0.0f, _image.width, _image.height), new Vector2 (0.5f, 0.5f), 100.0f);
			contentImage1.sprite = _sprite;
		}

		RectTransform _rect = contentImage1.gameObject.GetComponent<RectTransform> ();
		_rect.sizeDelta = new Vector2 (_rect.rect.width, _height);
	}

	public void setImage2(Texture2D _image, float _height){
		Sprite _sprite;
		if (_image) {
			_sprite = Sprite.Create (_image, new Rect (0.0f, 0.0f, _image.width, _image.height), new Vector2 (0.5f, 0.5f), 100.0f);
			contentImage2.sprite = _sprite;
		}

		RectTransform _rect = contentImage2.gameObject.GetComponent<RectTransform> ();
		_rect.sizeDelta = new Vector2 (_rect.rect.width, _height);
	}
}
