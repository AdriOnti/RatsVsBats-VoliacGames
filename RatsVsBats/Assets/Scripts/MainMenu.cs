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

    // AWAKE
    private void Awake()
    {
        if (instance != null && instance != this) Destroy(gameObject);
        else instance = this;
    }

    private void Start()
    {
        CheckLoad();
    }

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

    public void NewGame()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        PlayerPrefs.SetInt("loading", 0);
        SceneManager.LoadScene(1);
    }

    public void LoadGame()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        PlayerPrefs.SetInt("loading", 1);
        SceneManager.LoadScene(1);
    }

    public void DeleteGame()
    {
        DataManager.Instance.ConfirmDelete();
        CheckLoad();
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
