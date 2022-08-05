using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static PlayerController player;
    public static void SavePlayer (PlayerController player)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/playerp.save";
        FileStream stream = new FileStream(path, FileMode.Create);
        PlayerData data = new PlayerData(player);

        formatter.Serialize(stream, data);
        Debug.Log("—Œ’–¿Õ≈ÕŒ!");
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/playerp.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            SceneTransition.SwitchToSceneByIndex(data.level);
            stream.Close();
            Debug.Log("—À”◊»À—ﬂ œ≈–¬€… œ≈–≈’Œƒ!");
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

    public static PlayerData LoadDataPlayer()
    {
        string path = Application.persistentDataPath + "/playerp.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            Debug.Log("«¿√–”«»À»—‹ ƒ¿ÕÕ€≈ »√–Œ ¿!");
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

    public static void NewGame (PlayerController player)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/playerp.save";
        FileStream stream = new FileStream(path, FileMode.Create);
        PlayerData data = new PlayerData(player);
        data.level = player.scene.buildIndex;
        data.health = 100;
        Vector3 positionNewGame;
        positionNewGame.x = 212;
        positionNewGame.y = 290;
        positionNewGame.z = 0;
        data.postition[0] = positionNewGame.x;
        data.postition[1] = positionNewGame.y;
        data.postition[2] = positionNewGame.z;
        formatter.Serialize(stream, data);
        stream.Close();
        Debug.Log("NEW GAME!");
    }

}
