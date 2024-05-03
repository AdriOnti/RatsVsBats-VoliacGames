using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [Header("Inventory")]
    public List<Item> Items = new List<Item>();
    public Transform ItemContent;
    public GameObject InventoryItem;
    public Toggle EnableRemove;
    public InventoryItemController[] InventoryItems;
    public GameObject Inventory;

    [Header("MissionItem")]
    public GameObject missionItem;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    /// <summary>
    /// Add the collected item to the inventory list
    /// </summary>
    /// <param name="item"></param>
    public void Add(Item item)
    {
        Item newItem = item;
        Items.Add(newItem);
        PlayerController.Instance.ChangeItem();
    }

    /// <summary>
    /// Remove the selected item from the inventory
    /// </summary>
    /// <param name="item">An item from the invetory</param>
    public void Remove(Item item)
    {
        Items.Remove(item);

        if (missionItem != null && missionItem.GetComponent<ItemPickup>().item.status == item.status)
        {
            missionItem.GetComponent<ItemPickup>().Pickup();
        }
        PlayerController.Instance.ChangeItem();
    }

    /// <summary>
    /// Instantiate the items list in the inventory object
    /// </summary>
    public void ListItems()
    {
        foreach (var item in Items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<Text>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            var rmBtn = obj.transform.Find("RmBtn").GetComponent<Button>();

            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;

            if (EnableRemove.isOn) rmBtn.gameObject.SetActive(true);
        }
        SetInventoryItems();
    }

    /// <summary>
    /// Delete all the items without use them
    /// </summary>
    public void EnableItemsRemove()
    {
        // If is on, the remove button appears
        // If is off, the remove button disappears
        if (EnableRemove.isOn)
        {
            foreach (Transform item in ItemContent) item.Find("RmBtn").gameObject.SetActive(true);
        }
        else
        {
            foreach (Transform item in ItemContent) item.Find("RmBtn").gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Set the items like childs of the Content
    /// </summary>
    public void SetInventoryItems()
    {
        InventoryItems = ItemContent.GetComponentsInChildren<InventoryItemController>();

        for (int i = 0; i < Items.Count; i++)
        {
            if (Items[i] != null) InventoryItems[i].AddItem(Items[i]);
        }
    }

    /// <summary>
    /// Clear all the inventory items
    /// </summary>
    public void ClearInventoryItems()
    {
        // Este foreach estaba justo al principio de ListItems pero eso provacaba errores, en un comentario del tutorial salia esto y ¡HA FUNCIONADO!
        foreach (Transform item in ItemContent) Destroy(item.gameObject);
        CanvasManager.Instance.CloseInventory();
    }

    /// <summary>
    /// Search in any of the items in the inventory and remove the missionItem
    /// </summary>
    /// <param name="former"></param>
    public void ClearMissionItem(Item former)
    {
        foreach (Item item in Items)
        {
            if (item.status == former.status)
            {
                Items.Remove(item);
                return;
            }
        }
    }
}
