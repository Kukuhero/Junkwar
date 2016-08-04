using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Userinterface : MonoBehaviour {
	public static int Strom = 0;
	public Text Anzeige;

	// Use this for initialization
	void Start () {
		Anzeige = Anzeige.GetComponent<Text>();
		}
	public void spawn()
	{
		KuehlschrankSpawn.spawn = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
			Anzeige.text = "Strom: " + Strom + " Watt /" + Batterie.maxStrom + " Watt";
		

	
	}

}
