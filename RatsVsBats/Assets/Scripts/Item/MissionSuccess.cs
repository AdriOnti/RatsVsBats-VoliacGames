using UnityEngine;

public class MissionSuccess : MonoBehaviour
{
    [HideInInspector] GameObject item;

    /// <summary>
    /// Elimina el objeto de la misión, para evitar que el jugador tenga un objeto infinito
    /// </summary>
    /// <param name="other">Cualquier GameObject que tenga collider</param>
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerController>() != null && InventoryManager.Instance.missionItem != null)
        {
            item = InventoryManager.Instance.missionItem;
            InventoryManager.Instance.missionItem = null;
            InventoryManager.Instance.ClearMissionItem(item.GetComponent<ItemPickup>().item);
            if(item != null) Destroy(item);

        }
    }

    /// <summary>
    /// Pone su parametro item a null
    /// </summary>
    /// <param name="other">Cualquier GameObject que tenga collider</param>
    private void OnTriggerExit(Collider other)
    {
        item = null;
    }
}
