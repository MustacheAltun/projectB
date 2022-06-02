using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace projectB
{
    class Program
    {
        static void Main(string[] args)
        {
            //Kleur van tekst in console en grote van console aangepast.
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.SetWindowSize(160, 40);

            //instantiate de class
            Movies overview = new Movies();
            Registratie accountMaken = new Registratie();
            Login accountInloggen = new Login();
            Locatie locatie = new Locatie();
            Omzet omzet = new Omzet();
            Catering catering = new Catering();
            Gegevens AccGegevens = new Gegevens();
            HoofdScherm hoofdScherm = new HoofdScherm();

            // geef de url van de account json
            string url = "..\\..\\..\\account.json";

            // lees de file en zet alles in een string
            string strResultJson = File.ReadAllText("..\\..\\..\\account.json");

            // maak een lijst van alle informatie die er is
            List<Account> jsonList = JsonConvert.DeserializeObject<List<Account>>(strResultJson);
            List<movie> movieList = JsonConvert.DeserializeObject<List<movie>>(File.ReadAllText("..\\..\\..\\movies.json"));
            List<Cinema_adress> locatieList = JsonConvert.DeserializeObject<List<Cinema_adress>>(File.ReadAllText("..\\..\\..\\locatie.json"));

            //Updaten van de schema op de huidige datum.
            Movies.updateShowingFilm(movieList, locatieList);
            Locatie.updateSchedule();
            for(int i = 0; i < 10; i++)
            {
                Omzet.UpdateOmzet();
            }
            bool gebruikerLoggedIn = false;
            bool adminLoggedIn = false;
            int id = -1;
            string rol="gast";
            while (true)
            {
                string page;
                page = menu(hoofdScherm, gebruikerLoggedIn, adminLoggedIn);

                if (page == "Inloggen")
                {
                    id = accountInloggen.LoginScherm(jsonList);
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
                            break;
                        }
                        else
                        {
                            rol = null;
                        }
                    }
                }
               
                else if (page == "Films")
                {
                    overview.show(rol,id);

                }
                else if (page == "Uitloggen")
                {
                    rol = "gast";
                    adminLoggedIn = false;
                    gebruikerLoggedIn = false;
                }
                else if (page == "Locaties")
                {
                    locatie.viewLocations(rol);

                }
                else if (page == "Eten & Drinken")
                {
                    catering.etenMenu(rol, id);
                }
                else if (page == "Account Gegevens")
                {
                    AccGegevens.showGegevens(url, id);
                }
                else if (page == "Zoeken")
                {
                    Movies.searchFilm(movieList, rol, id);
                }
                else if (page == "Omzet")
                {
                    Omzet.ShowOmzet();
                }
            }


        }

        private static string menu(HoofdScherm hoofdScherm, bool gebruikerLoggedIn, bool adminLoggedIn)
        {
            string page;
            if (gebruikerLoggedIn)
            {
                page = hoofdScherm.GebruikerHoofdscherm(gebruikerLoggedIn);
            }
            else if (adminLoggedIn)
            {
                page = hoofdScherm.adminHoofdscherm(adminLoggedIn);
            }
            else
            {
                page = hoofdScherm.GebruikerHoofdscherm(gebruikerLoggedIn);
            }

            return page;
        }
    }
}
