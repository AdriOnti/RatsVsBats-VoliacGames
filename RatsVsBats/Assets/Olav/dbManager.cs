using System;
using System.Collections;
using System.Collections.Generic;
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

    public IEnumerator GetRequest(string endpoint, Action<string> callback)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(apiUrl + endpoint))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                string responseBody = webRequest.downloadHandler.text;
                callback(responseBody);
            }
        }
    }

    public IEnumerator PostRequest(string endpoint, string jsonBody, Action<string> callback)
    {
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonBody);

        using (UnityWebRequest webRequest = UnityWebRequest.PostWwwForm(apiUrl + endpoint, "POST"))
        {
            webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");

            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                string responseBody = webRequest.downloadHandler.text;
                callback(responseBody);
            }
        }
    }

    public void GetUsersWhereEmail(string userEmail, Action<string> callback)
    {
        StartCoroutine(GetRequest($"Users/email?email={userEmail}", callback));
    }

    public void UpdateUserProfile(int idProfiles, string jsonData, Action<string> callback)
    {
        StartCoroutine(PostRequest($"Profiles/{idProfiles}", jsonData, callback));
    }

}
