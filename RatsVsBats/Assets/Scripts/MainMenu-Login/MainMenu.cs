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
    [SerializeField] private GameObject mainMenu;

    [Header("Delete")]
    [SerializeField] private GameObject confirmDelete;
    [SerializeField] private GameObject confirmDelete2;

    [Header("Load")]
    [SerializeField] private GameObject loadScroll;
    [SerializeField] private GameObject loadDelete;

    [Header("Login")]
    [SerializeField] private GameObject login;

    [Header("Account")]
    public GameObject accountSettings;

    // AWAKE
    private void Awake()
    {
        if (instance != null && instance != this) Destroy(gameObject);
        else instance = this;
    }

    // START
    private void Start()
    {
        accountSettings.SetActive(false);
        confirmDelete.SetActive(false);
        confirmDelete2.SetActive(false);
        loadScroll.SetActive(false);
        loadDelete.SetActive(false);
        //login.SetActive(true);
        //mainMenu.SetActive(false);
        CheckLoad();
    }

    /// <summary>
    /// If a saved game do not exits, block the load button and delete button
    /// </summary>
    private void CheckLoad()
    {
        if (!DataManager.Instance.SaveExists()) CursorManager.Instance.BlockBtn(loadGameBtn);
        else CursorManager.Instance.NotBlockBtn(loadGameBtn);
    }

    /// <summary>
    /// Load the game with a especific parameters
    /// </summary>
    /// <param name="isNewGame">This bool decides if is a new Game or a previous game</param>
    public void Game(bool isNewGame)
    {
        CursorManager.Instance.ResetCursor();
        if (isNewGame) PlayerPrefs.SetInt("loading", 0);
        else PlayerPrefs.SetInt("loading", 1);
        PlayerPrefs.SetInt("profileID", Login.instance.idUser);
        PlayerPrefs.SetString("email", Login.instance.email.text);
        SceneManager.LoadScene(1);
    }

    public void LoadButton() { loadScroll.SetActive(!loadScroll.activeSelf); }

    /// <summary>
    /// Delete the saved game
    /// </summary>
    public void DeleteGame() { confirmDelete.SetActive(true); }

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

    public void EnableAccountSettings() { accountSettings.SetActive(true); }

    public void CloseLoadScroll()
    {
        loadScroll.SetActive(false);
        CursorManager.Instance.ResetCursor();
    }
}
