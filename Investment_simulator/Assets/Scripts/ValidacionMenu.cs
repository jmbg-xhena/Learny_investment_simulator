#if !UNITY_WEBGL
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Security.Cryptography;
using System.IO;
using System.Linq;
using System.Text;
using System;
using Hash;

namespace ValidacionMenu
{
	class Validacion {



        public static string datos;

        public static string checkString()
        {
            //
            string los_datos = datos;

            return los_datos;
        }

        /// <summary>
        /// Retorna verdadero o falso repecto a si se ha detectado una llamada del menu. 
        /// Uso:
        /// Importar 
        /// using ValidacionMenu;
        /// 
        /// if(Validacion.Validar()){
        /// 	Hacer algo...
        /// }
        /// else{
        /// 	Application.Quit();
        /// }
        /// </summary>
        public static bool Validar() {

            string key = "25573DB64F2FCDB68A558677D9234";
            string code, folder, license, hexKey, date, generatedCode;
            string[] comands;
            byte[] bytesKey;
            DateTime dateTime;

            bytesKey = Encoding.Default.GetBytes(key);
            hexKey = BitConverter.ToString(bytesKey);
            hexKey = hexKey.Replace("-", "");

            dateTime = DateTime.Now;
            date = dateTime.ToString("yyyyMdHm");

#if UNITY_EDITOR || UNITY_STANDALONE
            // Obtenemos los argumentos enviados a la aplicación de escritorio.
            comands = System.Environment.CommandLine.Split(',');
            try
            {
                license = comands[1];
                code = comands[2];
                folder = comands[comands.Length - 1];
            }
            catch
            {
                return false;
            }

            for (int i = 0; i < comands.Length; i++)
            {
                datos = datos + "\n" + comands[i];
            }

            folder = folder.Replace("\"", "");

            generatedCode = Encrypt.HashHMACHex(hexKey, folder + date + license);
            datos = datos + "\n" + generatedCode + "\n" + code;

                return String.Compare(generatedCode, code) == 0;

#elif UNITY_ANDROID || UNITY_IPHONE
					
				//Text TextBoxText = GameObject.Find("Canvas/Text").GetComponent<Text>();
				try{
						AndroidJavaClass UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"); 
						AndroidJavaObject currentActivity = UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
						if (currentActivity == null)
						{
							return false;
						}
						AndroidJavaObject intent = currentActivity.Call<AndroidJavaObject>("getIntent");
						if (intent == null)
						{
							return false;
						}
						string app = "";

						license = safeCallStringMethod(intent, "getStringExtra", "licencia");
						code = safeCallStringMethod(intent, "getStringExtra", "codigo");
						app = safeCallStringMethod(intent, "getStringExtra", "aplicacion");

						generatedCode = Encrypt.HashHMACHex(hexKey, app + date + license);
                        datos = generatedCode + "," + code + ", " +dateTime+","+date;
						return String.Compare(generatedCode, code) == 0;
				}
				catch (Exception e){
					Debug.Log(e.ToString());
					return false;
				}
#endif

        }

        public static string safeCallStringMethod(AndroidJavaObject javaObject, string methodName, params object[] args)
        {
			#if UNITY_2018_2_OR_NEWER
				if (args == null) args = new object[] { null };
				IntPtr methodID = AndroidJNIHelper.GetMethodID<string>(javaObject.GetRawClass(), methodName, args, false);
				jvalue[] jniArgs = AndroidJNIHelper.CreateJNIArgArray(args);

				try
				{
					IntPtr returnValue = AndroidJNI.CallObjectMethod(javaObject.GetRawObject(), methodID, jniArgs);
					if (IntPtr.Zero != returnValue)
					{
						var val = AndroidJNI.GetStringUTFChars(returnValue);
						AndroidJNI.DeleteLocalRef(returnValue);
						return val;
					}
				}
				finally
				{
					AndroidJNIHelper.DeleteJNIArgArray(args, jniArgs);
				}

				return null;
			#else
				return  javaObject.Call<string>(methodName, args);
			#endif
        }
	}
}
#endif