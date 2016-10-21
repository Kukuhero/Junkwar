using UnityEngine;
using System.Collections;

public class Feinderkennung : MonoBehaviour {

    private bool attack;
    public static GameObject[] target = new GameObject[50];
    public static int x = 0;
    public int Damage;
    public float splash;
    private bool inattack = false;


    // Use this for initialization
    void Start () 
    {
    }

    // Update is called once per frame
    void Update () {
        print(x);
        attack = gameObject.GetComponent<HeldController>().attack;
        if (attack) 
        {
            while (target [0] == null && x > 0) 
            {
                //print("inwhileArrayoverwrite");
                Arrayoverwrite (0);
                if (x <= 0) 
                {
                    //print("attack = false");
                    attack = false;
                }
            }

        }
        if (attack && !inattack) 
        {
            //print("anzahl Coroutinestart");
            inattack = true;
            StartCoroutine(makeDamage());
        }
        else 
        {
            attack = false;
        }

    }

    void OnTriggerEnter(Collider other)
    {
       GameObject other2 = null;
       int anzahl = 0;
       switch (other.tag) {
            case "Enemy":
                print(other.gameObject.name.Clone().ToString());
                if (other.gameObject.name.Clone().ToString() != "Enemy(Clone)" && !other.isTrigger)
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
                break;
        }
    }

    void OnTriggerExit(Collider other)
    {
        switch (other.tag) 
        {
            case "Enemy":
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
        }
    }
    IEnumerator makeDamage()
    {
        
        //print("makeDamage IEnumerator");
        yield return new WaitForSeconds(0.9f);
        //print("vor formakeDamage" + x);
        for (int i = 0; i < x; i++)
        {
            //print(target[i].gameObject);
            //print("informakeDamage");
            Vector3 Centerbox = new Vector3 (gameObject.GetComponent<BoxCollider>().center.x,0f,gameObject.GetComponent<BoxCollider>().center.z);
            float Abstand = Vectorlaenge(Vectorberechnung(target[i].transform.position, Centerbox))+0.1f;
            target[i].GetComponent<Health>().health -= Mathf.FloorToInt(Damage*splash/Abstand);
            //print(Mathf.FloorToInt(Damage*splash/Abstand));
        }
        yield return new WaitForSeconds(1);
        inattack = false;
        StopCoroutine(makeDamage());
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
 

