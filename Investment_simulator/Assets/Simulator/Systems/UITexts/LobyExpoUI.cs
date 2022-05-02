using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobyExpoUI : MonoBehaviour
{
    [SerializeField]private Text mapStandText;
    [SerializeField] private Text mapStandTextCloseUp;

    // Start is called before the first frame update
    void Start()
    {
        mapStandText.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='Expo_Loby_Map_stand']").InnerText;
        mapStandTextCloseUp.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='Expo_Loby_Map_stand_closeup']").InnerText;
    }
}
