using UnityEngine;

public class MissionSuccess : MonoBehaviour
{
    [HideInInspector] GameObject item;
    public int points;
    public StartMission mission;

    /// <summary>
    /// Elimina el objeto de la misión, para evitar que el jugador tenga un objeto infinito
    /// </summary>
    /// <param name="other">Cualquier GameObject que tenga collider</param>
    private async void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerController>() != null && MissionStatus())
        {
            // Destroy the mission Item
            item = InventoryManager.Instance.missionItem;
            InventoryManager.Instance.missionItem = null;
            InventoryManager.Instance.ClearMissionItem(item.GetComponent<ItemPickup>().item);
            if (item != null) Destroy(item);
            PlayerController.Instance.ChangeItem();

            // Desactivate the mission state and update the profile
            GameManager.Instance.isMission = false;
            GameManager.Instance.missionsCompleted += 1;
            await DataManager.Instance.UpdateProfile(points);

            // Make an auto save
            CanvasManager.Instance.AutoSave();
            DataManager.Instance.SaveGame();

            MissionManager.instance.MissionSuccess(mission.MissionNumber);
        }
    }

    /// <summary>
    /// Pone su parametro item a null
    /// </summary>
    /// <param name="other">Cualquier GameObject que tenga collider</param>
    private void OnTriggerExit(Collider other)
    {
        item = null;
    }

    private bool MissionStatus()
    {
        return InventoryManager.Instance.missionItem != null && GameManager.Instance.isMission 
            && mission.MissionNumber - 1 == GameManager.Instance.missionsCompleted
            && MissionManager.instance.missions[mission.MissionNumber - 1];
    }
}
