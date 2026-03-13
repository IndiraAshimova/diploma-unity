[System.Serializable]
public class RegisterData
{
    public string email;
    public string username;
    public string password;
}

[System.Serializable]
public class LoginData
{
    public string email;
    public string password;
}

[System.Serializable]
public class LoginResponse
{
    public string token;
}

[System.Serializable]
public class UserProfileResponse
{
    public string email;
    public string username;
    public int xp;
    public int level;
    public int streak;
}

[System.Serializable]
public class XPResponse
{
    public int xp;
    public int level;
    public int streak;
}

[System.Serializable]
public class XPRequest
{
    public int amount;
}