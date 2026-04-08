using System.Collections;
using UnityEngine;

public class ProfileService : BaseApiService
{
    public ProfileService(APIConfig config)
        : base(config)
    {
    }

    public IEnumerator GetProfile(
        System.Action<UserProfileResponse> onSuccess)
    {
        yield return SendGetRequest(
            config.Profile,
            (json) =>
            {
                var profile =
                    JsonUtility.FromJson<
                        UserProfileResponse>(json);

                PlayerModel.Instance
                    .UpdatePlayerData(
                        profile.xp,
                        profile.level,
                        profile.streak);

                onSuccess?.Invoke(profile);
            });
    }
}