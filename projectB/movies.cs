using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Movies { 
    public void FilmToevoegen(List<movie> accountList, string id, string name, string year, string[] categories, string storyline, string director, string releasedate )
    {
        accountList.Add(new movie()
        {
            id = id,
            name = name,
            year = year,
            categories = categories,
            storyline = storyline,
            director = director,
            releasedate = releasedate,


        });
        //verdander de lijst naar een json type
        var convertedJson = JsonConvert.SerializeObject(accountList, Formatting.Indented);
        //verander de hele file met de nieuwe json informatie
        string url = "..\\..\\..\\movies.json";
        File.WriteAllText(url, convertedJson);
    }
    public void show(List<movie> accountList)   
    {
        Console.Clear();
        Console.WriteLine("| Terug [1] | Weergave [2] |");
        Console.WriteLine("Kies de actie die u wilt uitvoeren: ");
        string Keuze = Console.ReadLine();
        while (Keuze != "2" && Keuze != "1")
        {
            Keuze = Console.ReadLine();
            if (Keuze != "2" && Keuze != "1")
            {
                Console.WriteLine("Invalide Input!");
            }
        }
        if (Keuze == "2")
        {
            // pak alle informatie per film
            foreach (dynamic item in accountList)
            {
                //maak een string voor alle categorieen
                string categories = "";
                //voeg alle categorieen toe aan categories
                foreach (string item2 in item.categories)
                {

                    if (item2 == item.categories[item.categories.Length - 1])
                    {
                        categories += item2;
                    }
                    else
                    {
                        categories += item2 + "\n                                   ";
                    }

                }
                //print alles uit
                Console.WriteLine("------------------------------------------------------------------------------------------");
                Console.WriteLine("                       movie id : " + item.id);
                Console.WriteLine("                       name: " + item.name);
                Console.WriteLine("                       year: " + item.year);
                Console.WriteLine("                       categories: " + categories);
                Console.WriteLine("------------------------------------------------------------------------------------------");
               

            }
            Console.WriteLine("Geef input om terug te gaan naar Hoofdscherm: ");
            string[] genres = new string[] {};
            genres = new string[] { "actie", "horror","thriller" };
            FilmToevoegen(accountList, "231", "banaan", "1933", genres, "banaan vloog uit de lucht", "kawish", "1999");
            Console.ReadKey();
        } 
       if (Keuze == "1")
        {
            return;
        }
    }
}
