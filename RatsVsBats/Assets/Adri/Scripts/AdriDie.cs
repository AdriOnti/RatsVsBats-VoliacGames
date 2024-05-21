using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdriDie : MonoBehaviour
{
    [SerializeField]
    private bool isDead;
    [SerializeField]
    private bool isHit;
    private int lavaHits;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        lavaHits = 0;
        //animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Lava"))
        {
            isHit = true;
            //animator.SetBool("isHit", true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Lava"))
        {
            lavaHits++;
            if (lavaHits > 70 && !isDead)
            {
                Debug.Log("u dead");
                isDead = true;
                //animator.SetBool("isDying", true);
            }
        }
    }
}
