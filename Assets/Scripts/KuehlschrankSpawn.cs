using UnityEngine;
using System.Collections;

public class KuehlschrankSpawn : MonoBehaviour {
	public Transform spawnpoint;
	public GameObject spawnthing;
	public static bool spawn = false;
	private int kosten = 20;

	void Start ()
	{
		
	}
	

	void Update()
	{
		if (spawn && Userinterface.Strom >= kosten) {
			StartCoroutine (wait ());
			
			spawn = false;
			Userinterface.Strom -= kosten;
		} else {
			spawn = false;
		}
	}

	IEnumerator wait() 
	{
			Spawn ();
			/*for (int i = 0; i < 2; i++) 
		{
			
			*/
			yield return new WaitForSeconds (2);
			/*Spawn ();
		}*/
	}

	void Spawn()
	{
		Instantiate (spawnthing, spawnpoint.position, spawnpoint.rotation);
	}


}
