using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

/// <summary>
/// this solution is not good for larger data
/// </summary>
[System.Serializable]
public class GameData
{
    public int Xp;
    public int Money;

    public List<int> BoughtItemsIds;

    #region untility

    public static GameData I;

    static string DataPath = Application.persistentDataPath + "GD.prog";
    public static void Save()
    {
        var formatter = new BinaryFormatter();
        var stream = new FileStream(DataPath, FileMode.Create);
        formatter.Serialize(stream, I);
        stream.Close();
    }
    public static void Load()
    {
        if (File.Exists(DataPath))
        {
            var formatter = new BinaryFormatter();
            var stream = new FileStream(DataPath, FileMode.Open);

            var data = formatter.Deserialize(stream) as GameData;
            stream.Close();

            I = data;
        }
        else
        {
            FirstEnter();
        }
    }

    public static void Delete()
    {
        if (File.Exists(DataPath))
        {
            File.Delete(DataPath);
            Debug.Log("data deleted successfully");
        }
        else
        {
            Debug.Log("data already deleted");
        }
    }

    static void FirstEnter()
    {
        I = new GameData();
        I.setDefaults();
        Save();
    }

    void setDefaults()
    {
        Money = 9999;
        BoughtItemsIds = new List<int>();
    }

    #endregion
}