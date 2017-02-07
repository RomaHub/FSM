using UnityEngine;

[System.Serializable]
public class MobNames
{

	public string[] names = {"NoName"};

	public string GetRandomName()
	{
		int random = Random.Range(0, names.Length-1);

		return names[random];
	}

}
