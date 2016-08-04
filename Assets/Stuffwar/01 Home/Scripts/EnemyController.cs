using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	public GameObject GameObject;
	private Vector3 offset;
	private bool damage = false;
	public GameObject target;
	public float speed;
	public GameObject KIDef;
	private bool stop = false;
	public int health = 100;
	public Texture2D healthTexture;
	private Vector3 screenPos;
	private int Position = 30;
	public int maxhealth = 100;
	CharacterController cc;
	AIPath aip;
	Seeker s;
	// Use this for initialization
	void Start () 
	{
		cc = gameObject.GetComponentInParent<CharacterController>();
		aip = gameObject.GetComponentInParent<AIPath>();
		s = GetComponentInParent <Seeker> ();
		StartCoroutine (makeDamage ());

	}

	// Update is called once per frame
	void Update ()
	{	
		
		if (health <= 0) 
		{
			Destroy (GameObject);
		}
		if (stop == false) {
			cc.enabled = true;
			aip.enabled = true;
			s.enabled = true;
		} else {
			cc.enabled = false;
			aip.enabled = false;
			s.enabled = false;

		}

			
	}
		

	void OnTriggerEnter (Collider other)
	{	
		switch (other.tag) 
		{
		case "KIDef":
			offset = other.gameObject.transform.position - transform.position;
			transform.rotation = Quaternion.LookRotation (offset);
			stop = true;
			target = other.gameObject;
			damage = true;

			break;

		case "Haus":
			
			stop = true;
			target = other.gameObject;
			StartCoroutine (makeDamageHouse ());

			break;

		case "plant":
			target = other.gameObject;

			break;
		
		}
	}

	void OnTriggerExit (Collider other)
	{
		switch (other.tag) 
		{
		case "KIDef":
			stop = false;
			damage = false;
			break;

		case "Haus":
			stop = false;
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

		while ((int)target.GetComponentInParent<HouseController> ().HouseHealth != 0) {
			if ((int)target.GetComponentInParent<HouseController> ().HouseHealth == 0) {
				stop = true;
				damage = false;
			} else {
				(int)target.GetComponentInParent<HouseController> ().HouseHealth -= 20;
				yield return new WaitForSeconds (1);
			}
		}
	}

	IEnumerator makeDamage()
	{
		
		while (true) {
			if (target == null) {
				stop = false;
				damage = false;
			} else {
				if (damage == true) 
				{
					(int)target.GetComponentInParent<KIDefController> ().KIDefhealth -= 20;
				}
			}
			yield return new WaitForSeconds (1);
		}
	}
		
}
