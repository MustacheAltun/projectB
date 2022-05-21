﻿using Newtonsoft.Json;
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
        //updateSchedule();
        string input = "";
        //While loop dat blijft runnen tenzij gebruiker "1" invoert.
        while (input != "1")
        {
            //Directory van Json bestand met locatie gegevens
            string url = "..\\..\\..\\locatie.json";
            //Omzet van gegevens in Json bestand over naar string en daarna in een lijst zetten.
            string locatieJson = File.ReadAllText(url);
            List<Cinema_adress> locatieList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Cinema_adress>>(locatieJson);

            //2D-array bedoeld voor tijdelijke opslag van locatiegegevens 
            object[][] arr = new object[locatieList.Count][];

            Console.Clear();

            Console.WriteLine("*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*");
            Console.WriteLine("|                                            Bioscopen                                              |");
            Console.WriteLine("*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*" + " \n");
            //Forloop om 2d array (inner array) in te vullen met bioscoop locatie gegevens.
            for (int i = 0; i < locatieList.Count; i++)
            {
                arr[i] = new object[] { locatieList[i].name, locatieList[i].address, locatieList[i].zipcode, locatieList[i].city, locatieList[i].telNr, locatieList[i].id };
             
            }
            //Een forloop die de verschillende locaties in de console weergeeft.
            for (int index = 0, locationNr = 1; index < locatieList.Count; index++, locationNr++)
            {
                Console.WriteLine("-----------------------------------------------------------------------------------------------------");
                Console.WriteLine("                         ID: " + arr[index][5]);
                Console.WriteLine("                         Naam locatie " + locationNr + " : "+ arr[index][0]);
                Console.WriteLine("                         Adres: " + arr[index][1]);
                Console.WriteLine("                         Postcode: " + arr[index][2]);
                Console.WriteLine("                         Stad: " + arr[index][3]);
                Console.WriteLine("                         Telnr: " + arr[index][4]);
                Console.WriteLine("-----------------------------------------------------------------------------------------------------");
            }
            if (rol == "gebruiker")
            {
                Console.WriteLine("\n" +
                                  "*-*-*-*-*-*-*");
                Console.WriteLine("| [1] Terug |" + "\n" +
                                  "-------------" + "\n");
            }
            else if (rol == "admin")
            {
                Console.WriteLine("\n" + 
                                  "*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*");
                Console.WriteLine("| [1] Terug | [2] Toevoegen | [3] Aanpassen | [4] Verwijderen |" + "\n" +
                                  "---------------------------------------------------------------");
            }
            else
            {
                {
                    Console.WriteLine("\n" +
                                      "*-*-*-*-*-*-*");
                    Console.WriteLine("| [1] Terug |" + "\n" +
                                      "-------------" + "\n");
                }
            }

            Console.WriteLine("Toets 1 om terug te gaan naar het hoofdmenu.");
            input = Console.ReadLine();
            Console.WriteLine();
            if (rol == "gebruiker")
            {
                while(input != "1")
                {
                    Console.WriteLine("Voer a.u.b. een van de mogelijke opties in.");
                    Console.WriteLine("Toets 1 om terug te gaan naar het hoofdmenu.");
                    input = Console.ReadLine();
                    Console.WriteLine();
                }
            }
            else if (rol == "admin")
            {
                while (input != "1" && input != "2"  && input != "3"  && input != "4" )
                {
                    Console.WriteLine("\nKies a.u.b. een van de bovenstaande opties.");
                    Console.WriteLine("Toets een getal en druk op enter om op de gewenste pagina te komen:\n");
                    input = Console.ReadLine();
                }
                switch (input.ToLower())
                {

                    case "2":
                        addLocation(locatieList, url);
                        break;
                    case "3":
                        editLocation(locatieList, url);
                        break;
                    case "4":
                        removeLocation(locatieList, url);
                        break;
                }
            }
            else
            {

            }
        }
       
    }

    public void addLocation(List<Cinema_adress> lijst, string url)
    {
        /*
         * geef alle informatie dus bioscoop naam postcode adres etc etc
         */
        Console.Clear();
        Console.WriteLine("*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*");
        Console.WriteLine("|          Nieuwe Locatie toevoegen           |");
        Console.WriteLine("*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*" + "\n");

        int id = 0;
        foreach (dynamic item in lijst)
        {
            //Check de laatste id
            id = item.id + 1;
        }

        Console.WriteLine("------------------------------------------------------------------");
        Console.WriteLine("|    Voer de naam van de locatie in: (typ '*' om te annuleren)   |");
        Console.WriteLine("------------------------------------------------------------------" + "\n");
        string bioscoop = Console.ReadLine();
        while (string.IsNullOrEmpty(bioscoop) || bioscoop.Trim().Length == 0)
        {
            Console.WriteLine("Vul in een geldige naam a.u.b!");
            Thread.Sleep(3000);
            bioscoop = Console.ReadLine();
        }
        if (bioscoop.Trim() == "*")
        {
            Console.WriteLine("Bewerking is geannuleerd!");
            Thread.Sleep(3000);
            Console.Clear();
            return;
        }


        Console.WriteLine("------------------------------------------------------------------------");
        Console.WriteLine("|    Voer de straat naam van de locatie in: (typ '*' om te annuleren)   |");
        Console.WriteLine("------------------------------------------------------------------------" + "\n");
        
        string straat = "";
        bool check = true;
        while (check)
        {
            straat = Console.ReadLine();
            if (int.TryParse(straat, out _))
            {
                Console.WriteLine("Ongeldig invoer!\nDe straatnaam mag geen nummer zijn!");
            }
            else
            {
                if (straat.Trim().ToCharArray().Any(char.IsDigit))
                {
                    Console.WriteLine("Een straatnaam mag geen nummer bevatten!");
                    Thread.Sleep(3000);
                }
                else if (straat.Trim() == "*")
                {
                    Console.WriteLine("Bewerking is geannuleerd!");
                    Thread.Sleep(3000);
                    Console.Clear();
                    return;
                }
                else if (straat.Trim().Length == 0)
                {
                    Console.WriteLine("Ongeldig invoer!\nVoer een geldige straatnaam in a.u.b!");
                    Thread.Sleep(3000);
                }
                else if (hasSpecialChar(straat.Trim()))
                {
                    Console.WriteLine("Een straatnaam mag geen speciale karakter bevatten!");
                    Thread.Sleep(3000);
                }
                else
                {
                    check = false;
                }
            }
        }

        Console.WriteLine("-------------------------------------------------------------------------");
        Console.WriteLine("|    Voer de huisnummer van de locatie in: (typ '*' om te annuleren)    |");
        Console.WriteLine("-------------------------------------------------------------------------" + "\n");
        string straatNr;
        check = true;
        int straatNrPlaceholder = 0;
        while (check)
        {
            straatNr = Console.ReadLine();
            if (int.TryParse(straatNr, out straatNrPlaceholder))
            {
                if (straatNrPlaceholder <= 0 || straatNrPlaceholder > 999)
                {
                    Console.WriteLine("Voer een geldige huisnummer in a.u.b!\n");
                    Thread.Sleep(3000);
                }
                else
                    check = false;
            }
            else
            {
                if (straatNr.Trim() == "*")
                {
                    Console.WriteLine("Bewerking is geannuleerd!");
                    Thread.Sleep(3000);
                    Console.Clear();
                    return;
                }
                else
                {
                    Console.WriteLine("Voer een geldige huisnummer in a.u.b!\n");
                    Thread.Sleep(3000);
                }
            }
        }
        straatNr = straatNrPlaceholder.ToString();


        Console.WriteLine("-------------------------------------------------------------------------");
        Console.WriteLine("|    Voer de postcode van de locatie in: (typ '*' om te annuleren)      |");
        Console.WriteLine("-------------------------------------------------------------------------" + "\n");
        string postcode = "";
        check = true;
        string postcodeNr;
        string postcodeString;
        while (check)
        {
            postcode = Console.ReadLine();
            if (postcode.Trim().Length == 6)
            {
                postcodeNr = postcode.Substring(0, 4);
                postcodeString = postcode.Substring(postcode.Length - 2);
                int poscodePlaceholder = 0;

                if (int.TryParse(postcodeNr, out poscodePlaceholder))
                {
                    if (poscodePlaceholder < 1000 || poscodePlaceholder > 9999)
                    {
                        Console.WriteLine("Voer een geldige postcode in a.u.b!\n");
                        Thread.Sleep(3000);
                    }
                    else if (int.TryParse(postcodeString, out _) || postcodeString.Trim().Length == 0 || hasSpecialChar(postcodeString.Trim()))
                    {
                        Console.WriteLine("Voer een geldige postcode in a.u.b!\n");
                        Thread.Sleep(3000);
                    }
                    else
                        check = false;
                    postcode = postcodeNr.ToString() + postcodeString;
                }
            }
            else
            {
                if (postcode.Trim() == "*")
                {
                    Console.WriteLine("Bewerking is geannuleerd!");
                    Thread.Sleep(3000);
                    Console.Clear();
                    return;
                }
                else
                {
                    Console.WriteLine("Voer een geldige postcode in a.u.b!\n");
                    Thread.Sleep(3000);
                }
            }
        }


        Console.WriteLine("-----------------------------------------------------------------------");
        Console.WriteLine("|    Voer de stad naam van de locatie in: (typ '*' om te annuleren)   |");
        Console.WriteLine("-----------------------------------------------------------------------" + "\n");
        string stad = "";

        check = true;
        while (check)
        {

            stad = Console.ReadLine();
            if (int.TryParse(stad, out _))
            {
                Console.WriteLine("Een stad naam mag geen nummer zijn!");
            }
            else
            {
                if (stad.Trim() == "*")
                {
                    Console.WriteLine("Bewerking is geannuleerd!");
                    Thread.Sleep(3000);
                    Console.Clear();
                    return;
                }
                else if (stad.Trim().Length == 0)
                {
                    Console.WriteLine("Ongeldig invoer!\n");
                    Thread.Sleep(3000);
                }
                else if (stad.Trim().ToCharArray().Any(char.IsDigit))
                {
                    Console.WriteLine("Een stad naam mag geen nummer bevatten!");
                    Thread.Sleep(3000);
                }
                else if (hasSpecialChar(stad.Trim()))
                {
                    Console.WriteLine("Een stad naam mag geen speciale karakter bevatten!");
                    Thread.Sleep(3000);
                }
                else
                {
                    check = false;
                }
            }
        }


        Console.WriteLine("-----------------------------------------------------------------------------");
        Console.WriteLine("|    Voer het telefoonnummer van de locatie in: (typ '*' om te annuleren)   |");
        Console.WriteLine("-----------------------------------------------------------------------------" + "\n");
        string nummer = "";
        check = true;
        while (check)
        {
            nummer = Console.ReadLine();
            if (int.TryParse(nummer, out _))
            {
                if (nummer.Length == 9)
                {
                    check = false;
                }
                else
                {
                    Console.WriteLine("Het telefoonnummer moet '9' nummers bevatten!\n");
                    Thread.Sleep(3000);
                }
            }
            else
            {
                if (nummer.Trim() == "*")
                {
                    Console.WriteLine("Bewerking is geannuleerd!");
                    Thread.Sleep(3000);
                    Console.Clear();
                    return;
                }
                else
                {
                    Console.WriteLine("Ongeldig invoer!\n");
                    Thread.Sleep(3000);
                }
            }
        }


        string zitplekken1 = "";
        for(int i=0; i<100; i++)
        {
            zitplekken1 += "T";
        }
        var tijdObj1 = new List<Tijden>();
        tijdObj1.Add(new Tijden()
        {
            tijd = "9-12",
            beschikbaar = zitplekken1
        });
        tijdObj1.Add(new Tijden()
        {
            tijd = "12-15",
            beschikbaar = zitplekken1
        });
        tijdObj1.Add(new Tijden()
        {
            tijd = "15-18",
            beschikbaar = zitplekken1
        });
        tijdObj1.Add(new Tijden()
        {
            tijd = "18-21",
            beschikbaar = zitplekken1
        });
        tijdObj1.Add(new Tijden()
        {
            tijd = "21-24",
            beschikbaar = zitplekken1
        });

        string zitplekken2 = "";
        for(int i=0; i<150; i++)
        {
            zitplekken2 += "T";
        }
        var tijdObj2 = new List<Tijden>();
        tijdObj2.Add(new Tijden()
        {
            tijd = "9-12",
            beschikbaar = zitplekken2
        });
        tijdObj2.Add(new Tijden()
        {
            tijd = "12-15",
            beschikbaar = zitplekken2
        });
        tijdObj2.Add(new Tijden()
        {
            tijd = "15-18",
            beschikbaar = zitplekken2
        });
        tijdObj2.Add(new Tijden()
        {
            tijd = "18-21",
            beschikbaar = zitplekken2
        });
        tijdObj2.Add(new Tijden()
        {
            tijd = "21-24",
            beschikbaar = zitplekken2
        });

        string zitplekken3 = "";
        for(int i=0; i<200; i++)
        {
            zitplekken3 += "T";
        }
        var tijdObj3 = new List<Tijden>();
        tijdObj3.Add(new Tijden()
        {
            tijd = "9-12",
            beschikbaar = zitplekken3
        });
        tijdObj3.Add(new Tijden()
        {
            tijd = "12-15",
            beschikbaar = zitplekken3
        });
        tijdObj3.Add(new Tijden()
        {
            tijd = "15-18",
            beschikbaar = zitplekken3
        });
        tijdObj3.Add(new Tijden()
        {
            tijd = "18-21",
            beschikbaar = zitplekken3
        });
        tijdObj3.Add(new Tijden()
        {
            tijd = "21-24",
            beschikbaar = zitplekken3
        });



        string date = DateTime.UtcNow.ToString("dd-MM-yyyy");
        
        List<Zalen> dag1 = new List<Zalen>();
        dag1.Add(new Zalen()
        {
            naam = "zaal 1",
            type = "2D",
            zitplekken = 100,
            tijden = tijdObj1,
            Create_Date = date,
            datum = date,
            prijs = 12.50
        });
        dag1.Add(new Zalen()
        {
            naam = "zaal 2",
            type = "3D",
            zitplekken = 150,
            tijden = tijdObj2,
            Create_Date = date,
            datum = date,
            prijs = 15.00
        });
        dag1.Add(new Zalen()
        {
            naam = "zaal 3",
            type = "IMAX",
            zitplekken = 200,
            tijden = tijdObj3,
            Create_Date = date,
            datum = date,
            prijs = 17.50
        });

        List<Zalen> dag2 = new List<Zalen>();
        dag2.Add(new Zalen()
        {
            naam = "zaal 1",
            type = "2D",
            zitplekken = 100,
            tijden = tijdObj1,
            Create_Date = date,
            datum = DateTime.Now.AddDays(1).ToString("dd-MM-yyyy"),
            prijs = 12.50
        });
        dag2.Add(new Zalen()
        {
            naam = "zaal 2",
            type = "3D",
            zitplekken = 150,
            tijden = tijdObj2,
            Create_Date = date,
            datum = DateTime.Now.AddDays(1).ToString("dd-MM-yyyy"),
            prijs = 15.00
        });
        dag2.Add(new Zalen()
        {
            naam = "zaal 3",
            type = "IMAX",
            zitplekken = 200,
            tijden = tijdObj3,
            Create_Date = date,
            datum = DateTime.Now.AddDays(1).ToString("dd-MM-yyyy"),
            prijs = 17.50
        });

        List<Zalen> dag3 = new List<Zalen>();
        dag3.Add(new Zalen()
        {
            naam = "zaal 1",
            type = "2D",
            zitplekken = 100,
            tijden = tijdObj1,
            Create_Date = date,
            datum = DateTime.Now.AddDays(2).ToString("dd-MM-yyyy"),
            prijs = 12.50
        });
        dag3.Add(new Zalen()
        {
            naam = "zaal 2",
            type = "3D",
            zitplekken = 150,
            tijden = tijdObj2,
            Create_Date = date,
            datum = DateTime.Now.AddDays(2).ToString("dd-MM-yyyy"),
            prijs = 15.00
        });
        dag3.Add(new Zalen()
        {
            naam = "zaal 3",
            type = "IMAX",
            zitplekken = 200,
            tijden = tijdObj3,
            Create_Date = date,
            datum = DateTime.Now.AddDays(2).ToString("dd-MM-yyyy"),
            prijs = 17.50
        });

        List<Zalen> dag4 = new List<Zalen>();
        dag4.Add(new Zalen()
        {
            naam = "zaal 1",
            type = "2D",
            zitplekken = 100,
            tijden = tijdObj1,
            Create_Date = date,
            datum = DateTime.Now.AddDays(3).ToString("dd-MM-yyyy"),
            prijs = 12.50
        });
        dag4.Add(new Zalen()
        {
            naam = "zaal 2",
            type = "3D",
            zitplekken = 150,
            tijden = tijdObj2,
            Create_Date = date,
            datum = DateTime.Now.AddDays(3).ToString("dd-MM-yyyy"),
            prijs = 15.00
        });
        dag4.Add(new Zalen()
        {
            naam = "zaal 3",
            type = "IMAX",
            zitplekken = 200,
            tijden = tijdObj3,
            Create_Date = date,
            datum = DateTime.Now.AddDays(3).ToString("dd-MM-yyyy"),
            prijs = 17.50
        });

        List<Zalen> dag5 = new List<Zalen>();
        dag5.Add(new Zalen()
        {
            naam = "zaal 1",
            type = "2D",
            zitplekken = 100,
            tijden = tijdObj1,
            Create_Date = date,
            datum = DateTime.Now.AddDays(4).ToString("dd-MM-yyyy"),
            prijs = 12.50
        });
        dag5.Add(new Zalen()
        {
            naam = "zaal 2",
            type = "3D",
            zitplekken = 150,
            tijden = tijdObj2,
            Create_Date = date,
            datum = DateTime.Now.AddDays(4).ToString("dd-MM-yyyy"),
            prijs = 15.00
        });
        dag5.Add(new Zalen()
        {
            naam = "zaal 3",
            type = "IMAX",
            zitplekken = 200,
            tijden = tijdObj3,
            Create_Date = date,
            datum = DateTime.Now.AddDays(4).ToString("dd-MM-yyyy"),
            prijs = 17.50
        });

        List<Zalen> dag6 = new List<Zalen>();
        dag6.Add(new Zalen()
        {
            naam = "zaal 1",
            type = "2D",
            zitplekken = 100,
            tijden = tijdObj1,
            Create_Date = date,
            datum = DateTime.Now.AddDays(5).ToString("dd-MM-yyyy"),
            prijs = 12.50
        });
        dag6.Add(new Zalen()
        {
            naam = "zaal 2",
            type = "3D",
            zitplekken = 150,
            tijden = tijdObj2,
            Create_Date = date,
            datum = DateTime.Now.AddDays(5).ToString("dd-MM-yyyy"),
            prijs = 15.00
        });
        dag6.Add(new Zalen()
        {
            naam = "zaal 3",
            type = "IMAX",
            zitplekken = 200,
            tijden = tijdObj3,
            Create_Date = date,
            datum = DateTime.Now.AddDays(5).ToString("dd-MM-yyyy"),
            prijs = 17.50
        });

        List<Zalen> dag7 = new List<Zalen>();
        dag7.Add(new Zalen()
        {
            naam = "zaal 1",
            type = "2D",
            zitplekken = 100,
            tijden = tijdObj1,
            Create_Date = date,
            datum = DateTime.Now.AddDays(6).ToString("dd-MM-yyyy"),
            prijs = 12.50
        });
        dag7.Add(new Zalen()
        {
            naam = "zaal 2",
            type = "3D",
            zitplekken = 150,
            tijden = tijdObj2,
            Create_Date = date,
            datum = DateTime.Now.AddDays(6).ToString("dd-MM-yyyy"),
            prijs = 15.00
        });
        dag7.Add(new Zalen()
        {
            naam = "zaal 3",
            type = "IMAX",
            zitplekken = 200,
            tijden = tijdObj3,
            Create_Date = date,
            datum = DateTime.Now.AddDays(6).ToString("dd-MM-yyyy"),
            prijs = 17.50
        });

        List<Zalen> dag8 = new List<Zalen>();
        dag8.Add(new Zalen()
        {
            naam = "zaal 1",
            type = "2D",
            zitplekken = 100,
            tijden = tijdObj1,
            Create_Date = date,
            datum = DateTime.Now.AddDays(7).ToString("dd-MM-yyyy"),
            prijs = 12.50
        });
        dag8.Add(new Zalen()
        {
            naam = "zaal 2",
            type = "3D",
            zitplekken = 150,
            tijden = tijdObj2,
            Create_Date = date,
            datum = DateTime.Now.AddDays(7).ToString("dd-MM-yyyy"),
            prijs = 15.00
        });
        dag8.Add(new Zalen()
        {
            naam = "zaal 3",
            type = "IMAX",
            zitplekken = 200,
            tijden = tijdObj3,
            Create_Date = date,
            datum = DateTime.Now.AddDays(7).ToString("dd-MM-yyyy"),
            prijs = 17.50
        });

        List<Zalen> dag9 = new List<Zalen>();
        dag9.Add(new Zalen()
        {
            naam = "zaal 1",
            type = "2D",
            zitplekken = 100,
            tijden = tijdObj1,
            Create_Date = date,
            datum = DateTime.Now.AddDays(8).ToString("dd-MM-yyyy"),
            prijs = 12.50
        });
        dag9.Add(new Zalen()
        {
            naam = "zaal 2",
            type = "3D",
            zitplekken = 150,
            tijden = tijdObj2,
            Create_Date = date,
            datum = DateTime.Now.AddDays(8).ToString("dd-MM-yyyy"),
            prijs = 15.00
        });
        dag9.Add(new Zalen()
        {
            naam = "zaal 3",
            type = "IMAX",
            zitplekken = 200,
            tijden = tijdObj3,
            Create_Date = date,
            datum = DateTime.Now.AddDays(8).ToString("dd-MM-yyyy"),
            prijs = 17.50
        });

        List<Zalen> dag10 = new List<Zalen>();
        dag10.Add(new Zalen()
        {
            naam = "zaal 1",
            type = "2D",
            zitplekken = 100,
            tijden = tijdObj1,
            Create_Date = date,
            datum = DateTime.Now.AddDays(9).ToString("dd-MM-yyyy"),
            prijs = 12.50
        });
        dag10.Add(new Zalen()
        {
            naam = "zaal 2",
            type = "3D",
            zitplekken = 150,
            tijden = tijdObj2,
            Create_Date = date,
            datum = DateTime.Now.AddDays(9).ToString("dd-MM-yyyy"),
            prijs = 15.00
        });
        dag10.Add(new Zalen()
        {
            naam = "zaal 3",
            type = "IMAX",
            zitplekken = 200,
            tijden = tijdObj3,
            Create_Date = date,
            datum = DateTime.Now.AddDays(9).ToString("dd-MM-yyyy"),
            prijs = 17.50
        });

        List<Zalen> dag11 = new List<Zalen>();
        dag11.Add(new Zalen()
        {
            naam = "zaal 1",
            type = "2D",
            zitplekken = 100,
            tijden = tijdObj1,
            Create_Date = date,
            datum = DateTime.Now.AddDays(10).ToString("dd-MM-yyyy"),
            prijs = 12.50
        });
        dag11.Add(new Zalen()
        {
            naam = "zaal 2",
            type = "3D",
            zitplekken = 150,
            tijden = tijdObj2,
            Create_Date = date,
            datum = DateTime.Now.AddDays(10).ToString("dd-MM-yyyy"),
            prijs = 15.00
        });
        dag11.Add(new Zalen()
        {
            naam = "zaal 3",
            type = "IMAX",
            zitplekken = 200,
            tijden = tijdObj3,
            Create_Date = date,
            datum = DateTime.Now.AddDays(10).ToString("dd-MM-yyyy"),
            prijs = 17.50
        });

        List<Zalen> dag12 = new List<Zalen>();
        dag12.Add(new Zalen()
        {
            naam = "zaal 1",
            type = "2D",
            zitplekken = 100,
            tijden = tijdObj1,
            Create_Date = date,
            datum = DateTime.Now.AddDays(11).ToString("dd-MM-yyyy"),
            prijs = 12.50
        });
        dag12.Add(new Zalen()
        {
            naam = "zaal 2",
            type = "3D",
            zitplekken = 150,
            tijden = tijdObj2,
            Create_Date = date,
            datum = DateTime.Now.AddDays(11).ToString("dd-MM-yyyy"),
            prijs = 15.00
        });
        dag12.Add(new Zalen()
        {
            naam = "zaal 3",
            type = "IMAX",
            zitplekken = 200,
            tijden = tijdObj3,
            Create_Date = date,
            datum = DateTime.Now.AddDays(11).ToString("dd-MM-yyyy"),
            prijs = 17.50
        });

        List<Zalen> dag13 = new List<Zalen>();
        dag13.Add(new Zalen()
        {
            naam = "zaal 1",
            type = "2D",
            zitplekken = 100,
            tijden = tijdObj1,
            Create_Date = date,
            datum = DateTime.Now.AddDays(12).ToString("dd-MM-yyyy"),
            prijs = 12.50
        });
        dag13.Add(new Zalen()
        {
            naam = "zaal 2",
            type = "3D",
            zitplekken = 150,
            tijden = tijdObj2,
            Create_Date = date,
            datum = DateTime.Now.AddDays(12).ToString("dd-MM-yyyy"),
            prijs = 15.00
        });
        dag13.Add(new Zalen()
        {
            naam = "zaal 3",
            type = "IMAX",
            zitplekken = 200,
            tijden = tijdObj3,
            Create_Date = date,
            datum = DateTime.Now.AddDays(12).ToString("dd-MM-yyyy"),
            prijs = 17.50
        });

        List<Zalen> dag14 = new List<Zalen>();
        dag14.Add(new Zalen()
        {
            naam = "zaal 1",
            type = "2D",
            zitplekken = 100,
            tijden = tijdObj1,
            Create_Date = date,
            datum = DateTime.Now.AddDays(13).ToString("dd-MM-yyyy"),
            prijs = 12.50
        });
        dag14.Add(new Zalen()
        {
            naam = "zaal 2",
            type = "3D",
            zitplekken = 150,
            tijden = tijdObj2,
            Create_Date = date,
            datum = DateTime.Now.AddDays(13).ToString("dd-MM-yyyy"),
            prijs = 15.00
        });
        dag14.Add(new Zalen()
        {
            naam = "zaal 3",
            type = "IMAX",
            zitplekken = 200,
            tijden = tijdObj3,
            Create_Date = date,
            datum = DateTime.Now.AddDays(13).ToString("dd-MM-yyyy"),
            prijs = 17.50
        });

        List<List<Zalen>> dagen = new List<List<Zalen>>();
        
        dagen.Add(dag1);
        dagen.Add(dag2);
        dagen.Add(dag3);
        dagen.Add(dag4);
        dagen.Add(dag5);
        dagen.Add(dag6);
        dagen.Add(dag7);
        dagen.Add(dag8);
        dagen.Add(dag9);
        dagen.Add(dag10);
        dagen.Add(dag11);
        dagen.Add(dag12);
        dagen.Add(dag13);
        dagen.Add(dag14);



        lijst.Add(new Cinema_adress()
        {
            id = id,
            name = bioscoop,
            address = straat + " " + straatNr,
            street = straat,
            streetNr = straatNr,
            zipcode = postcode,
            city = stad,
            telNr = "+31 " + nummer,
            dagen = dagen
        });

        //verdander de lijst naar een json type
        string cinemaLijst = JsonConvert.SerializeObject(lijst, Formatting.Indented);
        //verander de hele file met de nieuwe json informatie
        File.WriteAllText(url, cinemaLijst);
        Console.WriteLine("Bioscoop gegevens succesvol toegevoegd!");
        Thread.Sleep(3000);
        Console.Clear();

        
    }

    
    public void editLocation(List<Cinema_adress> lijst, string url)
    {
        Console.Clear();
        Console.WriteLine("*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*");
        Console.WriteLine("|          Locatie gegevens aanpassen         |");
        Console.WriteLine("*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*" + "\n");

        //maak een lijst van alle id's met een for loop
        int[] bioscoopNrArray = new int[lijst.Count()];
        for (int i = 0; i < lijst.Count(); i++)
        {
            bioscoopNrArray[i] = lijst[i].id;
        }
        
        //de huidige nummer is NinValue zodat dit geen goede id is
        int bioscoopNr = int.MinValue;
        string placeHolder;

        //als de bioscoopNr niet in de lijst zit dan vraag je gewoon telkens opnieuw totdat de persoon een geldig ID geeft
        while (!bioscoopNrArray.Contains(bioscoopNr))
        {
            
            Console.WriteLine("--------------------------------------------------------------------------------------------");
            Console.WriteLine("| Voer de ID nummer van de bioscoop locatie die u wilt aanpassen: (typ '*' om te annuleren)|");
            Console.WriteLine("--------------------------------------------------------------------------------------------");
            placeHolder = Console.ReadLine();

            if (int.TryParse(placeHolder, out bioscoopNr))
            {
                if (!bioscoopNrArray.Contains(bioscoopNr))
                {
                    Console.WriteLine("Er is geen locatie die overeenkomt met deze ID!\n");
                    bioscoopNr = int.MinValue;
                    Thread.Sleep(3000);
                }
                else
                {
                    foreach(dynamic item in lijst)
                    {
                        if (item.id == bioscoopNr)
                        {
                            Console.WriteLine("-----------------------------------------------");
                            Console.WriteLine("|              Geselecteerde locatie          |");
                            Console.WriteLine("-----------------------------------------------\n");
                            Console.WriteLine("-----------------------------------------------\n");
                            Console.WriteLine("        ID: " + item.id);
                            Console.WriteLine("        Naam Locatie: " + item.name);
                            Console.WriteLine("        Adres: " + item.address);
                            Console.WriteLine("        Postcode: " + item.zipcode);
                            Console.WriteLine("        Stad: " + item.city);
                            Console.WriteLine("        TelNr: " + item.telNr);
                            Console.WriteLine("-----------------------------------------------\n");
                            break;
                        }
                    }
                    
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
                    bioscoopNr = int.MinValue;
                    Console.WriteLine("Voer een geldige ID in a.u.b!");
                    Thread.Sleep(1000);
                }
            }
        }

        // alle informatie voor het veranderen van bioscoop informatie gebeurd hier
        Console.WriteLine("------------------------------------------------------------------------------------------------------------");
        Console.WriteLine("|  Voer de aangepaste naam van de bioscoop locatie in: (typ '/' om over te slaan of '*' om te annuleren)   |");
        Console.WriteLine("------------------------------------------------------------------------------------------------------------");
        string filmName = Console.ReadLine();
        while (string.IsNullOrEmpty(filmName) || filmName.Trim().Length == 0)
        {
            Console.WriteLine("Vul in een geldige naam a.u.b!");
            Thread.Sleep(3000);
            filmName = Console.ReadLine();
        }

        if (filmName.Trim() == "*")
        {
            Console.WriteLine("Bewerking is geannuleerd!");
            Thread.Sleep(3000);
            Console.Clear();
            return;
        }
        else if (filmName.Trim() == "/")
        {
            Console.WriteLine("Veld wordt overgeslagen!");
            Thread.Sleep(3000);
            for (int i = 0; i < lijst.Count(); i++)
            {
                if (lijst[i].id == bioscoopNr)
                {
                    filmName = lijst[i].name;
                }
            }
        }

        
        Console.WriteLine("------------------------------------------------------------------------------------------------------------------");
        Console.WriteLine("|  Voer de aangepaste straatnaam van de bioscoop locatie in: (typ '/' om over te slaan of '*' om te annuleren)   |");
        Console.WriteLine("------------------------------------------------------------------------------------------------------------------");

        string streetName = "";
        bool check = true;
        while (check)
        {
            streetName = Console.ReadLine();
            if (int.TryParse(streetName, out _))
            {
                Console.WriteLine("Ongeldig invoer!\nDe straatnaam mag geen nummer zijn!");
            }
            else
            {
                if (streetName.Trim().ToCharArray().Any(char.IsDigit))
                {
                    Console.WriteLine("Een straatnaam mag geen nummer bevatten!");
                    Thread.Sleep(3000);
                }
                else if(streetName.Trim() == "*")
                {
                    Console.WriteLine("Bewerking is geannuleerd!");
                    Thread.Sleep(3000);
                    Console.Clear();
                    return;
                }
                else if(streetName.Trim() == "/")
                {
                    Console.WriteLine("Veld wordt overgeslagen!");
                    Thread.Sleep(3000);
                    for (int i = 0; i < lijst.Count(); i++)
                    {
                        if (lijst[i].id == bioscoopNr)
                        {
                            streetName = lijst[i].street;
                        }
                    }
                    check = false;
                }
                else if (streetName.Trim().Length == 0)
                {
                    Console.WriteLine("Ongeldig invoer!\nVoer een geldige straatnaam in a.u.b!");
                    Thread.Sleep(3000);
                }
                else if (hasSpecialChar(streetName.Trim()))
                {
                    Console.WriteLine("Een straatnaam mag geen speciale karakter bevatten!");
                    Thread.Sleep(3000);
                }
                else
                {
                    check = false;
                }
            }
        }

        Console.WriteLine("------------------------------------------------------------------------------------------------------------------");
        Console.WriteLine("|  Voer de aangepaste huisnummer van de bioscoop locatie in: (typ '/' om over te slaan of '*' om te annuleren)   |");
        Console.WriteLine("------------------------------------------------------------------------------------------------------------------");
        string streetNr = "";
        check = true;
        int straatNrPlaceholder = 0;
        while (check)
        {
            streetNr = Console.ReadLine();
            if (int.TryParse(streetNr, out straatNrPlaceholder))
            {
                if (straatNrPlaceholder <= 0 || straatNrPlaceholder > 999)
                {
                    Console.WriteLine("Voer een geldige huisnummer in a.u.b!\n");
                    Thread.Sleep(3000);
                }
                else
                    check = false;
            }
            else
            {
                if (streetNr.Trim() == "*")
                {
                    Console.WriteLine("Bewerking is geannuleerd!");
                    Thread.Sleep(3000);
                    Console.Clear();
                    return;
                }
                else if(streetNr.Trim() == "/")
                {
                    Console.WriteLine("Veld wordt overgeslagen!");
                    Thread.Sleep(3000);
                    for (int i = 0; i < lijst.Count(); i++)
                    {
                        if (lijst[i].id == bioscoopNr)
                        {
                            streetNr = lijst[i].streetNr;
                        }
                    }
                    check = false;
                }
                else
                {
                    Console.WriteLine("Voer een geldige huisnummer in a.u.b!\n");
                    Thread.Sleep(3000);
                }
            }
        }

        Console.WriteLine("----------------------------------------------------------------------------------------------------------------");
        Console.WriteLine("|  Voer de aangepaste postcode van de bioscoop locatie in: (typ '/' om over te slaan of '*' om te annuleren)   |");
        Console.WriteLine("----------------------------------------------------------------------------------------------------------------");
        string postcode = "";
        check = true;
        string postcodeNr;
        string postcodeString;
        while (check)
        {
            postcode = Console.ReadLine();
            if (postcode.Trim().Length == 6)
            {
                postcodeNr = postcode.Substring(0, 4);
                postcodeString = postcode.Substring(postcode.Length - 2);
                int poscodePlaceholder = 0;

                if (int.TryParse(postcodeNr, out poscodePlaceholder))
                {
                    if (poscodePlaceholder < 1000 || poscodePlaceholder > 9999)
                    {
                        Console.WriteLine("Voer een geldige postcode in a.u.b!\n");
                        Thread.Sleep(3000);
                    }
                    else if (int.TryParse(postcodeString, out _) || postcodeString.Trim().Length == 0 || hasSpecialChar(postcodeString.Trim()))
                    {
                        Console.WriteLine("Voer een geldige postcode in a.u.b!\n");
                        Thread.Sleep(3000);
                    }
                    else
                        check = false;
                    
                }
            }
            else
            {
                if (postcode.Trim() == "*")
                {
                    Console.WriteLine("Bewerking is geannuleerd!");
                    Thread.Sleep(3000);
                    Console.Clear();
                    return;
                }
                else if(postcode.Trim() == "/")
                {
                    Console.WriteLine("Veld wordt overgeslagen!");
                    Thread.Sleep(3000);
                    for (int i = 0; i < lijst.Count(); i++)
                    {
                        if (lijst[i].id == bioscoopNr)
                        {
                            postcode = lijst[i].zipcode;
                        }
                    }
                    check = false;
                }
                else
                {
                    Console.WriteLine("Voer een geldige postcode in a.u.b!\n");
                    Thread.Sleep(3000);
                }
            }
        }

        Console.WriteLine("-----------------------------------------------------------------------------------------------------------------");
        Console.WriteLine("|  Voer de aangepaste stad naam van de bioscoop locatie in: (typ '/' om over te slaan of '*' om te annuleren)   |");
        Console.WriteLine("-----------------------------------------------------------------------------------------------------------------");
        string stad = "";
        check = true;
        while (check)
        {

            stad = Console.ReadLine();
            if (int.TryParse(stad, out _))
            {
                Console.WriteLine("Een stad mag geen nummer zijn!");
            }
            else
            {
                if (stad.Trim() == "*")
                {
                    Console.WriteLine("Bewerking is geannuleerd!");
                    Thread.Sleep(3000);
                    Console.Clear();
                    return;
                }
                else if (stad.Trim() == "/")
                {
                    Console.WriteLine("Veld wordt overgeslagen!");
                    Thread.Sleep(3000);
                    for (int i = 0; i < lijst.Count(); i++)
                    {
                        if (lijst[i].id == bioscoopNr)
                        {
                            stad = lijst[i].city;
                        }
                    }
                    check = false;
                }
                else if (stad.Trim().Length == 0)
                {
                    Console.WriteLine("Ongeldig invoer!\n");
                    Thread.Sleep(3000);
                }
                else if (stad.Trim().ToCharArray().Any(char.IsDigit))
                {
                    Console.WriteLine("Een stad naam mag geen nummer bevatten!");
                    Thread.Sleep(3000);
                }
                else if (hasSpecialChar(stad.Trim()))
                {
                    Console.WriteLine("Een stad naam mag geen speciale karakter bevatten!");
                    Thread.Sleep(3000);
                }
                else
                {
                    check = false;
                }
            }
        }


        Console.WriteLine("----------------------------------------------------------------------------------------------------------------------");
        Console.WriteLine("|  Voer de aangepaste telefoonnummer van de bioscoop locatie in: (typ '/' om over te slaan of '*' om te annuleren)   |");
        Console.WriteLine("----------------------------------------------------------------------------------------------------------------------");
        string nummer = "";
        check = true;
        while (check)
        {
            nummer = Console.ReadLine();
            if (int.TryParse(nummer, out _))
            {
                if (nummer.Length == 9)
                {
                    check = false;
                    nummer = "+31 " + nummer;
                }
                else
                {
                    Console.WriteLine("Het telefoonnummer moet '9' nummers bevatten!\n");
                    Thread.Sleep(3000);
                }
            }
            else
            {
                if (nummer.Trim() == "*")
                {
                    Console.WriteLine("Bewerking is geannuleerd!");
                    Thread.Sleep(3000);
                    Console.Clear();
                    return;
                }
                else if(nummer.Trim() == "/")
                {
                    Console.WriteLine("Veld wordt overgeslagen!");
                    Thread.Sleep(3000);
                    for (int i = 0; i < lijst.Count(); i++)
                    {
                        if (lijst[i].id == bioscoopNr)
                        {
                            nummer = lijst[i].telNr;
                        }
                    }
                    check = false;
                }
                else
                {
                    Console.WriteLine("Ongeldig invoer!\n");
                    Thread.Sleep(3000);
                }
            }
        }


        foreach(dynamic item in lijst)
        {
            if (item.id == bioscoopNr)
            {
                item.name = filmName.Trim();
                item.address = streetName.Trim() + " " + streetNr.Trim();
                item.streetNr = streetNr.Trim();
                item.street = streetName.Trim();
                item.zipcode = postcode.Trim();
                item.city = stad.Trim();
                item.telNr = nummer.Trim();
            }
        }
        

        //verdander de lijst naar een json type
        string cinemaLijst = JsonConvert.SerializeObject(lijst, Formatting.Indented);
        //verander de hele file met de nieuwe json informatie
        File.WriteAllText(url, cinemaLijst);
        Console.WriteLine("Bioscoop gegevens succesvol aangepast!");
        Thread.Sleep(3000);
        Console.Clear();
    }

    public void removeLocation(List<Cinema_adress> lijst, string url)
    {
        int[] bioscoopNrArray = new int[lijst.Count()];
        for (int i = 0; i < lijst.Count(); i++)
        {
            bioscoopNrArray[i] = lijst[i].id;
        }

        //de huidige nummer is minvalue zodat dit geen goede id is
        int bioscoopNr = int.MinValue;

        //als de bioscoopNr niet in de lijst zit dan vraag je gewoon telkens opnieuw totdat de persoon een geldig ID geeft
        while (!bioscoopNrArray.Contains(bioscoopNr))
        {
            Console.WriteLine("Voer de ID in van de bioscoop die u wilt verwijderen: (typ '*' om te annuleren)");
            string placeHolder = Console.ReadLine();
            if (int.TryParse(placeHolder, out bioscoopNr))
            {
                //Console.WriteLine();
                //bioscoopNr--;
                if (!bioscoopNrArray.Contains(bioscoopNr))
                {
                    Console.WriteLine("Er is geen film voor de ingevoerde ID!\n");
                    bioscoopNr = int.MinValue;
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
                bioscoopNr = int.MinValue;
                Console.WriteLine("Voer een geldige ID in a.u.b!");
                Thread.Sleep(1000);
            }
        }
        for (int i= 0; i<lijst.Count; i++)
        {
            if (lijst[i].id == bioscoopNr)
            {
                lijst.Remove(lijst[i]);
                break;
            }
        }


        //verdander de lijst naar een json type
        string cinemaLijst = JsonConvert.SerializeObject(lijst, Formatting.Indented);
        //verander de hele file met de nieuwe json informatie
        File.WriteAllText(url, cinemaLijst);
        Console.WriteLine("Locatie succesvol verwijderd!");
        Thread.Sleep(3000);
        Console.Clear();
    }

    private static bool hasSpecialChar(string input)
    {
        string specialChar = @"\|!#$%&/()=?»«@£§€{}.-;'<>_,*";
        foreach (var item in specialChar)
        {
            if (input.Contains(item)) return true;
        }

        return false;
    }

    public static void updateSchedule()
    {
        //Directory van Json bestand met locatie gegevens
        string url = "..\\..\\..\\locatie.json";
        //Omzet van gegevens in Json bestand over naar string en daarna in een lijst zetten.
        string locatieJson = File.ReadAllText(url);
        List<Cinema_adress> locatieList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Cinema_adress>>(locatieJson);

        string date = DateTime.UtcNow.ToString("dd-MM-yyyy");
        foreach (var locatie in locatieList)
        {
            if (locatie.id == 5)
            {
                foreach(var item in locatie.dagen)
                {
                    for(int i=0; i<item.Count;i++)
                    {
                        if(item[0].datum == date)
                        {
                            item.RemoveAt(0);
                        }
                     
                    }
                }
            }
        }


        string zitplekken1 = "";
        for (int i = 0; i < 100; i++)
        {
            zitplekken1 += "T";
        }
        var tijdObj1 = new List<Tijden>();
        tijdObj1.Add(new Tijden()
        {
            tijd = "9-12",
            beschikbaar = zitplekken1
        });
        tijdObj1.Add(new Tijden()
        {
            tijd = "12-15",
            beschikbaar = zitplekken1
        });
        tijdObj1.Add(new Tijden()
        {
            tijd = "15-18",
            beschikbaar = zitplekken1
        });
        tijdObj1.Add(new Tijden()
        {
            tijd = "18-21",
            beschikbaar = zitplekken1
        });
        tijdObj1.Add(new Tijden()
        {
            tijd = "21-24",
            beschikbaar = zitplekken1
        });

        string zitplekken2 = "";
        for (int i = 0; i < 150; i++)
        {
            zitplekken2 += "T";
        }
        var tijdObj2 = new List<Tijden>();
        tijdObj2.Add(new Tijden()
        {
            tijd = "9-12",
            beschikbaar = zitplekken2
        });
        tijdObj2.Add(new Tijden()
        {
            tijd = "12-15",
            beschikbaar = zitplekken2
        });
        tijdObj2.Add(new Tijden()
        {
            tijd = "15-18",
            beschikbaar = zitplekken2
        });
        tijdObj2.Add(new Tijden()
        {
            tijd = "18-21",
            beschikbaar = zitplekken2
        });
        tijdObj2.Add(new Tijden()
        {
            tijd = "21-24",
            beschikbaar = zitplekken2
        });

        string zitplekken3 = "";
        for (int i = 0; i < 200; i++)
        {
            zitplekken3 += "T";
        }
        var tijdObj3 = new List<Tijden>();
        tijdObj3.Add(new Tijden()
        {
            tijd = "9-12",
            beschikbaar = zitplekken3
        });
        tijdObj3.Add(new Tijden()
        {
            tijd = "12-15",
            beschikbaar = zitplekken3
        });
        tijdObj3.Add(new Tijden()
        {
            tijd = "15-18",
            beschikbaar = zitplekken3
        });
        tijdObj3.Add(new Tijden()
        {
            tijd = "18-21",
            beschikbaar = zitplekken3
        });
        tijdObj3.Add(new Tijden()
        {
            tijd = "21-24",
            beschikbaar = zitplekken3
        });


        foreach (var locatie in locatieList)
        {
            if (locatie.id == 5)
            {
                foreach (var item in locatie.dagen)
                {
                    item.Add(new Zalen()
                    {
                        naam = "zaal 1",
                        type = "2D",
                        zitplekken = 100,
                        tijden = tijdObj1,
                        Create_Date = date,
                        datum = DateTime.Today.AddDays(14).ToString("dd-MM-yyyy"),
                        prijs = 12.50
                    });

                    item.Add(new Zalen()
                    {
                        naam = "zaal 2",
                        type = "3D",
                        zitplekken = 150,
                        tijden = tijdObj2,
                        Create_Date = date,
                        datum = DateTime.Today.AddDays(14).ToString("dd-MM-yyyy"),
                        prijs = 15.00
                    });

                    item.Add(new Zalen()
                    {
                        naam = "zaal 3",
                        type = "IMAX",
                        zitplekken = 200,
                        tijden = tijdObj3,
                        Create_Date = date,
                        datum = DateTime.Today.AddDays(14).ToString("dd-MM-yyyy"),
                        prijs = 17.50
                    });
                }
            }
        }
        
        
        File.WriteAllText(url, JsonConvert.SerializeObject(locatieList, Formatting.Indented));
    }
}
