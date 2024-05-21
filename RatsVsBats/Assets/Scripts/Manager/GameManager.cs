using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // SINGLETON
    private static GameManager instance;
    public static GameManager Instance
    {
        get { return instance; }
    }

    // AWAKE
    private void Awake()
    {
        if (instance != null && instance != this) Destroy(gameObject);
        else instance = this;
    }

    [Header("Mission")]
    public int missionsCompleted;

    [Header("Bools")]
    public bool speedUsed;
    public bool isMission;
    public bool isFading;
    public TextMeshProUGUI UItext;

    // Get the pause menu
    public GameObject GetPauseMenu() { return FindObjectsByName("PauseMenu"); }

    // Get the map
    public GameObject GetMap() { return FindObjectsByName("Map"); }

    // Get the inventory
    public GameObject GetInventory() { return FindObjectsByName("Inventory"); }

    // Get the inventory button
    public GameObject GetInventoryBtn() { return FindObjectsByName("InventoryBtn"); }

    // Get the GameObject that contains all the canvas.
    public GameObject GetCanvasFather() { return FindObjectsByName("CanvasFather"); }

    // Get the GameObject on the missionItem will be instantiate
    public Transform MissionItemTransform() { return FindObjectsByName("MissionItems").transform; }

    // Get the HUD
    public GameObject GetHUD() { return FindObjectsByName("HUD"); }

    // Get the Fade father
    public GameObject GetFade() { return FindObjectsByName("Fade"); }

    // Get the saving icon (disquete)
    public GameObject GetDisquete() { return FindObjectsByName("Saving"); }

    // Get the info text in pause menu
    public GameObject GetInfoMenu() { return FindObjectsByName("InformationMenu"); }

    // Get the autosave canvas object
    public GameObject GetAutoSave() { return FindObjectsByName("AutoSave"); }

    /// <summary>
    /// Busca entre todos los objetos, tanto los activos como inactivos, el que se busca
    /// </summary>
    /// <param name="name">Nombre del GameObject que se busca</param>
    /// <returns>El gameobject que se buscaba</returns>
    public GameObject FindObjectsByName(string name)
    {
        Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i].hideFlags == HideFlags.None)
            {
                if (objs[i].name == name)
                {
                    return objs[i].gameObject;
                }
            }
        }
        return null;
    }

    /// <summary>
    /// Incrementa la velocidad del jugador
    /// </summary>
    /// <param name="item">Item que se consume</param>
    public void IncreasePlayerSpeed(Item item)
    {
        if (speedUsed)
        {
            Debug.Log("Ya se había consumido uno de estos objetos");
            InventoryManager.Instance.Add(item);
            return;
        }
        StartCoroutine(IncreaseSpeedCoroutine(item.value, item.waitTime));
    }

    public void IncreaseJumpBoost(Item item)
    {
        StartCoroutine(IncreaseJumpCoroutine(item.value, item.waitTime));
    }

    /// <summary>
    /// Incrementa la velocidad y luega la decrementa
    /// </summary>
    /// <param name="itemValue">Valor del item</param>
    /// <param name="waitTime">Tiempo de espero del item</param>
    /// <returns>Es una corrutina</returns>
    IEnumerator IncreaseSpeedCoroutine(float itemValue, float waitTime)
    {
        // Increment
        PlayerController.Instance.speed += itemValue;
        speedUsed = true;
        FindObjectsByName("TEST_Effect").SetActive(true);

        yield return new WaitForSeconds(waitTime);

        // Decrement
        PlayerController.Instance.speed -= itemValue;
        speedUsed = false;
        FindObjectsByName("TEST_Effect").SetActive(false);
    }

    IEnumerator IncreaseJumpCoroutine(float itemValue, float waitTime)
    {
        PlayerController.Instance.jumpForce += itemValue;
        FindObjectsByName("TEST_Effect").SetActive(true);

        yield return new WaitForSeconds(waitTime);

        PlayerController.Instance.jumpForce -= itemValue;
        FindObjectsByName("TEST_Effect").SetActive(false);
    }

    /// <summary>
    /// Actualizar el item
    /// </summary>
    /// <param name="item">El item pasado</param>
    public void UpdateItem(Item item)
    {
        Image actualItem = FindObjectsByName("ActualItem").GetComponent<Image>();

        if (item != null) actualItem.sprite = item.icon;
        else  actualItem.sprite = null;
    }

    public void UpdateText(string text)
    {
        UItext.text = text;
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKey(KeyCode.H))
        {
            PlayerController.Instance.transform.position = new Vector3(-40.2000008f, -33f, -15.9200001f);
        }
    }
#endif

    public void DoorKey(Item item)
    {
        Image actualItem = FindObjectsByName("ActualItem").GetComponent<Image>();

        if (item != null) actualItem.sprite = item.icon;
        else actualItem.sprite = null;

        int index = InventoryManager.Instance.Items.IndexOf(item);

        PlayerController.Instance.actualItem = item;
        PlayerController.Instance.inventoryIndex = index;
    }

}
