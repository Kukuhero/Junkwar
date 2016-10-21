using System.Xml;
using System.Xml.Serialization;

public class Gameobject  {

	[XmlAttribute("name")]
    public string Name;
	[XmlElement("damage")]
	public int damage;
	[XmlElement("health")]
    public int Health;
}
