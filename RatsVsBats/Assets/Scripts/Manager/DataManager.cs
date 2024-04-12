using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    // SINGLETON
    private static DataManager instance;
    public static DataManager Instance
    {
        get { return instance; }
    }

    // AWAKE
    private void Awake()
    {
        if (instance != null && instance != this) Destroy(gameObject);
        else instance = this;

        // Descomentar esta linea para testing o para el MainMeu
        //if (SaveExists()) LoadGame();
    }

    private bool SaveExists() { return File.Exists(GetPersistentPath() + "/data.json"); }

    private string GetPersistentPath() { return Path.Combine(Application.persistentDataPath, "Data"); }

    public void SaveGame()
    {
        CreatePersistance();
        PlayerData data = new PlayerData(PlayerController.Instance);
        string path = GetPersistentPath() + "/data.json";
        File.WriteAllText(path, JsonUtility.ToJson(data));
    }

    public void LoadGame()
    {
        string path = GetPersistentPath() + "/data.json";
        string playerData = File.ReadAllText(path);
        PlayerData data = JsonUtility.FromJson<PlayerData>(playerData);

        PlayerController.Instance.transform.position = data.position;

        InventoryManager.Instance.Items.Clear();
        foreach (Item item in data.inventory) InventoryManager.Instance.Add(item);

        if (CanvasManager.Instance.pauseInput) CanvasManager.Instance.PauseGame();
    }

    public void CreatePersistance()
    {
        string source = Path.Combine(Application.streamingAssetsPath, "Data");
        string target = GetPersistentPath();

        if(!Directory.Exists(Application.persistentDataPath)) Directory.CreateDirectory(Application.persistentDataPath);
        if(!Directory.Exists(target)) 
        { 
            Directory.CreateDirectory(target);
            string[] filesS = Directory.GetFiles(source);

            foreach (string file in filesS)
            {
                string targetFile = Path.Combine(target, Path.GetFileName(file));
                File.Copy(file, targetFile);
            }
        }

        string[] files = Directory.GetFiles(source);
        foreach(string file in files)
        {
            string fileName = Path.GetFileName(file);
            string targetFile = Path.Combine(target, fileName);

            if (!File.Exists(targetFile)) File.Copy(file, targetFile);
        }
    }

    public void DeleteSave()
    {
        if(SaveExists()) File.Delete(GetPersistentPath() + "/save.json");
    }
}
