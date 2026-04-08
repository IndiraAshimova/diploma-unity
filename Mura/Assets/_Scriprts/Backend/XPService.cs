using System.Collections;
using UnityEngine;

public class XPService : BaseApiService, IXPService
{
    public XPService(APIConfig config) : base(config) { }

    // Теперь принимает callback с XPResponse
    public IEnumerator AddXP(int amount, System.Action<XPResponse> onSuccess)
    {
        XPRequest requestData = new XPRequest { amount = amount };
        string json = JsonUtility.ToJson(requestData);

        yield return SendPostRequest(config.XPAdd, json, (responseJson) =>
        {
            XPResponse response = JsonUtility.FromJson<XPResponse>(responseJson);
            onSuccess?.Invoke(response);
            Debug.Log($"XPService: Получен ответ с сервера XP={response.xp}, Level={response.level}, Streak={response.streak}");
        });
    }
}