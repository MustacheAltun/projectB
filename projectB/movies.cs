using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

public class Movies
{

    public void show(string rol)
    {
        string url = "..\\..\\..\\movies.json";
        List<movie> movieList = JsonConvert.DeserializeObject<List<movie>>(File.ReadAllText(url));

        Console.Clear();
        string Keuze = "";

        while (Keuze != "1")
        {
            Console.WriteLine("-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*");
            Console.WriteLine("                                             Films");
            Console.WriteLine("-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*");
            // pak alle informatie per film
            foreach (dynamic item in movieList)
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

            if (rol == "gebruiker")
            {
                Console.WriteLine("| [1] Terug |");
            }
            else
            {
                Console.WriteLine("| [1] Terug | [2] toevoegen | [3] aanpassen | [4] verwijderen |");
            }
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("     Kies de actie die u wilt uitvoeren: ");
            Console.WriteLine("---------------------------------------------");
            Keuze = Console.ReadLine();

            if (rol == "gebruiker")
            {
                while (Keuze != "1" && Keuze.ToLower() != "terug")
                {
                    Keuze = "";
                    Console.WriteLine("Ongeldige invoer!\n");
                    Console.WriteLine("---------------------------------------------");
                    Console.WriteLine("     Kies de actie die u wilt uitvoeren:");
                    Console.WriteLine("---------------------------------------------");
                    Keuze = Console.ReadLine();
                }
            }
            else
            {
                while (Keuze != "1" && Keuze.ToLower() != "terug" && Keuze != "2" && Keuze.ToLower() != "toevoegen" && Keuze != "3" && Keuze.ToLower() != "aanpassen" && Keuze != "4" && Keuze.ToLower() != "verwijderen")
                {
                    Console.WriteLine("Ongeldige invoer!\n");
                    Console.WriteLine("---------------------------------------------");
                    Console.WriteLine("     Kies de actie die u wilt uitvoeren:");
                    Console.WriteLine("---------------------------------------------");
                    Keuze = Console.ReadLine();
                }
                switch (Keuze.ToLower())
                {

                    case "2":
                    case "toevoegen":
                        addFilm(movieList, url);
                        Keuze = "1";
                        show(rol);
                        break;
                    case "3":
                    case "aanpassen":
                        editFilm(movieList, url);
                        Keuze = "1";
                        show(rol);
                        break;
                    case "4":
                    case "verwijderen":
                        removeFilm(movieList, url);
                        Keuze = "1";
                        show(rol);
                        break;
                }
            }
        }
    }

    public static void addFilm(List<movie> movieList, string url)
    {
        Console.Clear();
        int id = 0;
        foreach (dynamic item in movieList)
        {
            //Check de laatste id
            id = item.id + 1;
        }


        Console.WriteLine("-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-");
        Console.WriteLine("                 Toevoegen");
        Console.WriteLine("-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-");
        Console.WriteLine("---------------------------------------------");
        Console.WriteLine("             Voer film naam in:");
        Console.WriteLine("---------------------------------------------");
        string name = Console.ReadLine();
        Console.WriteLine("---------------------------------------------");
        Console.WriteLine("             Voer film jaar in:");
        Console.WriteLine("---------------------------------------------");
        string year = Console.ReadLine();

        
        int lengthArr = 0;
        bool check = true;

        while (check)
        {
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("    Voer het aantal genres van de film in:");
            Console.WriteLine("---------------------------------------------");
            
            if (int.TryParse(Console.ReadLine(), out lengthArr))
            {
                
                check = false;
            }
            else
            {
                Console.WriteLine("Uw invoer is geen nummer!\n");
                Thread.Sleep(1000);
            }
        }

        string[] genreArr = new string[lengthArr];
        for (int i = 0; i < lengthArr; i++)
        {
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("               Vul in genre:");
            Console.WriteLine("---------------------------------------------");
            genreArr[i] = Console.ReadLine();
        }

        Console.WriteLine("---------------------------------------------");
        Console.WriteLine("      Typ de verhaallijn van de film:");
        Console.WriteLine("---------------------------------------------");
        string storyline = Console.ReadLine();

        Console.WriteLine("---------------------------------------------");
        Console.WriteLine("      Voer de naam van de regisseur in:");
        Console.WriteLine("---------------------------------------------");
        string director = Console.ReadLine();

        Console.WriteLine("---------------------------------------------");
        Console.WriteLine("  Voer in de film release datum:(DD-MM-YYYY)");
        Console.WriteLine("---------------------------------------------");
        string releasedate = Console.ReadLine();

        
        movieList.Add(new movie()
        {

            id = id,
            name = name,
            year = year,
            categories = genreArr,
            storyline = storyline,
            director = director,
            releasedate = releasedate,


        });
        //verdander de lijst naar een json type
        var convertedJson = JsonConvert.SerializeObject(movieList, Formatting.Indented);
        //verander de hele file met de nieuwe json informatie
        File.WriteAllText(url, convertedJson);
    }

