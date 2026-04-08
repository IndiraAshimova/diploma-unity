using UnityEngine.Networking;
using System.Collections;
using UnityEngine;

public abstract class BaseApiService
{
    protected APIConfig config;

    protected BaseApiService(APIConfig config)
    {
        this.config = config;
    }

    protected IEnumerator SendGetRequest(
        string url,
        System.Action<string> onSuccess)
    {
        Debug.Log("GET Request: " + url);

        UnityWebRequest request =
            UnityWebRequest.Get(url);

        AddAuthHeader(request);

        yield return request.SendWebRequest();

        if (request.result ==
            UnityWebRequest.Result.Success)
        {
            Debug.Log(
                "GET Success: " +
                request.downloadHandler.text);

            onSuccess?.Invoke(
                request.downloadHandler.text);
        }
        else
        {
            Debug.LogError(
                "GET Error: " +
                request.error);
        }
    }

    protected IEnumerator SendPostRequest(
        string url,
        string json,
        System.Action<string> onSuccess)
    {
        UnityWebRequest request =
            new UnityWebRequest(url, "POST");

        byte[] body =
            System.Text.Encoding.UTF8.GetBytes(json);

        request.uploadHandler =
            new UploadHandlerRaw(body);

        request.downloadHandler =
            new DownloadHandlerBuffer();

        request.SetRequestHeader(
            "Content-Type",
            "application/json");

        AddAuthHeader(request);

        yield return request.SendWebRequest();

        if (request.result ==
            UnityWebRequest.Result.Success)
        {
            onSuccess?.Invoke(
                request.downloadHandler.text);
        }
        else
        {
            UnityEngine.Debug.LogError(
                "POST Error: " + request.error);
        }
    }

    private void AddAuthHeader(
        UnityWebRequest request)
    {
        string token =
            PlayerPrefs.GetString("jwt_token");

        if (!string.IsNullOrEmpty(token))
        {
            request.SetRequestHeader(
                "Authorization",
                "Bearer " + token);
        }
    }
}