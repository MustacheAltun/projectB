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
            string showing = "";
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
                    if (item.showing)
                        showing = "NU TE ZIEN";
                    else
                        showing = "MOMENTEEL NIET TE ZIEN";

                }
                //print alles uit
                
                Console.WriteLine("-----------------------------------------------------------------------------------------------------");
                Console.WriteLine("                                         Film-ID : " + item.id);
                Console.WriteLine("                           Naam: " + item.name);
                Console.WriteLine("                           Publicatiejaar: " + item.year);
                Console.WriteLine("                           Genres: " + categories);
                Console.WriteLine("                           Status: " + showing);
                Console.WriteLine("-----------------------------------------------------------------------------------------------------");


            }

            if (rol == "gebruiker")
            {
                Console.WriteLine("\n" +"*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-" + "\n" + 
                                        "| [1] Terug | [2] Kijk Film | [3] Zoeken | [4] Filtreren |" + "\n" + 
                                        "----------------------------------------------------------");
                Console.WriteLine("----------------------------------------------------------------");
                Console.WriteLine("| Toets 1 om terug te keren naar het hoofdmenu.                |" + "\n" +
                                  "| Toets 2 om een compleet overzicht van een film te krijgen.   |" + "\n" +
                                  "| Toets 3 om een film op naam te zoeken.                       |" + "\n" +
                                  "| Toets 4 om films op genre te filteren.                       |");
                Console.WriteLine("----------------------------------------------------------------");
            }
            else if (rol == "admin")
            {
                Console.WriteLine("\n" + "*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-" + "\n" +
                                         "| [1] Terug | [2] toevoegen | [3] aanpassen | [4] verwijderen | [5] Kijk Film | [6] Zoeken | [7] Filtreren |" + "\n" +
                                         "------------------------------------------------------------------------------------------------------------");
                Console.WriteLine("----------------------------------------------------------------");
                Console.WriteLine("| Toets 1 om terug te keren naar het hoofdmenu.                |" + "\n" +
                                  "| Toets 2 om een nieuwe film toe te voegen.                    |" + "\n" +
                                  "| Toets 3 om de gegevens van een bestaande film aan te passen. |" + "\n" +
                                  "| Toets 4 om een bestaande film te verwijderen.                |" + "\n" +
                                  "| Toets 5 om een compleet overzicht van een film te krijgen.   |" + "\n" +
                                  "| Toets 6 om een film op naam te zoeken.                       |" + "\n" +
                                  "| Toets 7 om films op genre te filteren.                       |");
                Console.WriteLine("----------------------------------------------------------------");

            }
            else
            {
                Console.WriteLine("\n" + "*-*-*-*-*-*-*" + "\n" + "| [1] Terug |" + "\n" + "-------------");
                Console.WriteLine("\n" + "Toets 1 om terug te keren naar het hoofdscherm of toets een film-ID in om een film te bekijken:    ");
            }
                



            Keuze = Console.ReadLine();

            if (rol == "gebruiker")
            {
                while (Keuze != "1" && Keuze != "2" && Keuze != "3" && Keuze != "4")
                {
                    Keuze = "";
                    Console.WriteLine("Kies a.u.b. een van de bovenstaande opties.\n");                 
                    Keuze = Console.ReadLine();
                }
                switch (Keuze.ToLower())
                {

                    case "2":
                        viewFilm(movieList);
                        break;
                    case "3":
                        
                        break;
                    case "4":
                        
                        break;
                }
            }
            else if (rol =="admin")
            {
                while (Keuze != "1"  && Keuze != "2"  && Keuze != "3"  && Keuze != "4" && Keuze != "5" && Keuze != "6" && Keuze != "7")
                {
                    Console.WriteLine("Kies a.u.b. een van de bovenstaande opties.\n");
                    Keuze = Console.ReadLine();
                }
                switch (Keuze.ToLower())
                {

                    case "2":
                        addFilm(movieList, url);
                        break;
                    case "3":
                        editFilm(movieList, url);
                        break;
                    case "4":
                        removeFilm(movieList, url);
                        break;
                    case "5":
                        viewFilm(movieList);
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
        Console.WriteLine("--------------------------------------------------------");
        Console.WriteLine("|    Voer de filmtitel in: (typ '*' om te annuleren)   |");
        Console.WriteLine("--------------------------------------------------------" + "\n");
        string name = Console.ReadLine();
        while (string.IsNullOrEmpty(name) || name.Trim().Length == 0)
        {
            Console.WriteLine("Vul in een geldige naam a.u.b!");
            Thread.Sleep(3000);
            name = Console.ReadLine();
        }
        if(name.Trim() == "*")
        {
            Console.WriteLine("Bewerking is geannuleerd!");
            Thread.Sleep(3000);
            Console.Clear();
            return;
        }
        

        Console.WriteLine("----------------------------------------------------------------------");
        Console.WriteLine("|       Voer de publicatiejaar in: (typ '*' om te annuleren)        |");
        Console.WriteLine("----------------------------------------------------------------------" + "\n");
        
        bool check = true;
        int yearPlaceholder = 0;
        string year;
        while (check)
        {
            year = Console.ReadLine();
            if (int.TryParse(year, out yearPlaceholder))
            {
                if (yearPlaceholder < 1888 || yearPlaceholder > @DateTime.Now.Year)
                {
                    Console.WriteLine("Voer een geldige jaar in a.u.b!\n");
                    Thread.Sleep(3000);
                }
                else
                    check = false;
            }
            else
            {
                if (year.Trim() == "*")
                {
                    Console.WriteLine("Bewerking is geannuleerd!");
                    Thread.Sleep(3000);
                    Console.Clear();
                    return;
                }
                else
                {
                    Console.WriteLine("Voer een geldige jaar in a.u.b!\n");
                    Thread.Sleep(3000);
                }
            }
        }
        year = yearPlaceholder.ToString();



        int lengthArr = 0;
        check = true;

        while (check)
        {
            Console.WriteLine("-----------------------------------------------------------------------");
            Console.WriteLine("|   Voer het aantal genres van de film in: (typ '*' om te annuleren)  |");
            Console.WriteLine("-----------------------------------------------------------------------");
            
            string placeHolder = Console.ReadLine();
            if (int.TryParse(placeHolder, out lengthArr))
            {
                
                check = false;
            }
            else
            {
                if (placeHolder.Trim() == "*")
                {
                    check = false;
                    Console.WriteLine("Bewerking is geannuleerd!");
                    Thread.Sleep(3000);
                    Console.Clear();
                    return;
                }
                
                else
                {
                    Console.WriteLine("Voer een nummer in. \n");
                    Thread.Sleep(1000);
                }
                    
            }
        }

        string[] genreArr = new string[lengthArr];
        for (int i = 0; i < lengthArr; i++)
        {
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("|               Kies een genre:             |");
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("| [1] Actie | [2] Misdaad | [3] Avontuur | [4] SciFi | [5] KinderFilm | [6] Documentaire | [7] Romance | [8] Komedie |\n");

            string genreKeuze = Console.ReadLine();

            while (genreKeuze != "1" && genreKeuze != "2" && genreKeuze != "3" && genreKeuze != "4" && genreKeuze != "5" && genreKeuze != "6" && genreKeuze != "7" && genreKeuze != "8")
            {
                Console.WriteLine("Ongeldig keuze!");
                Console.WriteLine("Probeer opnieuw a.u.b!");
                genreKeuze = Console.ReadLine();
            }
            switch (genreKeuze)
            {
                case "1":
                    genreKeuze = "Actie";
                    break;
                case "2":
                    genreKeuze = "Misdaad";
                    break;
                case "3":
                    genreKeuze = "Avontuur";
                    break;
                case "4":
                    genreKeuze = "Scifi";
                    break;
                case "5":
                    genreKeuze = "KinderFilm";
                    break;
                case "6":
                    genreKeuze = "Documentaire";
                    break;
                case "7":
                    genreKeuze = "Romance";
                    break;
                case "8":
                    genreKeuze = "Komedie";
                    break;
            }

            genreArr[i] = genreKeuze;
        }

        Console.WriteLine("------------------------------------------------------------------");
        Console.WriteLine("|     Type de verhaal van de film:  (typ '*' om te annuleren)    |");
        Console.WriteLine("------------------------------------------------------------------" + "\n");
        string storyline = Console.ReadLine();
        while (string.IsNullOrEmpty(storyline) || storyline.Trim().Length == 0)
        {
            Console.WriteLine("Uw invoer is ongeldig!");
            Thread.Sleep(3000);
            storyline = Console.ReadLine();
        }
        if (storyline.Trim() == "*")
        {
            Console.WriteLine("Bewerking is geannuleerd!");
            Thread.Sleep(3000);
            Console.Clear();
            return;
        }
        

        Console.WriteLine("----------------------------------------------------------------------");
        Console.WriteLine("|     Voer de naam van de regisseur in: (typ '*' om te annuleren)    |");
        Console.WriteLine("----------------------------------------------------------------------" + "\n");
        string director = Console.ReadLine();
        while (string.IsNullOrEmpty(director) || director.Trim().Length == 0)
        {
            Console.WriteLine("Vul in een geldige naam a.u.b!");
            Thread.Sleep(3000);
            director = Console.ReadLine();
        }
        if (director.Trim() == "*")
        {
            Console.WriteLine("Bewerking is geannuleerd!");
            Thread.Sleep(3000);
            Console.Clear();
            return;
        }
        

        Console.WriteLine("----------------------------------------------------------------------");
        Console.WriteLine("|  Voer de publicatiedatum in:(DD-MM-YYYY) (typ '*' om te annuleren) |");
        Console.WriteLine("----------------------------------------------------------------------");
        string releasedate = Console.ReadLine();
        while (string.IsNullOrEmpty(releasedate) || releasedate.Trim().Length == 0)
        {
            Console.WriteLine("Vul in een geldige naam a.u.b!");
            Thread.Sleep(3000);
            releasedate = Console.ReadLine();
        }
        if (releasedate.Trim() == "*")
        {
            Console.WriteLine("Bewerking is geannuleerd!");
            Thread.Sleep(3000);
            Console.Clear();
            return;
        }
        


        movieList.Add(new movie()
        {

            id = id,
            name = name.Trim(),
            year = year,
            categories = genreArr,
            storyline = storyline.Trim(),
            director = director.Trim(),
            releasedate = releasedate.Trim(),
            showing = false


        });


        //verdander de lijst naar een json type
        var convertedJson = JsonConvert.SerializeObject(movieList, Formatting.Indented);
        //verander de hele file met de nieuwe json informatie
        File.WriteAllText(url, convertedJson);
        Console.WriteLine("Film succesvol toegevoegd!");
        Thread.Sleep(3000);
        Console.Clear();
    }


    public static void editFilm(List<movie> movieList, string url)
    {
        Console.Clear();
        Console.WriteLine("*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*");
        Console.WriteLine("|                     Aanpassen                   |");
        Console.WriteLine("*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*");
        
        int[] filmIdArr = new int[movieList.Count()];
        for(int i=0; i<movieList.Count(); i++)
        {
            filmIdArr[i] = movieList[i].id;
        }

        int filmId = int.MinValue;
        string placeHolder = "";
        while (!filmIdArr.Contains(filmId))
        {
            Console.WriteLine("----------------------------------------------------------------------------");
            Console.WriteLine("| Voer de ID in van de film die u wilt aanpassen: (typ '*' om te annuleren)|");
            Console.WriteLine("----------------------------------------------------------------------------");

            placeHolder = Console.ReadLine();
            if (int.TryParse(placeHolder, out filmId))
            {
                if (!filmIdArr.Contains(filmId))
                {
                    Console.WriteLine("Er is geen film voor de ingevoerde ID!\n");
                    filmId = int.MinValue;
                    Thread.Sleep(3000);
                }
            }
            else
            {
                if(placeHolder.Trim() == "*")
                {
                    Console.WriteLine("Bewerking is geannuleerd!");
                    Thread.Sleep(3000);
                    Console.Clear();
                    return;
                }
                else
                {
                    Console.WriteLine("Voer een geldige ID in a.u.b!");
                    Thread.Sleep(3000);
                    filmId = int.MinValue;
                }

            }
        }
        Console.WriteLine("------------------------------------------------------------------------------------------------");
        Console.WriteLine("|  Voer de aangepaste naam van de film in: (typ '/' om over te slaan of '*' om te annuleren)   |");
        Console.WriteLine("------------------------------------------------------------------------------------------------");
        string name = Console.ReadLine();
        while (string.IsNullOrEmpty(name) || name.Trim().Length == 0)
        {
            Console.WriteLine("Vul in een geldige naam a.u.b!");
            Thread.Sleep(3000);
            name = Console.ReadLine();
        }

        if (name.Trim() == "*")
        {
            Console.WriteLine("Bewerking is geannuleerd!");
            Thread.Sleep(3000);
            Console.Clear();
            return;
        }
        else if (name.Trim() == "/")
        {
            Console.WriteLine("Veld wordt overgeslagen!");
            Thread.Sleep(3000);
            for (int i = 0; i < movieList.Count(); i++)
            {
                if (movieList[i].id == filmId)
                {
                    name = movieList[i].name;
                }
            }
        }

        Console.WriteLine("-----------------------------------------------------------------------------------------------");
        Console.WriteLine("|  Voer de aangepaste jaar van de film in: (typ '/' om over te slaan of '*' om te annuleren)  |");
        Console.WriteLine("-----------------------------------------------------------------------------------------------");

        bool check = true;
        int yearPlaceholder = 0;
        string year;
        while (check)
        {
            year = Console.ReadLine();
            if (int.TryParse(year, out yearPlaceholder))
            {
                if (yearPlaceholder < 1888 || yearPlaceholder > @DateTime.Now.Year)
                {
                    Console.WriteLine("Voer een geldige jaar in a.u.b!\n");
                    Thread.Sleep(3000);
                }
                else
                    check = false;
            }
            else
            {
                if (year.Trim() == "*")
                {
                    Console.WriteLine("Bewerking is geannuleerd!");
                    Thread.Sleep(3000);
                    Console.Clear();
                    return;
                }
                else if (year.Trim() == "/")
                {
                    Console.WriteLine("Veld wordt overgeslagen!");
                    Thread.Sleep(3000);
                    for (int i = 0; i < movieList.Count(); i++)
                    {
                        if (movieList[i].id == filmId)
                        {
                            year = movieList[i].year;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Voer een geldige jaar in a.u.b!\n");
                    Thread.Sleep(3000);
                }
            }
        }
        year = yearPlaceholder.ToString();
        

        Console.WriteLine("-----------------------------------------------------------------------------------------------");
        Console.WriteLine("|   Voer de aantal genres van de film in: (typ '/' om over te slaan of '*' om te annuleren)   |");
        Console.WriteLine("-----------------------------------------------------------------------------------------------");
        int lengthArr = 0;
        check = true;
        string a = "";
        while (check)
        {
            placeHolder = Console.ReadLine();
            if (int.TryParse(placeHolder, out lengthArr))
            {

                check = false;
            }
            else
            {
                if (placeHolder.Trim() == "*")
                {
                    Console.WriteLine("Bewerking is geannuleerd!");
                    Thread.Sleep(3000);
                    Console.Clear();
                    return;
                }
                else if (placeHolder.Trim() == "/")
                {
                    Console.WriteLine("Veld wordt overgeslagen!");
                    Thread.Sleep(3000);
                    for(int i = 0; i < movieList.Count; i++)
                    {
                        if (movieList[i].id == filmId)
                        {
                            lengthArr = movieList[i].categories.Length;
                            break;
                        }
                    }
                    check = false;
                }
                else
                {
                    Console.WriteLine("Uw invoer is geen nummer!\n");
                    Thread.Sleep(1000);
                    
                }
                
            }
        }

        string[] genreArr = new string[lengthArr]; 
        if (placeHolder != "/")
        {
            
            for (int i = 0; i < lengthArr; i++)
            {
                Console.WriteLine("---------------------------------------------");
                Console.WriteLine("|               Kies een genre:             |");
                Console.WriteLine("---------------------------------------------");
                Console.WriteLine("| [1] Actie | [2] Misdaad | [3] Avontuur | [4] SciFi | [5] KinderFilm | [6] Documentaire | [7] Romance | [8] Komedie |\n");

                string genreKeuze = Console.ReadLine();

                while (genreKeuze != "1" && genreKeuze != "2" && genreKeuze != "3" && genreKeuze != "4" && genreKeuze != "5" && genreKeuze != "6" && genreKeuze != "7" && genreKeuze != "8")
                {
                    Console.WriteLine("Ongeldig keuze!");
                    Console.WriteLine("Probeer opnieuw a.u.b!");
                    genreKeuze = Console.ReadLine();
                }
                switch (genreKeuze)
                {
                    case "1":
                        genreKeuze = "Actie";
                        break;
                    case "2":
                        genreKeuze = "Misdaad";
                        break;
                    case "3":
                        genreKeuze = "Avontuur";
                        break;
                    case "4":
                        genreKeuze = "Scifi";
                        break;
                    case "5":
                        genreKeuze = "KinderFilm";
                        break;
                    case "6":
                        genreKeuze = "Documentaire";
                        break;
                    case "7":
                        genreKeuze = "Romance";
                        break;
                    case "8":
                        genreKeuze = "Komedie";
                        break;
                }

                genreArr[i] = genreKeuze;
            }
        }
        else
        {
            for (int i = 0; i < movieList.Count; i++)
            {
                if (movieList[i].id == filmId)
                {
                    for(int j = 0; j < movieList[i].categories.Length; j++)
                    genreArr[j] = movieList[i].categories[j];

                }
            }
        }
            

        Console.WriteLine("----------------------------------------------------------------------------------------------");
        Console.WriteLine("|    Voer de verhaal van de film in: (typ '/' om over te slaan of '*' om te annuleren)   |");
        Console.WriteLine("----------------------------------------------------------------------------------------------");
        string storyLine = Console.ReadLine();

        while (string.IsNullOrEmpty(storyLine) || storyLine.Trim().Length == 0)
        {
            Console.WriteLine("Uw invoer is ongeldig!");
            Thread.Sleep(3000);
            storyLine = Console.ReadLine();
        }
        if (storyLine.Trim() == "*")
        {
            Console.WriteLine("Bewerking is geannuleerd!");
            Thread.Sleep(3000);
            Console.Clear();
            return;
        }
        else if (storyLine.Trim() == "/")
        {
            Console.WriteLine("Veld wordt overgeslagen!");
            Thread.Sleep(3000);
            for (int i = 0; i < movieList.Count(); i++)
            {
                if (movieList[i].id == filmId)
                {
                    storyLine = movieList[i].storyline;
                }
            }
        }

        Console.WriteLine("------------------------------------------------------------------------------------------------");
        Console.WriteLine("|     Voer de naam van de regisseur in: (typ '/' om over te slaan of '*' om te annuleren)      |");
        Console.WriteLine("------------------------------------------------------------------------------------------------");
        string director = Console.ReadLine();
        if (director.Trim() == "*")
        {
            Console.WriteLine("Bewerking is geannuleerd!");
            Thread.Sleep(3000);
            Console.Clear();
            return;
        }
        else if (director.Trim() == "/")
        {
            Console.WriteLine("Veld wordt overgeslagen!");
            Thread.Sleep(3000);
            for (int i = 0; i < movieList.Count(); i++)
            {
                if (movieList[i].id == filmId)
                {
                    director = movieList[i].director;
                }
            }
        }

        Console.WriteLine("----------------------------------------------------------------------------------------------------");
        Console.WriteLine("|     Voer de publicatiedatum in:(DD-MM-YYYY) (typ '/' om over te slaan of '*' om te annuleren)    |");
        Console.WriteLine("----------------------------------------------------------------------------------------------------");
        string releasedate = Console.ReadLine();
        if (releasedate.Trim() == "*")
        {
            Console.WriteLine("Bewerking is geannuleerd!");
            Thread.Sleep(3000);
            Console.Clear();
            return;
        }
        else if (releasedate.Trim() == "/")
        {
            Console.WriteLine("Veld wordt overgeslagen!");
            Thread.Sleep(3000);
            for (int i = 0; i < movieList.Count(); i++)
            {
                if (movieList[i].id == filmId)
                {
                    releasedate = movieList[i].releasedate;
                }
            }
        }

        for (int i=0; i<movieList.Count; i++)
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
        Console.WriteLine("Film succesvol aangepast!");
        Thread.Sleep(3000);
        Console.Clear();
    }

    public static void removeFilm(List<movie> movieList, string url)
    {
        
        int[] filmArray = new int[movieList.Count()];
        for (int i = 0; i < movieList.Count(); i++)
        {
            filmArray[i] = movieList[i].id;
        }
        int filmNr = int.MinValue;
        //bool check = true;
        //string intCheck = "";
        Console.WriteLine("*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*");
        Console.WriteLine("|                   Verwijderen                     |");
        Console.WriteLine("*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*");
        while (!filmArray.Contains(filmNr))
        {
            
            Console.WriteLine("-------------------------------------------------------------------------------");
            Console.WriteLine("| Voer de ID in van de film die u wilt verwijderen: (typ '*' om te annuleren) |");
            Console.WriteLine("-------------------------------------------------------------------------------");

            string placeHolder = Console.ReadLine();
            if (int.TryParse(placeHolder, out filmNr))
            {
                if (!filmArray.Contains(filmNr))
                {
                    Console.WriteLine("Er is geen film voor de ingevoerde ID!\n");
                    filmNr = int.MinValue;
                    Thread.Sleep(3000);
                }
                
            }
            else
            {
                if (placeHolder.Trim() == "*")
                {
                    Console.WriteLine("Bewerking is geannuleerd!");
                    Thread.Sleep(3000);
                    Console.Clear();
                    return;
                }
                else
                {
                    Console.WriteLine("Voer een geldige ID in a.u.b!");
                    Thread.Sleep(3000);
                    filmNr = int.MinValue;
                }


            }
            

            
        }
        //movieList.RemoveAt(filmNr);
        for (int i = 0; i < movieList.Count; i++)
        {
            if (movieList[i].id == filmNr)
            {
                movieList.Remove(movieList[i]);
                break;
            }
        }

        //verdander de lijst naar een json type
        string cinemaLijst = JsonConvert.SerializeObject(movieList, Formatting.Indented);
        //verander de hele file met de nieuwe json informatie
        File.WriteAllText(url, cinemaLijst);
        Console.WriteLine("Film succesvol verwijderd!");
        Thread.Sleep(3000);
        Console.Clear();
    }

    public static void viewFilm(List<movie> movieList)
    {
        string Keuze = "";

        
            int[] filmIdArr = new int[movieList.Count()];
            for (int i = 0; i < movieList.Count(); i++)
            {
                filmIdArr[i] = movieList[i].id;
            }

            int filmId = int.MinValue;
            while (!filmIdArr.Contains(filmId))
            {
                Console.WriteLine("----------------------------------------------------------------------------");
                Console.WriteLine("| Voer de ID in van de film die u wilt kijken: (typ '*' om te annuleren)|");
                Console.WriteLine("----------------------------------------------------------------------------");

                string placeHolder = Console.ReadLine();
                if (int.TryParse(placeHolder, out filmId))
                {
                    if (!filmIdArr.Contains(filmId))
                    {
                        Console.WriteLine("Er is geen film voor de ingevoerde ID!\n");
                        filmId = int.MinValue;
                        Thread.Sleep(3000);
                    }
                    
                }
                else
                {
                    if (placeHolder.Trim() == "*")
                    {
                        Console.WriteLine("Bewerking is geannuleerd!");
                        Thread.Sleep(3000);
                        Console.Clear();
                        return;
                    }
                    else
                    {
                    Console.WriteLine("Voer een geldige ID in a.u.b!");
                    Thread.Sleep(3000);
                    filmId = int.MinValue;
                    }

                }
            }


        while(Keuze != "1")
        {

            Console.Clear();
            Console.WriteLine("*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*");
            Console.WriteLine("|                                            Filmoverzicht                                          |");
            Console.WriteLine("*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*" + " \n");




            string showing = "";
            foreach (dynamic item in movieList)
            {
                if (item.id == filmId)
                {
                    string genres = "";
                    foreach (string item2 in item.categories)
                    {

                        if (item2 == item.categories[item.categories.Length - 1])
                        {
                            genres += item2;
                        }
                        else
                        {
                            genres += item2 + "\n                                   ";
                        }

                    }
                    if (item.showing)
                        showing = "NU TE ZIEN";
                    else
                        showing = "MOMENTEEL NIET TE ZIEN";



                    Console.WriteLine("*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*\n");
                    Console.WriteLine("                                         Film-ID : " + filmId);
                    Console.WriteLine("                           Naam: " + item.name);
                    Console.WriteLine("                           Publicatiejaar: " + item.year);
                    Console.WriteLine("                           Genres: " + genres);
                    Console.WriteLine("                           Regisseur: " + item.director);
                    Console.WriteLine("                           Publicatie datum: " + item.releasedate);
                    Console.WriteLine("                           Status: " + showing + "\n");
                    //Console.WriteLine("                           Verhaal: " + item.storyline +"\n\n");
                    Console.WriteLine("*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*");


                    if (item.showing)
                    {
                        Console.WriteLine("\n" + "*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-" + "\n" +
                                                 "| [1] Terug | [2] Reserveren | " + "\n" +
                                                 "------------------------------");

                        Keuze = Console.ReadLine();
                        while (Keuze != "1" && Keuze != "2")
                        {
                            Keuze = "";
                            Console.WriteLine("Kies a.u.b. een van de bovenstaande opties.\n");
                            Keuze = Console.ReadLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine("\n" + "*-*-*-*-*-*-*" + "\n" +
                                                 "| [1] Terug |" + "\n" +
                                                 "-------------");

                        Keuze = Console.ReadLine();
                        while (Keuze != "1")
                        {
                            Keuze = "";
                            Console.WriteLine("Kies a.u.b. een van de bovenstaande opties.\n");
                            Keuze = Console.ReadLine();
                        }
                    }
                    break;
                }
                
            }

            switch (Keuze)
            {
                case "2":
                    Console.WriteLine("Reserveren");
                    Thread.Sleep(3000);
                    break;
            }
        }
    }

    private static bool IsNullOrEmpty(string name)
    {
        throw new NotImplementedException();
    }

}