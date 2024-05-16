using System;
using System.Data;
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
        //string tableName = "Profiles";
        //string[] columns = { "idProfiles", "nickname", "completedMissions", "completedBranches", "points" };
        //object[] values = { id, email };

        //if (Login.instance.isLogged) GetData(tableName, columns, values);
        if (Login.instance.isLogged) await CheckProfile(id, email);
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
    /// Get all the information from the SQL query
    /// </summary>
    /// <param name="tableName">Name of the table of the database</param>
    /// <param name="columns">Columns of the table</param>
    /// <param name="values">The values to check in the where</param>
    //public void GetData(string tableName, string[] columns, object[] values)
    //{
    //    // SELECT nickName, completedMissions, completedBranches, points FROM Profiles WHERE idProfiles = idUsers
    //    string query = $"SELECT {columns[1]}, {columns[2]}, {columns[3]}, {columns[4]} FROM {tableName} WHERE {columns[0]} = {values[0]}";
    //    DataSet resultDataSet = DatabaseManager.instance.ExecuteQuery(query);

    //    if (resultDataSet != null && resultDataSet.Tables.Count > 0 && resultDataSet.Tables[0].Rows.Count > 0)
    //    {
    //        DataRow row = resultDataSet.Tables[0].Rows[0];

    //        // Set the TMP text values with the result of the select query
    //        nickname.text = row[columns[1]].ToString();
    //        email.text = values[1].ToString();
    //        missionsCompleted.text = row[columns[2]].ToString();
    //        historyBranches.text = $"{row[columns[3]]}/{maxBranches}";
    //        points.text = row[columns[4]].ToString();

    //        Debug.Log("<color=green>Profile data get successfully</color>");
    //        Debug.Log($"<color=blue>Welcome {nickname.text} to RatsVsBats!!</color>");
    //    }
    //}

    void ProcessJSON(string json, string mail)
    {
        ProfileData profileData = JsonUtility.FromJson<ProfileData>(json);
        nickname.text = profileData.nickname;
        email.text = mail;
        missionsCompleted.text = profileData.completedMissions.ToString();
        historyBranches.text = profileData.completedBranches.ToString();
        points.text = profileData.points.ToString();
    }
}
