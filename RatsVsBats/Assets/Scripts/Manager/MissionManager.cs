using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public static MissionManager instance;
    public PrisonDoor prisonMission;
    public List<bool> missions; // 0: NONE; 1: NONE; 2: NONE; 3: JAIL; 4: NONE

    [Header("Invisible Wall")]
    public List<GameObject> invisibleWall;

    [Header("Jail Mission")]
    public List<GameObject> objectsOfTheMissionJail;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        // Jail Mission
        foreach (GameObject obj in objectsOfTheMissionJail) { obj.SetActive(false); }
    }

    public void MissionGoal(int missionId)
    {
        if (missionId == 1)
        {
            CanvasManager.Instance.ShowMSG("TODO");
            CanvasManager.Instance.MissionInfo("TODO");
        }
        if (missionId == 2)
        {
            CanvasManager.Instance.MissionInfo("TODO");
            CanvasManager.Instance.ShowMSG("TODO");
        }
        if (missionId == 3)
        {
            CanvasManager.Instance.ShowMSG("TODO");
            CanvasManager.Instance.MissionInfo("TODO");
        }
        if (missionId == 4)
        {
            CanvasManager.Instance.ShowMSG($"Steal the key from the bat and free the {prisonMission.ratsInJail.Count} Zauberer"); // Mago en Aleman
            CanvasManager.Instance.MissionInfo($"Steal the key from the bat and free the {prisonMission.ratsInJail.Count} Zauberer");
        }
        if (missionId == 5)
        {
            CanvasManager.Instance.ShowMSG("TODO");
            CanvasManager.Instance.MissionInfo("TODO");
        }
    }

    public void MissionSuccess(int missionId)
    {
        if (missionId == 1) CanvasManager.Instance.ShowMSG("TODO");
        if (missionId == 2) CanvasManager.Instance.ShowMSG("TODO");
        if (missionId == 3) CanvasManager.Instance.ShowMSG("TODO");
        if (missionId == 4) 
            CanvasManager.Instance.ShowMSG("Prision Mission Cleared!");
            CanvasManager.Instance.ClearMission();
        if (missionId == 5) CanvasManager.Instance.ShowMSG("TODO");
    }

    public void MissionObjects(int mission)
    {
        if(mission == 4)
        {
            foreach (GameObject go in objectsOfTheMissionJail) go.SetActive(true);
            DisableWalls(mission);
        }
    }

    public void DisableWalls(int mission)
    {
        //if (mission == 1) invisibleWall[0].SetActive(false);
        //if (mission == 2) invisibleWall[1].SetActive(false);
        //if (mission == 3) invisibleWall[2].SetActive(false);
        if (mission == 4) invisibleWall[3].SetActive(false);
        //if (mission == 5) invisibleWall[4].SetActive(false);
    }

    public void CheckMissionsCleared()
    {
        if (missions[0]) DisableWalls(1);
        if (missions[1]) DisableWalls(2);
        if (missions[2]) DisableWalls(3);
        if (missions[3]) DisableWalls(4);
        if (missions[4]) DisableWalls(5);
    }
}
