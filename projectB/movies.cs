using System;
using System.Collections.Generic;
using System.Linq;

public class Movies
{
	public void show(List<movie> accountList)
	{
        Console.Clear();
        // pak alle informatie per film
        foreach (dynamic item in accountList)
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
                    
            }
            //print alles uit
            Console.WriteLine("------------------------------------------------------------------------------------------");
            Console.WriteLine("                       movie id : " + item.id);
            Console.WriteLine("                       name: " + item.name);
            Console.WriteLine("                       year: " + item.year);
            Console.WriteLine("                       categories: " + categories);
            Console.WriteLine("------------------------------------------------------------------------------------------");
            
        }

        Console.ReadLine();
    }
}
