public class APIConfig
{
    public string BaseUrl { get; }

    public string Auth => BaseUrl + "/auth";
    public string Profile => BaseUrl + "/profile";
    public string XPAdd => BaseUrl + "/xp/add";
    public string Streak => BaseUrl + "/streak";

    public APIConfig(string baseUrl)
    {
        BaseUrl = baseUrl;
    }
}