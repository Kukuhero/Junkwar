using UnityEngine;
using System.Collections;

public class HeldController : MonoBehaviour {
    private int speed = 5;
    private Vector3 nRichtungsvector;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
        int layerMask = 1 << 8;
        Debug.DrawRay(ray.origin,ray.direction * 100, Color.black, 2f, true);
        if (Physics.Raycast(ray, out hit, 3000f,layerMask))
        {
            //print(Quaternion.RotateTowards(gameObject.transform.rotation, Quaternion.LookRotation(new Vector3(hit.point.x, /*gameObject.transform.position.y*/0f, hit.point.z)),100f* Time.deltaTime));
            gameObject.transform.rotation = Quaternion.RotateTowards(gameObject.transform.rotation, 
                                            Quaternion.LookRotation(new Vector3(hit.point.x, 0f, hit.point.z)),200f* Time.deltaTime);

            nRichtungsvector = Vectornormieren(Vectorberechnung(gameObject.transform.position, hit.point));
           // print(nRichtungsvector);
        }

        if(Input.anyKey)
        {
            gameObject.transform.position = 
                new Vector3(gameObject.transform.position.x + (Input.GetAxis("Horizontal") + nRichtungsvector.x) * Time.deltaTime * speed, 
                gameObject.transform.position.y,
                    gameObject.transform.position.z + (Input.GetAxis("Vertical") + nRichtungsvector.z) * Time.deltaTime * speed);
            print(Input.GetAxis("Horizontal"));
        }
	}

    Vector3 Vectorberechnung(Vector3 Start , Vector3 Ziel)
    {
        Vector3 vector = new Vector3(Ziel.x - Start.x, Ziel.y - Start.y, Ziel.z - Start.z);
        return vector;

    }

    Vector3 Vectornormieren(Vector3 vector)
    {
        float Länge = Mathf.Sqrt (vector.x * vector.x + vector.y * vector.y + vector.z * vector.z);
        return (1 / Länge) * vector; 
    }
}
