
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menu : BaseSimulator
{
    public Image Fundido_;

    private float TimeOver;
    public bool resetPlayerPrefs;
    // Use this for initialization
    void Start()
    {
#if UNITY_STANDALONE_OSX
        Application.targetFrameRate = 24;
#endif
        resetPrefs(resetPlayerPrefs);
        Fundido_ = GameObject.Find("Fundido").GetComponent<Image>();
        //Fundido_.CrossFadeAlpha(0, 0.5f, false);
        //GameObject.Find("Fundido").SetActive(false);

        base.startSimulator ();
        base.LoginActionEvent += LoginHanlder;

    }

    private void LoginHanlder()
    {
        Invoke("LaunchSit", 0.5f);
    }

	public void resetPrefs(bool reset){

		if(reset){
			PlayerPrefs.DeleteAll();
		}
	}

    public void LaunchSit()
    {
        SceneManager.LoadScene("situation_1");
    }
}
