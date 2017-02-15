using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelHandler : MonoBehaviour
{

	// Singleton pattern Implementation
	private static LevelHandler _instance;
	public static LevelHandler Instance {
		get {
			if (_instance == null)
			{
				_instance = GameObject.FindObjectOfType<LevelHandler> ();
			}

			return _instance;
		}
	}

	// Clock implementation, time in military time
	public struct ClockStruct
	{
		public int hours;
		public int minutes;
		public float seconds;

		public override string ToString ()
		{
			string noon = (hours < 12) ? "am" : "pm";  
			int parsedHour = (hours > 12) ? hours-12 : hours;
			parsedHour = (parsedHour == 0) ? 12 : parsedHour;
			return parsedHour.ToString ().PadLeft(2, '0') + ":" + minutes.ToString().PadLeft(2, '0') + ":" + Mathf.FloorToInt (seconds).ToString ().PadLeft(2, '0') + " " + noon;
		}

		public bool setTime (int h, int m, float s)
		{
			if (h >= 24 || m >= 60 || s >= 60.0f)
			{
				return false;
			} else
			{
				hours = h;
				minutes = m;
				seconds = s;
				return true;
			}
		}

		public void updateTime(float s, float td) 
		{
			seconds += s*td;
			if (seconds >= 60.0f)
			{
				int dm = Mathf.FloorToInt (seconds / 60.0f);	// Amount of new minutes
				minutes += dm;
				seconds = seconds - (60.0f*dm);
			}

			if (minutes >= 60)
			{
				int dh = minutes / 60;	// Amount of new hours
				hours += dh;
				minutes = minutes - (60*dh);
			}

			if (hours >= 24)
			{
				int dd = hours / 24;
				hours = hours - (24*dd);
			}
		}

		public void Copy(ClockStruct c)
		{
			hours = c.hours;
			minutes = c.minutes;
			seconds = c.seconds;
		}

	}

	private ClockStruct Clock;
	private bool pause = false;

	[Tooltip ("Amount of in-game seconds per real world second.")]
	public float timeDilation = 1.0f;

	void Awake ()
	{
		DontDestroyOnLoad (gameObject);
	}

	// Use this for initialization
	void Start ()
	{
		Clock.setTime (5, 30, 0);
	}
	
	// Update is called once per frame
	void Update ()
	{
		UpdateTime ();
	}

	void UpdateTime() 
	{
		Clock.updateTime (Time.deltaTime, getTimeDilation());
	}

	public ClockStruct getClock()
	{
		ClockStruct c = new ClockStruct();
		c.Copy (Clock);
		return c;
	}

	public float getTimeDilation()
	{
		return (!pause) ? timeDilation: 0.0f;
	}

	public void setClock(int hour, int minutes, float seconds){
		Clock.setTime (hour, minutes, seconds);
	}

	public void Pause() {
		pause = !pause;
	}
}
