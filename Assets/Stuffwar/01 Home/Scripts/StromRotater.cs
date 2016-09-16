using UnityEngine;
using System.Collections;

public class StromRotater : MonoBehaviour {
	
	void Start ()
	{
		StartCoroutine (Autodestroy ());
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.Rotate(new Vector3(0,0,30)* Time.deltaTime);
	}

	IEnumerator Autodestroy()
	{
		yield return new WaitForSeconds (15);
		Destroy (gameObject);

		if (Batterie.maxStrom >= Userinterface.Strom + 10) 
		{
			Userinterface.Strom += 10;
		} else {
			Userinterface.Strom = Batterie.maxStrom;

		}
	}

}
