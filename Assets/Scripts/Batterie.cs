using UnityEngine;
using System.Collections;

public class Batterie : MonoBehaviour {
	private int Level = 1;
	public static int maxStrom;
	public static bool OnGuibool = false;
	private Vector3 screenPos;
	private int Updgradekosten = 2;

	// Use this for initialization
	void Start () {
		maxStrom = 100;
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		switch (Level) 
		{
		case(1):
			maxStrom = 100;
			break;

		case(2):
			maxStrom = 200;

			break;

		case(3):
			maxStrom = 300;

			break;
		}

	}

	void OnGUI()
	{
		if (OnGuibool) 
		{
			screenPos = Camera.main.WorldToScreenPoint (transform.position);
			GUI.Box (new Rect (screenPos.x-100, Screen.height - screenPos.y-40, 200, 100),"Battery");
			GUI.Label (new Rect (screenPos.x-50, Screen.height - screenPos.y-20, 150, 30),"Level: " + Level);
			if (GUI.Button (new Rect (screenPos.x-50, Screen.height - screenPos.y, 100, 30), "Verbessern " + Updgradekosten)) 
			{
				if (HouseController.Zahnräder >= Updgradekosten) {
					HouseController.Zahnräder -= Updgradekosten;
					Level++;
					Updgradekosten++;
				} else 
				{
					print ("zu wenig Zahnräder");
					GUI.Box (new Rect (100, 100, 200, 200), "Zu wenig Zahnräder!");
					 //new WaitForSeconds (3);
				}
			}
		}
	}
}
