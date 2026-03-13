using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class XPService : MonoBehaviour
{
    private string xpUrl = APIConfig.XP_ADD;

    public IEnumerator AddXP(int amount, System.Action<int, int> onXPUpdated = null)
    {
        string json = "{\"amount\":" + amount + "}";

        UnityWebRequest request = new UnityWebRequest(xpUrl, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);

        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();

        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("jwt_token"));

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            XPResponse response = JsonUtility.FromJson<XPResponse>(request.downloadHandler.text);
            PlayerModel.Instance.UpdateXP(response.xp, response.level);
            onXPUpdated?.Invoke(response.xp, response.level);
            Debug.Log($"XP успешно обновлён: +{amount} | XP={response.xp} Level={response.level}");
        }
        else
        {
            Debug.LogError("Ошибка отправки XP: " + request.error);
        }
    }
}