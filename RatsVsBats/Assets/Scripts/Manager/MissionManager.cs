using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public static MissionManager instance;
    public PrisonDoor prisonMission;
    public List<bool> missions; // 0: FROZEN; 1: JAIL; 2: PARKOUR; 3: LABERYNTH

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
            CanvasManager.Instance.ShowMSG("Recover the frozen key");
            CanvasManager.Instance.MissionInfo("Recover the frozen key");
        }
        if (missionId == 2)
        {
            CanvasManager.Instance.ShowMSG($"Steal the key from the bat and free the prisioners");
            CanvasManager.Instance.MissionInfo($"Steal the key from the bat and free the prisioners");
        }
        if (missionId == 3)
        {
            CanvasManager.Instance.MissionInfo("Survive the lava parkour");
            CanvasManager.Instance.ShowMSG("Survive the lava parkour");
        }
        if (missionId == 4)
        {
            CanvasManager.Instance.ShowMSG("Pass the labyrinth");
            CanvasManager.Instance.MissionInfo("Pass the labyrinth");
        }
    }

    public void MissionSuccess(int missionId)
    {
        if (missionId == 1)
        {
            CanvasManager.Instance.ShowMSG("Frozek Mission Cleared!");
            CanvasManager.Instance.ClearMission();
        }
        if (missionId == 2)
        {
            CanvasManager.Instance.ShowMSG("Prision Mission Cleared!");
            CanvasManager.Instance.ClearMission();
        }
        if (missionId == 3)
        {
            CanvasManager.Instance.ShowMSG("Parkour Cleared!");
            CanvasManager.Instance.ClearMission();
        }
        if (missionId == 4)
        {
            CanvasManager.Instance.ShowMSG("Labyrinth Cleared!");
            CanvasManager.Instance.ClearMission();
        }

        StartCoroutine(ByeMSG());
    }

    IEnumerator ByeMSG()
    {
        yield return new WaitForSeconds(1f);
        CanvasManager.Instance.HideMSG();
    }

    public void MissionObjects(int mission)
    {
        if (mission == 1)
        {
            DisableWalls(mission);
        }
        if (mission == 2)
        {
            foreach (GameObject go in objectsOfTheMissionJail) go.SetActive(true);
            DisableWalls(mission);
        }
        if (mission == 3)
        {
            DisableWalls(mission);
        }
        if (mission == 4)
        {
            DisableWalls(mission);
        }
    }

    public void DisableWalls(int mission)
    {
        //if (mission == 1) invisibleWall[0].SetActive(false);
        //if (mission == 2) invisibleWall[1].SetActive(false);
        //if (mission == 3) invisibleWall[2].SetActive(false);
        //if (mission == 4) invisibleWall[3].SetActive(false);
    }

    public void CheckMissionsCleared()
    {
        if (missions[0]) DisableWalls(1);
        if (missions[1]) DisableWalls(2);
        if (missions[2]) DisableWalls(3);
        if (missions[3]) DisableWalls(4);
    }
}
