using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class situation_1 : BaseSituation{

    private Text TextBoton;

    public Image Fundido_;

    CalCient ScriptCalCient;

    public string DescriptionPractice;
    public string var_scene;

    public GameObject dataRegistry;

    void Start () {

        _cam1.enabled = true;
        _cam2.enabled = false;

        //TODO: check report name, for situation 1 is name_report_1
        Manager.Instance.currentPDFName = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='name_report_1']").InnerText;

        base.StartBaseSituation();

		GameObject infoBt = GameObject.Find("info_bt");
		Button btn1 = infoBt.GetComponent<Button>();
		btn1.onClick.AddListener(callInfoAlert);

		GameObject editBt = GameObject.Find("edit_bt");
		editBt.GetComponent<Button> ().onClick.AddListener (callEditAlert);

		GameObject trashBt = GameObject.Find("trash_bt");
		trashBt.GetComponent<Button> ().onClick.AddListener (callTrashAction);
 
		GameObject _help_obj = GameObject.Find("help_bt");
		_help_obj.GetComponent<Button>().onClick.AddListener(callHelpAlert);

		GameObject _notepad_obj = GameObject.Find("notepad_bt");
		_notepad_obj.GetComponent<Button>().onClick.AddListener(callNotepad);

        Fundido_ = GameObject.Find("Fundido").GetComponent<Image>();

        if (Manager.Instance.recoveryUpper == false)
        {
            Fundido_.CrossFadeAlpha(0, 0.5f, false);
            Fundido_.raycastTarget = false;

            situationAlert();
        } else
        {
            Fundido_.gameObject.SetActive(false);
        }
    }

    private void acceptback(){

        SceneManager.LoadScene(var_scene);
    }

	private void situationAlert()
    {
		_alert = Instantiate(_alertPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
		_alert.transform.SetParent(GameObject.Find("Canvas").transform, false);
		_alert.transform.localPosition = new Vector3(-700, 700, 0);
		DescriptionPractice = Manager.Instance.globalInfo.SelectSingleNode("/data/" + base.situationTag + "/description_practice").InnerText;
        //TODO: DescriptionPractice replace variables values
        _alert.GetComponent<Alert>().showAlert(3, Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='simulator_title']").InnerText, Manager.Instance.globalInfo.SelectSingleNode("/data/" + base.situationTag + "/name_practice").InnerText, DescriptionPractice);

    }

    private void callInfoAlert()
    {
        _alert = Instantiate(_alertPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        _alert.transform.SetParent(GameObject.Find("Canvas").transform, false);
        _alert.transform.localPosition = new Vector3(-700, 700, 0);
		DescriptionPractice = Manager.Instance.globalInfo.SelectSingleNode("/data/" + base.situationTag + "/description_practice").InnerText;
		_alert.GetComponent<Alert>().showAlert(5, Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='situation']").InnerText + "|" + Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='procedure']").InnerText + "|" + Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='equation']").InnerText, Manager.Instance.globalInfo.SelectSingleNode("/data/" + base.situationTag + "/name_practice").InnerText, DescriptionPractice + "|" + Manager.Instance.globalInfo.SelectSingleNode("/data/" + base.situationTag + "/procedure").InnerText + "|" + Manager.Instance.globalInfo.SelectSingleNode("/data/" + base.situationTag + "/ecuaciones").InnerText);
        //_alert.GetComponent<Alert>().showAlert(3, Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='instructions']").InnerText, "", Manager.Instance.globalInfo.SelectSingleNode("/data/instructions").InnerText);

    }

	private void callEditAlert(){
        dataRegistry.GetComponent<dataRegistrySit1>()._screenPict1 = screenCapture.captureImage(new Rect(0, 540, 1920, 1080), "Main Camera");

        dataRegistry.GetComponent<RectTransform>().SetAsLastSibling();
        dataRegistry.GetComponent<dataRegistrySit1>().showBg();

        iTween.MoveTo(
            dataRegistry,
            iTween.Hash(
                "position", new Vector3(0, 0, 0),
                "looktarget", Camera.main,
                "easeType", iTween.EaseType.easeOutExpo,
                "time", 1f,
                "islocal", true
            )
        );
    }

	private void callTrashAction(){
		//TODO: trash action
		Debug.Log("TO DO Trash");
        Manager.Instance.recoveryUpper = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void callHelpAlert()
    {
        _alert = Instantiate(_alertPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        _alert.transform.SetParent(GameObject.Find("Canvas").transform, false);
        _alert.transform.localPosition = new Vector3(-700, 700, 0);
		_alert.GetComponent<Alert>().showAlert(4, Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='help']").InnerText, "", Manager.Instance.globalInfo.SelectSingleNode("/data/" + base.situationTag + "/tips").InnerText);
    }

    private void callNotepad()
    {

        GameObject _notepad = GameObject.Find("notePad");
        _notepad.GetComponent<NotePad>().showNotePad();
    }

    public void addAttempt()
    {
        base._upperIndicator.GetComponent<UpperIndicator>().addAttempt();
    }

    public int getAttempts()
    {
        return base._upperIndicator.GetComponent<UpperIndicator>()._attempts;
    }

    public int getTime()
    {
        return base._upperIndicator.GetComponent<UpperIndicator>().getTime();
    }

    public void AddSkillLevel()
    {
        if(skillsProgress != null)
        {
            skillsProgress.GetComponent<SkillsProgress>().IncreaseLevel();
        }
    }

    public int GetSkillLevel()
    {
        if (skillsProgress != null)
        {
            return skillsProgress.GetComponent<SkillsProgress>().GetActualLevel();
        }

        return 0;
    }

    public void CallSkillsAlert()
    {
        string textFxDemo = ""; //Use default values
        string textVoiceDemo = ""; //Use default values

        //textFxDemo = "Sounds/ej1"; //Demo path
        //textVoiceDemo = "Sounds/ej2"; //Demo path

        skillsAlert = Instantiate(skillsAlertPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        skillsAlert.transform.SetParent(GameObject.Find("Canvas").transform, false);
        skillsAlert.GetComponent<RectTransform>().anchoredPosition = new Vector2(600, 16);
        skillsAlert.GetComponent<SkillsAlert>().ShowAlert(skillsAlertTime, textFxDemo, textVoiceDemo);
    }
}
