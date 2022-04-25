using System;
using System.Linq;
using System.Threading;

public class HoofdScherm
{
    string logo = "*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*" + "\n" + "\n"         + 
        @"      $$\      $$\  $$$$$$\  $$\    $$\ $$$$$$\ $$$$$$$$\ $$\    $$\ $$$$$$$$\ $$$$$$$\   $$$$$$\  $$$$$$$$\ 
      $$$\    $$$ |$$  __$$\ $$ |   $$ |\_$$  _|$$  _____|$$ |   $$ |$$  _____|$$  __$$\ $$  __$$\ $$  _____|
      $$$$\  $$$$ |$$ /  $$ |$$ |   $$ |  $$ |  $$ |      $$ |   $$ |$$ |      $$ |  $$ |$$ /  \__|$$ |      
      $$\$$\$$ $$ |$$ |  $$ |\$$\  $$  |  $$ |  $$$$$\    \$$\  $$  |$$$$$\    $$$$$$$  |\$$$$$$\  $$$$$\    
      $$ \$$$  $$ |$$ |  $$ | \$$\$$  /   $$ |  $$  __|    \$$\$$  / $$  __|   $$  __$$<  \____$$\ $$  __|   
      $$ |\$  /$$ |$$ |  $$ |  \$$$  /    $$ |  $$ |        \$$$  /  $$ |      $$ |  $$ |$$\   $$ |$$ |      
      $$ | \_/ $$ | $$$$$$  |   \$  /   $$$$$$\ $$$$$$$$\    \$  /   $$$$$$$$\ $$ |  $$ |\$$$$$$  |$$$$$$$$\ 
      \__|     \__| \______/     \_/    \______|\________|    \_/    \________|\__|  \__| \______/ \________|" + "\n" + "\n" + 
        "*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*" + "\n";
    public string GebruikerHoofdscherm(bool loggedIn)
	{
        
        //Haalt alles wat vooraf stond weg
        Console.Clear();
        string menu = menuMethod(0);
        string[] menuArr = new string[] { "Films", "Locaties", "Eten & Drinken", "Zoeken", "Inloggen"};
        string[] menuLenght = new string[] { "1","2","3","4","5"};
        if (loggedIn)
        { 
            //Als je bent ingelogd dan veranderd de menuBalk en de Lengte van het menu
            menu = menuMethod(1);
            menuArr = new string[] { "Films", "Locaties", "Eten & Drinken", "Account Gegevens", "Zoeken", "Uitloggen" };
            menuLenght = new string[] { "1", "2", "3", "4", "5","6" };
        }
        Console.WriteLine(menu);
        string Input = "";
        //Deze while loop blijft loopen totdat je input in de menuLenght zit
        while (!menuLenght.Contains(Input))
        {
            Console.Clear();
            Console.Write(logo);
            Console.WriteLine(menu + "\n");
            Console.WriteLine("Toets het nummer van de pagina die u wilt bezoeken en druk vervolgens op enter:");
            Input = Console.ReadLine();
            //Input
            if (!menuLenght.Contains(Input))
            {
                //Wordt geprint als je foute input geeft
                Console.WriteLine("Toets a.u.b. een van de bovenstaande nummers in.");
            }
            //1 sec cooldown totdat hij alles wat was geprint weghaald
            Thread.Sleep(2500);
        }
        //Als je juiste input hebt gegeven dan return hij de naam van het scherm waar je naartoe wilt gaan door de index te gebruiken menuArr[Int32.Parse(Input)-1] 
        Console.WriteLine(menuArr[Int32.Parse(Input) - 1]);
        return menuArr[Int32.Parse(Input)-1];
    }

    public string adminHoofdscherm(bool loggedIn)
    {
        //Haalt alles wat vooraf stond weg
        Console.Clear();
        string menu = menuMethod(2);
        string[] menuArr = new string[] { "Films", "Locaties", "Eten & Drinken", "Account Gegevens", "Zoeken", "Omzet", "Uitloggen" };
        string[] menuLenght = new string[] { "1", "2", "3", "4", "5", "6", "7" };

        Console.WriteLine(menu);
        string Input = "";
        //Deze while loop blijft loopen totdat je input in de menuLenght zit
        while (!menuLenght.Contains(Input))
        {
            Console.Clear();
            Console.Write(logo);
            Console.WriteLine(menu + "\n");
            Console.WriteLine("Toets het nummer van de pagina die u wilt bezoeken en druk vervolgens op enter:");
            Input = Console.ReadLine();
            //Input
            if (!menuLenght.Contains(Input))
            {
                //Wordt geprint als je foute input geeft
                Console.WriteLine("Toets a.u.b. een van de bovenstaande nummers in.");
            }
            //1 sec cooldown totdat hij alles wat was geprint weghaald
            Thread.Sleep(2000);
        }
        //Als je juiste input hebt gegeven dan return hij de naam van het scherm waar je naartoe wilt gaan door de index te gebruiken menuArr[Int32.Parse(Input)-1] 
        Console.WriteLine(menuArr[Int32.Parse(Input) - 1]);
        return menuArr[Int32.Parse(Input) - 1];
    }

    public string menuMethod(int index)
    {
        string[]menuInterface = new string[] 
        { "|  [1] Film | [2] Locaties | [3] Eten & Drinken | [4] Zoeken | [5] Inloggen -- Wachtwoord Vergeten -- Registreren |" + "\n" +
          "-------------------------------------------------------------------------------------------------------------------",
          "|        [1] Film | [2] Locaties | [3] Eten & Drinken | [4] Account Gegevens | [5] Zoeken | [6] Uitloggen         |" + "\n" +
          "-------------------------------------------------------------------------------------------------------------------",
          "|  [1] Film | [2] Locaties | [3] Eten & Drinken | [4] Account Gegevens | [5] Zoeken | [6] Omzet | [7] Uitloggen   |" + "\n" + 
          "------------------------------------------------------------------------------------------------------------------"
        };
        return menuInterface[index];
    }
}
