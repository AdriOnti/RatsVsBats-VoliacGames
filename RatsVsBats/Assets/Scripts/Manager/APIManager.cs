using System;
using System.Collections;
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

    public async Task<string> GetUsersWhereEmailAsync(string userEmail)
    {
        return await GetRequestAsync($"Users/email?email={userEmail}");
    }

    public async Task<string> GetProfileWhereIdUserAsync(int idUser)
    {
        return await GetRequestAsync($"Profiles/{idUser}");
    }

    public async Task UpdateProfileAsync(int idProfiles, int missions, int points)
    {
        var jsonBody = new { id = idProfiles, missions = missions, points = points };
        var jsonBodyString = JsonUtility.ToJson(jsonBody);
        await PutRequestAsync($"Profiles/{idProfiles}?missions={missions}&points={points}", jsonBodyString);
    }

}
