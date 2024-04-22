using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // SINGLETON
    private static MainMenu instance;
    public static MainMenu Instance
    {
        get { return instance; }
    }

    [SerializeField] private GameObject loadGameBtn;
    [SerializeField] private GameObject deleteGameBtn;

    [Header("Delete")]
    [SerializeField] private GameObject confirmDelete;
    [SerializeField] private GameObject confirmDelete2;

    // AWAKE
    private void Awake()
    {
        if (instance != null && instance != this) Destroy(gameObject);
        else instance = this;
    }

    // START
    private void Start()
    {
        confirmDelete.SetActive(false);
        confirmDelete2.SetActive(false);
        CheckLoad();
    }

    /// <summary>
    /// If a saved game do not exits, block the load button and delete button
    /// </summary>
    private void CheckLoad()
    {
        if (!DataManager.Instance.SaveExists())
        {
            CursorManager.Instance.BlockBtn(loadGameBtn);
            CursorManager.Instance.BlockBtn(deleteGameBtn);
        }
        else
        {
            CursorManager.Instance.NotBlockBtn(loadGameBtn);
            CursorManager.Instance.NotBlockBtn(deleteGameBtn);
        }
    }

    /// <summary>
    /// Start a new game
    /// </summary>
    public void NewGame()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        PlayerPrefs.SetInt("loading", 0);
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Load a previous game
    /// </summary>
    public void LoadGame()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        PlayerPrefs.SetInt("loading", 1);
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Delete the saved game
    /// </summary>
    public void DeleteGame()
    {
        confirmDelete.SetActive(true);
    }

    public void ConfirmDelete()
    {
        DataManager.Instance.ConfirmDelete();
        CheckLoad();
    }

    /// <summary>
    /// Quit the game
    /// </summary>
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
