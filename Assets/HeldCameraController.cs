using UnityEngine;
using System.Collections;

public class HeldCameraController : MonoBehaviour {
    public GameObject Held;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        transform.position += new Vector3( Held.transform.position.x - transform.position.x,0f,Held.transform.position.z - transform.position.z-10);
        transform.rotation = Quaternion.Euler(30f,Held.transform.rotation.y*20,0f);
	}
}
