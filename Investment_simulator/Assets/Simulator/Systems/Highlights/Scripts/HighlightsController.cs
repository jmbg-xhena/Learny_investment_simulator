using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighlightsController : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private Color highlightColor;
    [SerializeField] private Color highlightWhite;

    private List<GameObject> targets = new List<GameObject>();

    public void HighlightObject(GameObject target, Sprite over)
    {
        if (!targets.Contains(target))
        {
            GameObject highlight = Instantiate(target.gameObject, target.transform);
            highlight.GetComponent<Image>().sprite = over;
            highlight.tag = "highlight";
            targets.Add(target);
        }
    }

    public void DehighlightObject(GameObject target)
    {
        if(targets.Contains(target))
        {
            Destroy(target.FindComponentInChildWithTag<Image>("highlight", true).gameObject);
            target.GetComponent<Image>().color = Color.white;
            targets.Remove(target);
        }
    }

    void Update()
    {
        if (targets.Count > 0)
        {
            foreach (GameObject target in targets)
            {
                target.FindComponentInChildWithTag<Image>("highlight", true).color = Color.Lerp(highlightWhite, highlightColor, Mathf.PingPong(Time.time, 1));
            }
        }
    }
}
