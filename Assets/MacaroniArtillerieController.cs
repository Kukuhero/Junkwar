using UnityEngine;
using System.Collections;

public class MacaroniArtillerieController : MonoBehaviour {

	private int targets = 0;
	public GameObject normalTarget;
	public GameObject house;
	public GameObject[] target = new GameObject[10];
    public int intSpeed;
    private bool inshoot = false;
    public Transform Spawnpoint;
    public GameObject Munition;
	// Use this for initialization
	void Awake () {
        gameObject.transform.parent.GetComponent<Wegfindung>().speed = intSpeed;
        gameObject.transform.parent.GetComponent<Wegfindung> ().target = normalTarget.transform;
	}
	
	// Update is called once per frame
	void Update () {


            gameObject.transform.parent.transform.rotation = Quaternion.RotateTowards 
            (gameObject.transform.parent.transform.rotation, 
                Quaternion.LookRotation(new Vector3((gameObject.transform.parent.transform.position.x - gameObject.transform.parent.GetComponent<Wegfindung>().currenttarget.position.x), 0f, (gameObject.transform.parent.transform.position.z - gameObject.transform.parent.GetComponent<Wegfindung>().currenttarget.position.z))),
                15f * Time.deltaTime);
        

		//print (gameObject.GetComponent<Wegfindung> ().currenttarget);
		if (targets == 0) 
		{
			gameObject.transform.parent.GetComponent<Wegfindung> ().target = normalTarget.transform;
            gameObject.transform.parent.GetComponent<Wegfindung>().speed = intSpeed;
		}

		if (targets > 5) 
		{
			gameObject.transform.parent.GetComponent<Wegfindung> ().target = house.transform;
            gameObject.transform.parent.GetComponent<Wegfindung>().speed = intSpeed;


		}
		if (0 < targets && targets <= 5) 
		{
			print (target[0].gameObject.transform.parent.GetComponentInParent<Transform> ());
            gameObject.transform.parent.GetComponent<Wegfindung> ().target = target[0].transform;
            gameObject.transform.parent.GetComponent<Wegfindung>().speed = 0;
            if (!inshoot)
            {
                StartCoroutine(Laufrotieren());
                StartCoroutine(Shoot());
                inshoot = true;
            }
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
    IEnumerator Laufrotieren()
    {
        
        while (true)
        {
            float distance = Vectorlaenge(Vectorberechnung(transform.parent.position, target[0].transform.position));
            Quaternion rotationAuge = transform.FindChild("Untergeh_use").FindChild("Augen").transform.localRotation;
            Quaternion rotation = transform.FindChild("Chassie-Knochen").FindChild("Lauf-Bone").transform.localRotation;
            rotationAuge.x = (distance - 90f)/90f;
            rotation.y = (distance - 90f)/110f;
            transform.FindChild("Untergeh_use").FindChild("Augen").transform.localRotation = rotationAuge;
            transform.FindChild("Chassie-Knochen").FindChild("Lauf-Bone").transform.localRotation = rotation;
            //print(transform.FindChild("Chassie-Knochen").FindChild("Lauf-Bone").rotation);
            yield return new WaitForSeconds(0.1f);

        }
    }
    IEnumerator Shoot()
    {
        while (true)
        {
            Instantiate(Munition, Spawnpoint.transform.position, Spawnpoint.rotation, transform);
            print(Spawnpoint.rotation);
            yield return new WaitForSeconds(3f);
        }
    }

    Vector3 Vectorberechnung(Vector3 Start , Vector3 Ziel)
    {
        Vector3 vector = new Vector3(Ziel.x - Start.x, Ziel.y - Start.y, Ziel.z - Start.z);
        return vector;

    }
    float Vectorlaenge(Vector3 vector)
    {
        return Mathf.Sqrt (vector.x * vector.x + vector.y * vector.y + vector.z * vector.z);
    }
}
