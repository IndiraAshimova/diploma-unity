public class ServiceFactory
{
    private APIConfig config;

    public ProfileService Profile { get; }
    public XPService XP { get; }
    public StreakService Streak { get; }
    public AuthService Auth { get; }

    public ServiceFactory()
    {
        config =
            new APIConfig(
                "https://diploma-server-xnr4.onrender.com/api");

        Profile =
            new ProfileService(config);

        XP =
            new XPService(config);

        Streak =
            new StreakService(config);

        Auth =
            new AuthService(config);
    }
}