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
            Console.SetWindowSize(130, 40);

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
            UpdateOmzet();
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
                OmzetList.Add(new WeeklyEarning()
                {
                    weekendDate = (today.AddDays(7)).ToString("dd-MM-yyyy"),
                    amountEarned = 0,
                    dailyEarnings = null
                });
                string OmzetJson = JsonConvert.SerializeObject(OmzetList, Formatting.Indented);
                //verander de hele file met de nieuwe json informatie
                File.WriteAllText("..\\..\\..\\omzet.json", OmzetJson);
            }
            foreach (var WekenlijkseOmzet in OmzetList)
            {
                DateTime WekenlijkseConvert = Convert.ToDateTime(WekenlijkseOmzet.weekendDate);
                today = today;
                TimeSpan ts = today - WekenlijkseConvert;
                int differenceInDays = ts.Days;
                if (differenceInDays > 0)
                {
                    OmzetList.Add(new WeeklyEarning()
                    {
                        weekendDate = (WekenlijkseConvert.AddDays(7)).ToString("dd-MM-yyyy"),
                        amountEarned = 0,
                        dailyEarnings = null
                    });
                    string OmzetJson = JsonConvert.SerializeObject(OmzetList, Formatting.Indented);
                    //verander de hele file met de nieuwe json informatie
                    File.WriteAllText("..\\..\\..\\omzet.json", OmzetJson);
                    break;
                }
            }
        }
        public static void AddOmzet(double add)
        {
            string strOmzet = File.ReadAllText("..\\..\\..\\omzet.json");
            List<WeeklyEarning> OmzetList = JsonConvert.DeserializeObject<List<WeeklyEarning>>(strOmzet);
            DateTime today = DateTime.Now;
            foreach (var WekenlijkseOmzet in OmzetList)
            {
                DateTime WekenlijkseConvert = Convert.ToDateTime(WekenlijkseOmzet.weekendDate);
                today = today;
                TimeSpan ts = today - WekenlijkseConvert;
                int differenceInDays = ts.Days;
                if (differenceInDays >= -7 && differenceInDays <= 0)
                {
                }
            }
        }
    }
}
