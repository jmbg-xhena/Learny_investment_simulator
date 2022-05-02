using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OneColumnReport : MonoBehaviour {

	public GameObject header;
	public Text headerText;
	public Image contentImage;

	public void setText(string _text){
		if (_text != "") {
			headerText.font = (Font)Resources.Load("Fonts/ArialBold48");
			header.SetActive (true);
			headerText.text = TextUtility.SetText(_text);
		} else {
			header.SetActive (false);
		}
	}

	public void setImage(Texture2D _image = null, float _height = 200){
		if (_image != null) {
			Sprite _sprite;
			_sprite = Sprite.Create (_image, new Rect (0.0f, 0.0f, _image.width, _image.height), new Vector2 (0.5f, 0.5f), 100.0f);
			contentImage.sprite = _sprite;
		}
		RectTransform _rect = contentImage.gameObject.GetComponent<RectTransform> ();
		_rect.sizeDelta = new Vector2 (_rect.rect.width, _height);
	}
}
