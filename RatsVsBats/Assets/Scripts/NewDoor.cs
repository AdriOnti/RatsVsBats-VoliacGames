using UnityEngine;

public class NewDoor : BaseDoor
{
    public int missionId;
    protected override void OnInteract(PlayerController player)
    {
        animator.Play("OpenAnim");
        player.isInteracting = false;
        collision.SetActive(false);
        isOpened = true;
        CanvasManager.Instance.HideMSG();
        //MissionManager.instance.missions[missionId - 1] = true; // Por si abrir la puerta ha de completar la mision
    }
}
