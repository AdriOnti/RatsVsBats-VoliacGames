using System.Collections;
using UnityEngine;

public class PrisonDoor : BaseDoor
{
    public RatPrisioner[] ratsInJail;

    protected override bool CanOpenDoor(PlayerController player)
    {
        return player.actualItem != null && player.actualItem.itemType == Item.ItemType.PrisonKey;
    }

    protected override void OnInteract(PlayerController player)
    {
        animator.Play("OpenAnim");
        player.isInteracting = false;
        collision.SetActive(false);
        isOpened = true;
        CanvasManager.Instance.NonDoorMSG();

        StartCoroutine(FreeRats());
    }

    private IEnumerator FreeRats()
    {
        foreach (RatPrisioner rp in ratsInJail)
        {
            rp.isFree = true;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
