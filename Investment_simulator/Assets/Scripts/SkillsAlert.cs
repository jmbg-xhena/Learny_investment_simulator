using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsAlert : MonoBehaviour
{
    public AudioSource audioSource1;
    public AudioSource audioSource2;

    public AudioClip audioFxDefault;
    public AudioClip audioVoiceDefault;

    private int timeInScene = 3;
    private AudioClip audioFxSelected;
    private AudioClip audioVoiceSelected;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ShowAlert(int time, string audioFxLocation, string audioVoiceLocation)
    {
        gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(600, 16);

        audioFxSelected = audioFxDefault;
        audioVoiceSelected = audioVoiceDefault;
        timeInScene = time;

        if(audioFxLocation != "")
        {
            audioFxSelected = Resources.Load(audioFxLocation) as AudioClip;
        }

        if(audioVoiceLocation != "")
        {
            audioVoiceSelected = Resources.Load(audioVoiceLocation) as AudioClip;
        }

        audioSource1.clip = audioFxSelected;
        audioSource2.clip = audioVoiceSelected;

        audioSource1.Play();
        audioSource2.Play();

        iTween.MoveTo(
            gameObject,
            iTween.Hash(
                "position", new Vector3(gameObject.transform.localPosition.x - 616f, gameObject.transform.localPosition.y, 0),
                "looktarget", Camera.main,
                "easeType", iTween.EaseType.easeOutBack,
                "time", 0.7f,
                "islocal", true
            )
        );

        Invoke("HideAlert", timeInScene + 0.7f);
    }

    public void HideAlert()
    {
        iTween.MoveTo(
            gameObject,
            iTween.Hash(
                "position", new Vector3(gameObject.transform.localPosition.x + 616f, gameObject.transform.localPosition.y, 0),
                "looktarget", Camera.main,
                "easeType", iTween.EaseType.easeInBack,
                "time", 0.7f,
                "islocal", true
            )
        );

        Invoke("RemoveAlert", 1f);
    }

    private void RemoveAlert()
    {
        Destroy(gameObject);
    }
}
