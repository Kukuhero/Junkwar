using UnityEngine;
using System.Collections;

public class HeldController : MonoBehaviour {
    public float speed = 3f;
    private Vector3 nRichtungsvector;
    public static bool aktive;
    private Animator anim;
    public bool attack = false;
    public GameObject smoke;
    public GameObject crack;
    private int Sprunganzahl = 0;

	// Use this for initialization
	void Start () 
    {
        anim = gameObject.GetComponentInChildren<Animator>() ;
        aktive = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (aktive)
        {
            Heldensteuerung();
        }
       
	}

    Vector3 Vectorberechnung(Vector3 Start , Vector3 Ziel)
    {
        Vector3 vector = new Vector3(Ziel.x - Start.x, Ziel.y - Start.y, Ziel.z - Start.z);
        return vector;

    }

    Vector3 Normalenberechnung(Vector3 vector)
    {
        return vector = new Vector3(vector.z, 0f, -vector.x);
    }

    Vector3 Vectornormieren(Vector3 vector)
    {
        float Länge = Mathf.Sqrt (vector.x * vector.x + vector.y * vector.y + vector.z * vector.z);
        return (1 / Länge) * vector; 
    }
    void Heldensteuerung()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            anim.SetBool("sideWalk", true);
        }
        else
        {
            anim.SetBool("sideWalk", false);
        }


        if (Input.GetAxis("Vertical") != 0)
        {
           
            anim.SetBool("Walk", true);
        } else
        {
            anim.SetBool("Walk", false);
        }


        if (Input.GetAxis("Vertical") != 0)
        {
            anim.SetBool("Walk", true);
        }else
        {
            anim.SetBool("Walk", false);
        }


        if (Input.GetMouseButtonDown(0))
        {
            attack = true;

            StartCoroutine(Attack());
        }


            
        if (Input.GetKeyDown(KeyCode.Space) && transform.position.y <= 4 && Sprunganzahl < 2)
                {
            gameObject.transform.position += new Vector3(0f, 40f, 0f) * Time.deltaTime;
            Sprunganzahl++;
            anim.SetBool("Walk", false);
                }
        if(transform.position.y < 1f)
        {
            Sprunganzahl = 0;
        }


        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
        int layerMask = 1 << 8;
        Debug.DrawRay(ray.origin,ray.direction * 100, Color.black, 2f, true);
        if (Physics.Raycast(ray, out hit, 3000f, layerMask))
        {
            //if (Vectorlaenge(Vectorberechnung(transform.position, hit.point)) > 2f && Input.GetAxis("Mouse X") != 0 ||Input.GetAxis("Mouse Y") != 0 )
            if (Vectorlaenge(Vectorberechnung(transform.position, hit.point)) > 2f)
            {

                nRichtungsvector = Vectornormieren(Vectorberechnung(gameObject.transform.position, 
                    new Vector3(Camera.current.transform.position.x,0f,Camera.current.transform.position.z)))*-1f;
                Normalenberechnung(nRichtungsvector);
                Debug.DrawLine(gameObject.transform.position, hit.point);
                Debug.DrawRay(transform.position, Normalenberechnung(nRichtungsvector) * 30f, Color.red);

                //gameObject.transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
                Vector3 rotationVector = transform.rotation.eulerAngles;
                rotationVector.y += Input.GetAxis("Mouse X") * 10;
                gameObject.transform.rotation = Quaternion.Euler(rotationVector);
                print(Input.GetAxis("Mouse Y"));
            

                gameObject.transform.position += 
            new Vector3((Input.GetAxis("Vertical") * nRichtungsvector.x) + (Input.GetAxis("Horizontal") * Normalenberechnung(nRichtungsvector).x), 0,
                    (Input.GetAxis("Vertical") * nRichtungsvector.z) + (Input.GetAxis("Horizontal") * Normalenberechnung(nRichtungsvector).z)) * speed * Time.deltaTime;
            }
            else
            {
                anim.SetBool("sideWalk", false);
                anim.SetBool("Walk", false);

            }
        } 
    }

    void OnCollisionEnter(Collision other)
    {
        switch(other.gameObject.tag)
        {
            case "Zahnrad":
                Destroy (other.transform.gameObject);
                HouseController.Zahnräder += 1;
                break;

        }
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.5f);
        aktive = false;
        anim.SetBool("Attack", true);
        anim.SetBool("Walk", false);
        Vector3 rotationVector = transform.rotation.eulerAngles;
        rotationVector.x += 90;
        rotationVector.y+= -90;
        Instantiate(crack, new Vector3 (gameObject.transform.position.x,0.1f,gameObject.transform.position.z)+ 6*nRichtungsvector,Quaternion.Euler(rotationVector));
        Instantiate(smoke, new Vector3 (gameObject.transform.position.x,0f,gameObject.transform.position.z)+ 6*nRichtungsvector, gameObject.transform.rotation);
        yield return new WaitForSeconds(1);
        print("Instantiate");
        anim.SetBool("Attack", false);
        aktive = true;
        attack = false;

    }
    float Vectorlaenge(Vector3 vector)
    {
        return Mathf.Sqrt (vector.x * vector.x + vector.y * vector.y + vector.z * vector.z);
    }
}
