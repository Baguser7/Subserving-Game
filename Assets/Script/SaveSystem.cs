using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public static class SaveSystem 
{
   public static void SavePlayer(DataHandler data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.rock";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData dataPlayer = new PlayerData(data);

        formatter.Serialize(stream, dataPlayer);
        stream.Close();
    }

    public static PlayerData LoadSave()
    {
        string path = Application.persistentDataPath + "/player.rock";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData dataPlayer = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return dataPlayer;
        }
        else
        {
            Debug.Log("File not found on" + path);
            return null;
        }
    }
}
