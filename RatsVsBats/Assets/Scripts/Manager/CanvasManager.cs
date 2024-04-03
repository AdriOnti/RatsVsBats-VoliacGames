using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    private static CanvasManager instance;
    public static CanvasManager Instance
    {
        get { return instance; }
    }

    private InputManager inputManager;

    [SerializeField] public bool mapInput;
    [SerializeField] public bool pauseInput;

    // Canvas
    GameObject pauseMenu;
    GameObject map;

    private void Awake()
    {
        if (instance != null && instance != this) Destroy(gameObject);
        else instance = this;
    }

    private void Start()
    {
        inputManager = InputManager.Instance;
        CallToGameManager();
        SetActiveCanvas();
    }

    private void OnEnable()
    {
        InputManager.PlayerMap += OpenCloseMap;
        InputManager.PlayerPause += PauseGame;
    }

    private void OnDisable()
    {
        InputManager.PlayerMap -= OpenCloseMap;
        InputManager.PlayerPause -= PauseGame;
    }

    private void CallToGameManager()
    {
        pauseMenu = GameManager.Instance.GetPauseMenu();
        map = GameManager.Instance.GetMap();
    }

    private void SetActiveCanvas()
    {
        // SET ACTIVE: TRUE
        GameManager.Instance.GetCanvasFather().SetActive(true);

        // SET ACTIVE: FALSE
        pauseMenu.SetActive(false);
        map.SetActive(false);
    }

    public void OpenCloseMap()
    {
        mapInput = !mapInput;
        if(mapInput && !pauseInput) map.SetActive(true); 
        else map.SetActive(false);
    }

    public void PauseGame()
    {
        pauseInput = !pauseInput;
        if (pauseInput && !mapInput) { pauseMenu.SetActive(true); Time.timeScale = 0f; }
        else { pauseMenu.SetActive(false); Time.timeScale = 1f; }
    }
}
