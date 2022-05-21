using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

public class Movies
{

    public void show(string rol, int accountID)
    {
        //Omzet van gegevens in Json bestand over naar string en daarna in een lijst zetten.
        string url = "..\\..\\..\\movies.json";
        List<movie> movieList = JsonConvert.DeserializeObject<List<movie>>(File.ReadAllText(url));

        string locatieUrl = "..\\..\\..\\locatie.json";
        List<Cinema_adress> locatieList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Cinema_adress>>(File.ReadAllText(locatieUrl));

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
                Console.WriteLine("\n" + "*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*" + "\n" +
                                         "| [1] Terug | [2] Toevoegen | [3] Aanpassen | [4] Verwijderen | [5] Kijk Film | [6] Zoeken | [7] Filtreren | [8] Film toewijzen |" + "\n" +
                                         "---------------------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine("----------------------------------------------------------------");
                Console.WriteLine("| Toets 1 om terug te keren naar het hoofdmenu.                |" + "\n" +
                                  "| Toets 2 om een nieuwe film toe te voegen.                    |" + "\n" +
                                  "| Toets 3 om de gegevens van een bestaande film aan te passen. |" + "\n" +
                                  "| Toets 4 om een bestaande film te verwijderen.                |" + "\n" +
                                  "| Toets 5 om een compleet overzicht van een film te krijgen.   |" + "\n" +
                                  "| Toets 6 om een film op naam te zoeken.                       |" + "\n" +
                                  "| Toets 7 om films op genre te filteren.                       |" + "\n" +
                                  "| Toets 8 om een film in te roosteren.                         |");
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
                while (Keuze.Trim() != "1" && Keuze.Trim() != "2" && Keuze.Trim() != "3" && Keuze.Trim() != "4")
                {
                    Console.WriteLine("Kies a.u.b. een van de bovenstaande opties.\n");                 
                    Keuze = Console.ReadLine();
                }
                switch (Keuze.ToLower().Trim())
                {

                    case "2":
                        viewFilm(movieList, accountID);
                        break;
                    case "3":
                        
                        break;
                    case "4":
                        
                        break;
                }
            }
            else if (rol =="admin")
            {
                while (Keuze.Trim() != "1"  && Keuze.Trim() != "2"  && Keuze.Trim() != "3"  && Keuze.Trim() != "4" && Keuze.Trim() != "5" && Keuze.Trim() != "6" && Keuze.Trim() != "7" && Keuze.Trim() != "8")
                {
                    Console.WriteLine("Kies a.u.b. een van de bovenstaande opties.\n");
                    Keuze = Console.ReadLine();
                }
                switch (Keuze.ToLower().Trim())
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
                        viewFilm(movieList, accountID);
                        break;
                    case "6":
                        break;
                    case "7":
                        break;
                    case "8":
                        assignFilm(movieList, url, locatieList, locatieUrl);
                        break;

                }
            }
        }
    }

    private static void addFilm(List<movie> movieList, string url)
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


    private static void editFilm(List<movie> movieList, string url)
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

    private static void removeFilm(List<movie> movieList, string url)
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

    private static void viewFilm(List<movie> movieList,int id)
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


            string filmID = "";

            string showing = "";
            foreach (dynamic item in movieList)
            {
                if (item.id == filmId)
                {
                    filmID = item.id.ToString();
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
                    ReservatieFilms reservatie = new ReservatieFilms();
                    reservatie.reserveren(filmID, id);
                    Console.WriteLine("Reserveren");
                    Thread.Sleep(1000);
                    break;
            }
        }
        Console.Clear();
    }

    private static void assignFilm(List<movie> movielist,string movieUrl, List<Cinema_adress>locatieList, string locatieUrl)
    {
        Console.Clear();
        Console.WriteLine("*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*");
        Console.WriteLine("|               Film toewijzen              |");
        Console.WriteLine("*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*" + "\n");

        int[] filmIdArr = new int[movielist.Count()];
        for (int i = 0; i < movielist.Count(); i++)
        {
            filmIdArr[i] = movielist[i].id;
        }

        int filmId = int.MinValue;
        string filmIDplaceHolder = "";
        while (!filmIdArr.Contains(filmId))
        {
            Console.WriteLine("------------------------------------------------------------------------------");
            Console.WriteLine("| Voer de ID in van de film die u wilt inroosteren: (typ '*' om te annuleren)|");
            Console.WriteLine("------------------------------------------------------------------------------");

            filmIDplaceHolder = Console.ReadLine();
            if (int.TryParse(filmIDplaceHolder, out filmId))
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
                if (filmIDplaceHolder.Trim() == "*")
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
        foreach(dynamic item in locatieList)
        {
            Console.WriteLine("--------------------------------------------------------"); 
            Console.WriteLine("             ID: " + item.id);
            Console.WriteLine("             Locatie Naam: " + item.name);
            Console.WriteLine("--------------------------------------------------------\n");
        }

        int[] bioscoopNrArray = new int[locatieList.Count()];
        for (int i = 0; i < locatieList.Count(); i++)
        {
            bioscoopNrArray[i] = locatieList[i].id;
        }

        //de huidige nummer is NinValue zodat dit geen goede id is
        int bioscoopID = int.MinValue;
        string bioscoopIDplaceHolder;

        while (!bioscoopNrArray.Contains(bioscoopID))
        {

            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine("| Kies een locatie: (typ '*' om te annuleren) |");
            Console.WriteLine("-----------------------------------------------");
            bioscoopIDplaceHolder = Console.ReadLine();

            if (int.TryParse(bioscoopIDplaceHolder, out bioscoopID))
            {
                if (!bioscoopNrArray.Contains(bioscoopID))
                {
                    Console.WriteLine("Er is geen locatie die overeenkomt met deze ID!\n");
                    bioscoopID = int.MinValue;
                    Thread.Sleep(3000);
                }

            }
            else
            {
                if (bioscoopIDplaceHolder.Trim() == "*")
                {
                    Console.WriteLine("Bewerking is geannuleerd!");
                    Thread.Sleep(3000);
                    Console.Clear();
                    return;
                }
                else
                {
                    bioscoopID = int.MinValue;
                    Console.WriteLine("Voer een geldige ID in a.u.b!");
                    Thread.Sleep(1000);
                }
            }
        }
        Console.WriteLine("-------------------------------------------------------");
        Console.WriteLine("| Kies een filmtechnologie: (typ '*' om te annuleren) |");
        Console.WriteLine("-------------------------------------------------------");
        Console.WriteLine("------------------------------" + "\n" +
                          "| [1] 2D | [2] 3D | [3] IMAX |" + "\n" +
                          "------------------------------");
        string filmTechnologie = Console.ReadLine();
        while (filmTechnologie.Trim() != "1" && filmTechnologie.Trim() != "2" && filmTechnologie.Trim() != "3" && filmTechnologie.Trim() != "*")
        {
            Console.WriteLine("Kies a.u.b. een van de bovenstaande opties.\n");
            filmTechnologie = Console.ReadLine();
        }

        if (filmTechnologie.Trim() == "*")
        {
            Console.WriteLine("Bewerking is geannuleerd!");
            Thread.Sleep(3000);
            Console.Clear();
            return;
        }

        switch (filmTechnologie.Trim())
        {
            case "1":
                filmTechnologie = "2D";
                break;
            case "2":
                filmTechnologie = "3D";
                break;
            case"3":
                filmTechnologie = "IMAX";
                break;
        }

        string[] beschikbaarDatum;
        string[] helper;
        int countlength = 0;
        foreach(dynamic item in locatieList)
        {
            if(item.id == bioscoopID)
            {
                foreach(dynamic dag in item.dagen)
                {
                    for(int i=0; i<dag.Count; i++)
                    {
                        if(dag[i].type == filmTechnologie && dag[i].film_ID == null)
                        {
                           
                            countlength++;
                            
                        }
                    }
                }
                
            }
        }

        beschikbaarDatum = new string[countlength];
        helper = new string[countlength];

        foreach (dynamic item in locatieList)
        {
            if (item.id == bioscoopID)
            {
                int index = 0;
                int index2 = 0;
                foreach (dynamic dag in item.dagen)
                {
                    for (int i = 0; i < dag.Count; i++)
                    {
                        if (dag[i].type == filmTechnologie && dag[i].film_ID == null)
                        {
                            beschikbaarDatum[index++] = $"[{index}] Datum: {dag[i].datum}\n\n";
                            helper[index2++] = dag[i].datum;
                        }
                    }
                    
                }
            }
        }


        Console.WriteLine("-------------------------------------------------");
        Console.WriteLine("| Beschikbare datums: (typ '*' om te annuleren) |");
        Console.WriteLine("-------------------------------------------------\n");

        string Keuze;
        string gekozenDatum = "";
        if (beschikbaarDatum.Length == 0)
        {
            Console.WriteLine("Er zijn geen tijden beschikbaar! (typ '*' om te annuleren) \n");
            Keuze = Console.ReadLine();

            while (Keuze.Trim() != "*")
            {
                Console.WriteLine("Er zijn geen tijden beschikbaar! (typ '*' om te annuleren) \n");
                Keuze = Console.ReadLine();

            }

            if (Keuze.Trim() == "*")
            {
                Console.WriteLine("Bewerking is geannuleerd!");
                Thread.Sleep(3000);
                Console.Clear();
                return;
            }
        }
        else
        {
            for (int i = 0; i < beschikbaarDatum.Length; i++)
            {
                Console.WriteLine(beschikbaarDatum[i]);
            }

            Keuze = Console.ReadLine();

            switch (beschikbaarDatum.Length)
            {
                case 1:
                    while (Keuze.Trim() != "1" && Keuze.Trim() != "*")
                    {
                        Console.WriteLine("Kies a.u.b. een van de bovenstaande opties.\n");
                        Keuze = Console.ReadLine();
                    }
                    if (Keuze.Trim() == "*")
                    {
                        Console.WriteLine("Bewerking is geannuleerd!");
                        Thread.Sleep(3000);
                        Console.Clear();
                        return;
                    }
                    gekozenDatum = helper[Int32.Parse(Keuze) - 1];
                    break;
                case 2:
                    while (Keuze.Trim() != "1" && Keuze.Trim() != "2" && Keuze.Trim() != "*")
                    {
                        Console.WriteLine("Kies a.u.b. een van de bovenstaande opties.\n");
                        Keuze = Console.ReadLine();
                    }
                    if (Keuze.Trim() == "*")
                    {
                        Console.WriteLine("Bewerking is geannuleerd!");
                        Thread.Sleep(3000);
                        Console.Clear();
                        return;
                    }
                    gekozenDatum = helper[Int32.Parse(Keuze) - 1];
                    break;
                case 3:
                    while (Keuze.Trim() != "1" && Keuze.Trim() != "2" && Keuze.Trim() != "3" && Keuze.Trim() != "*")
                    {
                        Console.WriteLine("Kies a.u.b. een van de bovenstaande opties.\n");
                        Keuze = Console.ReadLine();
                    }
                    if (Keuze.Trim() == "*")
                    {
                        Console.WriteLine("Bewerking is geannuleerd!");
                        Thread.Sleep(3000);
                        Console.Clear();
                        return;
                    }
                    gekozenDatum = helper[Int32.Parse(Keuze) - 1];
                    break;
                case 4:
                    while (Keuze.Trim() != "1" && Keuze.Trim() != "2" && Keuze.Trim() != "3" && Keuze.Trim() != "4" && Keuze.Trim() != "*")
                    {
                        Console.WriteLine("Kies a.u.b. een van de bovenstaande opties.\n");
                        Keuze = Console.ReadLine();
                    }
                    if (Keuze.Trim() == "*")
                    {
                        Console.WriteLine("Bewerking is geannuleerd!");
                        Thread.Sleep(3000);
                        Console.Clear();
                        return;
                    }
                    gekozenDatum = helper[Int32.Parse(Keuze) - 1];
                    break;
                case 5:
                    while (Keuze.Trim() != "1" && Keuze.Trim() != "2" && Keuze.Trim() != "3" && Keuze.Trim() != "4" && Keuze.Trim() != "5" && Keuze.Trim() != "*")
                    {
                        Console.WriteLine("Kies a.u.b. een van de bovenstaande opties.\n");
                        Keuze = Console.ReadLine();
                    }
                    if (Keuze.Trim() == "*")
                    {
                        Console.WriteLine("Bewerking is geannuleerd!");
                        Thread.Sleep(3000);
                        Console.Clear();
                        return;
                    }
                    gekozenDatum = helper[Int32.Parse(Keuze) - 1];
                    break;
                case 6:
                    while (Keuze.Trim() != "1" && Keuze.Trim() != "2" && Keuze.Trim() != "3" && Keuze.Trim() != "4" && Keuze.Trim() != "5" && Keuze.Trim() != "6" && Keuze.Trim() != "*")
                    {
                        Console.WriteLine("Kies a.u.b. een van de bovenstaande opties.\n");
                        Keuze = Console.ReadLine();
                    }
                    if (Keuze.Trim() == "*")
                    {
                        Console.WriteLine("Bewerking is geannuleerd!");
                        Thread.Sleep(3000);
                        Console.Clear();
                        return;
                    }
                    gekozenDatum = helper[Int32.Parse(Keuze) - 1];
                    break;
                case 7:
                    while (Keuze.Trim() != "1" && Keuze.Trim() != "2" && Keuze.Trim() != "3" && Keuze.Trim() != "4" && Keuze.Trim() != "5" && Keuze.Trim() != "6" && Keuze.Trim() != "7" 
                        && Keuze.Trim() != "*")
                    {
                        Console.WriteLine("Kies a.u.b. een van de bovenstaande opties.\n");
                        Keuze = Console.ReadLine();
                    }
                    if (Keuze.Trim() == "*")
                    {
                        Console.WriteLine("Bewerking is geannuleerd!");
                        Thread.Sleep(3000);
                        Console.Clear();
                        return;
                    }
                    gekozenDatum = helper[Int32.Parse(Keuze) - 1];
                    break;
                case 8:
                    while (Keuze.Trim() != "1" && Keuze.Trim() != "2" && Keuze.Trim() != "3" && Keuze.Trim() != "4" && Keuze.Trim() != "5" && Keuze.Trim() != "6" && Keuze.Trim() != "7" 
                        && Keuze.Trim() != "8" && Keuze.Trim() != "*")
                    {
                        Console.WriteLine("Kies a.u.b. een van de bovenstaande opties.\n");
                        Keuze = Console.ReadLine();
                    }
                    if (Keuze.Trim() == "*")
                    {
                        Console.WriteLine("Bewerking is geannuleerd!");
                        Thread.Sleep(3000);
                        Console.Clear();
                        return;
                    }
                    gekozenDatum = helper[Int32.Parse(Keuze) - 1];
                    break;
                case 9:
                    while (Keuze.Trim() != "1" && Keuze.Trim() != "2" && Keuze.Trim() != "3" && Keuze.Trim() != "4" && Keuze.Trim() != "5" && Keuze.Trim() != "6" && Keuze.Trim() != "7" 
                        && Keuze.Trim() != "8" && Keuze.Trim() != "9" && Keuze.Trim() != "*")
                    {
                        Console.WriteLine("Kies a.u.b. een van de bovenstaande opties.\n");
                        Keuze = Console.ReadLine();
                    }
                    if (Keuze.Trim() == "*")
                    {
                        Console.WriteLine("Bewerking is geannuleerd!");
                        Thread.Sleep(3000);
                        Console.Clear();
                        return;
                    }
                    gekozenDatum = helper[Int32.Parse(Keuze) - 1];
                    break;
                case 10:
                    while (Keuze.Trim() != "1" && Keuze.Trim() != "2" && Keuze.Trim() != "3" && Keuze.Trim() != "4" && Keuze.Trim() != "5" && Keuze.Trim() != "6" && Keuze.Trim() != "7" 
                        && Keuze.Trim() != "8" && Keuze.Trim() != "9" && Keuze.Trim() != "10"  && Keuze.Trim() != "*")
                    {
                        Console.WriteLine("Kies a.u.b. een van de bovenstaande opties.\n");
                        Keuze = Console.ReadLine();
                    }
                    if (Keuze.Trim() == "*")
                    {
                        Console.WriteLine("Bewerking is geannuleerd!");
                        Thread.Sleep(3000);
                        Console.Clear();
                        return;
                    }
                    gekozenDatum = helper[Int32.Parse(Keuze) - 1];
                    break;
                case 11:
                    while (Keuze.Trim() != "1" && Keuze.Trim() != "2" && Keuze.Trim() != "3" && Keuze.Trim() != "4" && Keuze.Trim() != "5" && Keuze.Trim() != "6" && Keuze.Trim() != "7" 
                        && Keuze.Trim() != "8" && Keuze.Trim() != "9" && Keuze.Trim() != "10" && Keuze.Trim() != "11"  && Keuze.Trim() != "*")
                    {
                        Console.WriteLine("Kies a.u.b. een van de bovenstaande opties.\n");
                        Keuze = Console.ReadLine();
                    }
                    if (Keuze.Trim() == "*")
                    {
                        Console.WriteLine("Bewerking is geannuleerd!");
                        Thread.Sleep(3000);
                        Console.Clear();
                        return;
                    }
                    gekozenDatum = helper[Int32.Parse(Keuze) - 1];
                    break;
                case 12:
                    while (Keuze.Trim() != "1" && Keuze.Trim() != "2" && Keuze.Trim() != "3" && Keuze.Trim() != "4" && Keuze.Trim() != "5" && Keuze.Trim() != "6" && Keuze.Trim() != "7" 
                        && Keuze.Trim() != "8" && Keuze.Trim() != "9" && Keuze.Trim() != "10" && Keuze.Trim() != "11" && Keuze.Trim() != "12"  && Keuze.Trim() != "*")
                    {
                        Console.WriteLine("Kies a.u.b. een van de bovenstaande opties.\n");
                        Keuze = Console.ReadLine();
                    }
                    if (Keuze.Trim() == "*")
                    {
                        Console.WriteLine("Bewerking is geannuleerd!");
                        Thread.Sleep(3000);
                        Console.Clear();
                        return;
                    }
                    gekozenDatum = helper[Int32.Parse(Keuze) - 1];
                    break;
                case 13:
                    while (Keuze.Trim() != "1" && Keuze.Trim() != "2" && Keuze.Trim() != "3" && Keuze.Trim() != "4" && Keuze.Trim() != "5" && Keuze.Trim() != "6" && Keuze.Trim() != "7" && Keuze.Trim() != "8" && Keuze.Trim() != "9" && Keuze.Trim() != "10" && Keuze.Trim() != "11" && Keuze.Trim() != "12" && Keuze.Trim() != "13" &&  Keuze.Trim() != "*")
                    {
                        Console.WriteLine("Kies a.u.b. een van de bovenstaande opties.\n");
                        Keuze = Console.ReadLine();
                    }
                    if (Keuze.Trim() == "*")
                    {
                        Console.WriteLine("Bewerking is geannuleerd!");
                        Thread.Sleep(3000);
                        Console.Clear();
                        return;
                    }
                    gekozenDatum = helper[Int32.Parse(Keuze) - 1];
                    break;
                case 14:
                    while (Keuze.Trim() != "1" && Keuze.Trim() != "2" && Keuze.Trim() != "3" && Keuze.Trim() != "4" && Keuze.Trim() != "5" && Keuze.Trim() != "6" && Keuze.Trim() != "7" && Keuze.Trim() != "8" && Keuze.Trim() != "9" && Keuze.Trim() != "10" && Keuze.Trim() != "11" && Keuze.Trim() != "12" && Keuze.Trim() != "13" && Keuze.Trim() != "14" && Keuze.Trim() != "*")
                    {
                        Console.WriteLine("Kies a.u.b. een van de bovenstaande opties.\n");
                        Keuze = Console.ReadLine();
                    }
                    if (Keuze.Trim() == "*")
                    {
                        Console.WriteLine("Bewerking is geannuleerd!");
                        Thread.Sleep(3000);
                        Console.Clear();
                        return;
                    }
                    gekozenDatum = helper[Int32.Parse(Keuze) - 1];
                    break;
            }

        }

        
        foreach (dynamic item in locatieList)
        {
            if (item.id == bioscoopID)
            {
                foreach (dynamic dag in item.dagen)
                {
                    for (int i = 0; i < dag.Count; i++)
                    {
                        if (dag[i].type == filmTechnologie && dag[i].datum == gekozenDatum)
                        {
                            dag[i].film_ID = filmId;
                        }
                    }
                }
            }
        }

        

        foreach (dynamic item in movielist)
        {
            if (item.id == filmId)
            {
                item.showing = true;
                break;
            }
        }


        //verander de hele file met de nieuwe json informatie
        File.WriteAllText(locatieUrl, JsonConvert.SerializeObject(locatieList, Formatting.Indented));
        File.WriteAllText(movieUrl, JsonConvert.SerializeObject(movielist, Formatting.Indented));
        Console.WriteLine("Film is succesvol ingeroosterd!");
        Thread.Sleep(3000);
        Console.Clear();

    }
    private static bool IsNullOrEmpty(string name)
    {
        throw new NotImplementedException();
    }

}