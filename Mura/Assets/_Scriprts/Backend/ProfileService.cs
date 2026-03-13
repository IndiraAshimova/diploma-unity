using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class ProfileService : MonoBehaviour
{
    private string profileUrl = APIConfig.PROFILE;

    public IEnumerator GetProfile(System.Action<UserProfileResponse> onSuccess)
    {
        UnityWebRequest request = UnityWebRequest.Get(profileUrl);
        request.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("jwt_token"));

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            UserProfileResponse profile = JsonUtility.FromJson<UserProfileResponse>(request.downloadHandler.text);
            Debug.Log("PROFILE RAW: " + request.downloadHandler.text);

            PlayerModel.Instance.UpdatePlayerData(profile.xp, profile.level, profile.streak);

            onSuccess?.Invoke(profile);
        }
        else
        {
            Debug.LogError("Profile Error: " + request.error);
        }
    }
}