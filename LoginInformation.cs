using System;

public class LoginInformation
{
    public string Website { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }

    public LoginInformation(string website, string username, string password)
    {
        Website = website;
        Username = username;
        Password = password;
    }

    public LoginInformation() {
        Website = "";
        Username = "";
        Password = "";
    }

    //Might handle self encryption later? Undecided
}
