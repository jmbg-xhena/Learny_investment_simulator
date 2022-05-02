using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct reportImgElement {
	//Type 1: 1 element
	//Type 2: 2 elements
	public int type;
	public string title1;
	public string title2;
	public Texture2D image1;
	public Texture2D image2;
	public float height1;
	public float height2;
}
