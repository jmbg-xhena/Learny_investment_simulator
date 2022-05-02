using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpoEntrancViewInterface : MonoBehaviour
{
    public static ExpoEntrancViewInterface instance;
    [Header("Scriptable Events")]
    [SerializeField] private GameObjectGameEvent onAlertClose;
    [SerializeField] private GameObjectSpriteGameEvent onHighlightObjectRequest;

    [Header("View References")]
    [SerializeField] private GameObject expoEntrance;
    [SerializeField] private GameObject expoEntrance_root;
    [SerializeField] private GameObject canvas;

    [Header("Prefabs")]
    [SerializeField] private GameObject alertPrefab;

    [Header("Overs")]
    [SerializeField] private Sprite expoEntrance_over;

    GameObject alert;

    private void Awake()
    {
        instance = this;
        expoEntrance_root.SetActive(true);
    }


    public void ShowInitialAlert()
    {
        //show alert
        alert = Instantiate(alertPrefab, canvas.transform);
        alert.GetComponent<Alert>().showAlert(1, "", "", Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='ALR_bossoffice_intro_text']").InnerText, Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='ALR_bossoffice_intro_button']").InnerText, "", CloseInitialAlertCallback);
    }

    void CloseInitialAlertCallback()
    {
        expoEntrance.SetActive(false);
        onAlertClose.Raise(expoEntrance.transform.Find("Over_container").gameObject);
        alert.GetComponent<Alert>().closeAlert();
        GetComponent<Animator>().Play("SlideOpen");
        //other way if state is changed elsewhere:
        //onHighlightObjectRequest.Raise(laptop.transform.Find("screen").gameObject, laptopScreen_over);
        //onHighlightObjectRequest.Raise(laptop.transform.Find("keyboard").gameObject, laptopKeyboard_over);
    }

    public void CloseExpoEntrance()
    {
        expoEntrance_root.SetActive(false);
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
