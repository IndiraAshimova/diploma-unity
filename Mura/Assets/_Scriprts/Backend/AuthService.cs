using System.Collections;
using UnityEngine;

public class AuthService : BaseApiService
{
    public AuthService(APIConfig config)
        : base(config)
    {
    }

    public IEnumerator Register(
        RegisterData data,
        System.Action<bool> onResult)
    {
        string json =
            JsonUtility.ToJson(data);

        yield return SendPostRequest(
            config.Auth + "/register",
            json,
            (responseJson) =>
            {
                Debug.Log("Register success");

                onResult?.Invoke(true);
            });
    }

    public IEnumerator Login(
        LoginData data,
        System.Action<LoginResponse> onSuccess)
    {
        string json =
            JsonUtility.ToJson(data);

        yield return SendPostRequest(
            config.Auth + "/login",
            json,
            (responseJson) =>
            {
                LoginResponse response =
                    JsonUtility.FromJson<LoginResponse>(
                        responseJson);

                PlayerPrefs.SetString(
                    "jwt_token",
                    response.token);

                onSuccess?.Invoke(response);
            });
    }
}