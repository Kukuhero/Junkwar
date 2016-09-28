using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using UnityEngine;
using System.Collections;


[XmlRoot("GameObjectCollection")]
public class GameObjectInitalisierer {

    [XmlArray("GamObjects"),XmlArrayItem("GameObject")]
    public Gameobject[] Gameobjects;

    public void Save(string path)
    {
        var serializer = new XmlSerializer(typeof(GameObjectInitalisierer));
        using (var stream = new FileStream(path, FileMode.Create))
        {
            serializer.Serialize(stream, this);
        }

    }

    public static GameObjectInitalisierer Load(string path)
    {
        var serializer = new XmlSerializer(typeof(GameObjectInitalisierer));
        using (var stream = new FileStream(path, FileMode.Open))
        {
            
            return serializer.Deserialize(stream) as GameObjectInitalisierer;
        }
    }

                public static GameObjectInitalisierer LoadFromText(string text)
    {
        var serializer = new XmlSerializer(typeof(GameObjectInitalisierer));
        return serializer.Deserialize(new StringReader(text)) as GameObjectInitalisierer;
    }

}
