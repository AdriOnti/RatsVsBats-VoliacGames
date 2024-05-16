using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class dbManager : MonoBehaviour
{
    public static dbManager instance;
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

    public async Task<string> PostRequestAsync(string endpoint, string jsonBody)
    {
        using (UnityWebRequest webRequest = new UnityWebRequest(apiUrl + endpoint, "POST"))
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

    public async void UpdateUserProfileAsync(int idProfiles, string jsonData)
    {
        await PostRequestAsync($"Profiles/{idProfiles}", jsonData);
    }

}
