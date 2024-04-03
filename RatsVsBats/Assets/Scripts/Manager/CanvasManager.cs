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

    [SerializeField, /*HideInInspector*/] GameObject inventoryBtn;
    [SerializeField, /*HideInInspector*/] GameObject inventory;
    [/*SerializeField,*/ HideInInspector] bool inventoryOpened;

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
        InputManager.PlayerInventory += OpenInventory;
    }

    private void OnDisable()
    {
        InputManager.PlayerMap -= OpenCloseMap;
        InputManager.PlayerPause -= PauseGame;
        InputManager.PlayerInventory -= OpenInventory;
    }

    private void CallToGameManager()
    {
        pauseMenu = GameManager.Instance.GetPauseMenu();
        map = GameManager.Instance.GetMap();
        inventory = GameManager.Instance.GetInventory();
        inventoryBtn = GameManager.Instance.GetInventoryBtn();
    }

    private void SetActiveCanvas()
    {
        // SET ACTIVE: TRUE
        GameManager.Instance.GetCanvasFather().SetActive(true);

        // SET ACTIVE: FALSE
        pauseMenu.SetActive(false);
        map.SetActive(false);
        inventoryBtn.SetActive(true);
        inventory.SetActive(false);
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

    public void OpenInventory()
    {
        if (inventoryOpened == true) { CloseInventory(); return; }
        if (inventoryOpened == false)
        {
            inventoryOpened = true;
            inventoryBtn.SetActive(false);
            inventory.SetActive(true);
            InventoryManager.Instance.ListItems();
        }

    }

    public void CloseInventory()
    {
        inventoryBtn.SetActive(true);
        inventory.SetActive(false); 
        inventoryOpened = false;
    }
}
