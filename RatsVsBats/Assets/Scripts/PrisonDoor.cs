using UnityEditor;
using UnityEngine;

public class PrisonDoor : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerController player))
        {
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
        // TODO: Comprobar si esta pulsando la E
    }

    private void OnTriggerExit(Collider other)
    {
        CanvasManager.Instance.NonDoorMSG();
    }
}
