using UnityEngine;
using System.Collections;

public class Feinderkennung : MonoBehaviour {

    private bool attack;
    public static GameObject[] target = new GameObject[10];
    public static int x = 0;
    public int Damage;
    public float splash;


    // Use this for initialization
    void Start () 
    {
    }

    // Update is called once per frame
    void Update () {
        attack = gameObject.GetComponent<HeldController>().attack;
        if (attack) 
        {
            while (target [0] == null && attack) 
            {
                Arrayoverwrite (0);
                if (x <= 0) 
                {
                    attack = false;
                }
            }

        }
        if (attack) 
        {
            makeDamage(); 
        }
        else 
        {
            attack = false;
        }

    }

    void OnTriggerEnter(Collider other)
    {
        switch (other.tag) {
            case "Enemy":
                gameObject.GetComponent<HeldController>().attack = true;

                target [x] = other.gameObject;
                x++;


                break;
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
    void makeDamage()
    {
        print("makeDamage IEnumerator");
       new WaitForSeconds(2f);
        for (int i = 0; i < x; i++)
        {
            print("informakeDamage");
            Vector3 Centerbox = new Vector3 (gameObject.GetComponent<BoxCollider>().center.x,0f,gameObject.GetComponent<BoxCollider>().center.z);
            float Abstand = Vectorlaenge(Vectorberechnung(target[i].transform.position, Centerbox))+0.1f;
            target[i].GetComponent<Health>().health -= Mathf.FloorToInt(Damage*splash/Abstand);
            print(Mathf.FloorToInt(Damage*splash/Abstand));
        }
    }
    
        

    void Arrayoverwrite (int i)
{
        x--;
        while (i < target.Length-1) 
        {
            target [i] = target [i + 1];
            i++;
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
 

