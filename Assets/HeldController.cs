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
    private float Vorwärtsbew;
    private float Seitwärtsbew;
    public float Sprunghöhe;
    private float altSprunghöhe;
    private float gravity;
    int i = 0;
    private bool fall = false;

	// Use this for initialization
	void Start () 
    {
        anim = gameObject.GetComponentInChildren<Animator>() ;
        aktive = false;
        altSprunghöhe = Sprunghöhe;
        gravity = 0;
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
         /*   
        RaycastHit hit;

        Vector3 ray =  new Vector3(0f,-1f,0f);
        Debug.DrawRay (transform.position, ray, Color.green, 3f);
        //if (Physics.BoxCast(transform.position, GetComponent<Collider>().bounds.size / 2.3f, ray, out hit, transform.rotation))
        if (Physics.Raycast(transform.position,ray,out hit, 300f))
        {
            
            print(Mathf.Round(Vectorlaenge(Vectorberechnung(transform.position, hit.point))));
            if (Vectorlaenge(Vectorberechnung(transform.position, hit.point)) > 0.5f && Sprunghöhe <= 0)
            //if(transform.position.y > 0)
            {
                fall = true;
            }
            else
            {
                fall = false;
            }
            if(fall)
            {
                i++;
                print("in Luft");
                Vorwärtsbew = Input.GetAxis("Horizontal");
                Seitwärtsbew = Input.GetAxis("Vertical");
                Physics.gravity +=  new Vector3(0f,(-(9.81f * (i)*(i))/2)/60,0f);
                print("gravity" + gravity);
                print("i: " + i);
                gameObject.transform.position += 
                    new Vector3((Seitwärtsbew * nRichtungsvector.x) + (Vorwärtsbew * Normalenberechnung(nRichtungsvector).x), -gravity,
                    (Seitwärtsbew * nRichtungsvector.z) + (Vorwärtsbew * Normalenberechnung(nRichtungsvector).z)) * speed * Time.deltaTime;
                
            }
            else
            {
                //transform.position = new Vector3(transform.position.x, 0, transform.position.z);
                i=0;
            }
        }*/
            


        if (Input.GetMouseButtonDown(0))
        {
            attack = true;

           
        }
        if (attack && transform.position.y < 3f)
        {
            StartCoroutine(Attack());
        }


            
        if (Input.GetKeyDown(KeyCode.Space) && transform.position.y <= 20 && Sprunganzahl < 2)
         {
            Vorwärtsbew = Input.GetAxis("Horizontal")*0.9f;
            Seitwärtsbew = Input.GetAxis("Vertical")*0.9f;

            if (Sprunganzahl == 0)
            {
                StartCoroutine(Jump()); 
                anim.SetBool("Jump", true);
            }
            else
            {
                StopCoroutine(Jump());
                StartCoroutine(Jump()); 
            }

          }
            
           

                nRichtungsvector = Vectornormieren(Vectorberechnung(gameObject.transform.position, 
                        new Vector3(Camera.main.transform.position.x, 0f, Camera.main.transform.position.z))) * -1f;
                Normalenberechnung(nRichtungsvector);
                Debug.DrawRay(transform.position, Normalenberechnung(nRichtungsvector) * 30f, Color.red);

                //gameObject.transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
                Vector3 rotationVector = transform.rotation.eulerAngles;
                rotationVector.y += Input.GetAxis("Mouse X") * 10;
                gameObject.transform.rotation = Quaternion.Euler(rotationVector);
                //print(Input.GetAxis("Mouse Y"));
        if (Sprunganzahl == 0)
        {

            print("Sprunganzahl " + Sprunganzahl);

            if (Input.GetAxis("Horizontal") != 0 && Input.GetAxis("Vertical") != 0)
            {
                gameObject.transform.position += 
            new Vector3((Input.GetAxis("Vertical") * nRichtungsvector.x) + (Input.GetAxis("Horizontal") * Normalenberechnung(nRichtungsvector).x), 0,
                    (Input.GetAxis("Vertical") * nRichtungsvector.z) + (Input.GetAxis("Horizontal") * Normalenberechnung(nRichtungsvector).z)) * speed * Time.deltaTime * 1 / Mathf.Sqrt(2f);
            }
            else
            {
                gameObject.transform.position += 
                        new Vector3((Input.GetAxis("Vertical") * nRichtungsvector.x) + (Input.GetAxis("Horizontal") * Normalenberechnung(nRichtungsvector).x), 0,
                    (Input.GetAxis("Vertical") * nRichtungsvector.z) + (Input.GetAxis("Horizontal") * Normalenberechnung(nRichtungsvector).z)) * speed * Time.deltaTime;
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
        if (Sprunganzahl > 0)
        {
            anim.SetBool("Jump", false);
            Sprunganzahl = 0;
            StopCoroutine(Jump());
        }
        if (fall)
        {
            fall = false;
        }
    }

    IEnumerator Attack()
    {
                anim.SetBool("Attack", true);
                anim.SetBool("Walk", false);
                aktive = false;
                Vector3 rotationVector = transform.rotation.eulerAngles;
                rotationVector.x += 90;
                rotationVector.y += -90;
                Instantiate(crack, new Vector3(gameObject.transform.position.x, 0.1f, gameObject.transform.position.z) + 6 * nRichtungsvector, Quaternion.Euler(rotationVector));
                Instantiate(smoke, new Vector3(gameObject.transform.position.x, 0f, gameObject.transform.position.z) + 6 * nRichtungsvector, gameObject.transform.rotation);
                yield return new WaitForSeconds(1);
                print("Instantiate");
                anim.SetBool("Attack", false);
                aktive = true;
                attack = false;
                StopCoroutine(Attack());
        

    }
    float Vectorlaenge(Vector3 vector)
    {
        return Mathf.Sqrt (vector.x * vector.x + vector.y * vector.y + vector.z * vector.z);
    }
    IEnumerator Jump()
    {
        Sprunghöhe = altSprunghöhe;
        print(Sprunghöhe);
        Sprunganzahl++;
        yield return new WaitForSeconds(0.2f);
        while (Sprunghöhe > -altSprunghöhe-0.5f)
        {
            anim.SetBool("sideWalk", false);
            anim.SetBool("Walk", false);
            gameObject.transform.position += 
                new Vector3((Seitwärtsbew * nRichtungsvector.x) + (Vorwärtsbew * Normalenberechnung(nRichtungsvector).x), Sprunghöhe,
                    (Seitwärtsbew * nRichtungsvector.z) + (Vorwärtsbew * Normalenberechnung(nRichtungsvector).z)) * speed * Time.deltaTime;
            Sprunghöhe -= Sprunghöhe/10f;
            yield return new WaitForEndOfFrame();
        }
            
        StopCoroutine(Jump());
    }
        
}
