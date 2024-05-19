using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PrisonDoor : BaseDoor
{
    public List<RatPrisioner> ratsInJail;

    protected override void OnInteract(PlayerController player)
    {
        animator.Play("OpenAnim");
        player.isInteracting = false;
        collision.SetActive(false);
        isOpened = true;
        CanvasManager.Instance.HideMSG();

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
            MissionManager.instance.missions[3] = true;
        }
    }
}
