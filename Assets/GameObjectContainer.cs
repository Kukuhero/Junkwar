using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using UnityEngine;
using System.Collections;


[XmlRoot("GameObjectCollection")]
public class GameObjectContainer {

    [XmlArray("GameObjects")]
	[XmlArrayItem("Gameobject")]
	public List<Gameobject> Gameobjects = new List<Gameobject> ();

	public static GameObjectContainer Load(string path){
		TextAsset _xml = Resources.Load<TextAsset> (path);
	
		XmlSerializer serializer = new XmlSerializer (typeof(GameObjectContainer));

		StringReader reader = new StringReader (_xml.text);

		GameObjectContainer Gameobjects = serializer.Deserialize (reader) as GameObjectContainer;

		reader.Close();
	
		return Gameobjects;
	}


}