    public static void editFilm(List<movie> movieList, string url)
    {
        Console.WriteLine("-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-");
        Console.WriteLine("                   Aanpassen");
        Console.WriteLine("-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-");
        
        int[] filmIdArr = new int[movieList.Count()];
        for(int i=0; i<movieList.Count(); i++)
        {
            filmIdArr[i] = movieList[i].id;
        }

        int filmId = int.MinValue;
        while (!filmIdArr.Contains(filmId))
        {
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine(" Voer ID in van de film die u wilt aanpassen:");
            Console.WriteLine("---------------------------------------------");
            if (int.TryParse(Console.ReadLine(), out filmId))
            {

            }
            else
            {
                Console.WriteLine("Uw invoer is geen film ID!\n");
                filmId = int.MinValue;
                Thread.Sleep(1000);
            }
        }
        Console.WriteLine("---------------------------------------------");
        Console.WriteLine("   Voer de aangepaste naam van de film in:");
        Console.WriteLine("---------------------------------------------");
        string name = Console.ReadLine();

        Console.WriteLine("---------------------------------------------");
        Console.WriteLine("   Voer de aangepaste jaar van de film in:");
        Console.WriteLine("---------------------------------------------");
        string year = Console.ReadLine();

        Console.WriteLine("---------------------------------------------");
        Console.WriteLine("    Voer de aantal genres van de film in:");
        Console.WriteLine("---------------------------------------------");
        int lengthArr = 0;
        bool check = true;

        while (check)
        {
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("    Voer het aantal genres van de film in:");
            Console.WriteLine("---------------------------------------------");

            if (int.TryParse(Console.ReadLine(), out lengthArr))
            {

                check = false;
            }
            else
            {
                Console.WriteLine("Uw invoer is geen nummer!\n");
                Thread.Sleep(1000);
            }
        }

        string[] genreArr = new string[lengthArr];
        for (int i = 0; i < lengthArr; i++)
        {
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("               Vul in genre:");
            Console.WriteLine("---------------------------------------------");
            genreArr[i] = Console.ReadLine();
        }

        Console.WriteLine("---------------------------------------------");
        Console.WriteLine("      Voer de verhaallijn van de film in:");
        Console.WriteLine("---------------------------------------------");
        string storyLine = Console.ReadLine();

        Console.WriteLine("---------------------------------------------");
        Console.WriteLine("      Voer de naam van de regisseur in:");
        Console.WriteLine("---------------------------------------------");
        string director = Console.ReadLine();

        Console.WriteLine("---------------------------------------------------");
        Console.WriteLine(" Voer de release datum van de film in:(DD-MM-YYYY)");
        Console.WriteLine("---------------------------------------------------");
        string releasedate = Console.ReadLine();


        for(int i=0; i<movieList.Count; i++)
        {
            if(movieList[i].id == filmId)
            {
                movieList[i].name = name;
                movieList[i].year = year;
                movieList[i].categories = genreArr;
                movieList[i].storyline = storyLine;
                movieList[i].director = director;
                movieList[i].releasedate = releasedate;
                break;
            }
        }
        string updatedMovieList = JsonConvert.SerializeObject(movieList, Formatting.Indented);
        File.WriteAllText(url, updatedMovieList);
    }

    public static void removeFilm(List<movie> movieList, string url)
    {
        
        int[] filmArray = new int[movieList.Count()];
        for (int i = 0; i < movieList.Count(); i++)
        {
            filmArray[i] = i;
        }
        int filmNr = int.MinValue;
        //bool check = true;
        //string intCheck = "";
        Console.WriteLine("-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-");
        Console.WriteLine("                 Verwijderen");
        Console.WriteLine("-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-");
        while (!filmArray.Contains(filmNr))
        {

            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine("   Voer ID in van de film die u wilt verwijderen:");
            Console.WriteLine("----------------------------------------------------");
                
            if (int.TryParse(Console.ReadLine(), out filmNr))
            {
             
            }
            else
            {
                Console.WriteLine("Uw invoer is geen film ID!\n");
                filmNr = int.MinValue;
                Thread.Sleep(1000);
            }
            

            
        }
        movieList.RemoveAt(filmNr);
        //verdander de lijst naar een json type
        string cinemaLijst = JsonConvert.SerializeObject(movieList, Formatting.Indented);
        //verander de hele file met de nieuwe json informatie
        File.WriteAllText(url, cinemaLijst);
    
    }
}