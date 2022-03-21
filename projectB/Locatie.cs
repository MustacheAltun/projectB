using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Locatie
{
    
    public void viewLocations()
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
            Console.WriteLine("| [1] Terug |");
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

            Console.WriteLine("Kies de actie die u wilt uitvoeren:");
            input = Console.ReadLine();
            while(input != "1")
            {
                Console.WriteLine("Ongeldige invoer!");
                Console.WriteLine("Kies de actie die u wilt uitvoeren:");
                input = Console.ReadLine();
            }
            
        }
       
    }

    public void addLocation()
    {

    }

    public void editLocation()
    {

    }

    public void removeLocation()
    {

    }
}
