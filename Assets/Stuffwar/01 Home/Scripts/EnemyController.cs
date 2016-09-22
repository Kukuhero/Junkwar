using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	private Vector3 offset;
	private bool damage = false;
	public GameObject House;
	public GameObject KIDef;
	public int health = 100;
	public Texture2D healthTexture;
	private Vector3 screenPos;
	private int Position = 30;
	public int maxhealth = 100;
	public GameObject Zahnrad;
	private float Initiatedspeed;
	private bool inTriggerEnemy = false;

	// Use this for initialization
	void Start () 
	{
		gameObject.GetComponent<Wegfindung>().target = House.transform;
		StartCoroutine (makeDamage ());
		Initiatedspeed = gameObject.GetComponent<Wegfindung> ().speed;

	}

	// Update is called once per frame
	void Update ()
	{	
		//print (gameObject.GetComponent<Wegfindung>().speed);
		gameObject.transform.rotation = Quaternion.RotateTowards (gameObject.transform.rotation, Quaternion.LookRotation (new Vector3 (-(transform.position.x - gameObject.GetComponent<Wegfindung>().currenttarget.position.x), 0f,-( transform.position.z - gameObject.GetComponent<Wegfindung>().currenttarget.position.z))), 100f * Time.deltaTime);
	
		if (health <= 0) 
		{
			Destroy (gameObject);
			if (Random.Range(0f, 1f) <= 1f) 
			{
				Instantiate (Zahnrad, new Vector3(transform.position.x,1f,transform.position.z), gameObject.transform.rotation );
			}

		}
			
	}
		

	void OnTriggerEnter (Collider other)
	{	
		switch (other.tag) 
		{
		case "KIDef":
			offset = other.gameObject.transform.position - transform.position;
			transform.rotation = Quaternion.LookRotation (offset);
			gameObject.GetComponent<Wegfindung>().target = other.gameObject.transform;
			damage = true;

			break;

		case "Haus":
			
			gameObject.GetComponent<Wegfindung>().currenttarget = gameObject.transform;
			StartCoroutine (makeDamageHouse ());

			break;

		/*case "plant":
			gameObject.GetComponent<Wegfindung>().target = other.gameObject.transform;

			break;*/

		case "Enemy":
			
			if (!inTriggerEnemy) 
			{
				inTriggerEnemy = true;
				gameObject.GetComponent<Wegfindung> ().speed -= Random.Range (1, 2);
			}
			break;

		case "Zahnrad":
			gameObject.GetComponent<Wegfindung> ().Wegfinden (other.gameObject);
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
			gameObject.GetComponent<Wegfindung> ().speed = Initiatedspeed;
			break;
		}
	}

	void OnGUI()
	{
		if (health != maxhealth) 
		{
			screenPos = Camera.main.WorldToScreenPoint (transform.position);
			GUI.DrawTexture (new Rect (screenPos.x - 40, Screen.height - screenPos.y - Position, health / 2, 5), healthTexture);
			GUI.color = Color.black;
			GUI.Label (new Rect (screenPos.x - 35, Screen.height - screenPos.y - Position, 50, 30), "" + health + "/" + maxhealth);
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
			if (gameObject.GetComponent<Wegfindung>().target == null) {
				damage = false;
			} else {
				if (damage == true) 
				{
					(int)gameObject.GetComponent<Wegfindung>().target.GetComponentInParent<KIDefController> ().KIDefhealth -= 20;
				}
			}
			yield return new WaitForSeconds (1);
		}
	}
		
}
