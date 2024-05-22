using UnityEngine;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour
{
    Item item;
    public Button RemoveButton;

    /// <summary>
    /// Funcion que borra un item del inventario
    /// </summary>
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
            case Item.ItemType.PrisonKey:
            case Item.ItemType.KeyEye:
            case Item.ItemType.KeyTrebol:
            case Item.ItemType.KeyCircle:
            case Item.ItemType.KeyHeart:
            case Item.ItemType.KeyRombo:
            case Item.ItemType.FinalKey:
                GameManager.Instance.DoorKey(item);
                break;
            case Item.ItemType.JumpBoost:
                GameManager.Instance.IncreaseJumpBoost(item);
                break;
        }
        RemoveItem();
    }

}