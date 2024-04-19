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
        if (!DataManager.Instance.SaveExists()) CursorManager.Instance.BlockBtn(loadGameBtn);
        else CursorManager.Instance.NotBlockBtn(loadGameBtn);
    }

    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadGame()
    {

    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
