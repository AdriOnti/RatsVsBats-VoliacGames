using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkourCleared : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerController playerController) && GameManager.Instance.isMission && GameManager.Instance.missionsCompleted == 2)
        {
            MissionManager.instance.missions[2] = true;
        }
    }
}
