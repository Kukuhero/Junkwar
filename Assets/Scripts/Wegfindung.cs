using UnityEngine;
using System.Collections;


public class Wegfindung : MonoBehaviour {
	public Transform target;
	private Transform[] Wegpunkte = new Transform[15];
	private Transform[] Wegpunktenew;
	private float targetdistance;
	private float[] VectorlaengeArray = new float[15];
	private Wegpunkt[] WegpunkteArray = new Wegpunkt[15];
	private float kleinstestotaldistance;
	private int ikleinstestotaldistance = 1;
	private float zweitkleinstestotaldistance;
	private int izweitkleinstestotaldistance;
	public Transform currenttarget;
	private bool Wegpunktvar;
	public float speed;


	// Use this for initialization
	void Start () 
	{
		Wegpunkte [1] = target;
		currenttarget = target;
		FreierWeg ();

	}
	
	// Update is called once per frame
	void Update () 
	{
        /*if (currenttarget == null)
            currenttarget = target;*/
		//print (currenttarget);
		targetdistance = Vectorlaenge(Vectorberechnung(transform.position, currenttarget.transform.position));
		transform.position += (currenttarget.position - transform.position) * (1/targetdistance) * speed * Time.deltaTime;
		//////print (target);
	}

	void FreierWeg()
	{
		StopCoroutine(Triggerabfrage());
		////print ("infreierWeg");
		Wegpunktvar = true;
		RaycastHit hit;
		targetdistance = Vectorlaenge (Vectorberechnung (transform.position, target.transform.position));
		Vector3 ray = target.transform.position-transform.position;
		Debug.DrawRay (transform.position, ray, Color.blue, 3f);
		int layerMask = 1 << 10;
		//layerMask = ~layerMask;
		//////print (GetComponent<Collider>().bounds.size);
		if (Physics.BoxCast(transform.position,GetComponent<Collider>().bounds.size/2.3f,ray,out hit ,transform.rotation,targetdistance,layerMask)) 
		{
			////print ("inif5");
			switch (hit.collider.tag) 
			{
			case("Haus"):
				//print ("Hausteil " + hit.collider.gameObject);
				currenttarget = target;
				break;

			case("Hinderniss"):
				//print ("Weg versperrt" + hit.collider.gameObject);
				Wegfinden (hit.collider.gameObject);
				break;
				
				default:
				////print ("Weg versperrt2" + hit.collider.gameObject);
				//Wegfinden (hit.collider.gameObject);
				break;

				
			}
		}

	
	}
		

