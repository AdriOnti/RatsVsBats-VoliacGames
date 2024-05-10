using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "ScriptableObjects/Item/Create New Item", order = 0)]
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
        Speed,
        Key
    }

    public enum ItemStatus
    {
        Normal,
        Mission
    }
}
