using System.Xml.Serialization;

[System.Serializable]
public class MobData
{

    [XmlAttribute("Name")]
    public string mobName;
    [XmlElement("Health")]
    public int health;
    [XmlElement("PosX")]
    public float xPos;
    [XmlElement("PosY")]
    public float yPos;
    [XmlElement("PosZ")]
    public float zPos;
    [XmlElement("RotY")]
    public float yRot;
    [XmlElement("CurrentState")]
    public StateSerializer state;

}
