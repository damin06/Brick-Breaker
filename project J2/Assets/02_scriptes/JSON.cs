using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSON : MonoBehaviour
{
    public Data playerData;

    [ContextMenu("To Json Data")]
    public void SavePlayerDataToJson()
    {
        string JsonData = JsonUtility.ToJson(playerData, true);
        string path = Path.Combine(Application.dataPath, "PlayerData.json");

        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(JsonData);
        string code = System.Convert.ToBase64String(bytes);

        File.WriteAllText(path, code);
        Debug.Log(code);
    }

    [ContextMenu("Load Json Data")]
    public void LoadPlayerDataToJson()
    {
        string path = Path.Combine(Application.dataPath, "PlayerData.json");
        string jsonData = File.ReadAllText(path);

        byte[] bytes = System.Convert.FromBase64String(jsonData);
        string jdata = System.Text.Encoding.UTF8.GetString(bytes);
        playerData = JsonUtility.FromJson<Data>(jdata);
    }
}

[System.Serializable]
public class Data
{
    public int bestscore;
    public float sen;
    public float voluem;
}
