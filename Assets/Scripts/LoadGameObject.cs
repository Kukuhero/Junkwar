﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class LoadGameObject : MonoBehaviour {
	int i = 0;
	public const string path = "GameObjectstats";
	// Use this for initialization
	void Start () {
        
		GameObjectContainer go = GameObjectContainer.Load (path);
		print (go.Gameobjects.Count);
		GameObject[] Gameobjects = new GameObject[go.Gameobjects.Count];
		foreach (Gameobject Gameobject in go.Gameobjects) {
			Gameobjects [i] = new GameObject();
			Mesh holderMesh = new Mesh();
			ObjImporter newMesh = new ObjImporter();//print ("C:"+Application.dataPath + Gameobject.path);
            holderMesh = newMesh.ImportFile(Application.dataPath+Gameobject.path);

			Gameobjects[i].AddComponent<MeshRenderer>();
			Gameobjects[i].AddComponent<MeshFilter>();

			Gameobjects[i].GetComponent<MeshFilter>().mesh = holderMesh;
			print (Gameobject.Name);
			Gameobjects [i].name = Gameobject.Name;
            if (Gameobject.type == "Held")
            {
                Gameobjects[i].AddComponent<HeldController>();
                Gameobjects[i].GetComponent<HeldController>().speed = Gameobject.speed;
            }
            if (Gameobject.type == "Minion")
            {
                Gameobjects[i].AddComponent<Wegfindung>();
                Gameobjects[i].GetComponent<Wegfindung>().speed = Gameobject.speed;
            }
			Gameobjects [i].AddComponent<Health> ();
			Gameobjects [i].GetComponent<Health> ().health = Gameobject.Health;
			Gameobjects [i].AddComponent<Animator> ();
			Gameobjects [i].AddComponent<BoxCollider> ();
            Gameobjects [i].GetComponent<BoxCollider> ().size = new Vector3(Gameobject.collidersizex,Gameobject.collidersizey,Gameobject.collidersizez);
			//Gameobjects [i].GetComponent<BoxCollider> ().size.y = Gameobject.collidersizey;
			//Gameobjects [i].GetComponent<BoxCollider> ().size.z = Gameobject.collidersizez;
            Gameobjects [i].GetComponent<BoxCollider> ().center = new Vector3(Gameobject.colliderposx,Gameobject.colliderposy,Gameobject.colliderposz);
			//Gameobjects [i].GetComponent<BoxCollider> ().center.y = Gameobject.colliderposy;
			//Gameobjects [i].GetComponent<BoxCollider> ().center.z = Gameobject.colliderposz;
			print (Gameobject.Name);
			//Instantiate (Gameobjects [i]);
            Gameobjects[i].SetActive(false);
		}

	}

	// Update is called once per frame
	void Update () {
	
	}
}
