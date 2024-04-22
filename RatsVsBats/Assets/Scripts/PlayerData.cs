using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public Vector3 position;
    public float[] rotation;
    public float speed;
    public List<Transform> inventory;
    public int missionsCompleted;

    public PlayerData(PlayerController player) 
    {
        inventory = new List<Transform>();
        position = player.transform.position;
        rotation = new float[]
        {
            player.transform.rotation.x,
            player.transform.rotation.y,
            player.transform.rotation.z
        };

        speed = player.originalSpeed;
        missionsCompleted = GameManager.Instance.missionsCompleted;

        Transform transform = GameManager.Instance.ItemsTransform();

        for (int i = 0; i < transform.childCount; i++)
        {
            inventory.Add(transform.GetChild(i));
        }
    }
}
