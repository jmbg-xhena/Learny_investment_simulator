using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpperIndicator : MonoBehaviour {

	public Text UserName;
	public Text Time;
	public Text AttemptsLabel;
	public Text Attempts;

	public int _attempts = 1;
	public string _time;
	public string _username;

	private int seconds = 0;
	private int minutes = 0;
	private int hours = 0;
	// Use this for initialization
	void Start () {
		
		if (Manager.Instance.recoveryUpper == true) {
			Manager.Instance.recoveryUpper = false;
			_attempts = Manager.Instance.upperAttempts;
			seconds = Manager.Instance.upperSeconds;
			minutes = Manager.Instance.upperMinutes;
			hours = Manager.Instance.upperHours;
		} else {
			Manager.Instance.upperAttempts = 1;
			Manager.Instance.upperSeconds = 0;
			Manager.Instance.upperMinutes = 0;
			Manager.Instance.upperHours = 0;
		}

		AttemptsLabel.text = TextUtility.SetText(Manager.Instance.globalTexts.SelectSingleNode ("/data/element[@title='attempts']").InnerText);
		UserName.text = TextUtility.SetText(Manager.Instance.globalUser);
		Attempts.text = _attempts.ToString ();
		_username = Manager.Instance.globalUser;

	}

	public void startTimer(){
		StartCoroutine (addTime ());
	}

	public void addAttempt(){
		_attempts++;
		Manager.Instance.upperAttempts = _attempts;
		Attempts.text = _attempts.ToString ();
	}

	public int getTime()
    {
		return seconds + minutes * 60 + hours * 60 * 60;
	}

	IEnumerator addTime(){
		yield return new WaitForSeconds(1f);

		seconds = seconds + 1;
		if(seconds == 60)
		{
			seconds = 0;
			minutes = minutes + 1;

			if(minutes == 60)
			{
				minutes = 0;
				hours = hours + 1;

				if(hours < 10)
				{
					_time = "0" + hours.ToString () + ":";
				}
				else
				{
					_time = hours.ToString () + ":";
				}
			}
			else
			{
				if(hours < 10)
				{
					_time = "0" + hours.ToString () + ":";
				}
				else
				{
					_time = hours.ToString () + ":";
				}
			}


			if(minutes < 10)
			{
				_time = _time + "0" + minutes.ToString () + ":";
			}
			else
			{
				_time = _time + minutes.ToString () + ":";
			}
		}
		else
		{
			if(minutes == 60)
			{
				minutes = 0;
				hours = hours + 1;

				if(hours < 10)
				{
					_time = "0" + hours.ToString () + ":";
				}
				else
				{
					_time = hours.ToString () + ":";
				}
			}
			else
			{
				if(hours < 10)
				{
					_time = "0" + hours.ToString () + ":";
				}
				else
				{
					_time = hours.ToString () + ":";
				}
			}

			if(minutes < 10)
			{
				_time = _time + "0" + minutes.ToString () + ":";
			}
			else
			{
				_time = _time + minutes.ToString () + ":";
			}
		}

		if(seconds < 10)
		{
			_time = _time + "0" + seconds.ToString();
		}
		else
		{
			_time = _time + seconds.ToString();
		}

		Time.text = _time;

		Manager.Instance.upperSeconds = seconds;
		Manager.Instance.upperMinutes = minutes;
		Manager.Instance.upperHours = hours;

		StartCoroutine (addTime ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