	public void Wegfinden(GameObject Hindernis)
	{   
		int ii = 0;
		////print (Hindernis);
		////print ("inWegfinden");
		Wegpunktenew = Hindernis.GetComponentsInChildren<Transform> ();
		if (Wegpunkte[1].transform.position != Wegpunktenew[1].transform.position) 
		{
			//////print ("neues Object");
			Wegpunkte = Hindernis.GetComponentsInChildren<Transform>();
		
			kleinstestotaldistance = 300f;
			for (int i = 1; i < Wegpunkte.Length; i++) 
			{
				VectorlaengeArray [i] = Vectorlaenge (Vectorberechnung (transform.position, Wegpunkte [i].transform.position));
				WegpunkteArray [i] = new Wegpunkt ();
				WegpunkteArray [i].Wegpunkte_in_Array (Wegpunkte [i].transform.position, VectorlaengeArray [i] + Vectorlaenge (Vectorberechnung (Wegpunkte [i].transform.position, target.transform.position)), wegfreiberechnung (Wegpunkte [i].transform.position), Vectorlaenge (Vectorberechnung (transform.position, Wegpunkte [i].transform.position)), Vectorlaenge (Vectorberechnung (Wegpunkte [i].transform.position, target.transform.position)),Wegpunkte[i].tag);

				if (WegpunkteArray [i].wegfrei_aus_Array () &&  "Wegpunkt"== Wegpunkte[i].tag) 
				{
					ii++;
					if (WegpunkteArray [i].totaldistance_aus_Array () < kleinstestotaldistance) 
					{
						kleinstestotaldistance = WegpunkteArray [i].totaldistance_aus_Array ();
						ikleinstestotaldistance = i;
					}

					//////print (WegpunkteArray [i].totaldistance_aus_Array ());
				}
			}
			////print (ii + "anzahlii" );
			if (ii == 0) {
				//////print (Wegpunkte[ikleinstestotaldistance].position);
				//////print (ikleinstestotaldistance);
				RaycastHit hit;
				targetdistance = Vectorlaenge (Vectorberechnung (transform.position, Wegpunkte[ikleinstestotaldistance].position));
				Vector3 ray =  Wegpunkte[ikleinstestotaldistance].position - transform.position;
				Debug.DrawRay (transform.position, ray, Color.green, 3f);
				int layerMask = 1 << 10;
				//layerMask = ~layerMask;
				////print (Physics.Raycast (transform.position, ray, out hit, layerMask));
				if (Physics.BoxCast(transform.position,GetComponent<Collider>().bounds.size/2.3f,ray,out hit ,transform.rotation,targetdistance,layerMask)) {
					Wegfinden (hit.transform.gameObject);
					////print ("inifii"+hit.transform);
				}
				//currenttarget = Wegpunkte [2];
			} else {
				////print ("false "+ii+" "+Wegpunkte.Length);
				currenttarget = Wegpunkte [ikleinstestotaldistance];
			} 
		} 
		else 
		{
			//////print ("gleiches Object");
			kleinstestotaldistance = 300f;
			zweitkleinstestotaldistance = 300f;
			izweitkleinstestotaldistance = 0;
			int i = 1;
			for (i = 1; i < Wegpunkte.Length; i++) 
			{
				VectorlaengeArray [i] = Vectorlaenge (Vectorberechnung (transform.position, Wegpunkte [i].transform.position));
				WegpunkteArray [i].Wegpunkte_in_Array (Wegpunkte [i].transform.position, VectorlaengeArray [i] + Vectorlaenge (Vectorberechnung (Wegpunkte [i].transform.position, target.transform.position)), wegfreiberechnung (Wegpunkte [i].transform.position), Vectorlaenge (Vectorberechnung (transform.position, Wegpunkte [i].transform.position)), Vectorlaenge (Vectorberechnung (Wegpunkte [i].transform.position, target.transform.position)), Wegpunkte[i].tag);
				//WegpunkteArray [ikleinstestotaldistance].Wegpunkte_in_Array (Wegpunkte [ikleinstestotaldistance].transform.position, 300f, wegfreiberechnung ( Wegpunkte [ikleinstestotaldistance].transform.position), Vectorlaenge (Vectorberechnung (transform.position, Wegpunkte [ikleinstestotaldistance].transform.position)), Vectorlaenge (Vectorberechnung (Wegpunkte [ikleinstestotaldistance].transform.position, target.transform.position)));

				if (WegpunkteArray [i].wegfrei_aus_Array () && Wegpunkte[i].tag == "Wegpunkt") 
				{
					////print("inif"+i);
					if (WegpunkteArray [i].totaldistance_aus_Array () < kleinstestotaldistance) 
					{
						zweitkleinstestotaldistance = kleinstestotaldistance;
						izweitkleinstestotaldistance = ikleinstestotaldistance;
						kleinstestotaldistance = WegpunkteArray [i].totaldistance_aus_Array ();
						ikleinstestotaldistance = i;

					} else 
					{
						////print ("inelse");
						if (WegpunkteArray [i].totaldistance_aus_Array () < zweitkleinstestotaldistance) 
						{
							zweitkleinstestotaldistance = WegpunkteArray [i].totaldistance_aus_Array ();
							izweitkleinstestotaldistance = i;
						}
					}

				}
			}
			if (izweitkleinstestotaldistance == 0) 
			{
				currenttarget = Wegpunkte [ikleinstestotaldistance];
			} 
			else 
			{
				////print ("else + kleinstetotaldistance "+izweitkleinstestotaldistance);
				currenttarget = Wegpunkte [izweitkleinstestotaldistance];
				//currenttarget = Wegpunkte [ikleinstestotaldistance];
			}
		}
	}

