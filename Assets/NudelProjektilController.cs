using UnityEngine;
using System.Collections;

public class NudelProjektilController : MonoBehaviour {
    private GameObject target;
    private Vector3 offset;
    public float steigung;

	// Use this for initialization
	void Start () 
    {
        target = transform.parent.GetComponent<MacaroniArtillerieController>().target[0].transform.parent.gameObject;
        offset = transform.position - target.transform.position;
        steigung =(Vectorlaenge(Vectorberechnung(transform.position, target.transform.position))/4f);
        StartCoroutine(Destroy());
        transform.rotation = Quaternion.LookRotation(new Vector3(-offset.x, steigung, -offset.z));
	}
	
	// Update is called once per frame
	void Update () 
    {
       // print(steigung);
        transform.position += new Vector3(-offset.x,steigung,-offset.z)*Time.deltaTime;
        //if (Vectorlaenge(offset) > 0.5f && Vectorlaenge(offset) < 3f)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(-offset.x, steigung, -offset.z));
        }
        steigung -= 2f;
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

    IEnumerator Destroy()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            Destroy(gameObject);
        }
    }
}
