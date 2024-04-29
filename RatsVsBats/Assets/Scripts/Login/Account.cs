using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Account : MonoBehaviour
{
    [Header("Icon")]
    [SerializeField] private RawImage profileIcon;

    [Header("TMP")]
    [SerializeField] private TextMeshProUGUI nickname;
    [SerializeField] private TextMeshProUGUI email;
    [SerializeField] private TextMeshProUGUI points;
    [SerializeField] private TextMeshProUGUI historyBranches;
    [SerializeField] private TextMeshProUGUI missionsCompleted;

    [Header("Edit")]
    [SerializeField] private GameObject editWarning;
    [SerializeField] private string url;

    private void OnEnable()
    {
        editWarning.SetActive(false);
        // SELECT * FROM Profiles;
    }

    public void EditProfile() { editWarning.SetActive(true); }

    public void CloseEdit()
    {
        gameObject.SetActive(false);
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    public void CloseWarning()
    {
        editWarning.SetActive(false);
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    public void GoToWebsite() { Application.OpenURL(url); }
}
