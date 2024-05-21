using UnityEngine;

public class NewDoor : BaseDoor
{
    protected override void OnInteract(PlayerController player)
    {
        animator.Play("OpenAnim");
        player.isInteracting = false;
        collision.SetActive(false);
        isOpened = true;
        CanvasManager.Instance.HideMSG();
    }
}
