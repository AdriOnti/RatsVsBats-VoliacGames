using System;
using System.IO;
using System.Threading.Tasks;
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

    public int profileId;

    // AWAKE
    private void Awake()
    {
        if (instance != null && instance != this) Destroy(gameObject);
        else instance = this;
    }

    // START
    private void Start()
    {
        // Get the profile Id for make UPDATE
        if (PlayerPrefs.GetInt("profileID") > 0) profileId = PlayerPrefs.GetInt("profileID");

        // LoadGame
        if (SaveExists() && IsNotMainMenu() && PlayerPrefs.GetInt("loading") > 0) LoadGame();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public bool IsNotMainMenu()
    {
        return SceneManager.GetActiveScene().buildIndex != 0;
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
        if(CanvasManager.Instance.pauseInput) CanvasManager.Instance.Saving();
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
        PlayerData data = ProcessJSON<PlayerData>(playerData);

        // PlayerController
        PlayerController.Instance.hp = data.maxHP;
        PlayerController.Instance.currentHP = data.currentHP;
        PlayerController.Instance.jumpForce = data.jumpForce;
        PlayerController.Instance.healingForce = data.healingForce;
        PlayerController.Instance.transform.position = data.position;
        PlayerController.Instance.originalSpeed = data.speed;
        PlayerController.Instance.speed = data.speed;

        // Missions
        GameManager.Instance.missionsCompleted = data.missionsCompleted;
        MissionManager.instance.missions = data.missions;

        // Inventory
        InventoryManager.Instance.Items.Clear();
        InventoryManager.Instance.Items = data.items;

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
        if(IsNotMainMenu()) CanvasManager.Instance.NotLoad();
    }

    /// <summary>
    /// Delete the keys on the PlayerPrefs
    /// </summary>
    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey("loading");
        PlayerPrefs.DeleteKey("profileID");
        PlayerPrefs.DeleteKey("back");
        PlayerPrefs.DeleteKey("email");
    }

    public async Task UpdateProfile(int points)
    {
        try
        {
            await APIManager.instance.UpdateProfileAsync(profileId, 1, points);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error updating profile: {ex.Message}");
        }
    }

    /// <summary>
    /// Deserialize any type of JSON and returned like a class in the game
    /// </summary>
    /// <param name="json">The info in a string</param>
    /// <returns>A instance of the class selected</returns>
    public T ProcessJSON<T>(string json)
    {
        return JsonUtility.FromJson<T>(json);
    }
}
