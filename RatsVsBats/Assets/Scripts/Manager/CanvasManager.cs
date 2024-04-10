using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    // Instance
    private static CanvasManager instance;
    public static CanvasManager Instance
    {
        get { return instance; }
    }

    // InputManager
    private InputManager inputManager;

    // Bools
    [SerializeField] private bool mapInput;
    [SerializeField] public bool pauseInput;
    [SerializeField] private bool inventoryOpened;

    // Canvas
    GameObject pauseMenu;
    GameObject map;

    // Inventory
    [HideInInspector] GameObject inventoryBtn;
    [HideInInspector] GameObject inventory;

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

    /// <summary>
    /// Llama al GameManager para obtener distintos GameObjects
    /// </summary>
    private void CallToGameManager()
    {
        pauseMenu = GameManager.Instance.GetPauseMenu();
        map = GameManager.Instance.GetMap();
        inventory = GameManager.Instance.GetInventory();
        inventoryBtn = GameManager.Instance.GetInventoryBtn();
    }

    /// <summary>
    /// Activa y desactiva diversos elementos que necesitan cargarse en el principio del juego
    /// </summary>
    private void SetActiveCanvas()
    {
        // SET ACTIVE: TRUE
        GameManager.Instance.GetCanvasFather().SetActive(true);
        inventoryBtn.SetActive(true);
        inventory.transform.parent.gameObject.SetActive(true);

        // SET ACTIVE: FALSE
        pauseMenu.SetActive(false);
        map.SetActive(false);
        inventory.SetActive(false);
    }


    /// <summary>
    /// Abre o cierra el mapa segun el input del jugador
    /// </summary>
    public void OpenCloseMap()
    {
        mapInput = !mapInput;
        if(mapInput && !pauseInput) map.SetActive(true); 
        else map.SetActive(false);
    }

    /// <summary>
    /// Pausa o reanuda el juego según el input del jugador
    /// </summary>
    public void PauseGame()
    {
        // If the inventory is open, the user can't open pause the game
        if(inventoryOpened) return;

        pauseInput = !pauseInput;
        if (pauseInput && !mapInput) { pauseMenu.SetActive(true); Time.timeScale = 0f; }
        else { pauseMenu.SetActive(false); Time.timeScale = 1f; }
    }

    /// <summary>
    /// Abre el inventario o lo cierra si el jugador usa el input del inventario
    /// </summary>
    public void OpenInventory()
    {
        // If the pause menu is open, the user can't open the inventory
        if (pauseInput) return;

        if (inventoryOpened == true) { InventoryManager.Instance.ClearInventoryItems(); return; }
        if (inventoryOpened == false)
        {
            inventoryOpened = true;
            inventoryBtn.SetActive(false);
            inventory.SetActive(true);
            InventoryManager.Instance.ListItems();
            Time.timeScale = 0f;
        }

    }

    /// <summary>
    /// Cierra el inventario
    /// </summary>
    public void CloseInventory()
    {
        inventoryBtn.SetActive(true);
        inventory.SetActive(false); 
        inventoryOpened = false; 
        Time.timeScale = 1f;
    }
}
