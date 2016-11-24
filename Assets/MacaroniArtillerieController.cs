using UnityEngine;
using System.Collections;

public class MacaroniArtillerieController : MonoBehaviour {

	private int targets = 0;
	public GameObject normalTarget;
	public GameObject house;
	public static GameObject[] target = new GameObject[10];
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//print (gameObject.GetComponent<Wegfindung> ().currenttarget);
		if (targets == 0) 
		{
			gameObject.transform.parent.GetComponent<Wegfindung> ().target = normalTarget.transform;
		}

		if (targets > 5) 
		{
			gameObject.transform.parent.GetComponent<Wegfindung> ().target = house.transform;
		}
		if (0 < targets && targets <= 5) 
		{
			print (target[0].gameObject.transform.parent.GetComponentInParent<Transform> ());
			gameObject.transform.parent.GetComponent<Wegfindung> ().target = null;
		}
	}


	void OnTriggerEnter(Collider other)
	{

		if(other.gameObject.CompareTag("Enemy")) 
		{
			target [targets] = other.gameObject;
			targets++;
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

	void Arrayoverwrite (int i)
	{
		targets--;
		while (i < target.Length-1) 
		{
			target [i] = target [i + 1];
			i++;
		}
	}

}
