using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

public class Login
{
    private static void ChangePass(List<Account> accountList)
    {
        Registratie registerFunction = new Registratie();
        string[] verbodenKarakters = new string[30] { " ", ",", ".", "/", "'", ";", ":", "`", "~", "!", "@", "#", "$", "%", "^", "&", "*", "(", ")", "-", "_", "+", "=", "<", ">", "?", "{", "}", "[", "]" };
        Console.Clear();
        string url = "..\\..\\..\\account.json";
        Console.WriteLine("*-*-*-*-*-*-*-*-*-*-*-*-*-*" + "\n" +
                          "|   Wachtwoord vergeten   |" + "\n" +
                          "*-*-*-*-*-*-*-*-*-*-*-*-*-*" + "\n");
        Console.WriteLine("--------------------------------------------------------");
        Console.WriteLine("Voor uw gebruikersnaam in of voer * in om the annuleren:");
        Console.WriteLine("--------------------------------------------------------");
        string oldName = Console.ReadLine();
        if (oldName == "*")
        {
            return;
        }
        bool accountFound = false;
        foreach (var accounts in accountList)
        {
            if (accounts.username == oldName)
            {
                accountFound = true;
            }
        }
        while (accountFound == false)
        {
            Console.WriteLine("Account met deze gebruikersnaam niet gevonden!");
            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine("Voor uw gebruikersnaam in of voer * in om the annuleren:");
            Console.WriteLine("--------------------------------------------------------");
            oldName = Console.ReadLine();
            if (oldName == "*")
            {
                return;
            }
            foreach (var accounts in accountList)
            {
                if (accounts.username.ToLower() == oldName.ToLower())
                {
                    accountFound = true;
                }
            }
        }
        Console.WriteLine("--------------------------------------------------------");
        Console.WriteLine("Voor uw beveiliginscode in of voer * in om the annuleren:");
        Console.WriteLine("--------------------------------------------------------");
        string oldSec = Console.ReadLine();
        if (oldSec == "*")
        {
            return;
        }
        bool secFound = false;
        foreach (var accounts in accountList)
        {
            if (accounts.security.ToLower() == oldSec.ToLower() && accounts.username.ToLower() == oldName.ToLower())
            {
                secFound = true;
            }
        }
        while (secFound == false)
        {
            Console.WriteLine("Account met deze beveiliginscode niet gevonden!");
            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine("Voor uw beveiliginscode in of voer * in om the annuleren:");
            Console.WriteLine("--------------------------------------------------------");
            oldSec = Console.ReadLine();
            if (oldSec == "*")
            {
                return;
            }
            foreach (var accounts in accountList)
            {
                if (accounts.security.ToLower() == oldSec.ToLower() && accounts.username.ToLower() == oldName.ToLower())
                {
                    secFound = true;
                }
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
            if (password.Length < 9 && registerFunction.CheckVerbodenLetters(verbodenKarakters, password) == false)
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
            else if (registerFunction.CheckVerbodenLetters(verbodenKarakters, password) == false)
            {
                Console.WriteLine("Wachtwoord bevat verboden karakters!");
                Console.WriteLine("---------------------------------------------------------------------------------");
                Console.WriteLine("Voer uw gewenste wachtwoord in of voer * in om te annuleren:");
                Console.WriteLine("---------------------------------------------------------------------------------");
            }
            else
            {
                passwordcheck = true;
                foreach (var accounts in accountList)
                {
                    if (accounts.security.ToLower() == oldSec.ToLower() && accounts.username.ToLower() == oldName.ToLower())
                    {
                        accounts.password = password;
                    }
                }
                string convertedJson = JsonConvert.SerializeObject(accountList, Formatting.Indented);
                //verander de hele file met de nieuwe json informatie
                File.WriteAllText(url, convertedJson);
            }
        }

    }
    public static bool loginMethod (List<Account> accountList, string username, string password)
    {
        
        //Itereren door elke item van Json list.
        foreach(dynamic item in accountList)
        {
            //Check of gebruikersnaam en wachtwoord matchen
            if(item.username == username && item.password == password)
               return true;
        }
        return false;
       
    }
    public int LoginScherm(List<Account> accountList)
    {
        Registratie accountMaken = new Registratie();
        string url = "..\\..\\..\\account.json";
        string strResultJson = File.ReadAllText("..\\..\\..\\account.json");

        // maak een lijst van alle informatie die er is
        List<Account> jsonList = JsonConvert.DeserializeObject<List<Account>>(strResultJson);
        accountList = jsonList;
        string keuze = "";
        while(keuze != "1")
        {
            Console.Clear();
            string[] ArrayOpties = new string[] { "inloggen", "1", "2", "3", "4", "wachtwoord vergeten", "registreren", "terug" };
            string menu = "*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*" + "\n" + "|  [1] Terug |  [2] Inloggen | [3] Wachtwoord Vergeten | [4] Registreren  |" + "\n" +
                "---------------------------------------------------------------------------";
            while (!ArrayOpties.Contains(keuze))
            {
                Console.Clear();
                Console.WriteLine(menu);
                Console.WriteLine("Kies een van de bovenstaande opties.");
                keuze = Console.ReadLine();

                if (!ArrayOpties.Contains(keuze))
                {
                    Console.WriteLine("Kies a.u.b. een van de bovenstaande opties.");
                    Thread.Sleep(1000);
                }
            }
            //Input voor gebruikersnaam en wachtwoord
            if ((keuze == "2") || (keuze.ToLower() == "inloggen"))
            {
                while (keuze == "2")
                {

                    Console.Clear();
                    Console.WriteLine("*-*-*-*-*-*-*-*-*" + "\n" +
                                      "|   Inloggen    |" + "\n" +
                                      "*-*-*-*-*-*-*-*-*" + "\n");
                    Console.WriteLine("Voer uw gebruikersnaam in of voer '*' in om terug te gaan:");
                    string Username = Console.ReadLine();
                    while (Username.Trim().Length == 0)
                    {
                        Console.WriteLine("Voer in een geldige gebruikersnaam a.u.b!");
                        Username = Console.ReadLine();
                    }
                    if (Username.Trim() == "*")
                    {
                        return LoginScherm(accountList);
                    }

                    Console.WriteLine("Voer uw wachtwoord in of voer '*' in om terug te gaan:");
                    string Password = Console.ReadLine();
                    while (Password.Trim().Length == 0)
                    {
                        Console.WriteLine("Voer in een geldige wachtwoord a.u.b!");
                        Password = Console.ReadLine();
                    }
                    if (Password.Trim() == "*")
                    {
                        break;
                    }
                    //Gebruikt loginMethod om te checken of het juist of fout is
                    if (loginMethod(accountList, Username, Password))
                    {
                        Console.WriteLine("U bent ingelogd" + " " + Username);
                        //Thread.Sleep(1000);
                        return getId(accountList, Username, Password);
                    }
                    else
                    {
                        Console.WriteLine("Gebruikernaam en/of wachtwoord is onjuist");
                        Thread.Sleep(1000);
                    }
                }
            }
            else if ((keuze == "4") || (keuze.ToLower() == "registreren"))
            {
                accountMaken.RegistrerenFrontend(url, accountList);
                keuze = "";
                break;
            }
            else if ((keuze == "1") || (keuze.ToLower() == "terug"))
            {
                return getId(accountList, "", "");
            }
            else if ((keuze == "3") || (keuze.ToLower() == "wachtwoord vergeten"))
            {
                ChangePass(accountList);
                keuze = "";
            }

        }
        return -1;
    }








    public static int getId(List<Account> accountList, string username, string password)
    {
        //Itereren door elke item van Json list.
        foreach (dynamic item in accountList)
        {
            //Check of gebruikersnaam en wachtwoord matchen
            if (item.username == username && item.password == password)
                return item.id;
        }
        return -1;
    }
   
}
    
