using System.IO;
using System.Xml.Serialization;

public class XMLSerializer : ISerializer
{

    public void Serialize(string path, GameData gameData)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(GameData));
        FileStream stream = new FileStream(path, FileMode.Create);
        serializer.Serialize(stream, gameData);
        stream.Close();
    }

	public T Deserialize<T>(string path)
	{

		XmlSerializer serializer = new XmlSerializer(typeof(T));
		FileStream stream = new FileStream(path, FileMode.Open);
		T data = (T)serializer.Deserialize(stream);
		stream.Close();
		return data;

	}

}
