using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PrisonDoor : BaseDoor
{
    public List<RatPrisioner> ratsInJail;
    public Animator jailBars;
    public EnemyController3 enemyController;
    public GameObject romboKey;

    protected override void OnInteract(PlayerController player)
    {
        animator.Play("OpenAnim");
        jailBars.Play("JailBarsOpen");
        SoundManager.Instance.PlayEffect(Audios.effectDoorOpen);
        player.ratAnimator.SetBool("openDoor", true);
        player.isInteracting = false;
        collision.SetActive(false);
        isOpened = true;
        CanvasManager.Instance.HideMSG();
        InventoryManager.Instance.Remove(player.actualItem);
        player.ChangeItem();

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
            MissionManager.instance.missions[1] = true;
            enemyController.HP = 0;
            if(romboKey != null)
            {
                romboKey.SetActive(true);
            }
        }
    }
}
