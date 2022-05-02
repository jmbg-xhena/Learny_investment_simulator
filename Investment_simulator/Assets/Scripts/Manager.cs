using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class Manager : Singleton<Manager> {

	protected Manager () {} // guarantee this will be always a singleton only - can't use the constructor!

	public bool logged = false;
	public string globalMenuName = "menu_scene";
	public bool globalAula = false;
	public bool globalMonoUser = false;
	public string globalUrlAula = "";

	public string globalUser = "";
	public string globalCourse = "";
	public string globalCourseId = "";
	public string globalInstitution = "";
	public string globalLTIParameters = "";

	public string globalUserAula = "";

	public string globalMonoUserName = "";
	public string globalMonoUserInstitution = "";
	public string globalMonoUserEmail = "";

    public BaseSimulator.ComplexModes globalComplexMode = BaseSimulator.ComplexModes.U;

	public string globalLanguage = "";
	public bool globalLanguageEnabled;

	public BaseSimulator.TypeScoreList globalTypeScore = BaseSimulator.TypeScoreList.NUMERIC;
	public int[] globalNumericScore;
	public QualificationElement[] globalAlphabeticalScore;
	public float globalMinimalSuccessScore;
    public float globalMaxLimit;

    public XmlDocument globalTexts;
	public XmlDocument globalInfo;
    public XmlDocument globalAulaDoc;
    public XmlNodeList globalQuestions;
	public XmlNodeList globalComplementaryQuestions;
	public XmlDocument globalLanguages;

	public string currentSituationName = "";
	public string currentPDFName = "TemporalReport";
	public string currentAulaCode = "";

	public int upperAttempts = 0;
	public int upperSeconds = 0;
	public int upperMinutes = 0;
	public int upperHours = 0;
	public bool recoveryUpper = false;

	public string[] dataRegistry;

	public string globalAndroidUrl = "";
	public string globalUrlScore = "";

    public int newSituationId = 0;

    public bool globalWebGLMobile = false;
    public string globalWebGLCode = "";
    public string globalWebGLUrl = "";

    public string globalMonoPassword = "";
    public string url_entera = "";

	public string globalUserId = "";
	public bool globalLtiAula = false;
}
