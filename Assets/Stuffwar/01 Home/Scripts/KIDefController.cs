using UnityEngine;
using System.Collections;

public class KIDefController : MonoBehaviour 
{
	public float see;
	private bool attack = true;
	public float speed;
	private Vector3 abstand;
	public GameObject[] target;
	public int targeti =1;
	public int Schaden = 5;
	public int KIDefhealth =100;
	private int targetlength;
	private bool damage = false;
	private Vector3[] targetposition;
	private Vector3 screenPos;
	private int Position = 30;
	public Texture2D healthTexture;
	CharacterController cc;
	AIPath aip;
	Seeker s;
	Transform targettransform;
	private GameObject newtarget;

	// Use this for initialization
	void Start () 
	{
		target = GameObject.FindGameObjectsWithTag("Enemy");
		targetlength = target.Length;
		//Positionaktualisierung ();
		targeti = Random.Range (0, targetlength);
		targettransform = target [targeti].GetComponentInParent<Transform>();
		cc = gameObject.GetComponentInParent<CharacterController>();
		aip = gameObject.GetComponentInParent<AIPath>();
		s = GetComponentInParent <Seeker> ();
		gameObject.GetComponentInParent<AIPath> ().target = targettransform;
		StartCoroutine (Damage ());
	}
	
	// Update is called once per frame
	void Update () 
	{ 
		if (KIDefhealth <= 0) 
		{
			Destroy (transform.gameObject);
		}
			
		if (target [targeti] == null) 
		{
			target = GameObject.FindGameObjectsWithTag ("Enemy");
			targetlength = target.Length  ;
			if (targetlength > 0) {
				targeti = Random.Range (0, targetlength);
				targettransform = target [targeti].GetComponentInParent<Transform>();
				gameObject.GetComponentInParent<AIPath> ().target = targettransform;
				attack = true;
				damage = false;
			} else {
				attack = false;
				damage = false;
			}
		}
		if (attack == true) {
			cc.enabled = true;
			aip.enabled = true;
			s.enabled = true;
		} else {
			cc.enabled = false;
			aip.enabled = false;
			s.enabled = false;

		}
	}
		

	void OnTriggerEnter(Collider other)
	{
		
		if(other.gameObject.CompareTag("Enemy")) 
		{
			damage = true;
			newtarget = other.gameObject;
			print ("triggerenter");

			//Attackstop();
		}
	}
	void OnControllerColliderHit(ControllerColliderHit other){
		
		if (other.gameObject.CompareTag("Enemy")) 
		{
			print ("collision");
			newtarget = other.gameObject;

			attack = false;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject == target [targeti]) 
		{
			print ("triggerexit");
			attack = true;
			damage = false;
		}
	}
		
	void Positionaktualisierung()
	{
		for (int i = 0; i < target.Length; i++) 
		{
			targetposition[i]= target[i].transform.position;
			/*if (targetposition [E]) 
			{
				
			}*/
		}
	}

	IEnumerator Damage()
	{
		while (true) {
			if (damage) 
			{
				(int)newtarget.GetComponent<Health> ().health -= Schaden;
			}
				yield return new WaitForSeconds (3);

		}
	}
	void Attackstop(){
	
		new WaitForSeconds (1);
		attack = false;

	
	}
	void OnGUI()
	{
		if (KIDefhealth != 100) {
			screenPos = Camera.main.WorldToScreenPoint (transform.position);
			GUI.DrawTexture (new Rect (screenPos.x - 40, Screen.height - screenPos.y - Position, KIDefhealth / 2, 5), healthTexture);
			GUI.color = Color.black;
			GUI.Label (new Rect (screenPos.x - 35, Screen.height - screenPos.y - Position, 50, 30), "" + KIDefhealth + "/" + "100");
		}
	}
}
