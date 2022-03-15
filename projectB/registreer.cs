using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

public class Registratie
{
    public void RegistrerenMethode(string url, List<Account> accountList, string name, string password, string secret, int accountId, Ticket[] tickets = null)
	{
        //voeg de nieuwe informatie toe aan de bestaande lijst van de parameter
        accountList.Add(new Account()
        {
            id = accountId,
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
    public void RegistrerenFrontend(string url, List<Account> accountList)
    {
        int id = 0;
        foreach (dynamic item in accountList)
        {
            //Check de laatste id
            id = item.id + 1;
        }
        Console.Clear();
        //voer de informatie in
        Console.WriteLine("Voer username in:");
        string username = Console.ReadLine();
        Console.WriteLine("Voer password in:");
        string password = Console.ReadLine();
        Console.WriteLine("Voer beveiliging woord in (voor wachtwoordvergeten):");
        string secretWord = Console.ReadLine();

        //stuur de informatie naar de volgende methode
        RegistrerenMethode(url, accountList, username, password, secretWord,id, null);

        Console.WriteLine("Je account is gemaakt.");
        Thread.Sleep(1000);
    }
}
