using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public static MissionManager instance;
    public PrisonDoor prisonMission;
    public List<bool> missions; // 0: NONE; 1: NONE; 2: NONE; 3: JAIL; 4: NONE

    private void Awake()
    {
        instance = this;
    }

    public void MissionGoal(int missionId)
    {
        if (missionId == 1) CanvasManager.Instance.ShowMSG("TODO");
        if (missionId == 2) CanvasManager.Instance.ShowMSG("TODO");
        if (missionId == 3) CanvasManager.Instance.ShowMSG("TODO");
        if (missionId == 4) CanvasManager.Instance.ShowMSG($"Steal the key from the bat and free the {prisonMission.ratsInJail.Count} Zauberer"); // Mago en Aleman
        if (missionId == 5) CanvasManager.Instance.ShowMSG("TODO");
    }

    public void MissionSuccess(int missionId)
    {
        if (missionId == 1) CanvasManager.Instance.ShowMSG("TODO");
        if (missionId == 2) CanvasManager.Instance.ShowMSG("TODO");
        if (missionId == 3) CanvasManager.Instance.ShowMSG("TODO");
        if (missionId == 4) CanvasManager.Instance.ShowMSG("Prision Mission Cleared!");
        if (missionId == 5) CanvasManager.Instance.ShowMSG("TODO");
    }
}
