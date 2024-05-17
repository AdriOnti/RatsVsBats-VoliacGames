using UnityEngine;

public class StartMission : MonoBehaviour
{
    public int MissionNumber;
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerController pc) && !GameManager.Instance.isMission)
        {
            if (MissionNumber - 1 == GameManager.Instance.missionsCompleted)
            {
                MissionManager.instance.MissionGoal(MissionNumber);
                GameManager.Instance.isMission = true;
                CanvasManager.Instance.AutoSave();
                DataManager.Instance.SaveGame();
                return;
            }

            if(MissionNumber == GameManager.Instance.missionsCompleted)
            {
                CanvasManager.Instance.ShowMSG("This mission is cleared");
                return;
            }

            CanvasManager.Instance.ShowMSG("You can't do this mission right now");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        CanvasManager.Instance.HideMSG();
    }
}
