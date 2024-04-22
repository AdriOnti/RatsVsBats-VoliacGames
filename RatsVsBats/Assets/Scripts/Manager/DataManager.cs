using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    // SINGLETON
    private static DataManager instance;
    public static DataManager Instance
    {
        get { return instance; }
    }

    public List<GameObject> itemsInventory;

    // AWAKE
    private void Awake()
    {
        if (instance != null && instance != this) Destroy(gameObject);
        else instance = this;
    }

    private void Start()
    {
        if (SaveExists() && SceneManager.GetActiveScene().buildIndex != 0 /*&& PlayerPrefs.GetInt("loading") > 0*/)
        {
            LoadGame();
        }
    }

    public bool SaveExists() { return File.Exists(GetPersistentPath() + "/data.json"); }

    /// <summary>
    /// Get to the other functions and methods the persistent data path
    /// </summary>
    /// <returns>The persistent data path: Appdata/LocalLow/Company</returns>
    private string GetPersistentPath() { return Path.Combine(Application.persistentDataPath, "Data"); }

    /// <summary>
    /// Save the actual game
    /// </summary>
    public void SaveGame()
    {
        CanvasManager.Instance.Saving();
        CreatePersistance();
        PlayerData data = new PlayerData(PlayerController.Instance);
        string path = GetPersistentPath() + "/data.json";
        File.WriteAllText(path, JsonUtility.ToJson(data));
        CanvasManager.Instance.NotLoad();
    }

    /// <summary>
    /// Load the saved game
    /// </summary>
    public void LoadGame()
    {
        string path = GetPersistentPath() + "/data.json";
        string playerData = File.ReadAllText(path);
        PlayerData data = JsonUtility.FromJson<PlayerData>(playerData);

        PlayerController.Instance.transform.position = data.position;
        PlayerController.Instance.originalSpeed = data.speed;
        PlayerController.Instance.speed = data.speed;

        InventoryManager.Instance.Items.Clear();
        itemsInventory.Clear();

        foreach(Transform go in data.inventory)
        {
            go.GetComponent<ItemPickup>().Collected();
        }

        if (CanvasManager.Instance.pauseInput) CanvasManager.Instance.PauseGame();
    }

    /// <summary>
    /// Creates the persistance files
    /// </summary>
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

    /// <summary>
    /// If the save exists, the user can delete its save
    /// </summary>
    public void DeleteSave()
    {
        if (SaveExists()) CanvasManager.Instance.ConfirmDelete();
        else CanvasManager.Instance.NotConfirmDelete();
    }

    /// <summary>
    /// Deletes the saved game
    /// </summary>
    public void ConfirmDelete()
    { 
        File.Delete(GetPersistentPath() + "/data.json");
        if(SceneManager.GetActiveScene().buildIndex != 0) CanvasManager.Instance.NotLoad();
    }
}