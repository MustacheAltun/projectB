﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
public class Gegevens
{
    public void showGegevens(string url, int Accountid)
    {
        string strResultJson = File.ReadAllText("..\\..\\..\\account.json");

        // maak een lijst van alle informatie die er is
        List<Account> accountList = JsonConvert.DeserializeObject<List<Account>>(strResultJson);
        Console.Clear();
        Console.WriteLine("*-*-*-*-*-*-*-" + "\n" +
                                  "|  Gegevens  |" + "\n" +
                                  "*-*-*-*-*-*-*-" + "\n");
        bool noTickets = true;
        bool noFood = true;
        if (accountList[Accountid].tickets != null)
        {
            foreach (dynamic item in accountList)
            {
                //Check of gebruikersnaam en wachtwoord matchen
                if (item.id == Accountid)
                {
                    noTickets = false;
                    foreach (dynamic film in item.tickets)
                    {
                        string Movies = "..\\..\\..\\movies.json";
                        string locaties = "..\\..\\..\\locatie.json";
                        List<movie> movieList = JsonConvert.DeserializeObject<List<movie>>(File.ReadAllText(Movies));
                        List<Cinema_adress> locatieList = JsonConvert.DeserializeObject<List<Cinema_adress>>(File.ReadAllText(locaties));
                        string films = getMovieName(movieList, film.filmID);
                        string locatie = getBioscoopName(locatieList, film.bioscoopID);
                        Console.WriteLine("----------------------------Ticket---------------------------------" + "\n" + $"id: {film.id}\n" + $"FilmID: {films} {film.filmTechnologie}\n" + $"Kijker: {film.name}\n" + $"prijs: {film.prijs}\n" + $"Locatie: Biscoop:{locatie} Zaal:{film.zaalID} stoel:{film.stoel}\n" + $"Datum: {film.dag} {film.tijd}\n");
                    }
                    Console.WriteLine("-------------------------------------------------------------------");
                    break;
                }

            }
        }
        if (noTickets)
        {
            Console.WriteLine("| Je hebt nog geen film kaartjes bestelt |\n");
        }
        if (accountList[Accountid].etenBestelling != null)
        {
            foreach (dynamic item in accountList)
            {
                //Check of gebruikersnaam en wachtwoord matchen
                if (item.id == Accountid)
                {
                    noFood = false;
                    foreach (dynamic eten in item.etenBestelling)
                    {
                        string Movies = "..\\..\\..\\movies.json";
                        string locaties = "..\\..\\..\\locatie.json";
                        List<movie> movieList = JsonConvert.DeserializeObject<List<movie>>(File.ReadAllText(Movies));
                        List<Cinema_adress> locatieList = JsonConvert.DeserializeObject<List<Cinema_adress>>(File.ReadAllText(locaties));
                        string foodList = "";
                        foreach (dynamic foodItem in eten.orderList)
                        {
                            foodList += foodItem + " ";
                        }
                        Console.WriteLine("-----------------------------Eten----------------------------------" + "\n" + $"OrderID: {eten.orderID}\n" + $"Food Items: {foodList}\n" + $"Prijs: {eten.total}\n");
                    }
                    Console.WriteLine("-------------------------------------------------------------------");
                    break;
                }

            }
        }
        if (noFood)
        {
            Console.WriteLine("| Je hebt nog geen eten bestelt |");
        }
        Console.WriteLine("\n" +
                          "------------------------------------------------");
        Console.WriteLine("| [1] Terug | [2] Ticket reservering annuleren |" + "\n" +
                          "------------------------------------------------");
        string keuze = Console.ReadLine();
        if(keuze == "1")
        {
            return;
        }
        else if(keuze == "2")
        {
            removeTicket(url, Accountid);
        }
        else
        {
            Console.WriteLine("Onjuiste Invoer!");
            Thread.Sleep(1000);
            showGegevens(url, Accountid);
        }
    }
    public static string getMovieName(List<movie> movieList, string id)
    {
        foreach (dynamic films in movieList)
        {
            if (int.Parse(id) == films.id)
            {
                return films.name;
            }
        }
        return "";
    }
    public static string getBioscoopName(List<Cinema_adress> locatieList, int id)
    {
        foreach (dynamic locaties in locatieList)
        {
            if (id == locaties.id)
            {
                return locaties.name;
            }
        }
        return "";
    }
    public void removeTicket(string url, int Accountid)
    {
        string strResultJson = File.ReadAllText("..\\..\\..\\account.json");

        // maak een lijst van alle informatie die er is
        List<Account> accountList = JsonConvert.DeserializeObject<List<Account>>(strResultJson);
        Console.WriteLine("Voer de ID in van de ticket die u wilt annuleren of voer * in om terug te gaan:");
        string ticketId = Console.ReadLine();
        if(ticketId == "*")
        {
            return;
        }
        bool foundID = false;
        var biosID = 0;
        var zaalID = "";
        var dag = "";
        var t = "";
        var tID = 0;
        var zitString = "";
        var stoel = 0;
        if(accountList[Accountid].tickets == null)
        {
            Console.WriteLine("U heeft geen tickets gereserveerd");
            Thread.Sleep(2000);
            return;
        }
        foreach(var item in accountList){
            if (item.id == Accountid)
            {
                foreach (dynamic item2 in item.tickets)
                {
                    int input;
                    if (int.TryParse(ticketId, out input))
                    {
                        if(int.Parse(ticketId) == item2.id)
                        {
                        
                                foundID = true;
                                biosID = item2.bioscoopID;
                                zaalID = "zaal " + item2.zaalID;
                                dag = item2.dag;
                                t = item2.tijd;
                                stoel = item2.stoel;
                                tID = item2.id;
                        
                        }
                    }

                }
            }
        }
        if (foundID)
        {
            string Movies = "..\\..\\..\\movies.json";
            string locaties = "..\\..\\..\\locatie.json";
            List<movie> movieList = JsonConvert.DeserializeObject<List<movie>>(File.ReadAllText(Movies));
            List<Cinema_adress> locatieList = JsonConvert.DeserializeObject<List<Cinema_adress>>(File.ReadAllText(locaties));
            foreach (var locatie in locatieList)
            {
                if (locatie.id == biosID)
                {
                    foreach (var d in locatie.dagen)
                    {
                        foreach (var z in d)
                        {
                            if (z.datum == dag)
                            {
                                if (z.naam == zaalID)
                                {

                                    foreach (var tijd in z.tijden)
                                    {

                                        if (tijd.tijd == t)
                                        {
                                            zitString = tijd.beschikbaar;
                                            StringBuilder sb = new StringBuilder(zitString);
                                            sb[stoel - 1] = 'T';
                                            zitString = sb.ToString();
                                            tijd.beschikbaar = zitString;
                                            string convertedJson = JsonConvert.SerializeObject(locatieList, Formatting.Indented);
                                            //verander de hele file met de nieuwe json informatie
                                            File.WriteAllText(locaties, convertedJson);
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            foreach(var account in accountList)
            {
                if(account.id == Accountid)
                {
                    List<Ticket> ticketList = account.tickets.ToList();
                    foreach (var ticket in account.tickets)
                    {
                        if (tID == ticket.id)
                        {
                            ticketList.Remove(ticket);
                            account.tickets = ticketList.ToArray();
                            if(account.tickets.Length == 0)
                            {
                                account.tickets = null;
                            }
                            break;
                        }
                    }
                    
                    
                    string convertedJson2 = JsonConvert.SerializeObject(accountList, Formatting.Indented);
                    //verander de hele file met de nieuwe json informatie
                    File.WriteAllText("..\\..\\..\\account.json", convertedJson2);
                    break;
                }
            }
            Console.WriteLine("Reservering succesvol geannuleerd");
            Thread.Sleep(2000);
            return;
        }

        else
        {
            Console.WriteLine("ID niet gevonden... Probeer het nogmaals.");
            Thread.Sleep(2000);
            return;
        }
        Thread.Sleep(1000);
        Console.Clear();

    }
}