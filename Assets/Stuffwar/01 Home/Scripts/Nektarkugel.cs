using UnityEngine;
using System.Collections;

public class Nektarkugel : MonoBehaviour {

	private Vector3 targetposition;
	private int speed = 3;
	private float distancetotarget;

	// Use this for initialization
	void Start () {
		Projektilzerstoerung ();
		//distancetotarget = targetposition - transform.position;
		transform.rotation = Quaternion.LookRotation(transform.position - Narzisse.target[0].transform.position);
		targetposition = Narzisse.target[0].transform.position - transform.position;
	}

	// Update is called once per frame
	void Update () 
	{
		transform.position += targetposition * speed* Time.deltaTime /**(1/distancetotarget)*/;

	}
	void OnTriggerEnter(Collider other)
	{
		switch (other.tag) 
		{
		case "Enemy":
			(int)Narzisse.target[0].GetComponent<EnemyController>().health -= 50;
			Destroy (transform.gameObject);
			break;
		}
	}

	IEnumerator Projektilzerstoerung()
	{
		while (true) 
		{
			yield return new WaitForSeconds (5);
			Destroy (gameObject);
		}
	}
}
