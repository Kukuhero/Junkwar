using UnityEngine;
using System.Collections;

public class ApfelEnemyController : MonoBehaviour {
    private Vector3 offset;
    private bool damage = false;
    public GameObject House;
    public GameObject KIDef;
    public GameObject Zahnrad;
    private bool inTriggerEnemy = false;
    private Animator anim;
    public Transform Spawnpoint_r;
    public Transform Spawnpoint_l;
    public GameObject Apfelkern;
    private bool inattack = false;
    public GameObject[] target = new GameObject[50];
    public int x = 0;
    public GameObject Mündungsfeuer;


    // Use this for initialization
    void Awake () 
    {
        gameObject.transform.parent.GetComponent<Wegfindung>().target = House.transform;

        anim = gameObject.transform.parent.GetComponent<Animator>() ;
    }

    // Update is called once per frame
    void Update ()
    {   
        if (damage)
        {
            anim.SetBool("shoot", true);
            while (target[0] == null && x > 0)
            {
                //print("inwhileArrayoverwrite");
                Arrayoverwrite(0);
                if (x <= 0)
                {
                    //print("attack = false");
                    damage = false;
                }
            }
        }
                
        else 
        {
            anim.SetBool("shoot", false);
            StopCoroutine(makeDamage());
            damage = false;
        }
        if (!damage)
        {
            anim.SetBool("walk", true);
        
            //print (gameObject.GetComponent<Wegfindung>().speed);
            gameObject.transform.parent.transform.rotation = Quaternion.RotateTowards 
            (gameObject.transform.parent.transform.rotation, 
                Quaternion.LookRotation(new Vector3(-(gameObject.transform.parent.transform.position.x - gameObject.transform.parent.GetComponent<Wegfindung>().currenttarget.position.x), 0f, -(gameObject.transform.parent.transform.position.z - gameObject.transform.parent.GetComponent<Wegfindung>().currenttarget.position.z))),
                100f * Time.deltaTime);
        }

     }


    void OnTriggerEnter (Collider other)
    {
        GameObject other2 = null;
        int anzahl = 0;
        switch (other.tag) 
        {
            case "KIDef":
                offset = other.gameObject.transform.position - gameObject.transform.parent.transform.position;
                gameObject.transform.parent.transform.rotation = Quaternion.LookRotation (offset);
                gameObject.transform.parent.GetComponent<Wegfindung>().target = other.gameObject.transform;
                damage = true;

                break;

            case "Haus":

                gameObject.transform.parent.GetComponent<Wegfindung>().enabled = false;
                damage = true;
                StartCoroutine(makeDamage());
                target[0] = House;

                break;

                /*case "plant":
            gameObject.GetComponent<Wegfindung>().target = other.gameObject.transform;

            break;*/

            case "Zahnrad":
                gameObject.transform.parent.GetComponent<Wegfindung> ().Wegfinden (other.gameObject);
                break;

            /*case "Held":
                if (other.gameObject.name != "NagelHeld 1" && !other.isTrigger)
                {
                    other2 = other.gameObject.transform.parent.gameObject;
                    //print(other2.gameObject.name.Clone().ToString() == "Enemy");

                    if (x <= 0)
                    {
                        //print("noch keiner gespeichert");
                        target[0] = other2.gameObject;
                        x = 1;
                    }


                    for (int i = 0; i < target.Length && other2.gameObject != target[i]; i++)
                    {
                        ////print("other2.gamObject:" + other2.gameObject);
                        ////print("target[i]:" + target[i]);
                        if (i + 1 == target.Length)
                        {
                            print("gamobject ist neu" + other2.gameObject);
                            target[x] = other2.gameObject;
                            x++; 
                        }

                    }
                }
                StartCoroutine (makeDamage ());
                damage = true;
                    break;*/

        }
    }

    void OnTriggerExit (Collider other)
    {
        switch (other.tag) 
        {
            case "KIDef":
                damage = false;
                break;

            case "Held":
                print(other.gameObject);
                int a = 0;
                while (a <= target.Length - 1)
                {
                    //print("whileirgendwas");
                    if (other.gameObject == target[a])
                    {
                        print("gekillt"+other.gameObject);
                        Arrayoverwrite(a);

                    }
                    a++;

                }


                break;

            case "Haus":
                break;
        }
    }




    IEnumerator makeDamage()
    {

        while (true) {
            /*if (gameObject.GetComponentInParent<Wegfindung>().target == null) {
                damage = false;
                anim.SetBool("shoot", false);
            } else {*/
                
                    
            Instantiate(Apfelkern, Spawnpoint_r.transform.position, transform.rotation,transform.parent);
                    Instantiate(Mündungsfeuer, Spawnpoint_r.transform.position, transform.rotation);
                    Instantiate(Apfelkern, Spawnpoint_l.transform.position, transform.rotation, transform.parent);
                    Instantiate(Mündungsfeuer, Spawnpoint_l.transform.position, transform.rotation);

           // }
            yield return new WaitForSeconds (0.2f);
        }
    }

    void Arrayoverwrite (int i)
    {
        x--;
        print("Arrayoverwrite" + x);
        while (i < target.Length-1) 
        {
            target [i] = target [i + 1];
            i++;
        }
        target[i] = null;    }

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
