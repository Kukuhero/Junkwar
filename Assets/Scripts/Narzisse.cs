using UnityEngine;
using System.Collections;

public class Narzisse : MonoBehaviour {

	private bool shoot = false;
	public GameObject spawnthing;
	public GameObject Spawnpoint;
	public static GameObject[] target = new GameObject[10];
	public GameObject narzisse;
	public static int x = 0;

	// Use this for initialization
	void Start () 
	{
		StartCoroutine (wait ());
	}

	// Update is called once per frame
	void Update () {
		if (shoot) {
			while (target [0] == null && shoot) {
				Arrayoverwrite (0);
				if (x <= 0) {
					shoot = false;
				}
			}

		}
		if (shoot) {
			narzisse.transform.rotation = Quaternion.RotateTowards (narzisse.transform.rotation, Quaternion.LookRotation (new Vector3 (transform.position.x - target [0].transform.position.x, 0f, transform.position.z - target [0].transform.position.z)), 100f * Time.deltaTime);
		}
		else {
			shoot = false;
		}

	}

	void OnTriggerEnter(Collider other)
	{
		switch (other.tag) {
		case "Enemy":
			shoot = true;

			target [x] = other.gameObject;
			x++;


			break;
		}
	}

	void OnTriggerExit(Collider other)
	{
		switch (other.tag) 
		{
		case "Enemy":
			int a = 0;
			while(a<=target.Length-1)

			{
				if (other.gameObject == target[a]) 
				{
					Arrayoverwrite (a);

				}
				a++;

			}

			break;
		}
	}
	IEnumerator wait()
	{
		while (true) {
			if (shoot) {
			print ("inwhileshoot");
				if (Mathf.Abs(narzisse.transform.rotation.y) <= Mathf.Abs(Quaternion.LookRotation (transform.position - target [0].transform.position).y * 1.1f) && Mathf.Abs(narzisse.transform.rotation.y) >= Mathf.Abs(Quaternion.LookRotation (transform.position - target [0].transform.position).y * 0.9f)) 
				{
				print ("inIfrotationAbfrageNarzisse");
					Shoot ();
				}

				//yield return new WaitForSeconds (1);

			}
		yield return new WaitForSeconds(1);
		}
	}

	void Shoot()
	{
		Instantiate (spawnthing, Spawnpoint.transform.position , transform.rotation);


	}

	void Arrayoverwrite (int i)
	{
		x--;
		while (i < target.Length-1) 
		{
			target [i] = target [i + 1];
			i++;
		}
	}
}
