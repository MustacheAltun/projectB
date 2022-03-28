using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public class Locatie
{
    
    public void viewLocations(string rol)
    {
        //Directory van Json bestand met locatie gegevens
        string url = "..\\..\\..\\locatie.json";
        //Omzet van gegevens in Json bestand over naar string en daarna in een lijst zetten.
        string locatieJson = File.ReadAllText(url);
        List<Cinema_adress> locatieList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Cinema_adress>>(locatieJson);

        //2D-array bedoeld voor tijdelijke opslag van locatiegegevens 
        object[][] arr = new object[locatieList.Count][];

        Console.Clear();
        
        
        string input = "";
        //While loop dat blijft runnen tenzij gebruiker "1" invoert.
        while (input != "1")
        {
            
            //Forloop om 2d array (inner array) in te vullen met bioscoop locatie gegevens.
            for (int i = 0; i < locatieList.Count; i++)
            {
                arr[i] = new object[] { locatieList[i].name, locatieList[i].street, locatieList[i].zipcode, locatieList[i].city, locatieList[i].telNr };
             
            }
            //Een forloop die de verschillende locaties in de console weergeeft.
            for (int index = 0, locationNr = 1; index < locatieList.Count; index++, locationNr++)
            {
                Console.WriteLine("------------------------------------------------------------------------------------------");
                Console.WriteLine("                       Naam locatie " + locationNr + " : "+ arr[index][0]);
                Console.WriteLine("                       Adress: " + arr[index][1]);
                Console.WriteLine("                       Postcode: " + arr[index][2]);
                Console.WriteLine("                       Stad: " + arr[index][3]);
                Console.WriteLine("                       Telnr: " + arr[index][4]);
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
            Console.WriteLine("Kies de actie die u wilt uitvoeren:");
            input = Console.ReadLine();
            if (rol == "gebruiker")
            {
                while(input != "1" && input.ToLower() != "terug")
                {
                    input = "";
                    Console.WriteLine("Ongeldige invoer!");
                    Console.WriteLine("Kies de actie die u wilt uitvoeren:");
                    input = Console.ReadLine();
                }
            }
            else
            {
                while (input != "1" && input.ToLower() != "terug" && input != "2" && input.ToLower() != "toevoegen" && input != "3" && input.ToLower() != "aanpassen" && input != "4" && input.ToLower() != "verwijderen")
                {
                    Console.WriteLine("Ongeldige invoer!");
                    Console.WriteLine("Kies de actie die u wilt uitvoeren:");
                    input = Console.ReadLine();
                }
                switch (input.ToLower())
                {

                    case "2":
                    case "toevoegen":
                        addLocation(locatieList, url);
                        input = "1";
                        viewLocations(rol);
                        break;
                    case "3":
                    case "aanpassen":
                        editLocation(locatieList, url);
                        input = "1";
                        viewLocations(rol);
                        break;
                    case "4":
                    case "verwijderen":
                        removeLocation(locatieList, url);
                        input = "1";
                        viewLocations(rol);
                        break;
                }
            }
        }
       
    }

    public void addLocation(List<Cinema_adress> lijst, string url)
    {
        Console.Clear();
        Console.WriteLine("Voer bioscoop naam in:");
        string bioscoop = Console.ReadLine();
        Console.WriteLine("Voer straat in:");
        string straat = Console.ReadLine();
        Console.WriteLine("Voer postcode in:");
        string postcode = Console.ReadLine();
        Console.WriteLine("Voer stad in:");
        string stad = Console.ReadLine();
        Console.WriteLine("Voer telefoon nummer in:");
        string nummer = Console.ReadLine();
        lijst.Add(new Cinema_adress()
        {
            name = bioscoop,
            street = straat,
            zipcode = postcode,
            city = stad,
            telNr = nummer
        });
        //verdander de lijst naar een json type
        string cinemaLijst = JsonConvert.SerializeObject(lijst, Formatting.Indented);
        //verander de hele file met de nieuwe json informatie
        File.WriteAllText(url, cinemaLijst);
    }

    public void editLocation(List<Cinema_adress> lijst, string url)
    {
        Console.WriteLine("Voer ID in van de bioscoop die u wilt aanpassen:");
        
        int[] bioscoopNrArray = new int[lijst.Count()+1];
        for (int i = 0; i < lijst.Count(); i++)
        {
            bioscoopNrArray[i] = i;
        }
        int bioscoopNr = -1;
        while (!bioscoopNrArray.Contains(bioscoopNr))
        {
            try
            {
                bioscoopNr = Convert.ToInt32(Console.ReadLine())-1;
            }
            catch (Exception)
            {
                Console.WriteLine("Voer ID nummer in van de bioscoop die u wilt aanpassen:");
                bioscoopNr = Convert.ToInt32(Console.ReadLine());
            }
        }
        Console.WriteLine("Voer de aangepaste naam van de bioscoop in:");
        lijst[bioscoopNr].name = Console.ReadLine();
        Console.WriteLine("Voer de aangepaste straat van de bioscoop in:");
        lijst[bioscoopNr].street = Console.ReadLine();
        Console.WriteLine("Voer de aangepaste postcode van de bioscoop in:");
        lijst[bioscoopNr].zipcode = Console.ReadLine();
        Console.WriteLine("Voer de aangepaste stad van de bioscoop in:");
        lijst[bioscoopNr].city = Console.ReadLine();
        Console.WriteLine("Voer de aangepaste telefoon nummer van de bioscoop in:");
        lijst[bioscoopNr].telNr = Console.ReadLine();

        //verdander de lijst naar een json type
        string cinemaLijst = JsonConvert.SerializeObject(lijst, Formatting.Indented);
        //verander de hele file met de nieuwe json informatie
        File.WriteAllText(url, cinemaLijst);
    }

    public void removeLocation(List<Cinema_adress> lijst, string url)
    {
        //Console.WriteLine("Voer ID in van de bioscoop die u wilt verwijderen:");

        int[] bioscoopNrArray = new int[lijst.Count() + 1];
        for (int i = 0; i < lijst.Count(); i++)
        {
            bioscoopNrArray[i] = i;
        }
        int bioscoopNr = -1;
        while (!bioscoopNrArray.Contains(bioscoopNr))
        {
            Console.WriteLine("Voer een correcte ID in van de bioscoop die u wilt verwijderen:");
            if (int.TryParse(Console.ReadLine(), out _))
            {
                bioscoopNr = Convert.ToInt32(Console.ReadLine()) - 1;
            }
            else
            {
                Console.WriteLine("Uw invoer is geen nummer.\n");
                Thread.Sleep(1000);
            }
        }
        lijst.RemoveAt(bioscoopNr);
        //verdander de lijst naar een json type
        string cinemaLijst = JsonConvert.SerializeObject(lijst, Formatting.Indented);
        //verander de hele file met de nieuwe json informatie
        File.WriteAllText(url, cinemaLijst);
    }
}
