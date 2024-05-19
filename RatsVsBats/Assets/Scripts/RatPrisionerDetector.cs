using UnityEngine;

public class RatPrisionerDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out RatPrisioner rp))
        {
            rp.GetComponent<Animator>().SetBool("isShooting", true);
            rp.isFree = false;
            rp.targetArrived = true;
        }
    }
}
