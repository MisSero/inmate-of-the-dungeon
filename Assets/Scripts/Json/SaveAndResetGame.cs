using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveAndResetGame : MonoBehaviour
{
    private SaveData saveData;
    private string path;


    private void Start()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        path = Path.Combine(Application.persistentDataPath, "Save.json");
#else
        path = Path.Combine(Application.dataPath, "Save.json");
#endif
        saveData = new SaveData();
    }

    public void SaveGame() // вызывается в levelGenerator
    {
        saveData = ReferencesToObjects.SaveData;
        saveData.SetData();

        File.WriteAllText(path, JsonUtility.ToJson(saveData));
    }
    public void ResetGame()
    {
        if (File.Exists(path))
        {
            File.Delete(path);
        }
        else
            Debug.Log("Нет Сейва(reset)");
    }
}
