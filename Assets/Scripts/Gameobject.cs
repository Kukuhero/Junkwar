using System.Xml;
using System.Xml.Serialization;

public class Gameobject  {

	[XmlAttribute("name")]
    public string Name;
	[XmlElement("damage")]
	public int damage;
	[XmlElement("health")]
    public int Health;
	[XmlElement("splash")]
	public int splash;
	[XmlElement("collidersizex")]
	public int collidersizex;
	[XmlElement("collidersizey")]
	public int collidersizey;
	[XmlElement("collidersizez")]
	public int collidersizez;
	[XmlElement("colliderposx")]
	public int colliderposx;
	[XmlElement("colliderposy")]
	public int colliderposy;
	[XmlElement("colliderposz")]
	public int colliderposz;
	[XmlElement("loot")]
	public int loot;
	[XmlElement("speed")]
	public int speed;
	[XmlElement("path")]
	public string path;
    [XmlElement("script")]
    public string script;
    [XmlElement("type")]
    public string type;
}
