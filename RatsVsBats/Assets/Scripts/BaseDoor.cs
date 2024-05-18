using UnityEngine;

public abstract class BaseDoor : MonoBehaviour
{
    public Animator animator;
    public bool isOpened;
    [HideInInspector] public GameObject collision;
    [HideInInspector] public GameObject lockDoor;

    private void Start()
    {
        collision = gameObject.transform.parent.Find("Collision").gameObject;
        lockDoor = gameObject.transform.parent.Find("Lock").gameObject;
    }

    protected virtual bool CanOpenDoor(PlayerController player)
    {
        return false;
    }

    protected virtual void OnInteract(PlayerController player)
    {
        // Implement the interaction behavior for specific doors
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerController player))
        {
            if (isOpened)
            {
                CanvasManager.Instance.HideMSG();
                return;
            }

            if(CanOpenDoor(player))
            {
                CanvasManager.Instance.ShowMSG("Press [E] to open this door");
            }
            else
            {
                CanvasManager.Instance.ShowMSG("You can't open this door");
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.TryGetComponent(out PlayerController player))
        {
            if (CanOpenDoor(player))
            {
                if (player.isInteracting)
                {
                    lockDoor.SetActive(false);
                    OnInteract(player);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        CanvasManager.Instance.HideMSG();
    }
}
