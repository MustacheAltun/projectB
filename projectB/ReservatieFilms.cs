using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class ReservatieFilms
{
	public void showBeschikbaar(string bioscoopNaam, string zaalNaam, string tijdNaam)
	{
		//pak alle json informatie
		string url = "..\\..\\..\\locatie.json";
		string locatieJson = File.ReadAllText(url);

		//maak een nieuwe dictionary om alle informatie op te slaan voor later
		List<Cinema_adress> locatieList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Cinema_adress>>(locatieJson);

		// vraag de input
		Console.WriteLine("B: Beschikbaar		O: Onbeschikbaar		G:Gebroken");
		Console.WriteLine("------------------------------------------------SCHERM------------------------------------------------");
		int counter = 0;
		Dictionary<int, string> stoelen = new Dictionary<int, string>();
		int row = 0;
		string chairResult = "";

		/*
		 * maak een forloop om te checken welke stoelen genomen, gebroken en beschikbaar zijn
		 * voor elke locatie in de lijst van locaties check je of het overeenkomt met de bioscoopNaam
		 * voor elke zaal in zalen van de locatie check je of het overeenkomt met de zaalNaam
		 * voor elke tijd in tijden check je of het overeenkomt met tijdNaam
		 * en nu pak je alle informatie en zet je het in de stoelen dictionary
		 * 
		 */
		foreach (var locatie in locatieList)
		{
			if (locatie.name == bioscoopNaam)
			{ 
				foreach (var zaal in locatie.zalen)
				{
					if (zaal.naam == zaalNaam)
					{
						row = zaal.zitplekken/10;
						foreach (var tijd in zaal.tijden)
						{
							if (tijd.tijd == tijdNaam)
							{
								foreach (var number in tijd.gebroken)
								{
									if (!stoelen.ContainsKey(Int32.Parse(number.Key)))
									{
										if (number.Value == true)
										{
											stoelen.Add(Int32.Parse(number.Key), "G");
										}
									}


								}
								foreach (var number in tijd.beschikbaar)
								{
									if (!stoelen.ContainsKey(Int32.Parse(number.Key)))
									{
										if (number.Value == true)
										{
											stoelen.Add(Int32.Parse(number.Key), "B");
										}
										else
										{
											stoelen.Add(Int32.Parse(number.Key), "O");
										}

									}

								}
							}
						}
					}
				}
			}
		}

		//nu moet je alles filteren op nummer en laten zien
		foreach (var stoel in stoelen.OrderBy(x => x.Key))
        {
            if (stoel.Value == "G")
            {
				chairResult += stoel.Key + " : G	";
			}
            else if (stoel.Value == "O")
            {
				chairResult += stoel.Key + " : O	";
			}
            else
            {
				chairResult += stoel.Key + " : B	";

			}
			counter++;
            if (counter % row == 0)
            {
				chairResult += "\n";
            }
        }
		Console.WriteLine(chairResult);
		Console.WriteLine("------------------------------------------------ACHTERKANT------------------------------------------------");
	}

	public string stoelKiezen(string bioscoopNaam, string zaalNaam, string tijdNaam)
    {
		//pak alle json informatie
		string url = "..\\..\\..\\locatie.json";
		string locatieJson = File.ReadAllText(url);
		List<Cinema_adress> locatieList = JsonConvert.DeserializeObject<List<Cinema_adress>>(locatieJson);

		//maak een nieuwe dictionary om alle informatie op te slaan voor later
		Dictionary<int, string> stoelen = new Dictionary<int, string>();

		// vraag de input
		Console.WriteLine("\nWelke stoel wilt u, geef de nummer aan:");
		string input = Console.ReadLine();
		bool wrong = true;


		/*
		 * maak een forloop om te checken welke stoelen genomen, gebroken en beschikbaar zijn
		 * voor elke locatie in de lijst van locaties check je of het overeenkomt met de bioscoopNaam
		 * voor elke zaal in zalen van de locatie check je of het overeenkomt met de zaalNaam
		 * voor elke tijd in tijden check je of het overeenkomt met tijdNaam
		 * en nu pak je alle informatie en zet je het in de stoelen dictionary
		 * 
		 */
		foreach (var locatie in locatieList)
		{
			if (locatie.name == bioscoopNaam)
			{
				foreach (var zaal in locatie.zalen)
				{
					if (zaal.naam == zaalNaam)
					{
						foreach (var tijd in zaal.tijden)
						{
							if (tijd.tijd == tijdNaam)
							{
								foreach (var number in tijd.gebroken)
								{
									if (!stoelen.ContainsKey(Int32.Parse(number.Key)))
									{
										if (number.Value == true)
										{
											stoelen.Add(Int32.Parse(number.Key), "G");
										}
									}


								}
								foreach (var number in tijd.beschikbaar)
								{
									if (!stoelen.ContainsKey(Int32.Parse(number.Key)))
									{
										if (number.Value == true)
										{
											stoelen.Add(Int32.Parse(number.Key), "B");
										}
										else
										{
											stoelen.Add(Int32.Parse(number.Key), "O");
										}

									}

								}
							}
						}
					}
				}
			}
		}
		/*
		 * hier worden de checks gedaan voor de input van stoelen
		 * dus check als het letters zijn of als het invalid nummer is of als het gebroken stoel is etc etc
		 */
        while (wrong)
		{
			if (!input.All(Char.IsDigit))
			{
				Console.WriteLine("\nU moet de nummer aangeven van de stoel zonder letters, probeer nogmaals:");
				input = Console.ReadLine();
			}
			else if (!stoelen.ContainsKey(Int32.Parse(input)))
			{
				Console.WriteLine("\nUw nummer staat niet in de lijst, probeer nogmaals:");
				input = Console.ReadLine();
			}
			else
			{
				bool brokenchair = false;
				bool taken = false;
				foreach (var chair in stoelen)
				{
					if (chair.Key == Int32.Parse(input))
					{
						if (chair.Value == "G")
						{
							brokenchair = true;
						}
						else if (chair.Value == "O")
						{
							taken = true;
						}
					}
				}
				if (brokenchair)
				{
					Console.WriteLine("\nUw stoel die u heeft gekozen is een gebroken stoel, probeer nogmaals:");
					input = Console.ReadLine();
				}
				else if (taken)
				{
					Console.WriteLine("\nUw stoel die u heeft gekozen is al genomen, probeer nogmaals:");
					input = Console.ReadLine();
				}
				if (brokenchair == false && taken == false)
				{
					wrong = false;
				}
			}
		}

		//verander deze data met genomen in de json file
		locatieList.Single(locatie => locatie.name == bioscoopNaam)
			.zalen.Single(zaal => zaal.naam == zaalNaam)
			.tijden.Single(tijd => tijd.tijd == tijdNaam)
			.beschikbaar[input] = false;

		string changedLocatie = JsonConvert.SerializeObject(locatieList, Formatting.Indented);
		//verander de hele file met de nieuwe json informatie
		File.WriteAllText(url, changedLocatie);
		return input;
    }
}
