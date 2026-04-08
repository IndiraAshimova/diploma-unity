using System.Collections;

public class StreakService : BaseApiService
{
    public StreakService(APIConfig config)
        : base(config)
    {
    }

    public IEnumerator GetStreak(
        System.Action<int> onSuccess = null)
    {
        yield return SendGetRequest(
            config.Streak,
            (json) =>
            {
                int streak =
                    int.Parse(json);

                PlayerModel.Instance
                    .UpdateStreak(streak);

                onSuccess?.Invoke(streak);
            });
    }
}