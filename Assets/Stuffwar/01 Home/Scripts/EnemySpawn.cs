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
		
		yield return new WaitForSeconds (0.2f);

		v = spawnpoint.position;
		for (int i = 0; i < 5; i++) 
		{
			Instantiate (spawnthing, v , spawnpoint.rotation);
			yield return new WaitForSeconds (0.2f);
			v.x += 5;
		}
	}
			

}
