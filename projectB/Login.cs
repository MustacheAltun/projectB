using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

public class Login
{
    public bool loginMethod (List<Account> accountList, string username, string password)
    {
           
        //Itereren door elke item van Json list.
        foreach(dynamic item in accountList)
        {
            //Check of gebruikersnaam en wachtwoord matchen
            if(item.username.ToLower() == username.ToLower() && item.password == password)
               return true;
        }
        return false;
       
    }
    public void loginScherm (List<Account> accountList)
    {
        //Input voor gebruikersnaam en wachtwoord
        Console.WriteLine("Voer username in:");
        string Username = Console.ReadLine();

        Console.WriteLine("Voer password in:");
        string Password = Console.ReadLine();

        //Gebruikt loginMethod om te checken of het juist of fout is
        if (loginMethod(accountList, Username, Password))
            Console.WriteLine("U bent Ingelogd" +" "+ Username);
        else
            Console.WriteLine("Gebruikernaam of wachtwoord is onjuist");
    }

}
