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
            Console.WriteLine("*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*");
            Console.WriteLine("|                                                Films                                              |");
            Console.WriteLine("*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*" + " \n");
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
                
                Console.WriteLine("-----------------------------------------------------------------------------------------------------");
                Console.WriteLine("                                         Film-ID : " + item.id);
                Console.WriteLine("                           Naam: " + item.name);
                Console.WriteLine("                           Publicatiejaar: " + item.year);
                Console.WriteLine("                           Genres: " + categories);
                Console.WriteLine("-----------------------------------------------------------------------------------------------------");


            }

            if (rol == "gebruiker")
            {
                Console.WriteLine("\n" + "*-*-*-*-*-*-*" + "\n" +"| [1] Terug |" + "\n" + "-------------");
                Console.WriteLine("\n" + "Toets 1 om terug te keren naar het hoofdscherm of toets een film-ID in om een film te bekijken:    ");
            }
            else
            {
                Console.WriteLine("\n" + "*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*" + "\n" +
                                         "| [1] Terug | [2] toevoegen | [3] aanpassen | [4] verwijderen |" + "\n" +
                                         "---------------------------------------------------------------" + "\n");
                Console.WriteLine("Toets 1 om terug te keren naar het hoofdmenu.                " + "\n" +
                                  "Toets 2 om een nieuwe film toe te voegen.                    " + "\n" +
                                  "Toets 3 om de gegevens van een bestaande film aan te passen. " + "\n" +
                                  "Toets 4 om een bestaande film te verwijderen.                ");
                Console.WriteLine("");

            }
            
            
            
            Keuze = Console.ReadLine();

            if (rol == "gebruiker")
            {
                while (Keuze != "1" && Keuze.ToLower() != "terug")
                {
                    Keuze = "";
                    Console.WriteLine("Kies a.u.b. een van de bovenstaande opties.\n");
                    
                    Console.WriteLine("Toets het getal van uw gewenste actie in:");
                    
                    Keuze = Console.ReadLine();
                }
            }
            else
            {
                while (Keuze != "1" && Keuze.ToLower() != "terug" && Keuze != "2" && Keuze.ToLower() != "toevoegen" && Keuze != "3" && Keuze.ToLower() != "aanpassen" && Keuze != "4" && Keuze.ToLower() != "verwijderen")
                {
                    Console.WriteLine("Kies a.u.b. een van de bovenstaande opties.\n");
                    Console.WriteLine("----------------------------------------------------------------");
                    Console.WriteLine("| Toets 1 om terug te keren naar het hoofdmenu.                |" + "\n" +
                                      "| Toets 2 om een nieuwe film toe te voegen.                    |" + "\n" +
                                      "| Toets 3 om de gegevens van een bestaande film aan te passen. |" + "\n" +
                                      "| Toets 4 om een bestaande film te verwijderen.                |" );
                    Console.WriteLine("----------------------------------------------------------------");
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


        Console.WriteLine("*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*");
        Console.WriteLine("|          Nieuwe film toevoegen              |");
        Console.WriteLine("*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*" + "\n");
        Console.WriteLine("-----------------------------------------------");
        Console.WriteLine("|            Voer de filmtitel in:            |");
        Console.WriteLine("-----------------------------------------------" + "\n");
        string name = Console.ReadLine();
        Console.WriteLine("---------------------------------------------");
        Console.WriteLine("|       Voer de publicatiedatum in:         |");
        Console.WriteLine("---------------------------------------------" + "\n");
        string year = Console.ReadLine();

        
        int lengthArr = 0;
        bool check = true;

        while (check)
        {
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("|   Voer het aantal genres van de film in:  |");
            Console.WriteLine("---------------------------------------------");
            
            if (int.TryParse(Console.ReadLine(), out lengthArr))
            {
                
                check = false;
            }
            else
            {
                Console.WriteLine("Voer een nummer in. \n");
                Thread.Sleep(1000);
            }
        }

        string[] genreArr = new string[lengthArr];
        for (int i = 0; i < lengthArr; i++)
        {
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("|             Vul een genre in:             |");
            Console.WriteLine("---------------------------------------------");
            genreArr[i] = Console.ReadLine();
        }

        Console.WriteLine("---------------------------------------------");
        Console.WriteLine("|     Type de verhaallijn van de film:      |");
        Console.WriteLine("---------------------------------------------" + "\n");
        string storyline = Console.ReadLine();

        Console.WriteLine("---------------------------------------------");
        Console.WriteLine("|     Voer de naam van de regisseur in:     |");
        Console.WriteLine("---------------------------------------------" + "\n");
        string director = Console.ReadLine();

        Console.WriteLine("---------------------------------------------");
        Console.WriteLine("|  Voer de publicatiedatum in:(DD-MM-YYYY)  |");
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
        Console.WriteLine("*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*");
        Console.WriteLine("|                     Aanpassen                   |");
        Console.WriteLine("*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*");
        
        int[] filmIdArr = new int[movieList.Count()];
        for(int i=0; i<movieList.Count(); i++)
        {
            filmIdArr[i] = movieList[i].id;
        }

        int filmId = int.MinValue;
        while (!filmIdArr.Contains(filmId))
        {
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("| Voer de ID in van de film die u wilt aanpassen: |");
            Console.WriteLine("---------------------------------------------------");
            if (int.TryParse(Console.ReadLine(), out filmId))
            {

            }
            else
            {
                Console.WriteLine("Er is geen film voor de ingevoerde ID.\n");
                filmId = int.MinValue;
                Thread.Sleep(1000);
            }
        }
        Console.WriteLine("---------------------------------------------------");
        Console.WriteLine("|  Voer de aangepaste naam van de film in:        |");
        Console.WriteLine("---------------------------------------------------");
        string name = Console.ReadLine();

        Console.WriteLine("---------------------------------------------------");
        Console.WriteLine("|  Voer de aangepaste jaar van de film in:        |");
        Console.WriteLine("---------------------------------------------------");
        string year = Console.ReadLine();

        Console.WriteLine("---------------------------------------------------");
        Console.WriteLine("|   Voer de aantal genres van de film in:         |");
        Console.WriteLine("---------------------------------------------------");
        int lengthArr = 0;
        bool check = true;

        while (check)
        {
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("|   Voer het aantal genres van de film in:        |");
            Console.WriteLine("---------------------------------------------------");

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
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("|              Vul een genre in:                  |");
            Console.WriteLine("---------------------------------------------------");
            genreArr[i] = Console.ReadLine();
        }

        Console.WriteLine("---------------------------------------------------");
        Console.WriteLine("|     Voer de verhaallijn van de film in:         |");
        Console.WriteLine("---------------------------------------------------");
        string storyLine = Console.ReadLine();

        Console.WriteLine("---------------------------------------------------");
        Console.WriteLine("|     Voer de naam van de regisseur in:           |");
        Console.WriteLine("---------------------------------------------------");
        string director = Console.ReadLine();

        Console.WriteLine("---------------------------------------------------");
        Console.WriteLine("|     Voer de publicatiedatum in:(DD-MM-YYYY)     |");
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
        Console.WriteLine("*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*");
        Console.WriteLine("|                   Verwijderen                     |");
        Console.WriteLine("*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*");
        while (!filmArray.Contains(filmNr))
        {
            Console.WriteLine("-----------------------------------------------------");
            Console.WriteLine("| Voer de ID in van de film die u wilt verwijderen: |");
            Console.WriteLine("-----------------------------------------------------");
                
            if (int.TryParse(Console.ReadLine(), out filmNr))
            {
             
            }
            else
            {
                Console.WriteLine("Er is geen film voor het ingevoerde ID. \n");
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