using System;
using System.Linq;
using System.Threading;

public class HoofdScherm
{
	public string GebruikerHoofdscherm(bool loggedIn)
	{
        //Haalt alles wat vooraf stond weg
        Console.Clear();
        string menu = "| [1] Film | [2] Locaties | [3] Eten & Drinken | [4] Zoeken | [5] Inloggen | [6] Registreren";
        string[] menuArr = new string[6] { "Films", "Locaties", "Eten & Drinken", "Zoeken", "Inloggen","Registreren"};
        string[] menuLenght = new string[6] { "1","2","3","4","5","6"};
        if (loggedIn)
        { 
            //Als je bent ingelogd dan veranderd de menuBalk en de Lengte van het menu
            menu = "| [1] Film | [2] Locaties | [3] Eten & Drinken | [4] Account Gegevens | [5] Zoeken | [6] Uitloggen |";
            menuArr = new string[6] { "Films", "Locaties", "Eten & Drinken", "Account Gegevens", "Zoeken", "Uitloggen" };
            menuLenght = new string[6] { "1", "2", "3", "4", "5","6" };
        }
        Console.WriteLine(menu);
        string Input = "";
        //Deze while loop blijft loopen totdat je input in de menuLenght zit
        while (!menuLenght.Contains(Input))
        {
            Console.Clear();
            Console.WriteLine(menu);
            Console.WriteLine("Geef input:");
            Input = Console.ReadLine();
            //Input
            if (!menuLenght.Contains(Input))
            {
                //Wordt geprint als je foute input geeft
                Console.WriteLine("Invalide Input!");
            }
            //1 sec cooldown totdat hij alles wat was geprint weghaald
            Thread.Sleep(2000);
        }
        //Als je juiste input hebt gegeven dan return hij de naam van het scherm waar je naartoe wilt gaan door de index te gebruiken menuArr[Int32.Parse(Input)-1] 
        Console.WriteLine(menuArr[Int32.Parse(Input) - 1]);
        return menuArr[Int32.Parse(Input)-1];
    }
    //public static string AdminHoofdscherm()
    //{

    //}
}
