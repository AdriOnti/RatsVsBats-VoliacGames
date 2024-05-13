using UnityEngine;

public class StartMission : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerController pc))
        {
            GameManager.Instance.isMission = true;
        }
    }
}
