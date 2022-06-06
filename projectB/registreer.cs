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
            if (item.username == naam)
            {
                return true;
            }
        }
        return false;
    }
    public void RegistrerenFrontend(string url, List<Account> accountList)
    {
        //onthoudt alle verboden characters
        string[] verbodenKarakters = new string[30] { " ", ",", ".", "/", "'", ";", ":", "`", "~", "!", "@", "#", "$", "%", "^", "&", "*", "(", ")", "-", "_", "+", "=", "<", ">", "?", "{" , "}" , "[", "]"};
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
        string username = "";
        bool usernameCheck = false;
        string verboden = "";
        string s = "";
        foreach(var character in verbodenKarakters)
        {
            verboden += character + " + ";
            if(character == verbodenKarakters[verbodenKarakters.Length - 1])
            {
                s = "Verboden Karakters:" + " " + verboden + "\n\n";
                Console.WriteLine(s);
            }
        }
        Console.WriteLine("---------------------------------------Account Specificaties-----------------------------------------");
        Console.WriteLine("| Gebruikersnaam mag niet een bestaande naam hebben en ook geen verboden karakters!                 |");
        Console.WriteLine("| Wachtwoord mag niet korter dan 9 karakters zijn en mag ook geen verboden karakters bevatten!      |");
        Console.WriteLine("| Beveiligingscode mag niet korter dan 9 karakters zijn en mag ook geen verboden karakters bevatten!|\n-----------------------------------------------------------------------------------------------------\n\n");
        Console.WriteLine("---------------------------------------------------------------------------------");
        Console.WriteLine("Voer uw gewenste gebruikersnaam in of voer * in om te annuleren:");
        Console.WriteLine("---------------------------------------------------------------------------------");
        while (usernameCheck == false && username != "*")
        {
            username = Console.ReadLine();
            if(username == "*")
            {
                return;
                break;
            }
            if (CheckBestaandeNaam(accountList, username) && CheckVerbodenLetters(verbodenKarakters, username) == false)
            {
                Console.WriteLine("Gebruikersnaam bestaat al en bevat verboden karakters!");
                Console.WriteLine("---------------------------------------------------------------------------------");
                Console.WriteLine("Voer uw gewenste gebruikersnaam in of voer * in om te annuleren:");
                Console.WriteLine("---------------------------------------------------------------------------------");
            }
            else if (CheckBestaandeNaam(accountList, username))
            {
                Console.WriteLine("Gebruikersnaam bestaat al!");
                Console.WriteLine("---------------------------------------------------------------------------------");
                Console.WriteLine("Voer uw gewenste gebruikersnaam in of voer * in om te annuleren:");
                Console.WriteLine("---------------------------------------------------------------------------------");
            }
            else if (CheckVerbodenLetters(verbodenKarakters, username) == false)
            {
                Console.WriteLine("Gebruikersnaam bevat verboden karakters!");
                Console.WriteLine("---------------------------------------------------------------------------------");
                Console.WriteLine("Voer uw gewenste gebruikersnaam in of voer * in om te annuleren:");
                Console.WriteLine("---------------------------------------------------------------------------------");
            }
            else
            {
                usernameCheck = true;
            }
        }
        string password = "";
        bool passwordcheck = false;
        Console.WriteLine("---------------------------------------------------------------------------------");
        Console.WriteLine("Voer uw gewenste wachtwoord in of voer * in om te annuleren:");
        Console.WriteLine("---------------------------------------------------------------------------------");
        while (passwordcheck == false && password != "*")
        {
            password = Console.ReadLine();
            if (password == "*")
            {
                return;
                break;
            }
            if (password.Length < 9 && CheckVerbodenLetters(verbodenKarakters, password) == false)
            {
                Console.WriteLine("Wachtwoord is kleiner dan 9 karakter en bevat verboden karakters!");
                Console.WriteLine("---------------------------------------------------------------------------------");
                Console.WriteLine("Voer uw gewenste wachtwoord in of voer * in om te annuleren:");
                Console.WriteLine("---------------------------------------------------------------------------------");
            }
            else if (password.Length < 9)
            {
                Console.WriteLine("Wachtwoord is kleiner dan 9 karakters!");
                Console.WriteLine("---------------------------------------------------------------------------------");
                Console.WriteLine("Voer uw gewenste wachtwoord in of voer * in om te annuleren:");
                Console.WriteLine("---------------------------------------------------------------------------------");
            }
            else if (CheckVerbodenLetters(verbodenKarakters, password) == false)
            {
                Console.WriteLine("Wachtwoord bevat verboden karakters!");
                Console.WriteLine("---------------------------------------------------------------------------------");
                Console.WriteLine("Voer uw gewenste wachtwoord in of voer * in om te annuleren:");
                Console.WriteLine("---------------------------------------------------------------------------------");
            }
            else
            {
                passwordcheck = true;
            }
        }
        string security = "";
        bool securitCheck = false;
        Console.WriteLine("---------------------------------------------------------------------------------");
        Console.WriteLine("Voer uw gewenste Beveilingscode in of voer * in om te annuleren:");
        Console.WriteLine("---------------------------------------------------------------------------------");
        while (securitCheck == false && security != "*")
        {
            security = Console.ReadLine();
            if (security == "*")
            {
                return;
                break;
            }
            if (security.Length < 9 && CheckVerbodenLetters(verbodenKarakters, security) == false)
            {
                Console.WriteLine("Beveilingscode is kleiner dan 9 karakter en bevat verboden karakters!");
                Console.WriteLine("---------------------------------------------------------------------------------");
                Console.WriteLine("Voer uw gewenste Beveilingscode in of voer * in om te annuleren:");
                Console.WriteLine("---------------------------------------------------------------------------------");
            }
            else if (security.Length < 9)
            {
                Console.WriteLine("Beveilingscode is kleiner dan 9 karakters!");
                Console.WriteLine("---------------------------------------------------------------------------------");
                Console.WriteLine("Voer uw gewenste Beveilingscode in of voer * in om te annuleren:");
                Console.WriteLine("---------------------------------------------------------------------------------");
            }
            else if (CheckVerbodenLetters(verbodenKarakters, security) == false)
            {
                Console.WriteLine("Beveilingscode bevat verboden karakters!");
                Console.WriteLine("---------------------------------------------------------------------------------");
                Console.WriteLine("Voer uw gewenste Beveilingscode in of voer * in om te annuleren:");
                Console.WriteLine("---------------------------------------------------------------------------------");
            }
            else
            {
                securitCheck = true;
            }
        }
        RegistrerenMethode(url,accountList,username,password,security,id);
        Console.WriteLine("Account Sucessvol gecreerd!");
        return;
        Thread.Sleep(1000);
    }
}