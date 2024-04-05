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

    // Obtiene el menu de pausa
    public GameObject GetPauseMenu() { return FindInActiveObjectByName("PauseMenu"); }

    // Obtiene el mapa
    public GameObject GetMap() { return FindInActiveObjectByName("Map"); }

    // Obtiene el inventario
    public GameObject GetInventory() { return FindInActiveObjectByName("Inventory"); }

    // Obtiene el boton del inventario
    public GameObject GetInventoryBtn() { return FindInActiveObjectByName("InventoryBtn"); }

    // Obtiene el objeto donde estan todos los canvas
    public GameObject GetCanvasFather() { return FindInActiveObjectByName("CanvasFather"); }

    /// <summary>
    /// Busca entre todos los objetos, tanto los activos como inactivos, el que se busca
    /// </summary>
    /// <param name="name">Nombre del GameObject que se busca</param>
    /// <returns>El gameobject que se buscaba</returns>
    GameObject FindInActiveObjectByName(string name)
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

    public void IncreasePlayerSpeed(float itemValue, float waitTime)
    {
        StartCoroutine(IncreaseSpeedCoroutine(itemValue, waitTime));
    }

    IEnumerator IncreaseSpeedCoroutine(float itemValue, float waitTime)
    {
        PlayerController.Instance.speed += itemValue;
        yield return new WaitForSeconds(waitTime);
        PlayerController.Instance.speed -= itemValue;
    }
}
