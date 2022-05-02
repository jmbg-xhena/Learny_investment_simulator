using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillsProgress : MonoBehaviour
{
    public Image level1Done;
    public Image level2Done;
    public Image level3Done;
    public Image level4Done;
    public Image level5Done;

    private int actualLevel = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void IncreaseLevel()
    {
        if (actualLevel >= 5)
        {
            return;
        }
        actualLevel++;
        switch (actualLevel)
        {
            case 1:
                level1Done.enabled = true;
                ActivateLevel(level1Done.gameObject);
                break;
            case 2:
                level2Done.enabled = true;
                ActivateLevel(level2Done.gameObject);
                break;
            case 3:
                level3Done.enabled = true;
                ActivateLevel(level3Done.gameObject);
                break;
            case 4:
                level4Done.enabled = true;
                ActivateLevel(level4Done.gameObject);
                break;
            case 5:
                level5Done.enabled = true;
                ActivateLevel(level5Done.gameObject);
                break;
        }

    }

    public int GetActualLevel()
    {
        return actualLevel;
    }

    private void ActivateLevel(GameObject level)
    {
        iTween.ScaleTo(level, iTween.Hash(
                "scale", new Vector3(0.5f, 0.5f, 0.5f),
                "looktarget", Camera.main,
                "easeType", iTween.EaseType.easeOutExpo,
                "time", 0.5f
            )
        );
    }

}
