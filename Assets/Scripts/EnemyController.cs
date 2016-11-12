using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	private Vector3 offset;
	private bool damage = false;
	public GameObject House;
	public GameObject KIDef;
	public GameObject Zahnrad;
	private float Initiatedspeed;
	private bool inTriggerEnemy = false;

	// Use this for initialization
	void Awake () 
	{
		gameObject.transform.parent.GetComponent<Wegfindung>().target = House.transform;
		StartCoroutine (makeDamage ());
		Initiatedspeed = gameObject.transform.parent.GetComponent<Wegfindung> ().speed;
	}

	// Update is called once per frame
	void Update ()
	{	

		//print (gameObject.GetComponent<Wegfindung>().speed);
		gameObject.transform.parent.transform.rotation = Quaternion.RotateTowards 
			(gameObject.transform.parent.transform.rotation, 
			Quaternion.LookRotation (new Vector3 (-(gameObject.transform.parent.transform.position.x - gameObject.transform.parent.GetComponent<Wegfindung>().currenttarget.position.x),0f,-( gameObject.transform.parent.transform.position.z - gameObject.transform.parent.GetComponent<Wegfindung>().currenttarget.position.z))),
				100f * Time.deltaTime);
	
		
			
	}
		

	void OnTriggerEnter (Collider other)
	{	
		switch (other.tag) 
		{
		case "KIDef":
			offset = other.gameObject.transform.position - gameObject.transform.parent.transform.position;
			gameObject.transform.parent.transform.rotation = Quaternion.LookRotation (offset);
			gameObject.transform.parent.GetComponent<Wegfindung>().target = other.gameObject.transform;
			damage = true;

			break;

		case "Haus":
			
			gameObject.gameObject.transform.parent.GetComponent<Wegfindung>().currenttarget = gameObject.gameObject.transform.parent.transform;
			StartCoroutine (makeDamageHouse ());

			break;

		/*case "plant":
			gameObject.GetComponent<Wegfindung>().target = other.gameObject.transform;

			break;*/

		case "Enemy":
			
			if (!inTriggerEnemy) 
			{
				inTriggerEnemy = true;
				gameObject.transform.parent.GetComponent<Wegfindung> ().speed -= Random.Range (1, 2);
			}
			break;

		case "Zahnrad":
			gameObject.transform.parent.GetComponent<Wegfindung> ().Wegfinden (other.gameObject);
			break;

		
		
		}
	}

	void OnTriggerExit (Collider other)
	{
		switch (other.tag) 
		{
		case "KIDef":
			damage = false;
			break;

		case "Haus":
			break;

		case "Enemy":
			inTriggerEnemy = false;
			gameObject.transform.parent.GetComponent<Wegfindung> ().speed = Initiatedspeed;
			break;
		}
	}

	

	IEnumerator makeDamageHouse()
	{

		while ((int)House.GetComponentInParent<HouseController> ().HouseHealth != 0) {
			if ((int)House.GetComponentInParent<HouseController> ().HouseHealth == 0) {
				damage = false;
			} else {
				(int)House.GetComponentInParent<HouseController> ().HouseHealth -= 20;
				yield return new WaitForSeconds (1);
			}
		}
	}

	IEnumerator makeDamage()
	{
		
		while (true) {
			if (gameObject.GetComponentInParent<Wegfindung>().target == null) {
				damage = false;
			} else {
				if (damage == true) 
				{
					(int)gameObject.GetComponentInParent<Wegfindung>().target.GetComponentInParent<KIDefController> ().KIDefhealth -= 20;
				}
			}
			yield return new WaitForSeconds (1);
		}
	}
		
}
