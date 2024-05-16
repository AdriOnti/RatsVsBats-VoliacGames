using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Account : MonoBehaviour
{
    public static Account Instance;
    [Header("Icon")]
    [SerializeField] private RawImage profileIcon;

    [Header("TMP")]
    [SerializeField] private TextMeshProUGUI nickname;
    [SerializeField] private TextMeshProUGUI email;
    [SerializeField] private TextMeshProUGUI points;
    [SerializeField] private TextMeshProUGUI historyBranches;
    [SerializeField] private TextMeshProUGUI missionsCompleted;
    [SerializeField] private int maxBranches = 3;

    [Header("Edit")]
    [SerializeField] private GameObject editWarning;
    [SerializeField] private GameObject account;
    [SerializeField] private string URL;

    private void Awake() { Instance = this; }

    private void OnEnable() { editWarning.SetActive(false); }

    /// <summary>
    /// Function that is called after the login. This makes a select query to the profiles table
    /// </summary>
    public async void JustLogged(int id, string email)
    {
        if (Login.instance.isLogged) await CheckProfile(id, email);
        else Debug.Log($"IsLogged: {Login.instance.isLogged}, the profile data can't appeared");
    }

    private async Task CheckProfile(int id, string email)
    {
        try
        {
            string response = await APIManager.instance.GetProfileWhereIdUserAsync(id);
            if (string.IsNullOrEmpty(response))
            {
                return;
            }

            ProcessJSON(response, email);

        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
        }
    }

    /// <summary>
    /// Open the warning
    /// </summary>
    public void EditProfile() { editWarning.SetActive(true); }

    /// <summary>
    /// Close the actual game object
    /// </summary>
    /// <param name="isWarning">If is true, only closes the warning</param>
    public void CloseObject(bool isWarning)
    {
        if(isWarning) editWarning.SetActive(false);
        else account.SetActive(false);
        CursorManager.Instance.ResetCursor();
    }

    /// <summary>
    /// Go to the Website for change the data
    /// </summary>
    public void GoToWebsite() { Application.OpenURL(URL); }

    /// <summary>
    /// Deserialize the json returned by the API and show the info
    /// </summary>
    /// <param name="json"></param>
    /// <param name="mail"></param>
    void ProcessJSON(string json, string mail)
    {
        ProfileData profileData = JsonUtility.FromJson<ProfileData>(json);
        nickname.text = profileData.nickname;
        email.text = mail;
        missionsCompleted.text = profileData.completedMissions.ToString();
        historyBranches.text = profileData.completedBranches.ToString();
        points.text = profileData.points.ToString();
        
        Debug.Log($"<color=blue>Welcome {nickname.text} to RatsVsBats!!</color>");
    }
}
