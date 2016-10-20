using UnityEngine;
using System.Collections;

public class CamaraController : MonoBehaviour {
	public GameObject Kamera;
	private Vector3 mousePosition, newMousePosition;
	private Ray ray;
	public Userinterface userinterface;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {



		//Stromzähler
		if (Input.GetMouseButtonDown (0)) 
		{
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit, 80f)) 
			{
				switch(hit.collider.tag) 
				{
				case("Strom"):
					Destroy (hit.transform.gameObject);

					if (Batterie.maxStrom >= Userinterface.Strom + 10) {
						Userinterface.Strom += 10;
					} else {
						Userinterface.Strom = Batterie.maxStrom;
					}
					break;

				case("Zahnrad"):
					Destroy (hit.transform.gameObject);
					HouseController.Zahnräder += 1;
					break;

				case("Batterie"):
					Batterie.OnGuibool = true;
					break;
                 
                    case("Held"):
                        StartCoroutine(aktive());
                        break;

				default:
					Batterie.OnGuibool = false;
					break;
					}
				}
			}

        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 80f))
            {
                switch (hit.collider.tag)
                {
                    default:
                        HeldController.aktive = false;
                        break;
                }
            }
        }


		if (Input.GetMouseButtonDown (0)) 
		{
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit, 60f)) 
			{
				if (hit.collider.gameObject.name == "Batterie") 
				{
					print ("Batterie");
					/*
					if (Batterie.maxStrom >= Userinterface.Strom + 10) 
					{
						Userinterface.Strom += 10;
					} else {
						Userinterface.Strom = Batterie.maxStrom;

					}*/
				}
			}
		}

		

		if (Input.mouseScrollDelta.y != 0) {
			if (transform.position.y + Input.mouseScrollDelta.y >= 20f) {
				if (transform.position.y + Input.mouseScrollDelta.y <= 40f) {
					transform.position = new Vector3 (transform.position.x, transform.position.y + Input.mouseScrollDelta.y, transform.position.z);
				} else {
					transform.position = new Vector3 (transform.position.x, 40f, transform.position.z);
				}
			} else {
				transform.position = new Vector3 (transform.position.x, 20f, transform.position.z);
			}

			
		}
		newMousePosition = mousePosition - Input.mousePosition;
		if (Input.GetMouseButton (1)) {
			newMousePosition = new Vector3 (Mathf.Round (newMousePosition.y * -1f) / 6, 0f, Mathf.Round (newMousePosition.x) / 6);

			if (transform.position.x + newMousePosition.x >= -27f) 
			{
				if (transform.position.x + newMousePosition.x <= 44f) 
				{

					if (transform.position.z + newMousePosition.z >= -9f) 
					{
						if (transform.position.z + newMousePosition.z <= 70f) 
						{

							transform.position = transform.position + newMousePosition;

						} else 
						{
							transform.position = new Vector3 (transform.position.x + newMousePosition.x, transform.position.y + newMousePosition.y, 70f );
						}

					} else 
					{
						transform.position = new Vector3 (transform.position.x + newMousePosition.x, transform.position.y + newMousePosition.y, -9f );
					}
				} else 
				{
					transform.position = new Vector3 (44f, transform.position.y + newMousePosition.y, transform.position.z + newMousePosition.z);
				}
				
			} else 
			{
				transform.position = new Vector3 (-27f, transform.position.y + newMousePosition.y, transform.position.z + newMousePosition.z);
			}

			

		}
		mousePosition = Input.mousePosition;
	}
    IEnumerator aktive()
    {
        yield return new WaitForSeconds(1);
        HeldController.aktive = true;
        StopCoroutine(aktive());
    }
}
