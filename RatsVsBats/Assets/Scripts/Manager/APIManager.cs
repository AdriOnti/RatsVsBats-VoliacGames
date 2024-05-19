using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class APIManager : MonoBehaviour
{
    public static APIManager instance;
    private string apiUrl = "https://rvbvoliacgamesapi.azurewebsites.net/api/";

    private void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// Function to make a SELECT query to the API.
    /// </summary>
    /// <param name="endpoint">Endpoint of the API</param>
    public async Task<string> GetRequestAsync(string endpoint)
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(apiUrl + endpoint);
        UnityWebRequestAsyncOperation asyncOperation = webRequest.SendWebRequest();

        while (!asyncOperation.isDone)
        {
            await Task.Yield();
        }

        if (webRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error: " + webRequest.error);
            throw new Exception("Error: " + webRequest.error);
        }

        return webRequest.downloadHandler.text;
    }

    /// <summary>
    /// Function to make a UPDATE query to the API
    /// </summary>
    /// <param name="endpoint">Endpoint of the API</param>
    /// <param name="jsonBody">A json body with the new parameters</param>
    public async Task<string> PutRequestAsync(string endpoint, string jsonBody)
    {
        using (UnityWebRequest webRequest = new UnityWebRequest(apiUrl + endpoint, "PUT"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonBody);
            webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");

            UnityWebRequestAsyncOperation asyncOperation = webRequest.SendWebRequest();

            while (!asyncOperation.isDone)
            {
                await Task.Yield();
            }

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + webRequest.error);
                throw new Exception("Error: " + webRequest.error);
            }

            return webRequest.downloadHandler.text;
        }
    }

    /// <summary>
    /// Get the user info using the email
    /// </summary>
    /// <param name="userEmail">The email written in the login input field</param>
    public async Task<string> GetUsersWhereEmailAsync(string userEmail)
    {
        return await GetRequestAsync($"Users/email?email={userEmail}");
    }

    /// <summary>
    /// Get all the data from the profile using the idUser
    /// </summary>
    /// <param name="idUser">The id gived by GetUsersWhereEmailAsync</param>
    public async Task<string> GetProfileWhereIdUserAsync(int idUser)
    {
        return await GetRequestAsync($"Profiles/{idUser}");
    }

    /// <summary>
    /// Function to update the missions and points of the profile
    /// </summary>
    /// <param name="idProfiles">Id of the profile</param>
    /// <param name="missions">Number of the missions completed</param>
    /// <param name="points">Points gained</param>
    public async Task UpdateProfileAsync(int idProfiles, int missions, int points)
    {
        var jsonBody = new { id = idProfiles, missions = missions, points = points };
        var jsonBodyString = JsonUtility.ToJson(jsonBody);
        await PutRequestAsync($"Profiles/{idProfiles}?missions={missions}&points={points}", jsonBodyString);
    }
}
