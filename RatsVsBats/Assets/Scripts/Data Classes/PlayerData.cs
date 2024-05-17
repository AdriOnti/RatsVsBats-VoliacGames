using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public float maxHP;
    public float currentHP;
    public float jumpForce;
    public float healingForce;

    public Vector3 position;
    public float[] rotation;
    public float speed;
    public int missionsCompleted;
    public List<Item> items;
    public List<bool> missions;

    public PlayerData(PlayerController player) 
    {
        maxHP = player.hp; 
        currentHP = player.currentHP; 
        jumpForce = player.jumpForce; 
        healingForce = player.healingForce;

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
        missions = MissionManager.instance.missions;
    }
}
