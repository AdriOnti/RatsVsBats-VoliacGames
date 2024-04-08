using UnityEngine;

public class MissionSuccess : MonoBehaviour
{
    public GameObject item;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerController>() != null)
        {
            item = InventoryManager.Instance.missionItem;
            InventoryManager.Instance.missionItem = null;


            //InventoryManager.Instance.Remove(item.GetComponent<ItemPickup>().item);

            InventoryManager.Instance.ClearMissionItem(item.GetComponent<ItemPickup>().item);

            Destroy(item);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        item = null;
    }
}
