using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            rol = "gebruiker",
            tickets = tickets
        });
        //verdander de lijst naar een json type
        string convertedJson = JsonConvert.SerializeObject(accountList, Formatting.Indented);
        //verander de hele file met de nieuwe json informatie
        File.WriteAllText(url, convertedJson);
    }
    public bool CheckVerbodenLetters(string[] array, string text)
    {
        //check of iets in de array voor komt bij de text
        foreach (var item in array)
        {
            if (text.Contains(item))
            {
                return false;
            }
        }
        return true;
    }
    public bool CheckBestaandeNaam(List<Account> accountList, string naam)
    {
        //check of iets in de array voor komt bij de text
        foreach (dynamic item in accountList)
        {
            //Check de laatste id
            //id = item.id + 1;
            if (item.username.ToLower() == naam.ToLower())
            {
                return true;
            }
        }
        return false;
    }
    public void RegistrerenFrontend(string url, List<Account> accountList)
    {
        //onthoudt alle verboden characters
        string[] verbodenKarakters = new string[27] { " ", ",", ".", "/", "'", "\"", ";", ":", "`", "~", "!", "@", "#", "$", "%", "^", "&", "*", "(", ")", "-", "_", "+", "=", "<", ">", "?" };
        //om de account een ID te geven is er een forloop om te checken wat de laatste id is
        int id = 0;

        foreach (dynamic item in accountList)
        {
            //Check de laatste id
            id = item.id + 1;
        }

        //maak de cmd schoon van wat er ervoor is getypt
        Console.Clear();
        //pak alle informatie de de gebruiker doorgeeft en check gelijk of hij goed of fout is. geef de resultaat pas op het einde nadat alle data is ingevoerd
        Console.WriteLine("*-*-*-*-*-*-*-*-*-*" + "\n" +
                          "|   Registeren    |" + "\n" +
                          "*-*-*-*-*-*-*-*-*-*" + "\n");
        Console.WriteLine("Voer uw gewenste gebruikersnaam in:");
        string username = Console.ReadLine();
        bool usernameCheck = CheckVerbodenLetters(verbodenKarakters, username);
        Console.WriteLine("Voer uw gewenste wachtwoord in:");
        string password = Console.ReadLine();
        bool passwordCheck = CheckVerbodenLetters(verbodenKarakters, password);
        Console.WriteLine("Als u uw wachtwoord vergeet moet u als controle een beveiligingswoord invoeren." + "\n" + "Voer uw gewenste beveiligingswoord in:");
        string secretWord = Console.ReadLine();
        bool secretCheck = CheckVerbodenLetters(verbodenKarakters, secretWord);



        //als een van de volgende statements false zijn dan is de account fout
        if (secretCheck == false || passwordCheck == false || usernameCheck == false)
        {
            Console.WriteLine("Gebruikersnaam, wachtwoord en/of beveiligingswoord is incorrect!");
            Thread.Sleep(2000);
            //herhaal de functie, want account heeft niet de juiste data
        }
        else if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password) || String.IsNullOrEmpty(secretWord))
        {
            Console.WriteLine("Een of meerdere velden zijn niet ingevuld");
            Thread.Sleep(2500);
            //herhaal de functie, want account heeft niet de juiste data
        }
        else if (CheckBestaandeNaam(accountList, username))
        {
            Console.WriteLine("Uw gekozen gebruikersnaam bestaat al. Kies a.u.b. een andere gebruikersnaam");
            Thread.Sleep(2500);
            //herhaal de functie, want account heeft niet de juiste data
        }
        else if (password.Length < 8) 
        {
            Console.WriteLine("Uw wachtwoord is kleiner dan 9 karakters" + "\n" + "Voer een wachtwoord in dat groter  is dan 8 karakters.");
            Thread.Sleep(3000);
        } 
        else
        {
            //hier maakt hij de account en mag hij terug naar de hoofdmenu
            RegistrerenMethode(url, accountList, username, password, secretWord, id, null);
            Console.WriteLine("Uw account is aangemaakt.");
            Thread.Sleep(1500);
            return;

        }
        Thread.Sleep(1000);
        Console.Clear();
    }
}