using UnityEngine;

public class AutoSave : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        CanvasManager.Instance.AutoSave();
        DataManager.Instance.SaveGame();
    }
}
