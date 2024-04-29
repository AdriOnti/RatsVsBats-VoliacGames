using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive : MonoBehaviour
{
    public bool hasDialog;
    public GameObject priceObject;
    public string description = "Recycle blud";

    public static Interactive instance;

    private void Start()
    {
        instance = this;
    }

    public void GiveItem()
    {
        priceObject.transform.position = new Vector3(transform.position.x, transform.position.y + 5f, transform.position.z);
        priceObject.SetActive(true);
    }
}
