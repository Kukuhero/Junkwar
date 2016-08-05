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

			transform.position += (currenttarget.position - transform.position) * 0.7f * Time.deltaTime;
		
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
			case("target"):
				print ("bewegen");
				currenttarget = target;
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

				if (WegpunkteArray [i].wegfrei_aus_Array ()) 
				{
				
					if (WegpunkteArray [i].totaldistance_aus_Array () < kleinstestotaldistance) 
					{
						kleinstestotaldistance = WegpunkteArray [i].totaldistance_aus_Array ();
						ikleinstestotaldistance = i;
					}

					//print (WegpunkteArray [i].totaldistance_aus_Array ());
				}
			}

			currenttarget = Wegpunkte [ikleinstestotaldistance];
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
					print (WegpunkteArray [i].totaldistance_aus_Array ());
				}
			}
			if (izweitkleinstestotaldistance == 0) 
			{
				currenttarget = Wegpunkte [ikleinstestotaldistance];
			} 
			else 
			{
				print ("else");
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
				if (hit.transform.gameObject.tag == "triger") 
				{
					print("Targethit");
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
		if (other.gameObject.transform == currenttarget) 
		{
			print ("istTarget");
			FreierWeg ();
		}
	}

}

	