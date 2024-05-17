using System.Collections;
using System.Linq;
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
            yield return new WaitForSeconds(0.7f);
        }
    }

    private void Update()
    {
        if (ratsInJail.All(rat => rat.targetArrived)) 
        {
            Debug.Log("The Last Rat arrived, the Bat Beast gonna die");
        }
    }
}
