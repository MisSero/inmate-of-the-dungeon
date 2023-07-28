using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadData : MonoBehaviour
{
    private SaveData saveData;
    private Button resumeButton;
    private Player player;

    private string path;
    private void Start()
    {
        resumeButton = gameObject.GetComponent<Button>();
#if UNITY_ANDROID && !UNITY_EDITOR
        path = Path.Combine(Application.persistentDataPath, "Save.json");
#else
        path = Path.Combine(Application.dataPath, "Save.json");
#endif
        if (File.Exists(path))
            resumeButton.interactable = true;
        else
            resumeButton.interactable = false;

    }
    public void LoadGame()
    {
        if (File.Exists(path))
        {
            saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(path));

            ReferencesToObjects.Loaded = true;
            ReferencesToObjects.SaveData = saveData;
            ReferencesToObjects.Rand = new System.Random(saveData.RandSeed);
            ReferencesToObjects.RandSeed = saveData.RandSeed;
            ReferencesToObjects.LvlNumber = saveData.LvlNumber;
            // Для загрузки хп, атаки, лвл, экспы и скиллпоинтов используется методв в Player

            ReferencesToObjects.ObjectsKeeper.EnableObjects();//включение объектов
            SceneManager.LoadScene(saveData.LvlNumber);

            // 4 строчки на удаление из общегно списка скиллов и предметов и загрузку их дополнительных эффектов
            GetSkillController.receivedSkills = saveData.ReceivedSkills;
            GetSkillController.UpdateSkillsLoad();
            GetItemController.receivedCommonItems = saveData.ReceivedCommonItems;
            GetItemController.receivedRareItems = saveData.ReceivedRareItems;
            GetItemController.receivedMythicalItems = saveData.ReceivedMythicalItems;

            ReferencesToObjects.ItemController.UpdateItemsLoad();

            player = ReferencesToObjects.Player;

            // Предметы и их влияние
            HpPotion.hpMultiply = saveData.HpPotionMultiply;
            player.Vampirism = saveData.Vampirism;
            player.PhoenixFeather = saveData.PhoenixFeather;
            player.Hourglass = saveData.Hourglass;
            player.CrackedHourglass = saveData.CrackedHourglass;

            if (player is Warrior warrior)
            {
                warrior.Armor = saveData.Armor;
                warrior.FixedAttack = saveData.FixedAttack;
                warrior.Block = saveData.Block;
            }

        }
        else
            Debug.Log("net seiva");
    }
}
