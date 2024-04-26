using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Create New Item")]
public class Item : ScriptableObject
{
    public int id;
    public string itemName;
    public float value;
    public float waitTime;
    public Sprite icon;
    public ItemType itemType;
    public ItemStatus status;

    public enum ItemType
    {
        Speed
    }

    public enum ItemStatus
    {
        Normal,
        Mission
    }
}
