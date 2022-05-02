using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class screenCapture : MonoBehaviour {

	#region methods public
	/// <summary>
	/// Captura en una textura el rectTransform objetivo
	/// </summary>
	/// <param name="argRectTransformObjetivoCaptura">RectTransform que se quiere capturar</param>
	/// <returns>Textura del rectransform capturado</returns>
	public static Texture2D captureImage(Rect _area, string cameraName, bool isReport = false)
	{
		
		Camera c = GameObject.Find(cameraName).GetComponent<Camera>();


		int resWidth = Mathf.FloorToInt(_area.width);
		int resHeight = Mathf.FloorToInt(_area.height);

		int resWidthEnd = Mathf.FloorToInt(_area.width);
		int resHeightEnd = Mathf.FloorToInt(_area.height);

		float positionX = 0f;
		float positionY = 0f;

		Debug.Log ("c.pixelWidth: " + c.pixelWidth);
		Debug.Log ("c.pixelHeight: " + c.pixelHeight);
		Debug.Log ("c.pixelRect.x: " + c.pixelRect.x);
		Debug.Log ("c.pixelRect.y: " + c.pixelRect.y);

		if (isReport == false) {
			resWidth = c.pixelWidth;
			resHeight = c.pixelHeight;
			resWidthEnd = c.pixelWidth;
			resHeightEnd = c.pixelHeight;
			positionX = c.pixelRect.x;
			positionY = c.pixelRect.y;
		}

		RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
		c.targetTexture = rt;
		Texture2D screenShot = new Texture2D(resWidthEnd, resHeightEnd, TextureFormat.RGB24, false);
		c.Render();
		RenderTexture.active = rt;
		screenShot.ReadPixels(new Rect(positionX, positionY, resWidthEnd, resHeightEnd), 0, 0);
		screenShot.Apply();
		c.targetTexture = null;
		RenderTexture.active = null; // JC: added to avoid errors
		Destroy(rt);

		if (isReport == false) {
			float scaleW = (float)resWidth / 1920f;

			int posY = resHeight/2 - Mathf.FloorToInt(_area.y*scaleW);

            int xArea = Mathf.FloorToInt(_area.x * scaleW);
            int yArea = posY;
            int wArea = Mathf.FloorToInt(_area.width * scaleW);
            int hArea = Mathf.FloorToInt(_area.height * scaleW);

            if (xArea < 0)
            {
                xArea = 0;
            }

            if (yArea < 0)
            {
                yArea = 0;
            }

            if (xArea + wArea > screenShot.width)
            {
                wArea = screenShot.width - xArea;
            }

            if (yArea + hArea > screenShot.height)
            {
                hArea = screenShot.height - yArea;
            }

            Color[] pix = screenShot.GetPixels(xArea, yArea, wArea, hArea);

			Texture2D destTex = new Texture2D(wArea, hArea);
			destTex.SetPixels(pix);
			destTex.Apply();

			return destTex;
		} else {

			return screenShot;
		}

	}


	#endregion

}
