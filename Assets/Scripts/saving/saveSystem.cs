using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public static class saveSystem
{
	/// <summary>
	/// will save the player data in a fill in appdata
	/// </summary>
	/// <param name="playerData">the public data from the playerData script</param>
	public static void saveData(playerStats playerData) 
	{
		BinaryFormatter formatter = new BinaryFormatter();
		string path = Application.persistentDataPath + "/player.rsu";

		FileStream stream = new FileStream(path, FileMode.Create);

		playerData data = new playerData(playerData);
		formatter.Serialize(stream, data);
		stream.Close();
	}

	/// <summary>
	/// will load the okayer data and return a playerData class with the loaded data
	/// </summary>
	/// <returns></returns>
	public static playerData loadPlayer()
	{
		string path = Application.persistentDataPath + "/player.rsu";
		if (File.Exists(path))
		{
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream stream = new FileStream(path, FileMode.Open);

			playerData data = formatter.Deserialize(stream) as playerData;

			stream.Close();

			return data;
		}
		else
		{
			Debug.LogError("File not found");
			return null;
		}
	}
}
