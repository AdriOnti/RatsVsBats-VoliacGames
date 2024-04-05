using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour
{
    Item item;
    public Button RemoveButton;

    // Funcion que borra un item del inventario
    public void RemoveItem()
    {
        InventoryManager.Instance.Remove(item);
        Destroy(gameObject);
    }

    /// <summary>
    /// Add a new item to the inventory
    /// </summary>
    /// <param name="newItem">The new Item to show in the inventory</param>
    public void AddItem(Item newItem) { item = newItem; }

    /// <summary>
    /// Use an item.
    /// When is used, it's removed from the inventory
    /// </summary>
    public void UseItem()
    {
        switch (item.itemType)
        {
            case Item.ItemType.Speed:
                GameManager.Instance.IncreasePlayerSpeed(item);
                break;
        }
        RemoveItem();
        Debug.Log($"Se ha usado {item.itemName}");
    }

}