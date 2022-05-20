using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

public class Gegevens
{
    public void showGegevens(List<Account> accountList, int Accountid)
    {
        string Movies = "..\\..\\..\\movies.json";
        string locaties = "..\\..\\..\\locatie.json";
        List<movie> movieList = JsonConvert.DeserializeObject<List<movie>>(File.ReadAllText(Movies));
        List<Cinema_adress> locatieList = JsonConvert.DeserializeObject<List<Cinema_adress>>(File.ReadAllText(locaties));
        Console.Clear();
        Console.WriteLine("*-*-*-*-*-*-*-" + "\n" +
                                  "|  Gegevens  |" + "\n" +
                                  "*-*-*-*-*-*-*-" + "\n");
        bool noTickets = true;
        bool noFood = true;
        if (accountList[Accountid].tickets != null) {
            foreach (dynamic item in accountList)
            {
                //Check of gebruikersnaam en wachtwoord matchen
                if (item.id == Accountid)
                {
                    noTickets = false;
                    foreach (dynamic film in item.tickets)
                    {
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
        if (accountList[Accountid].tickets != null) {
            foreach (dynamic item in accountList)
            {
                //Check of gebruikersnaam en wachtwoord matchen
                if (item.id == Accountid)
                {
                    noFood = false;
                    foreach (dynamic eten in item.etenBestelling)
                    {
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

        string beans = Console.ReadLine();
        return;
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
}