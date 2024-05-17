using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public static MissionManager instance;
    public List<bool> missions;

    private void Awake()
    {
        instance = this;
    }

    public void MissionGoal(int missionId)
    {
        if (missionId == 1) CanvasManager.Instance.DoorMSG("TODO");
        if (missionId == 2) CanvasManager.Instance.DoorMSG("TODO");
        if (missionId == 3) CanvasManager.Instance.DoorMSG("TODO");
        if (missionId == 4) CanvasManager.Instance.DoorMSG("Steal the key from the bat and free your race");
        if (missionId == 5) CanvasManager.Instance.DoorMSG("TODO");
    }

    public void MissionSuccess(int missionId)
    {
        if (missionId == 1) CanvasManager.Instance.DoorMSG("TODO");
        if (missionId == 2) CanvasManager.Instance.DoorMSG("TODO");
        if (missionId == 3) CanvasManager.Instance.DoorMSG("TODO");
        if (missionId == 4) CanvasManager.Instance.DoorMSG("Prision Mission Cleared!");
        if (missionId == 5) CanvasManager.Instance.DoorMSG("TODO");
    }
}
