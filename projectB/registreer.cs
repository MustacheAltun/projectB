using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

public class Registratie
{
    public void Registreren(string url, List<Account> accountList, string name, string password, string secret, Ticket[] tickets = null)
	{
        //voeg de nieuwe informatie toe aan de bestaande lijst van de parameter
        accountList.Add(new Account()
        {
            username = name,
            password = password,
            security = secret,
            tickets = tickets
        });
        //verdander de lijst naar een json type
        var convertedJson = JsonConvert.SerializeObject(accountList, Formatting.Indented);
        //verander de hele file met de nieuwe json informatie
        File.WriteAllText(url, convertedJson);
    }
}
