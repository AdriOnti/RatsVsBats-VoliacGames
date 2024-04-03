using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;

    public void Pickup()
    {
        InventoryManager.Instance.Add(item);
        if (item.itemType != Item.ItemType.Key) Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.GetComponent<PlayerController>() != null) Pickup();
    }
}
