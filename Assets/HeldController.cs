using UnityEngine;
using System.Collections;

public class HeldController : MonoBehaviour {
    private float speed = 5f;
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
            gameObject.transform.LookAt(new Vector3(hit.point.x,transform.position.y, hit.point.z));
            /*gameObject.transform.rotation = Quaternion.RotateTowards(gameObject.transform.rotation, 
                                            Quaternion.LookRotation(new Vector3(hit.point.x, 0f, hit.point.z)),200f* Time.deltaTime);*/

            nRichtungsvector = Vectornormieren(Vectorberechnung(gameObject.transform.position, hit.point));
            Normalenberechnung(nRichtungsvector);
            Debug.DrawLine(gameObject.transform.position, hit.point);
            Debug.DrawRay(transform.position, Normalenberechnung(nRichtungsvector)*30f,Color.red);
        }

        if(Input.anyKey)
        {
            gameObject.transform.position += 
                new Vector3((Input.GetAxis("Vertical") * nRichtungsvector.x) + (Input.GetAxis("Horizontal")* Normalenberechnung(nRichtungsvector).x), 0,
                    (Input.GetAxis("Vertical") * nRichtungsvector.z) + (Input.GetAxis("Horizontal")* Normalenberechnung(nRichtungsvector).z))*speed * Time.deltaTime;
            //print(/*nRichtungsvector= */ Mathf.Sqrt (nRichtungsvector.x * nRichtungsvector.x + nRichtungsvector.y * nRichtungsvector.y + nRichtungsvector.z * nRichtungsvector.z));
        }
	}

    Vector3 Vectorberechnung(Vector3 Start , Vector3 Ziel)
    {
        Vector3 vector = new Vector3(Ziel.x - Start.x, Ziel.y - Start.y, Ziel.z - Start.z);
        return vector;

    }

    Vector3 Normalenberechnung(Vector3 vector)
    {
        print(vector.z + vector.y - vector.x);
        return vector = new Vector3(vector.z, 0f, -vector.x);
    }

    Vector3 Vectornormieren(Vector3 vector)
    {
        float Länge = Mathf.Sqrt (vector.x * vector.x + vector.y * vector.y + vector.z * vector.z);
        return (1 / Länge) * vector; 
    }
}
