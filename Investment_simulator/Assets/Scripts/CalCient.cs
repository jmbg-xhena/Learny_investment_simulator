using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalCient : MonoBehaviour {

    public GameObject BackGroundPref;
	private GameObject _background;
    private Vector3 Position_scene;

    float CentroY;
	private int bgInitialIndex;

	// Use this for initialization
	void Start () {

        CentroY = gameObject.GetComponent<RectTransform>().position.y;
		Debug.Log(CentroY);
	}
	
    //VISUALIZACION

    public void Get_in()
    {
		GameObject _background = GameObject.Find ("CalcBg");
		if (_background) {
			bgInitialIndex = _background.transform.GetSiblingIndex ();
			_background.transform.SetSiblingIndex (gameObject.transform.GetSiblingIndex ()-1);
		} else {
			_background = Instantiate(BackGroundPref, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
			_background.name = "CalcBg";
			_background.transform.SetParent (GameObject.Find("Canvas").transform, false);
			_background.transform.localPosition = new Vector3(0,0,0);
			//_background.transform.SetSiblingIndex (gameObject.transform.GetSiblingIndex () - 1);
		}

		gameObject.transform.SetSiblingIndex (_background.transform.GetSiblingIndex ());
        
		Position_scene = new Vector3(0f, 0f, 0f);

        this.MoveObjectScene();
    }

    public void Get_out()
    {
        //BackGround.SetActive(false);
		GameObject bg = GameObject.Find ("CalcBg");

		Image _bg = bg.GetComponent<Image>();
		_bg.CrossFadeAlpha (0f, 1f, false);
		Destroy(bg, 1.2f);

		Position_scene = new Vector3(0f, 1400f, 0f);
        this.MoveObjectScene();
    }


    private void MoveObjectScene()
    {
		
		iTween.MoveTo(
			gameObject,
			iTween.Hash(
				"position", Position_scene,
				"looktarget", Camera.main,
				"easeType", iTween.EaseType.easeOutExpo,
				"time", 1f,
				"islocal",true
			)
		);
    }
}
