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

	// Use this for initialization
	void Start () 
	{
		gameObject.GetComponent<Wegfindung>().target = House.transform;
		StartCoroutine (makeDamage ());

	}

	// Update is called once per frame
	void Update ()
	{	
		
		if (health <= 0) 
		{
			Destroy (gameObject);
			if (Random.Range(0f, 1f) <= 0.1f) 
			{
				Instantiate (Zahnrad, transform.position, gameObject.transform.rotation );
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
			
			gameObject.GetComponent<Wegfindung>().target = other.gameObject.transform;
			StartCoroutine (makeDamageHouse ());

			break;

		/*case "plant":
			gameObject.GetComponent<Wegfindung>().target = other.gameObject.transform;

			break;*/
		
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
