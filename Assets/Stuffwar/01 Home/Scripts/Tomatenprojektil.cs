using UnityEngine;
using System.Collections;

public class Tomatenprojektil : MonoBehaviour {
	private Vector3 targetposition;
	private int speed = 2;
	float step;
	float flugbahn = 12f;

	// Use this for initialization
	void Start () {
		Projektilzerstoerung ();
		step = speed * Time.deltaTime;
		transform.rotation = Quaternion.LookRotation(transform.position - Tomate.target[0].transform.position);
		targetposition = Tomate.target[0].transform.position - transform.position;
	}

	// Update is called once per frame
	void Update () 
	{
		flugbahn -= 1.945f;
		transform.position += new Vector3( targetposition.x,targetposition.y + flugbahn,targetposition.z) * step;
	}
	void OnTriggerEnter(Collider other)
	{
		switch (other.tag) 
		{
		case "Enemy":
			(int)Tomate.target[0].GetComponent<EnemyController>().health -= 20;
			Destroy (transform.gameObject);
			break;
		}
	}

	IEnumerator Projektilzerstoerung()
	{
		while (true) {
			new WaitForSeconds (5);
			Destroy (transform.gameObject);
		}
	}

}