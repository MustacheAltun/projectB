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
            //instantiate de class
            Registratie accountMaken = new Registratie();

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

            //geef alles naar de registreer funtie
            //note: ticketlijst heeft een default value, dus je kan die leeg laten als je wilt
            //note: als je dit nog een keer runned maakt hij een account, als dit niet wilt dan moet je lijn 36 commenten
            accountMaken.Registreren(url, jsonList, "werk", "niet", "secreto", tickelijst);
            bool loggedIn = false;
            HoofdScherm hoofdScherm = new HoofdScherm();
            hoofdScherm.GebruikerHoofdscherm(loggedIn);
            loggedIn = true;
            hoofdScherm.GebruikerHoofdscherm(loggedIn);
        }
    }
}
