using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Userinterface : MonoBehaviour {
	public static int Strom = 0;
	public Text AnzeigeStrom;
	public Text AnzeigeZahnräder;

	// Use this for initialization
	void Start () {
		AnzeigeStrom = AnzeigeStrom.GetComponent<Text>();
		AnzeigeZahnräder = AnzeigeZahnräder.GetComponent<Text>();
		}
	public void spawn()
	{
		KuehlschrankSpawn.spawn = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
			AnzeigeStrom.text = ": " + Strom + "W /" + Batterie.maxStrom + "W";
			AnzeigeZahnräder.text =": " +HouseController.Zahnräder;
		

	
	}

}
