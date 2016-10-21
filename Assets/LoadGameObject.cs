using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class LoadGameObject : MonoBehaviour {

	public const string path = "GameObjectstats";
	// Use this for initialization
	void Start () {

		GameObjectContainer go = GameObjectContainer.Load (path);
		print (go.Gameobjects.Count);
		foreach (Gameobject Gameobject in go.Gameobjects) {
			print (Gameobject.Name);
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
