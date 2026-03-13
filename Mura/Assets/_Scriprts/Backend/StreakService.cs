using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class StreakService : MonoBehaviour
{
    private string streakUrl = APIConfig.STREAK;

    // Получаем streak с сервера и обновляем PlayerModel
    public IEnumerator GetStreak(System.Action<int> onSuccess = null)
    {
        UnityWebRequest request = UnityWebRequest.Get(streakUrl);
        request.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("jwt_token"));

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            int streak = int.Parse(request.downloadHandler.text);
            PlayerModel.Instance.UpdateStreak(streak);
            onSuccess?.Invoke(streak);
        }
        else
        {
            Debug.LogError("Streak Error: " + request.error);
        }
    }
}