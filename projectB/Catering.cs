using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

class Catering
{
    public void etenMenu(string rol, int accountID)
    {

        //Omzet van gegevens in Json bestand over naar string en daarna in een lijst zetten.
        string cateringUrl = "..\\..\\..\\Catering.json";
        List<Eten> etenMenu = JsonConvert.DeserializeObject<List<Eten>>(File.ReadAllText(cateringUrl));

        Console.Clear();
        string Keuze = "";

        while (Keuze != "1")
        {
            int snacksCount = 0;
            int drinksCount = 0;
            Console.WriteLine("*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*");
            Console.WriteLine("|                                               Snacks                                              |");
            Console.WriteLine("*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*" + " \n");
            foreach(dynamic item in etenMenu)
            {
                if (item.productType == "Eten")
                {
                    Console.WriteLine("-----------------------------------------------------------------------------------------------------");
                    Console.WriteLine("                                 Product ID: " + item.productID);
                    Console.WriteLine("                                 Product Naam: " + item.productName);
                    Console.WriteLine("                                 Aantal: " + item.amount + "X");
                    Console.WriteLine("                                 Prijs: $" + item.price);
                    Console.WriteLine("-----------------------------------------------------------------------------------------------------");
                    snacksCount++;
                }
            }

            if(snacksCount == 0)
            {
                Console.WriteLine("Er zijn geen snacks beschikbaar!\n");
            }

            Console.WriteLine("\n*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*");
            Console.WriteLine("|                                              Dranken                                              |");
            Console.WriteLine("*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*" + " \n");
            foreach (dynamic item in etenMenu)
            {
                if (item.productType == "Drank")
                {
                    Console.WriteLine("-----------------------------------------------------------------------------------------------------");
                    Console.WriteLine("                                 Product ID: " + item.productID);
                    Console.WriteLine("                                 Product Naam: " + item.productName);
                    Console.WriteLine("                                 Aantal: " + item.amount + "X");
                    Console.WriteLine("                                 Prijs: $" + item.price);
                    Console.WriteLine("-----------------------------------------------------------------------------------------------------");
                    drinksCount++;
                }
            }

            if (drinksCount == 0)
            {
                Console.WriteLine("Er zijn geen dranken beschikbaar!\n");
            }

            if (rol == "gebruiker")
            {
                Console.WriteLine("\n" + "*-*-*-*-*-*-*-*-*-*-*-*-*-*-*" + "\n" +
                                        "| [1] Terug | [2] Bestellen |" + "\n" +
                                        "-----------------------------");
            }
            else if(rol == "admin")
            {
                Console.WriteLine("\n" + "*-*-*-*-*-*-*-*-*-*-*-*-*-*-*" + "\n" +
                                        "| [1] Terug | [2] Bestellen |" + "\n" +
                                        "-----------------------------");
            }
            else
            {
                Console.WriteLine("\n" + "*-*-*-*-*-*-*" + "\n" + "| [1] Terug |" + "\n" + "-------------");
            }

            
            Keuze = Console.ReadLine();

            if (rol == "gebruiker")
            {
                while (Keuze.Trim() != "1" && Keuze.Trim() != "2")
                {
                    Console.WriteLine("Kies a.u.b. een van de bovenstaande opties.\n");
                    Keuze = Console.ReadLine();
                }
                switch (Keuze.ToLower().Trim())
                {

                    case "2":
                        orderFood(rol, accountID, etenMenu);
                        break;
                }
            }
            else if (rol == "admin")
            {
                while (Keuze.Trim() != "1" && Keuze.Trim() != "2")
                {
                    Console.WriteLine("Kies a.u.b. een van de bovenstaande opties.\n");
                    Keuze = Console.ReadLine();
                }
                switch (Keuze.ToLower().Trim())
                {

                    case "2":
                        orderFood(rol, accountID, etenMenu);
                        break;

                }
            }
            else
            {
                while (Keuze.Trim() != "1")
                {
                    Console.WriteLine("Kies a.u.b. een van de bovenstaande opties.\n");
                    Keuze = Console.ReadLine();
                }
            }
        }
    }