	Vector3 Vectorberechnung(Vector3 Start , Vector3 Ziel)
	{
		 Vector3 Wegpunktvector = new Vector3(Ziel.x - Start.x, Ziel.y - Start.y, Ziel.z - Start.z);
		return Wegpunktvector;

	}

	float Vectorlaenge(Vector3 vector)
	{
		return Mathf.Sqrt (vector.x * vector.x + vector.y * vector.y + vector.z * vector.z);
	}

	bool wegfreiberechnung(Vector3 Wegpunktposition)
	{
		RaycastHit hit;
		targetdistance = Vectorlaenge (Vectorberechnung (transform.position, Wegpunktposition));
		Vector3 ray =  Wegpunktposition - transform.position;
		Debug.DrawRay (transform.position, ray, Color.red, 3f);
		int layerMask = 1 << 10;
		//layerMask = ~layerMask;
		if (Physics.BoxCast(transform.position,GetComponent<Collider>().bounds.size/2.3f,ray,out hit ,transform.rotation,targetdistance,layerMask)) 
		{
			
			if (hit.transform.gameObject.transform.position == Wegpunktposition) 
			{
				//////print ("true "+hit.transform.gameObject);
				return true;
			} else 
			{
				if (hit.transform.gameObject.tag == "Enemy") 
				{
					////print("EnemyraycastWegpunkthit");
					return true;
				} else {
					//////print ("false " + hit.transform.gameObject);
					//////print(hit.transform.gameObject.transform.position);
					return false;
				}
			}
				
		} 
		else 
		{
			////print ("true");
			return true;
		}

	}



	void OnTriggerEnter(Collider other)
	{
		//////print ("inTrigger"+other.transform.gameObject);
		if (other.tag == "plant") 
		{
			////print ("planttrigger");
		}
		if (other.gameObject.transform == currenttarget) 
		{
			////print ("istTarget");
			//StartCoroutine (Triggerabfrage());
			FreierWeg();
		}
	}

/*	void OnCollisionEnter(Collision collision)
	{
		////print (collision.gameObject);
		if (collision.gameObject.tag != "Basic Ground") {
			Wegfinden (collision.gameObject);
		} 
	}*/

	IEnumerator Triggerabfrage()
	{
		bool Coroutinebool = true; 
		while (Coroutinebool)
		{
			//print("inTriggerabfrage");
			if (gameObject.transform.position.x <= currenttarget.position.x + 0.1f && gameObject.transform.position.z <= currenttarget.position.z+ 0.01f
				&& gameObject.transform.position.x >= currenttarget.position.x - 0.1f && gameObject.transform.position.z >= currenttarget.position.z - 0.01f)
			{
				//print(" inIfTriggerabfrage");
				FreierWeg();
				Coroutinebool = false;
				//print("nicht break");
			}
			yield return new WaitForSeconds(0.01f);
		}
	}

}

class Wegpunkt
{
	private Vector3 position;
	private float totaldistance;
	private bool wegfrei;
	private float Entfernung;
	private float distancetotarget;
	private string tag;

	public void Wegpunkte_in_Array(Vector3 position2, float totaldistance2, bool wegfrei2, float Entfernung2, float distancetotarget2, string tag2)
	{
		position = position2;
		totaldistance = totaldistance2;
		wegfrei = wegfrei2;
		Entfernung = Entfernung2;
		distancetotarget = distancetotarget2;
		tag = tag2;

	}


	public Vector3 Position_aus_Array ()
	{
		return position;

	}

	public float totaldistance_aus_Array ()
	{
		return totaldistance;

	}

	public bool wegfrei_aus_Array ()
	{
		return wegfrei;

	}

	public float Entfernung_aus_Array ()
	{
		return Entfernung;

	}

	public float distancetotarget_aus_Array ()
	{
		return distancetotarget;

	}
	public string Tag_aus_Array()
	{
		return tag;
	}
}
