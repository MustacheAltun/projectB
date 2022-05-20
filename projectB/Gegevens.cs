
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
        bool noTickets = false;
        foreach (dynamic item in accountList)
        {
            //Check of gebruikersnaam en wachtwoord matchen
            if (item.id == Accountid)
            {
                foreach(dynamic film in item.tickets)
                {
                    string films = getMovieName(movieList, film.filmID);
                    string locatie = getBioscoopName(locatieList, film.bioscoopID);
                    Console.WriteLine("-------------------------------------------------------------------" + "\n" + $"id: {film.id}\n" + $"FilmID: {films} {film.filmTechnologie}\n" + $"Kijker: {film.name}\n" + $"prijs: {film.prijs}\n" + $"Locatie: Biscoop:{locatie} Zaal:{film.zaalID} stoel:{film.stoel}\n" + $"Datum: {film.dag} {film.tijd}\n");
                }
                break;
            }

        }
        Console.WriteLine("-------------------------------------------------------------------");

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