    public void orderFood(string rol, int accountID, List<Eten> etenMenu)
    {
        //Omzet van gegevens in Json bestand over naar string en daarna in een lijst zetten.
        List<Account> accounts = JsonConvert.DeserializeObject<List<Account>>(File.ReadAllText("..\\..\\..\\account.json"));

        Dictionary<int, int> orderList = new Dictionary<int, int>();
        string Keuze = "1";
        double total = 0.0;
        while (Keuze == "1" || Keuze == "3")
        {
            Console.Clear();
            int snacksCount = 0;
            int drinksCount = 0;
            Console.WriteLine("*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*");
            Console.WriteLine("|                                               Snacks                                              |");
            Console.WriteLine("*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*" + " \n");
            foreach (dynamic item in etenMenu)
            {
                if (item.productType == "Eten")
                {
                    Console.WriteLine("-----------------------------------------------------------------------------------------------------");
                    Console.WriteLine("                                 Product ID: " + item.productID);
                    Console.WriteLine("                                 Product Naam: " + item.productName);
                    Console.WriteLine("                                 Aantal: " + item.amount + "X");
                    Console.WriteLine("                                 Prijs: $" + item.price);
                    Console.WriteLine("-----------------------------------------------------------------------------------------------------");
                    snacksCount++;
                }
            }

            if (snacksCount == 0)
            {
                Console.WriteLine("Er zijn geen snacks beschikbaar!\n");
            }

            Console.WriteLine("\n*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*");
            Console.WriteLine("|                                              Dranken                                              |");
            Console.WriteLine("*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*" + " \n");
            foreach (dynamic item in etenMenu)
            {
                if (item.productType == "Drank")
                {
                    Console.WriteLine("-----------------------------------------------------------------------------------------------------");
                    Console.WriteLine("                                 Product ID: " + item.productID);
                    Console.WriteLine("                                 Product Naam: " + item.productName);
                    Console.WriteLine("                                 Aantal: " + item.amount + "X");
                    Console.WriteLine("                                 Prijs: $" + item.price);
                    Console.WriteLine("-----------------------------------------------------------------------------------------------------");
                    drinksCount++;
                }
            }

            if (drinksCount == 0)
            {
                Console.WriteLine("Er zijn geen dranken beschikbaar!\n");
            }

            if (orderList.Count > 0)
            {
                Console.WriteLine("Uw Bestelling:\n");
                foreach (dynamic item in orderList)
                {

                    foreach (dynamic item2 in etenMenu)
                    {
                        if (item2.productID == item.Key)
                        {
                            Console.WriteLine("         Product ID: " + item.Key);
                            Console.WriteLine("         Product Naam: " + item2.productName);
                            Console.WriteLine("         Aantal: " + item.Value + "X");
                            Console.WriteLine("         Prijs: $" + item2.price + "\n");
                            total = total + (item.Value * item2.price);
                        }
                    }

                }
            }

            int[] productIDarr = new int[etenMenu.Count()];
            for (int i = 0; i < etenMenu.Count(); i++)
            {
                productIDarr[i] = etenMenu[i].productID;
            }

            int productID = int.MinValue;
            if (Keuze == "1")
            {
                while (!productIDarr.Contains(productID))
                {
                    Console.WriteLine("-------------------------------------------------------------------------------");
                    Console.WriteLine("| Voer de ID in van de item die u wilt bestellen: (typ '*' om te annuleren)   |");
                    Console.WriteLine("-------------------------------------------------------------------------------");

                    string placeHolder = Console.ReadLine();
                    if (int.TryParse(placeHolder, out productID))
                    {
                        if (!productIDarr.Contains(productID))
                        {
                            Console.WriteLine("Geen resultaat gevonden!\n");
                            productID = int.MinValue;
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
                            productID = int.MinValue;
                        }

                    }
                }

                bool checkInventory = true;
                int productAmount = int.MinValue;
                while (checkInventory)
                {
                    Console.WriteLine("--------------------------------------------------------------------------------");
                    Console.WriteLine("| Voer de aantal van de item die u wilt bestellen: (typ '*' om te annuleren)   |");
                    Console.WriteLine("--------------------------------------------------------------------------------");
                    string placeHolder = Console.ReadLine();

                    if (int.TryParse(placeHolder, out productAmount))
                    {
                        foreach (dynamic item in etenMenu)
                        {
                            if (item.amount < productAmount)
                            {
                                Console.WriteLine("Excuses, niet genoeg van deze product in voorraad!");
                                Console.WriteLine("Probeer nogmaals a.u.b!");
                                break;
                            }
                            else
                            {
                                checkInventory = false;
                                break;
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
                            Console.WriteLine("Voer een geldige bedrag in!");
                            Thread.Sleep(3000);
                            productAmount = int.MinValue;
                        }

                    }
                }

                if (!orderList.ContainsKey(productID))
                    orderList.Add(productID, productAmount);
                else
                    orderList[productID] = productAmount;
            }
            else
            {
                while (!productIDarr.Contains(productID))
                {
                    Console.WriteLine("-------------------------------------------------------------------------------");
                    Console.WriteLine("| Voer de ID in van de item die u wilt verwijderen: (typ '*' om te annuleren) |");
                    Console.WriteLine("-------------------------------------------------------------------------------");

                    string placeHolder = Console.ReadLine();
                    if (int.TryParse(placeHolder, out productID))
                    {
                        if (!productIDarr.Contains(productID))
                        {
                            Console.WriteLine("Geen resultaat gevonden!\n");
                            productID = int.MinValue;
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
                            productID = int.MinValue;
                        }
                    }
                }
                orderList.Remove(productID);
                Console.WriteLine("Item verwijderd!\n");
                Thread.Sleep(1000);
            }

            if(orderList.Count > 0)
            {
                Console.WriteLine("     [1] Voeg meer items toe\n");
                Console.WriteLine("     [2] Bevestigen\n");
                Console.WriteLine("     [3] Item verwijderen\n");
                Console.WriteLine("     [*] Annuleren\n");



                Keuze = Console.ReadLine();

                while (Keuze.Trim() != "1" && Keuze.Trim() != "2" && Keuze.Trim() != "3" && Keuze != "*")
                {
                    Console.WriteLine("Kies a.u.b. een van de bovenstaande opties.\n");
                    Keuze = Console.ReadLine();
                }
                if (Keuze == "2")
                {
                    confirmOrder(accountID, etenMenu, orderList);
                }
                else if (Keuze.Trim() == "*")
                {
                    Console.WriteLine("Bewerking is geannuleerd!");
                    Thread.Sleep(3000);
                    Console.Clear();
                    return;
                }
            }
            else
            {
                Console.WriteLine("     [1] Voeg items toe\n");
                Console.WriteLine("     [*] Annuleren\n");

                while (Keuze.Trim() != "1" && Keuze != "*")
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
            }

        }
        
    }

    public void confirmOrder(int accountID, List<Eten> etenMenu, Dictionary<int, int> orderList)
    {
        string url = "..\\..\\..\\account.json";
        List<Account> accounts = JsonConvert.DeserializeObject<List<Account>>(File.ReadAllText("..\\..\\..\\account.json"));
        List<EtenBestelling> order =  new List<EtenBestelling>();
        Console.Clear();
        Console.WriteLine("*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*");
        Console.WriteLine("|                                             Bevestigen                                            |");
        Console.WriteLine("*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*" + " \n");
        

        double total = 0.0;
        Console.WriteLine("Uw Bestelling:\n");
        foreach (dynamic item in orderList)
        {
            
            foreach(dynamic item2 in etenMenu)
            {
                if (item2.productID == item.Key)
                {
                    Console.WriteLine("         Product ID: " + item.Key);
                    Console.WriteLine("         Product Naam: " + item2.productName);
                    Console.WriteLine("         Aantal: " + item.Value + "X");
                    Console.WriteLine("         Prijs: $" + item2.price + "\n");
                    total = total + (item.Value * item2.price);
                }
            }
            
        }
        Console.WriteLine("         Totaal: $" + Math.Round(total, 2));

        Console.WriteLine("\n" + "*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*" + "\n" +
                                 "| [1] Betalen | [2] Annuleren |" + "\n" +
                                 "-------------------------------");
        string Keuze = Console.ReadLine();

        while (Keuze.Trim() != "1" && Keuze.Trim() != "2")
        {
            Console.WriteLine("Kies a.u.b. een van de bovenstaande opties.\n");
            Keuze = Console.ReadLine();
        }

        if (Keuze.Trim() == "1")
        {
            Console.Clear();
            Console.WriteLine("*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*");
            Console.WriteLine("|                                             Betaal opties                                         |");
            Console.WriteLine("*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*" + " \n");
            Console.WriteLine("         [1] iDEAL\n");
            Console.WriteLine("         [2] PayPal\n");
            Console.WriteLine("         [3] CreditCard\n");
            Console.WriteLine("         [*] Annuleren\n");

            Keuze = Console.ReadLine();

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


            Dictionary<string, int> orderConfirmation = new Dictionary<string, int>();
            foreach(dynamic item in etenMenu)
            {
                if (orderList.ContainsKey(item.productID))
                {
                    orderConfirmation.Add(item.productName, orderList[item.productID]);
                }
            }

            
            foreach(dynamic account in accounts)
            {
                if (account.id == accountID)
                {
                    if (account.etenBestelling == null)
                    {
                        account.etenBestelling = new EtenBestelling[]
                        {
                            new EtenBestelling()
                            {
                                orderID = 1,
                                orderList = orderConfirmation,
                                total = Math.Round(total, 2)
                            }
                        };
                    }
                    else
                    {
                        var etenBestelling = new EtenBestelling[account.etenBestelling.Length + 1];
                        int bestellingCounter = 0;
                        foreach (var item2 in account.etenBestelling)
                        {
                            etenBestelling[bestellingCounter] = item2;
                            bestellingCounter++;
                        }
                        etenBestelling[etenBestelling.Length - 1] = new EtenBestelling()
                        {
                            orderID = etenBestelling.Length + 1,
                            orderList = orderConfirmation,
                            total = Math.Round(total, 2)
                        };

                        account.etenBestelling = etenBestelling;
                    }
                }
                
            }

            string bestelling = JsonConvert.SerializeObject(accounts, Formatting.Indented);
            //verander de hele file met de nieuwe json informatie
            File.WriteAllText(url, bestelling);

            foreach(dynamic item in orderList)
            {
                for (int i = 0; i < etenMenu.Count(); i++)
                {
                    if (etenMenu[i].productID == item.Key)
                    {
                        etenMenu[i].amount = etenMenu[i].amount - item.Value;
                    }
                }
            }
            
            File.WriteAllText("..\\..\\..\\Catering.json", JsonConvert.SerializeObject(etenMenu, Formatting.Indented));

            Console.WriteLine("Bedankt voor uw bestelling!");
            Console.WriteLine("Fijne dag verder!");
            Thread.Sleep(3000);
            Console.Clear();
        }
        else
        {
            Console.WriteLine("Bewerking is geannuleerd!");
            Thread.Sleep(3000);
            Console.Clear();
            return;
        }
    }
}

