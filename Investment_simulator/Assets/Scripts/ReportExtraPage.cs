using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReportExtraPage : MonoBehaviour
{
	public Text _title;

	public GameObject _oneColumnPref;
	public GameObject _twoColumnPref;

	public GameObject _bodyElement;

    public GameObject container;

    public IEnumerator loadData(List<reportImgElement> _imgElements = null){
		_title.fontSize = 48;
		_title.font = (Font)Resources.Load("Fonts/ArialBold48");
		_title.text = TextUtility.SetText(Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='simulator_title']").InnerText);

		if (_imgElements != null) {
			for (int imgIndex = 0; imgIndex < _imgElements.Count; imgIndex++) {
				if (_imgElements [imgIndex].type == 1) {
					GameObject _oneColumn = Instantiate (_oneColumnPref, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
					_oneColumn.transform.SetParent (_bodyElement.transform, false);
					_oneColumn.transform.localPosition = new Vector3 (0, 0, 0);
					_oneColumn.GetComponent<OneColumnReport>().setText(_imgElements [imgIndex].title1);
					_oneColumn.GetComponent<OneColumnReport> ().setImage (_imgElements [imgIndex].image1, _imgElements [imgIndex].height1);
				} else {
					GameObject _twoColumn = Instantiate (_twoColumnPref, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
					_twoColumn.transform.SetParent (_bodyElement.transform, false);
					_twoColumn.transform.localPosition = new Vector3 (0, 0, 0);
					_twoColumn.GetComponent<TwoColumnReport>().setText1(_imgElements [imgIndex].title1);
					_twoColumn.GetComponent<TwoColumnReport>().setText2(_imgElements [imgIndex].title2);
					_twoColumn.GetComponent<TwoColumnReport> ().setImage1 (_imgElements [imgIndex].image1, _imgElements [imgIndex].height1);
					_twoColumn.GetComponent<TwoColumnReport> ().setImage2 (_imgElements [imgIndex].image2, _imgElements [imgIndex].height2);
				}
			}
		}

        //yield return new WaitForSeconds (0.5f);
        //_bodyElement.AddComponent<Image> ();
        LayoutRebuilder.ForceRebuildLayoutImmediate(container.GetComponent<RectTransform>());
        //Destroy (gameObject, 2.0f);

        yield return null;
    }

    private void FixedUpdate()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(_bodyElement.GetComponent<RectTransform>());
        LayoutRebuilder.ForceRebuildLayoutImmediate(container.GetComponent<RectTransform>());
    }
}