using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class LoadGameObject : MonoBehaviour {

	// Use this for initialization
	void Start () {



        var GameObjectCollection = GameObjectInitalisierer.Load(Path.Combine(Application.dataPath, "GameObjectstats.xml"));

        var xmlData = @"<GameObjectCollection><GameObjects><GameObject name= ""a""><Health>5</Health></GameObject></GameObjects></GameObjectCollection>";
        GameObjectCollection = GameObjectInitalisierer.LoadFromText(xmlData);
        print(GameObjectCollection);

        GameObjectCollection.Save(Path.Combine(Application.persistentDataPath, "GameObjectstats.xml"));


	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
