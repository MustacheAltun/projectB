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
            Catering catering = new Catering();
            Gegevens AccGegevens = new Gegevens();
            HoofdScherm hoofdScherm = new HoofdScherm();

            // geef de url van de account json
            string url = "..\\..\\..\\account.json";

            // lees de file en zet alles in een string
            string strResultJson = File.ReadAllText("..\\..\\..\\account.json");

            // maak een lijst van alle informatie die er is
            List<Account> jsonList = JsonConvert.DeserializeObject<List<Account>>(strResultJson);
            for(int i = 0; i < 10; i++)
            {
               UpdateOmzet();
            }
            List<movie> movieList = JsonConvert.DeserializeObject<List<movie>>(File.ReadAllText("..\\..\\..\\movies.json"));
            List<Cinema_adress> locatieList = JsonConvert.DeserializeObject<List<Cinema_adress>>(File.ReadAllText("..\\..\\..\\locatie.json"));

            //Updaten van de schema op de huidige datum.
            Movies.updateShowingFilm(movieList, locatieList);
            Locatie.updateSchedule();

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
        public static void UpdateOmzet()
        {
            string strOmzet = File.ReadAllText("..\\..\\..\\omzet.json");
            List<WeeklyEarning> OmzetList = JsonConvert.DeserializeObject<List<WeeklyEarning>>(strOmzet);
            DateTime today = DateTime.Now;

            if (OmzetList.Count == 0)
            {
                List<Dictionary<string, double>> Earning = new List<Dictionary<string, double>>();
                Dictionary<string, double> D1 = new Dictionary<string, double>();
                D1.Add((today.AddDays(0)).ToString("dd-MM-yyyy"), 0.0);
                Dictionary<string, double> D2 = new Dictionary<string, double>();
                D2.Add((today.AddDays(1)).ToString("dd-MM-yyyy"), 0.0);
                Dictionary<string, double> D3 = new Dictionary<string, double>();
                D3.Add((today.AddDays(2)).ToString("dd-MM-yyyy"), 0.0);
                Dictionary<string, double> D4 = new Dictionary<string, double>();
                D4.Add((today.AddDays(3)).ToString("dd-MM-yyyy"), 0.0);
                Dictionary<string, double> D5 = new Dictionary<string, double>();
                D5.Add((today.AddDays(4)).ToString("dd-MM-yyyy"), 0.0);
                Dictionary<string, double> D6 = new Dictionary<string, double>();
                D6.Add((today.AddDays(5)).ToString("dd-MM-yyyy"), 0.0);
                Dictionary<string, double> D7 = new Dictionary<string, double>();
                D7.Add((today.AddDays(6)).ToString("dd-MM-yyyy"), 0.0);
                Dictionary<string, double> D8 = new Dictionary<string, double>();
                D8.Add((today.AddDays(7)).ToString("dd-MM-yyyy"), 0.0);
                Earning.Add(D1);
                Earning.Add(D2);
                Earning.Add(D3);
                Earning.Add(D4);
                Earning.Add(D5);
                Earning.Add(D6);
                Earning.Add(D7);
                Earning.Add(D8);
                OmzetList.Add(new WeeklyEarning()
                {
                    weekendDate = (today.AddDays(7)).ToString("dd-MM-yyyy"),
                    amountEarned = 0,
                    dailyEarnings = Earning
                });
                string OmzetJson = JsonConvert.SerializeObject(OmzetList, Formatting.Indented);
                //verander de hele file met de nieuwe json informatie
                File.WriteAllText("..\\..\\..\\omzet.json", OmzetJson);
            }
            else
            {
                int i = 1;
                foreach (var WekenlijkseOmzet in OmzetList)
                {   
                    if(i == OmzetList.Count)
                    {
                        DateTime WekenlijkseConvert = Convert.ToDateTime(WekenlijkseOmzet.weekendDate);
                        today = today;
                        TimeSpan ts = today - WekenlijkseConvert;
                        int differenceInDays = ts.Days;

                        List<Dictionary<string, double>> Earning = new List<Dictionary<string, double>>();
                        Dictionary<string, double> D1 = new Dictionary<string, double>();
                        D1.Add((WekenlijkseConvert.AddDays(0)).ToString("dd-MM-yyyy"), 0.0);
                        Dictionary<string, double> D2 = new Dictionary<string, double>();
                        D2.Add((WekenlijkseConvert.AddDays(1)).ToString("dd-MM-yyyy"), 0.0);
                        Dictionary<string, double> D3 = new Dictionary<string, double>();
                        D3.Add((WekenlijkseConvert.AddDays(2)).ToString("dd-MM-yyyy"), 0.0);
                        Dictionary<string, double> D4 = new Dictionary<string, double>();
                        D4.Add((WekenlijkseConvert.AddDays(3)).ToString("dd-MM-yyyy"), 0.0);
                        Dictionary<string, double> D5 = new Dictionary<string, double>();
                        D5.Add((WekenlijkseConvert.AddDays(4)).ToString("dd-MM-yyyy"), 0.0);
                        Dictionary<string, double> D6 = new Dictionary<string, double>();
                        D6.Add((WekenlijkseConvert.AddDays(5)).ToString("dd-MM-yyyy"), 0.0);
                        Dictionary<string, double> D7 = new Dictionary<string, double>();
                        D7.Add((WekenlijkseConvert.AddDays(6)).ToString("dd-MM-yyyy"), 0.0);
                        Dictionary<string, double> D8 = new Dictionary<string, double>();
                        D8.Add((WekenlijkseConvert.AddDays(7)).ToString("dd-MM-yyyy"), 0.0);
                        Earning.Add(D1);
                        Earning.Add(D2);
                        Earning.Add(D3);
                        Earning.Add(D4);
                        Earning.Add(D5);
                        Earning.Add(D6);
                        Earning.Add(D7);
                        Earning.Add(D8);

                        if (differenceInDays > 0)
                        {
                            OmzetList.Add(new WeeklyEarning()
                            {
                                weekendDate = (WekenlijkseConvert.AddDays(7)).ToString("dd-MM-yyyy"),
                                amountEarned = 0,
                                dailyEarnings = Earning
                            });
                            string OmzetJson = JsonConvert.SerializeObject(OmzetList, Formatting.Indented);
                            //verander de hele file met de nieuwe json informatie
                            File.WriteAllText("..\\..\\..\\omzet.json", OmzetJson);
                            break;
                        }
                    }
                    i++;
                }
            }
        }
        /*public static void AddOmzet(double add)
        {
            string strOmzet = File.ReadAllText("..\\..\\..\\omzet.json");
            List<WeeklyEarning> OmzetList = JsonConvert.DeserializeObject<List<WeeklyEarning>>(strOmzet);
            DateTime today = DateTime.Now;
            string dateT = today.ToString("dd-MM-yyyy");
            foreach(var Weeklies in OmzetList)
            {
                foreach(var dailies in Weeklies.dailyEarnings)
                {
                    
                }
            }
        }*/
    }
}
