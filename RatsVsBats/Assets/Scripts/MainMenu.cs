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

    [Header("Delete")]
    [SerializeField] private GameObject confirmDelete;
    [SerializeField] private GameObject confirmDelete2;
    [SerializeField] private GameObject loadScroll;
    [SerializeField] private GameObject loadDelete;
    [SerializeField] private GameObject login;

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
        loadScroll.SetActive(false);
        loadDelete.SetActive(false);
        login.SetActive(true);
        gameObject.SetActive(false);
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
        }
        else
        {
            CursorManager.Instance.NotBlockBtn(loadGameBtn);
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

    public void LoadButton()
    {
        loadScroll.SetActive(true);
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
        loadScroll.SetActive(false);
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
