using UnityEngine;

public class MissionCleared : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerController playerController) && GameManager.Instance.isMission)
        {
            if(GameManager.Instance.missionsCompleted == 2)
            {
                MissionManager.instance.missions[2] = true;
            }

            if(GameManager.Instance.missionsCompleted == 3) 
            {
                MissionManager.instance.missions[3] = true;
            }
        }
    }
}
