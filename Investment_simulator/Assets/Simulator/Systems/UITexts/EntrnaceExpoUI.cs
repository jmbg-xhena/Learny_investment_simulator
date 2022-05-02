using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntrnaceExpoUI : MonoBehaviour
{
    [SerializeField] private Text baseTabletText;
    // Start is called before the first frame update
    void Start()
    {
        baseTabletText.text = Manager.Instance.globalTexts.SelectSingleNode("/data/element[@title='Expo_Register_Asist']").InnerText;
    }
}
