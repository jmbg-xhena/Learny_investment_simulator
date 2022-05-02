using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using ArabicSupport;
using UnityEngine;
using UnityEngine.UI;

public static class TextUtility
{	
	public static string SetText(string text, bool isTextDraw = false, bool isTextMeshPro = false)
	{
		string isRTL;

		XmlNode languageNode = Manager.Instance.globalLanguages.SelectSingleNode("/data/language[@code='" + Manager.Instance.globalLanguage + "']");
		isRTL = languageNode.Attributes["isRTL"].Value;

		if (isRTL == "true")
		{
			if (isTextDraw == true)
			{
				if (text.Contains("\\opens{") == true)
				{
					string resultString = "";
					string[] items = text.Split(new string[] { "\\opens{" }, StringSplitOptions.None);
					for (int i = 0; i < items.Length; i++)
					{
						if (i > 0)
						{
							resultString += "\\opens{";
							resultString += ArabicFixer.Fix(items[i].Substring(0, items[i].IndexOf('}')), false, false);
							resultString += items[i].Substring(items[i].IndexOf('}'));
						}
						else
						{
							resultString += items[i];
						}
					}

					return resultString;
				}
				else
				{
					return ArabicFixer.Fix(text, false, false);
				}
			}
			else
			{
				return ArabicFixer.Fix(text, false, false);

			}
		}
		else
		{
			return text;
		}
	}
}
