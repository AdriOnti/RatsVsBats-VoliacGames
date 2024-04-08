using System.Collections;
using UnityEngine;

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

    [Header("Bools")]
    public bool speedUsed;

    // Get the pause menu
    public GameObject GetPauseMenu() { return FindInActiveObjectByName("PauseMenu"); }

    // Get the map
    public GameObject GetMap() { return FindInActiveObjectByName("Map"); }

    // Get the inventory
    public GameObject GetInventory() { return FindInActiveObjectByName("Inventory"); }

    // Get the inventory button
    public GameObject GetInventoryBtn() { return FindInActiveObjectByName("InventoryBtn"); }

    // Get the GameObject that contains all the canvas.
    public GameObject GetCanvasFather() { return FindInActiveObjectByName("CanvasFather"); }

    // Get the GameObject on the missionItem will be instantiate
    public Transform MissionItemTransform() { return FindInActiveObjectByName("MissionItems").transform; }

    /// <summary>
    /// Busca entre todos los objetos, tanto los activos como inactivos, el que se busca
    /// </summary>
    /// <param name="name">Nombre del GameObject que se busca</param>
    /// <returns>El gameobject que se buscaba</returns>
    public GameObject FindInActiveObjectByName(string name)
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
        FindInActiveObjectByName("TEST_Effect").SetActive(true);

        yield return new WaitForSeconds(waitTime);

        // Decrement
        PlayerController.Instance.speed -= itemValue;
        speedUsed = false;
        FindInActiveObjectByName("TEST_Effect").SetActive(false);
    }
}
