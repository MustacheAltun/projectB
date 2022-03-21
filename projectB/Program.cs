using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace projectB
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
                  

            //instantiate de class
            Registratie accountMaken = new Registratie();

            //object gemaakt van class Login
            Login accountInloggen = new Login();

            Locatie locatie = new Locatie();

            // geef de url van de json
            string url = "..\\..\\..\\account.json";

            // lees de file en zet alles in een string
            string strResultJson = File.ReadAllText(url);


            // maak een lijst van alle informatie die er is
            List<Account> jsonList = JsonConvert.DeserializeObject<List<Account>>(strResultJson);

            //voeg een lijst van tickets toe als je dat wilt, het hoeft niet
            Ticket[] tickelijst = new Ticket[]
            {
                new Ticket() { id = 3, name = "Red" },
                new Ticket() { id = 4, name = "Black" },
                new Ticket() { id = 5, name = "Yellow" }
            };

            //Als je dit roept ga je checken of de naam en wachtwoord die je hebt ingevoerd komt voor in Json file.
            //Console.WriteLine(accountInloggen.loginMethod(jsonList, username, password));
            //accountInloggen.loginScherm(jsonList);
            //geef alles naar de registreer funtie
            //note: ticketlijst heeft een default value, dus je kan die leeg laten als je wilt
            //note: als je dit nog een keer runned maakt hij een account, als dit niet wilt dan moet je lijn 36 commenten
            HoofdScherm hoofdScherm = new HoofdScherm();
            bool loggedIn = false;
            while (true)
            {
                string page = hoofdScherm.GebruikerHoofdscherm(loggedIn);
                if (page == "Inloggen")
                {
                    loggedIn = accountInloggen.loginScherm(jsonList);
                }
                else if (page == "Registreren")
                {
                    accountMaken.RegistrerenFrontend(url, jsonList);
                }
                else if (page == "Uitloggen")
                {
                    loggedIn = false;
                }
                else if (page == "Locaties")
                {
                    locatie.viewLocations();
                }
            }
            //accountMaken.RegistrerenFrontend(url, jsonList);

            //loggedIn = accountInloggen.loginScherm(jsonList);
            //hoofdScherm.GebruikerHoofdscherm(loggedIn);
        }
    }
}
