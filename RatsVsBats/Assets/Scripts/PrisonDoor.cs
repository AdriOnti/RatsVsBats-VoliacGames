using UnityEditor;
using UnityEngine;

public class PrisonDoor : MonoBehaviour
{
    public Animator animator;
    public bool isOpened;
    public RatPrisioner[] ratsInJail;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerController player))
        {
            if (isOpened) 
            {
                CanvasManager.Instance.NonDoorMSG();
                return;
            }

            if (player.actualItem != null && player.actualItem.itemType == Item.ItemType.PrisonKey)
            {
                CanvasManager.Instance.JailDoorMSG("Press [E] to open this cell");
                return;
            }
            else
            {
                CanvasManager.Instance.JailDoorMSG("You can't open this door");
            }

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out PlayerController player))
        {
            if (player.actualItem != null && player.actualItem.itemType == Item.ItemType.PrisonKey)
            {
                Interact(player);
            }
        }
    }

    private void Interact(PlayerController player)
    {
        if (player.isInteracting)
        {
            animator.Play("OpenAnim");
            player.isInteracting = false;
            gameObject.transform.parent.Find("Collision").gameObject.SetActive(false);
            isOpened = true;
            CanvasManager.Instance.NonDoorMSG();

            foreach(RatPrisioner rp in ratsInJail)
            {
                rp.isFree = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        CanvasManager.Instance.NonDoorMSG();
    }
}
