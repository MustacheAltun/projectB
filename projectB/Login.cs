using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

public class Login
{
    public bool loginMethod (List<Account> accountList, string username, string password)
    {
        //Boolean om inlogstatus te checken.
        bool loggedIn = false;
        
        //Itereren door elke item van Json list.
        foreach(dynamic item in accountList)
        {
            //Check of gebruikersnaam en wachtwoord matchen
            if(item.username.ToLower() == username.ToLower() && item.password == password)
               loggedIn = true;
        }
        return loggedIn;
        
        
    }
}
