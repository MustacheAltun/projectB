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

        /*
         * Gebruiker ziet alle beschikbare eten en drinken items die te koop zijn.
         * Gast mag niet bestellen.
         * Als gebruiker kiest om selectie te maken wordt hij dan doorgestuurd naar orderFood functie.
         */

        Console.Clear();
        string Keuze = "";

        while (Keuze != "1")
        {
            menuOverview(etenMenu);

            if (rol == "gebruiker")
            {
                Console.WriteLine("\n" + "*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*" + "\n" +
                                        "| [1] Terug | [2] Selectie maken |" + "\n" +
                                        "----------------------------------");
            }
            else if (rol == "admin")
            {
                Console.WriteLine("\n" + "*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-" + "\n" +
                                        "| [1] Terug | [2] Selectie maken | [3] Item toevoegen | [4] Item wijzigen | [5] Item verwijderen | [6] Voorraad bijwerken |" + "\n" +
                                        "---------------------------------------------------------------------------------------------------------------------------");
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
                while (Keuze.Trim() != "1" && Keuze.Trim() != "2" && Keuze.Trim() != "3" && Keuze.Trim() != "4" && Keuze.Trim() != "5" && Keuze.Trim() != "6")
                {
                    Console.WriteLine("Kies a.u.b. een van de bovenstaande opties.\n");
                    Keuze = Console.ReadLine();
                }
                switch (Keuze.ToLower().Trim())
                {

                    case "2":
                        orderFood(rol, accountID, etenMenu);
                        break;
                    case "3":
                        addMenuItem(etenMenu);
                        break;
                    case "4":
                        editItem(etenMenu);
                        break;
                    case "5":
                        removeItem(etenMenu);
                        break;
                    case "6":
                        updateInventory(etenMenu);
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
    private static void selectedOverview(int itemID, List<Eten> etenMenu)
    {
        foreach (dynamic item in etenMenu)
        {
            if (item.productID == itemID)
            {
                Console.WriteLine("*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*");
                Console.WriteLine("|                                       Geslecteerde Item                                           |");
                Console.WriteLine("*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*" + " \n");
                Console.WriteLine("-----------------------------------------------------------------------------------------------------");
                Console.WriteLine("                                 Product ID: " + item.productID);
                Console.WriteLine("                                 Product Naam: " + item.productName);
                Console.WriteLine("                                 Aantal: " + item.amount + "X");
                Console.WriteLine("                                 Prijs: " + item.price + " Euro"); 
                Console.WriteLine("-----------------------------------------------------------------------------------------------------");
                break;
            }
        }
    }
    private static void menuOverview(List<Eten> etenMenu)
    {
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
                Console.WriteLine("                                 Prijs: " + item.price + " Euro");
                Console.WriteLine("-----------------------------------------------------------------------------------------------------");
                snacksCount++;
            }
        }

        if (snacksCount == 0)
        {
            Console.WriteLine("                 Er zijn geen snacks beschikbaar!\n");
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
                Console.WriteLine("                                 Prijs: " + item.price + " Euro");
                Console.WriteLine("-----------------------------------------------------------------------------------------------------");
                drinksCount++;
            }
        }

        if (drinksCount == 0)
        {
            Console.WriteLine("                 Er zijn geen dranken beschikbaar!\n");
        }
    }
    private void orderFood(string rol, int accountID, List<Eten> etenMenu)
    {
        //Omzet van gegevens in Json bestand over naar string en daarna in een lijst zetten.
        List<Account> accounts = JsonConvert.DeserializeObject<List<Account>>(File.ReadAllText("..\\..\\..\\account.json"));

        Dictionary<int, int> orderList = new Dictionary<int, int>();
        string Keuze = "1";
        double total = 0.0;
        while (Keuze == "1" || Keuze == "3")
        {
            Console.Clear();

            menuOverview(etenMenu);

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
                            Console.WriteLine("         Prijs per stuk: " + item2.price + " Euro\n");
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
                            if (item.productID == productID)
                            {
                                if (item.amount < productAmount && item.productID == productID)
                                {
                                    Console.WriteLine("Excuses, niet genoeg van deze product in voorraad!");
                                    Console.WriteLine("Probeer nogmaals a.u.b!");
                                    break;
                                }
                                else if (productAmount <= 0)
                                {
                                    Console.WriteLine("Ongelding invoer!");
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

            if (orderList.Count > 0)
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

    private void confirmOrder(int accountID, List<Eten> etenMenu, Dictionary<int, int> orderList)
    {
        string url = "..\\..\\..\\account.json";
        List<Account> accounts = JsonConvert.DeserializeObject<List<Account>>(File.ReadAllText("..\\..\\..\\account.json"));
        List<EtenBestelling> order = new List<EtenBestelling>();
        Console.Clear();
        Console.WriteLine("*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*");
        Console.WriteLine("|                                             Bevestigen                                            |");
        Console.WriteLine("*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*" + " \n");


        double total = 0.0;
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
                    Console.WriteLine("         Prijs per stuk: " + item2.price + " Euro\n");
                    total = total + (item.Value * item2.price);
                }
            }

        }
        Console.WriteLine("         Datum: " + DateTime.Now.ToString("dd-MM-yyyy"));
        Console.WriteLine("         Totaal: " + Math.Round(total, 2) + " Euro");

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
            foreach (dynamic item in etenMenu)
            {
                if (orderList.ContainsKey(item.productID))
                {
                    orderConfirmation.Add(item.productName, orderList[item.productID]);
                }
            }


            foreach (dynamic account in accounts)
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
                                date = DateTime.Now.ToString("dd-MM-yyyy"),
                                total = Math.Round(total, 2)
                            }
                        };
                        Omzet.AddOrRemoveOmzet(Math.Round(total, 2));
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
                        Omzet.AddOrRemoveOmzet(Math.Round(total, 2));
                    }
                }

            }

            string bestelling = JsonConvert.SerializeObject(accounts, Formatting.Indented);
            //verander de hele file met de nieuwe json informatie
            File.WriteAllText(url, bestelling);

            foreach (dynamic item in orderList)
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

    private void addMenuItem(List<Eten> etenMenu)
    {
        int itemID;
        if (etenMenu.Count == 0)
            itemID = etenMenu.Count() + 1;
        else
            itemID = etenMenu[etenMenu.Count() - 1].productID + 1;
        Console.Clear();
        
        menuOverview(etenMenu);

        
        string itemName = "";
        bool check = true;
        while (check)
        {
            Console.WriteLine("--------------------------------------------------------------------------------");
            Console.WriteLine("| Voer de naam in van het item die u wilt toevoegen: (typ '*' om te annuleren) |");
            Console.WriteLine("--------------------------------------------------------------------------------");
            itemName = Console.ReadLine();
            //Check of jaar een nummer is dat gelijk of groter is dan 1888 en niet groter that huidige jaar.
            if (int.TryParse(itemName, out _))
            {
                Console.WriteLine("Ongeldig invoer!\nItem naam mag niet alleen nummers bevatten.\nProbeer opnieuw a.u.b!");
                Thread.Sleep(2000);
            }
            else
            {
                if (itemName.Trim() == "*")
                {
                    Console.WriteLine("Bewerking is geannuleerd!");
                    Thread.Sleep(3000);
                    Console.Clear();
                    return;
                }
                else if(itemName.Trim().Length == 0)
                {
                    Console.WriteLine("Ongeldig invoer!");
                }
                else
                {
                    check = false;
                }
            }
        }

        
        int itemInventory = 0; 
        check = true;
        while (check)
        {
            Console.WriteLine("-----------------------------------------------------------------------------------------");
            Console.WriteLine("| Voer de aantal voorraad van het item die u wilt toevoegen: (typ '*' om te annuleren)  |");
            Console.WriteLine("-----------------------------------------------------------------------------------------");
            string placeHolder = Console.ReadLine();
            if(int.TryParse(placeHolder, out itemInventory))
            {
                if (itemInventory > 0 && itemInventory < 10000)
                {
                    check = false;
                }
                else if (itemInventory >= 10000)
                {
                    Console.WriteLine("Ingevoegde hoeveelheid te groot!\nVul een nummer tussen 1 en 9999\nProbeer opnieuw a.u.b!");
                    Thread.Sleep(1000);
                }
                else
                {
                    Console.WriteLine("Ingevoegde hoeveelheid is ongeldig!\nVul een nummer tussen 1 en 9999\nProbeer opnieuw a.u.b!");
                    Thread.Sleep(1000);
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
                    Console.WriteLine("Ongeldige invoer!");
                    Console.WriteLine("Probeer opniew!");
                }
                
            }
        }

        
        double itemPrice = 0.0;
        check = true;
        while (check)
        {
            Console.WriteLine("---------------------------------------------------------------------------------");
            Console.WriteLine("| Voer de prijs in van het item die u wilt toevoegen: (typ '*' om te annuleren) |");
            Console.WriteLine("---------------------------------------------------------------------------------");
            string placeHolder = Console.ReadLine();
            if (double.TryParse(placeHolder, out itemPrice))
            {
                if (itemPrice <= 0)
                {
                    Console.WriteLine("Ongeldig invoer!\nPrijs mag niet 0.00 of minder zijn!");
                }
                else
                {
                    itemPrice = Math.Round(itemPrice, 2);
                    check = false;
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
                    Console.WriteLine("Ongeldig invoer!");
                    Console.WriteLine("Probeer opniew!");
                }
            }
        }

        Console.WriteLine("-----------------------------------------------------------------------------");
        Console.WriteLine("| Kies de type van het item die u wilt toevoegen: (typ '*' om te annuleren) |");
        Console.WriteLine("-----------------------------------------------------------------------------");
        Console.WriteLine(" | [1] Eten | [2] Drank | ");
        string itemType = Console.ReadLine();
        while (itemType.Trim() != "*" && itemType.Trim() != "1" && itemType.Trim() != "2")
        {
            Console.WriteLine("Kies a.u.b een van de weergegeven opties!\n");
            itemType = Console.ReadLine();
        }
        switch (itemType.Trim())
        {
            case "1":
                itemType = "Eten";
                break;
            case "2":
                itemType = "Drank";
                break ;
            case "*":
                Console.WriteLine("Bewerking is geannuleerd!");
                Thread.Sleep(3000);
                Console.Clear();
                return;
        }
        etenMenu.Add(new Eten
        {
            productID = itemID,
            productName = itemName,
            amount = itemInventory,
            price = itemPrice,
            productType = itemType
        });

        
        File.WriteAllText("..\\..\\..\\Catering.json", JsonConvert.SerializeObject(etenMenu, Formatting.Indented));
        Console.WriteLine("Item succesvol toegevoegd!");
        Thread.Sleep(2000);
        Console.Clear();
    }

    private void removeItem(List<Eten> etenMenu)
    {
        var itemIDArray = new int[etenMenu.Count()];
        int index = 0;
        foreach (var item in etenMenu)
        {
            itemIDArray[index++] = item.productID;
        }

        Console.Clear();
        menuOverview(etenMenu);

        
        bool check = true;
        int itemID = 0;
        while (check)
        {
            Console.WriteLine("--------------------------------------------------------------------------------");
            Console.WriteLine("| Voer de ID in van het item die u wilt verwijderen: (typ '*' om te annuleren) |");
            Console.WriteLine("--------------------------------------------------------------------------------");
            string placeHolder = Console.ReadLine();
            if(int.TryParse(placeHolder, out itemID))
            {
                if (itemIDArray.Contains(itemID))
                {
                    check = false;
                }
                else
                {
                    Console.WriteLine("Geen resultaat gevonden!\nProbeer opnieuw a.u.b!");
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
                    Console.WriteLine("Ongeldig invoer!");
                }
            }
        }

        foreach(var item in etenMenu)
        {
            if (item.productID == itemID)
            {
                etenMenu.Remove(item);
                break;
            }
        }

        File.WriteAllText("..\\..\\..\\Catering.json", JsonConvert.SerializeObject(etenMenu, Formatting.Indented));
        Console.WriteLine("Item succesvol verwijderd!");
        Thread.Sleep(2000);
        Console.Clear();
    }

    private void editItem(List<Eten> etenMenu)
    {
        var itemIDArray = new int[etenMenu.Count()];
        int index = 0;
        foreach (var item in etenMenu)
        {
            itemIDArray[index++] = item.productID;
        }

        Console.Clear();
        menuOverview(etenMenu);
        
        bool check = true;
        int itemID = 0;
        while (check)
        {
            Console.WriteLine("-----------------------------------------------------------------------------");
            Console.WriteLine("| Voer de ID in van het item die u wilt wijzigen: (typ '*' om te annuleren) |");
            Console.WriteLine("-----------------------------------------------------------------------------");
            string placeHolder = Console.ReadLine();
            if (int.TryParse(placeHolder, out itemID))
            {
                if (itemIDArray.Contains(itemID))
                {
                    check = false;
                }
                else
                {
                    Console.WriteLine("Geen resultaat gevonden!\nProbeer opnieuw a.u.b!");
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
                    Console.WriteLine("Ongeldig invoer!");
                    Console.WriteLine("Probeer opniew!");
                }
            }
        }

        Console.Clear();
        selectedOverview(itemID, etenMenu);

        check = true;
        string itemName = "";
        while (check)
        {
            Console.WriteLine("----------------------------------------------------------------------------------");
            Console.WriteLine("| Voer de nieuwe naam in: (typ '/' om veld over te slaan of '*' om te annuleren) |");
            Console.WriteLine("----------------------------------------------------------------------------------");
            itemName = Console.ReadLine();
            if(int.TryParse(itemName, out _))
            {
                Console.WriteLine("Ongeldig invoer!\nItem naam mag niet alleen nummers bevatten.\nProbeer opnieuw a.u.b!");
                Thread.Sleep(2000);
            }
            else
            {
                if (itemName.Trim() == "*")
                {
                    Console.WriteLine("Bewerking is geannuleerd!");
                    Thread.Sleep(3000);
                    Console.Clear();
                    return;
                }
                else if(itemName.Trim() == "/")
                {
                    foreach(var item in etenMenu)
                    {
                        if(itemID == item.productID)
                        {
                            itemName = item.productName;
                            break;
                        }
                    }
                    check = false;
                }
                else if(itemName.Trim().Length == 0)
                {
                    Console.WriteLine("Ongeldig invoer!");
                    Console.WriteLine("Probeer opnieuw!");
                }
                else
                {
                    check = false;
                }
            }
        }

        
        check = true;
        double itemPrice = 0.0;
        while (check)
        {
            Console.WriteLine("-----------------------------------------------------------------------------------");
            Console.WriteLine("| Voer de nieuwe prijs in: (typ '/' om veld over te slaan of '*' om te annuleren) |");
            Console.WriteLine("-----------------------------------------------------------------------------------");
            string placeHolder = Console.ReadLine();
            if(double.TryParse(placeHolder, out itemPrice))
            {
                if(itemPrice <= 0.0)
                {
                    Console.WriteLine("Ongeldig invoer!\nPrijs mag niet 0.00 of minder zijn!");
                }
                else
                {
                    itemPrice = Math.Round(itemPrice,2);
                    check = false;
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
                else if(placeHolder.Trim() == "/")
                {
                    foreach(var item in etenMenu)
                    {
                        if(itemID == item.productID)
                        {
                            itemPrice = item.price;
                            break;
                        }
                    }
                    check=false;
                }
                else
                {
                    Console.WriteLine("Ongeldig invoer!\nPrijs moet een nummer zijn!");
                }
            }
        }

        Console.WriteLine("------------------------------------------------------------------------");
        Console.WriteLine("| Kies de type: (typ '/' om veld over te slaan of '*' om te annuleren) |");
        Console.WriteLine("------------------------------------------------------------------------");
        Console.WriteLine(" | [1] Eten | [2] Drank | ");
        string itemType = Console.ReadLine();
        while(itemType.Trim() != "1" && itemType.Trim() != "2" && itemType.Trim() != "/" && itemType.Trim() != "*")
        {
            Console.WriteLine("Kies a.u.b een van de weergegeven opties!\n");
            itemType = Console.ReadLine();
        }
        switch (itemType.Trim())
        {
            case "1":
                itemType = "Eten";
                break;
            case "2":
                itemType = "Drank";
                break ;
            case "/":
                foreach(var item in etenMenu)
                {
                    if(item.productID == itemID)
                    {
                        itemType = item.productType;
                        break;
                    }
                }
                break;
            case "*":
                Console.WriteLine("Bewerking is geannuleerd!");
                Thread.Sleep(3000);
                Console.Clear();
                return;
        }

        foreach(var item in etenMenu)
        {
            if(itemID == item.productID)
            {
                item.productName = itemName;
                item.price = itemPrice;
                item.productType = itemType;
            }
        }

        File.WriteAllText("..\\..\\..\\Catering.json", JsonConvert.SerializeObject(etenMenu, Formatting.Indented));
        Console.WriteLine("Item succesvol gewijzigd!");
        Thread.Sleep(2000);
        Console.Clear();
    }

    private void updateInventory(List<Eten> etenMenu)
    {
        var itemIDArray = new int[etenMenu.Count()];
        int index = 0;
        foreach (var item in etenMenu)
        {
            itemIDArray[index++] = item.productID;
        }
        Console.Clear();
        menuOverview(etenMenu);
        
        bool check = true;
        int itemID = 0;
        while (check)
        {
            Console.WriteLine("--------------------------------------------------------------------------------------------");
            Console.WriteLine("| Voer de ID in het item waarvan u de inventaris wilt bijwerken: (typ '*' om te annuleren) |");
            Console.WriteLine("--------------------------------------------------------------------------------------------");
            string placeHolder = Console.ReadLine();
            if (int.TryParse(placeHolder, out itemID))
            {
                if (itemIDArray.Contains(itemID))
                {
                    check = false;
                }
                else
                {
                    Console.WriteLine("Geen resultaat gevonden!\nProbeer opnieuw a.u.b!");
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
                    Console.WriteLine("Ongeldig invoer!");
                    Console.WriteLine("Probeer opniew!");
                }
            }
        }

        Console.Clear();
        selectedOverview(itemID, etenMenu);

        Console.WriteLine("----------------------------------------------");
        Console.WriteLine("| Kies de functie: (typ '*' om te annuleren) |");
        Console.WriteLine("----------------------------------------------");
        Console.WriteLine(" | [1] Toevoegen aan voorraad | [2] Aftrekken van de voorraad | ");
        string functieKeuze = Console.ReadLine();
        while( functieKeuze.Trim() != "1" && functieKeuze.Trim() != "2" && functieKeuze.Trim() != "*")
        {
            Console.WriteLine("Kies a.u.b een van de weergegeven opties!\n");
            functieKeuze = Console.ReadLine();
        }
        int itemAmount = 0;
        switch (functieKeuze.Trim())
        {
            case "1":
                addToInventory(itemID, etenMenu);
                break;
            case "2":
                deductFromInventory(itemID, etenMenu);
                break;
            case "*":
                Console.WriteLine("Bewerking is geannuleerd!");
                Thread.Sleep(3000);
                Console.Clear();
                return;
        }
    }

    private void addToInventory(int itemID, List<Eten> etenMenu)
    {
        int currentAmount = 0;
        foreach (Eten item in etenMenu)
        {
            if (item.productID == itemID)
            {
                currentAmount = item.amount;
            }
        }
        
        bool check = true;
        int itemAmount = 0;
        while (check)
        {
            Console.WriteLine("-----------------------------------------------------------------------------------------");
            Console.WriteLine("| Voer de aantal die u wilt toevoegen aan voorraad het item : (typ '*' om te annuleren) |");
            Console.WriteLine("-----------------------------------------------------------------------------------------");
            string placeHolder = Console.ReadLine();
            if(int.TryParse(placeHolder, out itemAmount))
            {
                if (itemAmount <= 0)
                {
                    Console.WriteLine("Het aantal moet minimaal 1 zijn!\nProbeer opnieuw a.u.b!");
                }
                else if(itemAmount + currentAmount > 9999)
                {
                    Console.WriteLine("Het aantal dat u wilt toevoegen overschrijdt de drempel van 9999 artikelen op voorraad!\nProbeer opnieuw a.u.b!");
                }
                else
                {
                    itemAmount = itemAmount + currentAmount;
                    check = false;
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
                    Console.WriteLine("Ongeldig invoer!");
                    Console.WriteLine("Probeer opniew!");
                }
            }
        }

        foreach (Eten item in etenMenu)
        {
            if (item.productID == itemID)
            {
                item.amount = itemAmount;
                break;
            }
        }
        File.WriteAllText("..\\..\\..\\Catering.json", JsonConvert.SerializeObject(etenMenu, Formatting.Indented));
        Console.WriteLine("Item voorraad succesvol bijgewerkt!");
        Thread.Sleep(2000);
        Console.Clear();
    }

    private void deductFromInventory(int itemID, List<Eten> etenMenu)
    {
        int currentAmount = 0;
        foreach (Eten item in etenMenu)
        {
            if (item.productID == itemID)
            {
                currentAmount = item.amount;
            }
        }
        
        bool check = true;
        int itemAmount = 0;
        while (check)
        {
            Console.WriteLine("-----------------------------------------------------------------------------------------");
            Console.WriteLine("| Voer de aantal die u wilt aftrekken van voorraad het item : (typ '*' om te annuleren) |");
            Console.WriteLine("-----------------------------------------------------------------------------------------");
            string placeHolder = Console.ReadLine();
            if (int.TryParse(placeHolder, out itemAmount))
            {
                if (itemAmount <= 0)
                {
                    Console.WriteLine("Het aantal moet minimaal 1 zijn!\nProbeer opnieuw a.u.b!");
                }
                else if (currentAmount - itemAmount < 0)
                {
                    Console.WriteLine("Het aantal dat u wilt aftrekken is groter dan het bedrag dat momenteel op voorraad is!\nProbeer opnieuw a.u.b!");
                }
                else
                {
                    itemAmount = currentAmount - itemAmount;
                    check = false;
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
                    Console.WriteLine("Ongeldig invoer!");
                    Console.WriteLine("Probeer opniew!");
                }
            }
        }

        foreach (Eten item in etenMenu)
        {
            if (item.productID == itemID)
            {
                item.amount = itemAmount;
                break;
            }
        }
        File.WriteAllText("..\\..\\..\\Catering.json", JsonConvert.SerializeObject(etenMenu, Formatting.Indented));
        Console.WriteLine("Item voorraad succesvol bijgewerkt!");
        Thread.Sleep(2000);
        Console.Clear();
    }
}
