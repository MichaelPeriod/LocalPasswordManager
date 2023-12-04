using System;

public class LoginInformation
{
    private readonly string _website;
    private readonly string _username;
    private readonly string _password;

    public LoginInformation(string website, string username, string password)
    {
        _website = website;
        _username = username;
        _password = password;
    }

    public String GetWebsite()
    {
        return _website;
    }

    public String GetUsername() { return _username; }

    public String GetPassword() { return _password; } //used for encryption later
}
