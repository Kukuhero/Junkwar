using System.Xml;
using System.Xml.Serialization;

public class Gameobject  {

    [XmlAttribute("name")]
    public string Name;

    public int Health;
}
