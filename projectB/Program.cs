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
            //List<movie> movieList = JsonConvert.DeserializeObject<List<movie>>(File.ReadAllText("..\\..\\..\\movies.json"));
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
            bool gebruikerLoggedIn = false;
            bool adminLoggedIn = false;
            int id;
            string rol;
            while (true)
            {
                string page;
                if (gebruikerLoggedIn)
                {
                    page = hoofdScherm.GebruikerHoofdscherm(gebruikerLoggedIn);
                }else if (adminLoggedIn)
                {
                    page = hoofdScherm.adminHoofdscherm(adminLoggedIn);
                }
                else
                {
                    page = hoofdScherm.GebruikerHoofdscherm(gebruikerLoggedIn);
                }
                
                if (page == "Inloggen")
                {
                    id = accountInloggen.loginScherm(jsonList);
                    foreach (dynamic item in jsonList)
                    {
                        //Check of gebruikersnaam en wachtwoord matchen en kijk gelijk ook of hij admin of gebruiker is
                        if (item.id == id)
                        {
                            rol = item.rol;
                            if (rol == "admin")
                            {
                                gebruikerLoggedIn = false;
                                adminLoggedIn = true;
                            }
                            else if (rol == "gebruiker")
                            {
                                adminLoggedIn = false;
                                gebruikerLoggedIn = true;
                            }
                            else
                            {
                                adminLoggedIn = false;
                                gebruikerLoggedIn = false;
                            }
                        }
                        else
                        {
                            rol = null;
                        }
                    }
                }
                else if (page == "Registreren")
                {
                    accountMaken.RegistrerenFrontend(url, jsonList);
                }
                else if (page == "Films")
                {
                    if (adminLoggedIn)
                    {
                        overview.show("admin");
                    }
                    else
                    {
                        overview.show("gebruiker");
                    }
                    
                }
                else if (page == "Uitloggen")
                {
                    adminLoggedIn = false;
                    gebruikerLoggedIn = false;
                }
                else if (page == "Locaties")
                {
                    locatie.viewLocations();
                }
            }


        }
    }
}
