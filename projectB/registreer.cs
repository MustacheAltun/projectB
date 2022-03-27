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
        //kijk of de gebruiker wilt registeren of een missclick heeft gemaakt
        Console.WriteLine("| [1] Terug | [2] Registreren |");
        Console.WriteLine("Kies de actie die u wilt uitvoeren:");
        //lees de input
        string register = Console.ReadLine();

        //als de input geen register of return is dan moet hij de input accepteren totdat de gebruiker een goede input geft
        while (register != "1" && register != "2")
        {
            //geef een error en wacht op de juiste input
            Console.WriteLine("Ongeldig Invoer!");
            register = Console.ReadLine();
        }


        //als register overeenkomt met de input "register" dan gaat hij in deze loop
        while (register == "2")
        {
            //pak alle informatie de de gebruiker doorgeeft en check gelijk of hij goed of fout is. geef de resultaat pas op het einde nadat alle data is ingevoerd
            Console.Clear();
            Console.WriteLine("Voer username in:");
            string username = Console.ReadLine();
            bool usernameCheck = CheckVerbodenLetters(verbodenKarakters, username);
            Console.WriteLine("Voer password in:");
            string password = Console.ReadLine();
            bool passwordCheck = CheckVerbodenLetters(verbodenKarakters, password);
            Console.WriteLine("Voer beveiliging woord in (voor wachtwoordvergeten):");
            string secretWord = Console.ReadLine();
            bool secretCheck = CheckVerbodenLetters(verbodenKarakters, secretWord);



            //als een van de volgende statements false zijn dan is de account fout
            if (secretCheck == false || passwordCheck == false || usernameCheck == false || password.Length < 8)
            {
                Console.WriteLine("Gebruikersnaam,Wachtwoord of Beveiliging is incorrect!");
                Thread.Sleep(2000);
                register = "";
                //herhaal de functie, want account heeft niet de juiste data
                RegistrerenFrontend(url, accountList);
            }else if(String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password) || String.IsNullOrEmpty(secretWord))
            {
                Console.WriteLine("een of meerdere input velden zijn leeg");
                Thread.Sleep(2000);
                register = "";
                //herhaal de functie, want account heeft niet de juiste data
                RegistrerenFrontend(url, accountList);
            }else if (CheckBestaandeNaam(accountList,username))
            {
                Console.WriteLine("naam bestaat al");
                Thread.Sleep(2000);
                register = "";
                //herhaal de functie, want account heeft niet de juiste data
                RegistrerenFrontend(url, accountList);
            }
            else
            {
                register = "";
                //hier maakt hij de account en mag hij terug naar de hoofdmenu
                RegistrerenMethode(url, accountList, username, password, secretWord, id, null);
                Console.WriteLine("Je account is gemaakt.");
                Thread.Sleep(1000);
                return;

            }
            Thread.Sleep(1000);
            Console.Clear();
        }
    }
}