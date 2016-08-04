using UnityEngine;
using System.Collections;

public class Batterie : MonoBehaviour {
	private bool upgrade = false;
	private int Level = 1;
	public static int maxStrom;

	// Use this for initialization
	void Start () {
		maxStrom = 100;
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (upgrade) 
		{
			Level++;

		}

	}
}
