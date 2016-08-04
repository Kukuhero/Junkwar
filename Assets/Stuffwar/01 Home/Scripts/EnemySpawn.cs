using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour 
{
	public Transform spawnpoint;
	public GameObject spawnthing;
	private Vector3 v;

	// Use this for initialization
	void Start () 
	{
		StartCoroutine (Wait());
	}

	IEnumerator Wait() 
	{
		spawn ();
		yield return new WaitForSeconds (8);
		spawn ();
		yield return new WaitForSeconds (8);
		spawn ();
		}	

	void spawn()
	{
		v = spawnpoint.position;
		for (int i = 0; i < 4; i++) 
		{
			Instantiate (spawnthing, v , spawnpoint.rotation);
			v.x += 5 ;
		}
	}
			

}
