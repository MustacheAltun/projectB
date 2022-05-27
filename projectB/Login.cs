using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

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
    public int LoginScherm(List<Account> accountList)
    {
        Registratie accountMaken = new Registratie();
        string url = "..\\..\\..\\account.json";
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
                    Console.WriteLine("Voer uw gebruikersnaam in of voer * in om terug te gaan:");
                    string Username = Console.ReadLine();
                    if (Username == "*")
                    {
                        return LoginScherm(accountList);
                    }

                    Console.WriteLine("Voer uw wachtwoord in of voer * in om terug te gaan:");
                    string Password = Console.ReadLine();
                    if (Password == "*")
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
            }
            else if ((keuze == "1") || (keuze.ToLower() == "terug"))
            {
                return getId(accountList, "", "");
            }
            else if ((keuze == "3") || (keuze.ToLower() == "wachtwoord vergeten"))
            {
                Console.Clear();
                Console.WriteLine("*-*-*-*-*-*-*-*-*-*-*-*-*-*" + "\n" +
                                  "|   Wachtwoord vergeten   |" + "\n" +
                                  "*-*-*-*-*-*-*-*-*-*-*-*-*-*" + "\n");
                Console.WriteLine("Voor uw gebruikersnaam in of voer * in om the annuleren:");
                string oldName = Console.ReadLine();
                if (oldName != "*")
                {
                    Console.WriteLine("Voor uw beveligingswoord in of voer * in om the annuleren:");
                    string oldSec = Console.ReadLine();
                    if (oldSec != "*")
                    {
                        ChangePass(accountList, oldName, oldSec);
                    }
                    else
                    {
                        Console.WriteLine("Actie geannuleerd");
                    }
                }
                else
                {
                    Console.WriteLine("Actie geannuleerd");
                }
                Thread.Sleep(2000);
                keuze = "";
            }

        }
        return -1;
    }








    private int getId(List<Account> accountList, string username, string password)
    {
        //Itereren door elke item van Json list.
        foreach (dynamic item in accountList)
        {
            //Check of gebruikersnaam en wachtwoord matchen
            if (item.username.ToLower() == username.ToLower() && item.password == password)
                return item.id;
        }
        return -1;
    }
    private static void ChangePass(List<Account> accountList, string username, string security)
    {
        string[] verbodenKarakters = new string[27] { " ", ",", ".", "/", "'", "\"", ";", ":", "`", "~", "!", "@", "#", "$", "%", "^", "&", "*", "(", ")", "-", "_", "+", "=", "<", ">", "?" };
        //Itereren door elke item van Json list.
        bool InJson = false;
        int i = 0;
        foreach (dynamic item in accountList)
        {
            //Check of gebruikersnaam en wachtwoord matchen

            if (item.username.ToLower() == username.ToLower() && item.security == security)
            {
                InJson = true;
                i = item.id;
                break;
            }
        }
        if(InJson)
        {
            string newPass = "";
            Console.WriteLine("Voer uw nieuwe wachtwoord in");
            while ((newPass.Length < 8) || (verbodenKarakters.Contains(newPass)))
            {
                newPass = Console.ReadLine();
                if ((newPass.Length < 8) || (verbodenKarakters.Contains(newPass)))
                {
                    Console.WriteLine("Uw nieuwe wachtwoord is kleiner dan 9 karakters en/of bevat verboden karakters");
                }
            }
            accountList[i].password = newPass;
            string accList = JsonConvert.SerializeObject(accountList, Formatting.Indented);
            //verander de hele file met de nieuwe json informatie
            File.WriteAllText("..\\..\\..\\account.json", accList);
            Console.WriteLine("Uw wachtwoord is aangepast!");
            Thread.Sleep(500);
        }
        else
        {
            Console.WriteLine("Gebruikersnaam of beveiligingswoord is ongeldig!");
            Thread.Sleep(500);
        }
    }
}
    
