using UnityEngine;

public class Interactive : MonoBehaviour
{
    public bool hasDialog;
    private GameObject priceObject;
    public string description;

    public static Interactive instance;

    private void Start()
    {
        instance = this;
        priceObject = transform.GetChild(0).gameObject;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<PlayerController>(out PlayerController player))
        {
            if (PlayerController.Instance.isInteracting)
            {
                GiveItem();
            }
        }
    }

    private void GiveItem()
    {
        if (priceObject != null)
        {
            priceObject.SetActive(true);
        }
    }
}
