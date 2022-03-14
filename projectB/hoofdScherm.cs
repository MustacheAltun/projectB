using System;

public class HoofdScherm
{
	public void GebruikerHoofdscherm(bool loggedIn)
	{
        string menu = "| [1] Film | [2] Locaties | [3] Eten & Drinken | [4] Zoeken | [5] Inloggen |";
        if (loggedIn)
        {
            menu = "| [1] Film | [2] Locaties | [3] Eten & Drinken | [4] Account Gegevens | [5] Zoeken | [6] Uitloggen |";
        }
        Console.WriteLine(menu);

    }
    //public static string AdminHoofdscherm()
    //{

    //}
}
