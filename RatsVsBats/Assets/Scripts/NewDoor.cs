using UnityEngine;

public class NewDoor : BaseDoor
{
    public NewDoor siblingdoor;
    protected override void OnInteract(PlayerController player)
    {
        animator.Play("OpenAnim");
        SoundManager.Instance.PlayEffect(Audios.effectDoorOpen);
        if (siblingdoor != null) siblingdoor.SimulateInteract(player);
        player.isInteracting = false;
        player.ratAnimator.SetBool("openDoor", true);
        collision.SetActive(false);
        isOpened = true;
        CanvasManager.Instance.HideMSG();
    }

    public void SimulateInteract(PlayerController player)
    {
        OnInteract(player);
    }
}
