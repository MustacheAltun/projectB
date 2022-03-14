using System;
using System.Linq;
using System.Threading;

public class HoofdScherm
{
	public string GebruikerHoofdscherm(bool loggedIn)
	{
        Console.Clear();
        string menu = "| [1] Film | [2] Locaties | [3] Eten & Drinken | [4] Zoeken | [5] Inloggen |";
        string[] menuArr = new string[5] { "Films", "Locaties", "Eten & Drinken", "Zoeken", "Inloggen" };
        string[] menuLenght = new string[5] { "1","2","3","4","5"};
        if (loggedIn)
        {
            menu = "| [1] Film | [2] Locaties | [3] Eten & Drinken | [4] Account Gegevens | [5] Zoeken | [6] Uitloggen |";
            menuArr = new string[6] { "Films", "Locaties", "Eten & Drinken", "Account Gegevens", "Zoeken", "Uitloggen" };
            menuLenght = new string[6] { "1", "2", "3", "4", "5","6" };
        }
        Console.WriteLine(menu);
        string Input = "";
        while (!menuLenght.Contains(Input))
        {
            Console.Clear();
            Console.WriteLine(menu);
            Console.WriteLine("Geef input:");
            Input = Console.ReadLine();
            if (!menuLenght.Contains(Input))
            {
                Console.WriteLine("Invalide Input!");
            }
            Thread.Sleep(1000);
        }
        Console.WriteLine(menuArr[Int32.Parse(Input) - 1]);
        return menuArr[Int32.Parse(Input)-1];
    }
    //public static string AdminHoofdscherm()
    //{

    //}
}
