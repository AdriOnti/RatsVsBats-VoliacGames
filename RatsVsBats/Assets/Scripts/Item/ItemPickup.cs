using UnityEngine;

public class ItemPickup : MonoBehaviour, ICollectable
{
    public Item item;
    public bool isFromMission;

    /// <summary>
    /// Nos aseguramos de que este script tenga el bool activado
    /// </summary>
    private void Start()
    {
        if(item.status == Item.ItemStatus.Mission) isFromMission = true;
    }


    /// <summary>
    /// Recoge el item que se quiere
    /// </summary>
    public void Pickup()
    {
        if (item.itemType == Item.ItemType.PrisonKey && GameManager.Instance.missionsCompleted < 4)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
        // If the bool is true, it makes a clone of the item and assign it to the InventoryManager
        if (isFromMission) { InventoryManager.Instance.missionItem = Instantiate(gameObject, GameManager.Instance.MissionItemTransform()); }

        // Add the item to the inventory and destroy the gameobject
        InventoryManager.Instance.Add(item); 
        Destroy(gameObject);
    }

    public void Collected()
    {
        Pickup();
    }
}
