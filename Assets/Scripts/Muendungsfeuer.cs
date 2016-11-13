using UnityEngine;
using System.Collections;

public class Muendungsfeuer : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(destroy());
	}
	
    IEnumerator destroy ()
    {
        yield return new WaitForSeconds(4.2f);
        Destroy(gameObject);
    }
}
