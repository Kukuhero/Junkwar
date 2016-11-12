using UnityEngine;
using System.Collections;

public class HouseController : MonoBehaviour {

	public int HouseHealth = 400;
	public int maxHouseHealth = 400;
	private Vector3 screenPos;
	private int Position = 30;
	public Texture2D healthTexture;
	public GameObject House;
	public static int Zahnräder;
	
	// Update is called once per frame
	void Update () {
		if (HouseHealth <= 0) 
		{
			HouseHealth = 0;
			print ("Verloren, dein Haus wurde erobert");
		}
	}
	void OnGUI()
	{
		if (HouseHealth != maxHouseHealth) {
			screenPos = Camera.main.WorldToScreenPoint (transform.position);
			GUI.DrawTexture (new Rect (screenPos.x - 40, Screen.height - screenPos.y - Position, HouseHealth / 8, 5), healthTexture);
			GUI.DrawTexture (new Rect (10, 10, HouseHealth / 2, 10), healthTexture);
			GUI.color = Color.black;
			GUI.Label (new Rect (screenPos.x - 35, Screen.height - screenPos.y - Position, 50, 30), "" + HouseHealth + "/" + maxHouseHealth);
			GUI.Label (new Rect (50, 50, 100, 30), "" + HouseHealth + "/" + maxHouseHealth);
		}
	}

}
