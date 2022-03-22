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
            List<movie> movieList = JsonConvert.DeserializeObject<List<movie>>(File.ReadAllText("..\\..\\..\\movies.json"));
            Movies overview = new Movies();
            Registratie accountMaken = new Registratie();

            //object gemaakt van class Login
            Login accountInloggen = new Login();

            Locatie locatie = new Locatie();

            // geef de url van de json
            string url = "..\\..\\..\\account.json";

            // lees de file en zet alles in een string
            string strResultJson = File.ReadAllText("..\\..\\..\\account.json");


            // maak een lijst van alle informatie die er is
            List<Account> jsonList = JsonConvert.DeserializeObject<List<Account>>(strResultJson);

            //voeg een lijst van tickets toe als je dat wilt, het hoeft niet
            Ticket[] tickelijst = new Ticket[]
            {
                new Ticket() { id = 3, name = "Red" },
                new Ticket() { id = 4, name = "Black" },
                new Ticket() { id = 5, name = "Yellow" }
            };

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
                else if (page == "Films")
                {
                    overview.show(movieList);
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
            hoofdScherm.GebruikerHoofdscherm(loggedIn);

            //dit alles is voor films



        }
    }
}
