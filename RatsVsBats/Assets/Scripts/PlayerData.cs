using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public Vector3 position;
    public float[] rotation;
    public float speed;
    public int missionsCompleted;
    public List<Item> items;

    public PlayerData(PlayerController player) 
    {
        position = player.transform.position;
        rotation = new float[]
        {
            player.transform.rotation.x,
            player.transform.rotation.y,
            player.transform.rotation.z
        };

        speed = player.originalSpeed;
        missionsCompleted = GameManager.Instance.missionsCompleted;
        items = InventoryManager.Instance.Items;
    }
}
