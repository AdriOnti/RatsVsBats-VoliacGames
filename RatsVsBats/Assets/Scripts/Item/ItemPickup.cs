using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;
    public bool isFromMission;

    private void Start()
    {
        if(item.status == Item.ItemStatus.Mission) isFromMission = true;
    }

    public void Pickup()
    {
        if(isFromMission) { InventoryManager.Instance.missionItem = Instantiate(gameObject, GameManager.Instance.MissionItemTransform()); }
        InventoryManager.Instance.Add(item); 
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.GetComponent<PlayerController>() != null) Pickup();
    }
}
