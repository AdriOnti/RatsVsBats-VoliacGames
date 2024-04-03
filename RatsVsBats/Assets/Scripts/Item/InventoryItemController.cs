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

    // Añade un nuevo item al inventario
    public void AddItem(Item newItem) { item = newItem; }

    // Funcion para usar un item. Una vez que se usa, se elimina del inventario
    public void UseItem()
    {
        if (item.itemType == Item.ItemType.Key) { return; }
        switch (item.itemType)
        {
            case Item.ItemType.Heart:
                //PlayerController.Instance.IncreaseMaxHealth(item.value);
                break;
        }
        RemoveItem();
    }

}