using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

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

    [Header("Bools")]
    [SerializeField] private bool mapInput;
    [SerializeField] public bool pauseInput;
    [SerializeField] private bool inventoryOpened;

    [Header("Canvas")]
    [HideInInspector] GameObject pauseMenu;
    [HideInInspector] GameObject map;
    [HideInInspector] GameObject disquete;
    [HideInInspector] GameObject info;
    [HideInInspector] GameObject missionInfo;

    [Header("Inventory")]
    [HideInInspector] GameObject inventoryBtn;
    [HideInInspector] GameObject inventory;

    [Header("AutoSave")]
    [HideInInspector] GameObject autoSave;

    [Header("Delete")]
    [HideInInspector] GameObject notConfirm;
    [HideInInspector] GameObject confirmDelete;
    [HideInInspector] GameObject confirm2ndCour;

    [Header("Prison")]
    [HideInInspector] GameObject message;

    [Header("Finale")]
    [HideInInspector] GameObject finale;
    public TextMeshProUGUI finaleMSG;

    [Header("Intro")]
    public RawImage rawImage;
    public VideoPlayer vp;
    public VideoClip bookIntro;
    public VideoClip animIntro;
    [HideInInspector] GameObject introCanvas;

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
        disquete = GameManager.Instance.GetDisquete();
        info = GameManager.Instance.GetInfoMenu();
        autoSave = GameManager.Instance.GetAutoSave();

        notConfirm = GameManager.Instance.FindObjectsByName("NotConfirm");
        confirmDelete = GameManager.Instance.FindObjectsByName("ConfirmDelete");
        confirm2ndCour = GameManager.Instance.FindObjectsByName("ConfirmDelete2Cour");
        message = GameManager.Instance.FindObjectsByName("MSG");
        missionInfo = GameManager.Instance.FindObjectsByName("MissionInfo");
        finale = GameManager.Instance.FindObjectsByName("Finale");
        introCanvas = GameManager.Instance.FindObjectsByName("Intro");
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
        GameManager.Instance.GetFade().SetActive(true);

        // SET ACTIVE: FALSE
        pauseMenu.SetActive(false);
        map.SetActive(false);
        inventory.SetActive(false);
        disquete.SetActive(false);
        info.SetActive(false);
        autoSave.SetActive(false);
        notConfirm.SetActive(false);
        confirmDelete.SetActive(false);
        confirm2ndCour.SetActive(false);
        message.SetActive(false);
        missionInfo.SetActive(false);
        finale.SetActive(false);
        introCanvas.SetActive(false);
    }


    /// <summary>
    /// Abre o cierra el mapa segun el input del jugador
    /// </summary>
    public void OpenCloseMap()
    {
        mapInput = !mapInput;
        if(mapInput && !pauseInput && !GameManager.Instance.isFading) map.SetActive(true); 
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
        if (pauseInput && !mapInput && !GameManager.Instance.isFading)
        { 
            pauseMenu.SetActive(true); 
            Time.timeScale = 0.0f; 
        }
        else
        { 
            pauseMenu.SetActive(false);
            notConfirm.SetActive(false);
            confirmDelete.SetActive(false);
            confirm2ndCour.SetActive(false);
            Time.timeScale = 1f; 
            CursorManager.Instance.ResetCursor(); 
        }

        NotLoad();
        IsMission();
    }

    /// <summary>
    /// Abre el inventario o lo cierra si el jugador usa el input del inventario
    /// </summary>
    public void OpenInventory()
    {
        // If the pause menu is open, the user can't open the inventory
        if (pauseInput || GameManager.Instance.isFading) return;

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
        CursorManager.Instance.ResetCursor();
    }

    /// <summary>
    /// Detecta si esta en una mision para bloquear o desbloquear el botón de guardar
    /// </summary>
    void IsMission()
    {
        GameObject go = pauseMenu.transform.Find("SaveBtn").gameObject;
        if (GameManager.Instance.isMission) CursorManager.Instance.BlockBtn(go);
        else CursorManager.Instance.NotBlockBtn(go);
    }

    /// <summary>
    /// Detecta si hay un archivo guardado. Si no hay ninguno bloquea el botón de cargar
    /// </summary>
    public void NotLoad()
    {
        GameObject go = pauseMenu.transform.Find("LoadBtn").gameObject;
        if (!DataManager.Instance.SaveExists()) CursorManager.Instance.BlockBtn(go);
        else CursorManager.Instance.NotBlockBtn(go);
    }

    /// <summary>
    /// Disable the hud elements in the fade out
    /// </summary>
    public void HUDFadesOut()
    {
        inventoryBtn.SetActive(false);
        GameManager.Instance.GetHUD().SetActive(false);
    }

    /// <summary>
    /// Enable the hud elements in the fade in
    /// </summary>
    public void HUDFadesIn()
    {
        inventoryBtn.SetActive(true);
        GameManager.Instance.GetHUD().SetActive(true);
    }

    /// <summary>
    /// Función que se llama al momento de guardar partida
    /// </summary>
    public void Saving() { StartCoroutine(SavingGame()); }

    /// <summary>
    /// Corrutina que ejecuta la animación del disquete de guardado
    /// </summary>
    /// <returns></returns>
    private IEnumerator SavingGame()
    {
        disquete.SetActive(true);
        yield return new WaitForSecondsRealtime(2.0f);
        disquete.SetActive(false);
    }

    /// <summary>
    /// Activa la opción de confirmar el borrado de la partida guardada
    /// </summary>
    public void ConfirmDelete() { confirmDelete.SetActive(true);  }

    /// <summary>
    /// Activa el objeto que indica que no hay ninguna partida guardada
    /// </summary>
    public void NotConfirmDelete() { notConfirm.SetActive(true); }

    /// <summary>
    /// Muestra un texto si el objeto esta bloqueado
    /// </summary>
    /// <param name="goName">El nombre del GameObject</param>
    public void InformativeText(string goName)
    {
        info.SetActive(true);
        if (goName == "SaveBtn") info.GetComponentInChildren<TextMeshProUGUI>().text = "You are in a mission, you cannot save game";
        if (goName == "LoadBtn") info.GetComponentInChildren<TextMeshProUGUI>().text = "Doesn't exist any saved game to load";
    }

    public void AutoSave()
    {
        StartCoroutine(StartAutoSave());
    }

    private IEnumerator StartAutoSave()
    {
        autoSave.SetActive(true);
        yield return new WaitForSecondsRealtime(2.0f);
        autoSave.SetActive(false);
    }

    public void BackToMainMenu()
    {
        PlayerPrefs.SetInt("back", 1);
        CursorManager.Instance.ResetCursor();
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void ShowMSG(string text)
    {
        message.SetActive(true);
        message.GetComponentInChildren<TextMeshProUGUI>().text = text;
    }

    public void HideMSG()
    {
        message.SetActive(false);
    }

    public void MissionInfo(string info)
    {
        missionInfo.SetActive(true);
        missionInfo.GetComponentInChildren<TextMeshProUGUI>().text = info;
    }

    public void ClearMission()
    {
        missionInfo.SetActive(false);
    }

    public void Credits(string text)
    {
        finale.SetActive(true);
        finaleMSG.text = text;
    }

    public void StartIntro()
    {
        //introCanvas.SetActive(true);
        //StartCoroutine(Intro());
    }

    IEnumerator Intro()
    {
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.GetHUD().SetActive(false);
        vp.clip = bookIntro;
        vp.Play();
        vp.loopPointReached += SecondIntro;
    }

    public void SecondIntro(VideoPlayer vp)
    {
        vp.clip = animIntro;
        vp.Play();
        vp.loopPointReached += NewGameStart;
    }

    public void NewGameStart(VideoPlayer vp)
    {
        vp.gameObject.SetActive(false);
        introCanvas.SetActive(false);
        HUDFadesIn();
    }
}
