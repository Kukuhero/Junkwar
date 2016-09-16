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
	private int ikleinstestotaldistance;
	private float zweitkleinstestotaldistance;
	private int izweitkleinstestotaldistance;
	private Transform currenttarget;
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
		//print (currenttarget);
		targetdistance = Vectorlaenge(Vectorberechnung(transform.position, currenttarget.transform.position));
		transform.position += (currenttarget.position - transform.position) * (1/targetdistance) * speed * Time.deltaTime;
		print (target);
	}

	void FreierWeg()
	{
		print ("infreierWeg");
		Wegpunktvar = true;
		RaycastHit hit;
		targetdistance = Vectorlaenge (Vectorberechnung (transform.position, target.transform.position));
		Vector3 ray = target.transform.position-transform.position;
		Debug.DrawRay (transform.position, ray, Color.blue, 3f);
		if (Physics.Raycast (transform.position,ray, out hit, targetdistance)) 
		{
			switch (hit.collider.tag) 
			{
			case("Haus"):
				print ("bewegen");
				currenttarget = target;
				break;

			case("Enemy"):
				print ("raycasthitEnemy");
				currenttarget = hit.transform;
				break;
			
			case ("Wegpunkt"):
				print ("Wegpunkt hit");	
				while (Wegpunktvar) {
					targetdistance = Vectorlaenge (Vectorberechnung (hit.collider.transform.position, target.transform.position));
					ray = target.transform.position - hit.collider.transform.position;
					Debug.DrawRay (hit.collider.transform.position, ray, Color.black, 3f);
					if (Physics.Raycast (hit.collider.transform.position, ray, out hit, targetdistance)) {
						switch (hit.collider.tag) {
						case("target"):
							print ("bewegen");
							currenttarget = target;
							Wegpunktvar = false;
							break;

						case ("Wegpunkt"):
							print ("Wegpunkt hit");	
							Wegpunktvar = true;
							break;

						case("Basic Ground"):
							print ("ground");
							break;

						case("Enemy"):
							print ("raycasthitEnemy");
							currenttarget = hit.transform;
							break;

						default:
							print ("Weg versperrt2");
							Wegfinden (hit.collider.gameObject);
							Wegpunktvar = false;
							break;


						}
					}
				}
					break;
				
				default:
				print ("Weg versperrt2");
				Wegfinden (hit.collider.gameObject);
				break;

				
			}
		}

	
	}
		

	void Wegfinden(GameObject Hindernis)
	{   
		int ii = 0;
		print (Hindernis);
		print ("inWegfinden");
		Wegpunktenew = Hindernis.GetComponentsInChildren<Transform> ();
		if (Wegpunkte[1].transform.position != Wegpunktenew[1].transform.position) 
		{
			//print ("neues Object");
			Wegpunkte = Hindernis.GetComponentsInChildren<Transform> ();
		
			kleinstestotaldistance = 300f;
			for (int i = 1; i < Wegpunkte.Length; i++) 
			{
				VectorlaengeArray [i] = Vectorlaenge (Vectorberechnung (transform.position, Wegpunkte [i].transform.position));
				WegpunkteArray [i] = new Wegpunkt ();
				WegpunkteArray [i].Wegpunkte_in_Array (Wegpunkte [i].transform.position, VectorlaengeArray [i] + Vectorlaenge (Vectorberechnung (Wegpunkte [i].transform.position, target.transform.position)), wegfreiberechnung (Wegpunkte [i].transform.position), Vectorlaenge (Vectorberechnung (transform.position, Wegpunkte [i].transform.position)), Vectorlaenge (Vectorberechnung (Wegpunkte [i].transform.position, target.transform.position)));

				if (WegpunkteArray [i].wegfrei_aus_Array ()) {
				
					if (WegpunkteArray [i].totaldistance_aus_Array () < kleinstestotaldistance) {
						kleinstestotaldistance = WegpunkteArray [i].totaldistance_aus_Array ();
						ikleinstestotaldistance = i;
					}

					//print (WegpunkteArray [i].totaldistance_aus_Array ());
				} else 
				{
					ii++;
				}
			}
			print (ii + "anzahlii");
			if (ii == 4) {
				print ("keinwegfrei");
				RaycastHit hit;
				targetdistance = Vectorlaenge (Vectorberechnung (transform.position, Wegpunkte[ikleinstestotaldistance].position));
				Vector3 ray =  Wegpunkte[ikleinstestotaldistance].position - transform.position;
				Debug.DrawRay (transform.position, ray, Color.red, 3f);
				if (Physics.Raycast (transform.position, ray, out hit)) {
					currenttarget = hit.transform;
					print ("inifii"+hit.transform);
				}
				//currenttarget = Wegpunkte [2];
			} else {
				print ("false "+ii+" "+Wegpunkte.Length);
				currenttarget = Wegpunkte [ikleinstestotaldistance];
			}
		} 
		else 
		{
			//print ("gleiches Object");
			kleinstestotaldistance = 300f;
			zweitkleinstestotaldistance = 300f;
			izweitkleinstestotaldistance = 0;
			int i = 1;
			for (i = 1; i < Wegpunkte.Length; i++) 
			{
				VectorlaengeArray [i] = Vectorlaenge (Vectorberechnung (transform.position, Wegpunkte [i].transform.position));
				WegpunkteArray [i].Wegpunkte_in_Array (Wegpunkte [i].transform.position, VectorlaengeArray [i] + Vectorlaenge (Vectorberechnung (Wegpunkte [i].transform.position, target.transform.position)), wegfreiberechnung (Wegpunkte [i].transform.position), Vectorlaenge (Vectorberechnung (transform.position, Wegpunkte [i].transform.position)), Vectorlaenge (Vectorberechnung (Wegpunkte [i].transform.position, target.transform.position)));
				//WegpunkteArray [ikleinstestotaldistance].Wegpunkte_in_Array (Wegpunkte [ikleinstestotaldistance].transform.position, 300f, wegfreiberechnung ( Wegpunkte [ikleinstestotaldistance].transform.position), Vectorlaenge (Vectorberechnung (transform.position, Wegpunkte [ikleinstestotaldistance].transform.position)), Vectorlaenge (Vectorberechnung (Wegpunkte [ikleinstestotaldistance].transform.position, target.transform.position)));

				if (WegpunkteArray [i].wegfrei_aus_Array ()) 
				{
					print("inif"+i);
					if (WegpunkteArray [i].totaldistance_aus_Array () < kleinstestotaldistance) 
					{
						zweitkleinstestotaldistance = kleinstestotaldistance;
						izweitkleinstestotaldistance = ikleinstestotaldistance;
						kleinstestotaldistance = WegpunkteArray [i].totaldistance_aus_Array ();
						ikleinstestotaldistance = i;

					} else 
					{
						print ("inelse");
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
				print ("else"+izweitkleinstestotaldistance);
				currenttarget = Wegpunkte [izweitkleinstestotaldistance];
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
		if (Physics.Raycast (transform.position, ray, out hit, targetdistance)) 
		{
			
			if (hit.transform.gameObject.transform.position == Wegpunktposition) 
			{
				print ("true "+hit.transform.gameObject);
				return true;
			} else 
			{
				if (hit.transform.gameObject.tag == "Enemy") 
				{
					print("EnemyraycastWegpunkthit");
					return true;
				} else {
					print ("false " + hit.transform.gameObject);
					//print(hit.transform.gameObject.transform.position);
					return false;
				}
			}
				
		} 
		else 
		{
			print ("true");
			return true;
		}

	}



	void OnTriggerEnter(Collider other)
	{
		print ("inTrigger"+other.transform.gameObject);
		if (other.tag == "plant") 
		{
			print ("planttrigger");
		}
		if (other.gameObject.transform == currenttarget) 
		{
			print ("istTarget");
			FreierWeg ();
		}
	}

/*	void OnCollisionEnter(Collision collision)
	{
		print (collision.gameObject);
		if (collision.gameObject.tag != "Basic Ground") {
			Wegfinden (collision.gameObject);
		} 
	}*/

}

class Wegpunkt
{
	private Vector3 position;
	private float totaldistance;
	private bool wegfrei;
	private float Entfernung;
	private float distancetotarget;

	public void Wegpunkte_in_Array(Vector3 position2, float totaldistance2, bool wegfrei2, float Entfernung2, float distancetotarget2)
	{
		position = position2;
		totaldistance = totaldistance2;
		wegfrei = wegfrei2;
		Entfernung = Entfernung2;
		distancetotarget = distancetotarget2;

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
}
