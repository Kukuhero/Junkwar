using UnityEngine;
using System.Collections;

public class Stromspawn : MonoBehaviour {
	
	public GameObject Strom;
	private new Vector3 Randomp;

	// Use this for initialization
	void Start () {
		StartCoroutine (Wait());
	
	}
	
	// Update is called once per frame
	void Update () {
		

		}
	IEnumerator Wait() 
	{
		yield return new WaitForSeconds (2);
		Spawn ();
		yield return new WaitForSeconds (4);
		Spawn ();
		yield return new WaitForSeconds (6);
		Spawn ();
		while (true) {
			yield return new WaitForSeconds (4);
			Spawn ();

		}

	}

	void Spawn()
	{
		Randomp = new Vector3 (Random.Range(-5F, 5F),0f,(Random.Range(-2F, 2F)));
		Instantiate (Strom, transform.position + Randomp,  Quaternion.Euler(-90,0,0) ); 
	}

}
