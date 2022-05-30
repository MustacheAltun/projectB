using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

public class ReservatieFilms
{
	private void showBeschikbaar(int bioscoopNaam, string zaalNaam, string tijdNaam, string datum)
	{
		//pak alle json informatie
		string url = "..\\..\\..\\locatie.json";
		string locatieJson = File.ReadAllText(url);

		//maak een nieuwe dictionary om alle informatie op te slaan voor later
		List<Cinema_adress> locatieList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Cinema_adress>>(locatieJson);

		// vraag de input
		Console.WriteLine("B: Beschikbaar		O: Onbeschikbaar		");
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
		bool done = false;
		foreach (var locatie in locatieList)
		{
			if (locatie.id == bioscoopNaam)
			{
				foreach (var dag in locatie.dagen)
				{
					foreach (var item in dag)
					{
						if (item.datum == datum)
						{
							if (item.naam == zaalNaam)
							{
								row = item.zitplekken / 10;
								foreach (var tijd in item.tijden)
								{
									if (tijd.tijd == tijdNaam)
									{
										for (int i = 0; i < tijd.beschikbaar.Length; i++)
										{
											if (tijd.beschikbaar[i] == 'T')
											{
												stoelen.Add(i + 1, "B");
											}
											else
											{
												stoelen.Add(i + 1, "O");
											}
										}
										done = true;
										break;

									}
									if (done)
									{
										break;
									}
								}
								if (done)
								{
									break;
								}
							}
							if (done)
							{
								break;
							}
						}
						if (done)
						{
							break;
						}
					}
					if (done)
					{
						break;
					}


				}
				if (done)
				{
					break;
				}

			}
			if (done)
			{
				break;
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

	private string stoelKiezen(int bioscoopNaam, string zaalNaam, string tijdNaam, string datum)
	{
		//pak alle json informatie
		string url = "..\\..\\..\\locatie.json";
		string locatieJson = File.ReadAllText(url);
		List<Cinema_adress> locatieList = JsonConvert.DeserializeObject<List<Cinema_adress>>(locatieJson);

		//maak een nieuwe dictionary om alle informatie op te slaan voor later
		Dictionary<int, string> stoelen = new Dictionary<int, string>();

		// vraag de input
		Console.WriteLine("\nWelke stoel wilt u, geef de nummer aan :");
		Console.WriteLine("U kunt '/' typen om al uw keuzes opnieuw te maken.");
		Console.WriteLine("U kunt '*' typen om terug te gaan.");
		string input = Console.ReadLine();
		
		bool wrong = true;
		int row = 0;
		string jsonBeschikbaar = "";

		/*
		 * maak een forloop om te checken welke stoelen genomen, gebroken en beschikbaar zijn
		 * voor elke locatie in de lijst van locaties check je of het overeenkomt met de bioscoopNaam
		 * voor elke zaal in zalen van de locatie check je of het overeenkomt met de zaalNaam
		 * voor elke tijd in tijden check je of het overeenkomt met tijdNaam
		 * en nu pak je alle informatie en zet je het in de stoelen dictionary
		 * 
		 */
		bool done = false;
		int stoelencounter = 0;
		foreach (var locatie in locatieList)
		{
			if (locatie.id == bioscoopNaam)
			{
				foreach (var dag in locatie.dagen)
				{
					foreach (var z in dag)
					{
						if (z.datum == datum)
						{
							if (z.naam == zaalNaam)
							{

								row = z.zitplekken / 10;
								foreach (var tijd in z.tijden)
								{

									if (tijd.tijd == tijdNaam)
									{
										jsonBeschikbaar = tijd.beschikbaar;
										for (int i = 0; i < tijd.beschikbaar.Length; i++)
										{
											if (tijd.beschikbaar[i] == 'T')
											{
												stoelen.Add(i + 1, "B");
											}
											else
											{
												stoelen.Add(i + 1, "O");
											}
										}
                                        if (stoelencounter == tijd.beschikbaar.Length)
                                        {
											done = true;
											break;
										}
										

									}
									if (done)
									{
										break;
									}
								}
								if (done)
								{
									break;
								}
							}	
						}
					}
					if (done)
					{
						break;
					}


				}
				if (done)
				{
					break;
				}

			}
		}
		/*
		 * hier worden de checks gedaan voor de input van stoelen
		 * dus check als het letters zijn of als het invalid nummer is of als het gebroken stoel is etc etc
		 */
		while (wrong)
		{
			if (input.Trim() == "/")
			{
				return input;

			}
            else if (input.Trim() == "*")
            {
				return input;
			}
			else if (!input.All(Char.IsDigit))
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

		StringBuilder nieuweBeschikbaar = new StringBuilder(jsonBeschikbaar);
		nieuweBeschikbaar[Int32.Parse(input) - 1] = 'F';
		jsonBeschikbaar = nieuweBeschikbaar.ToString();

		//verander deze data met genomen in de json file
		foreach (var locatie in locatieList)
		{
			if (locatie.id == bioscoopNaam)
			{
				foreach (var dag in locatie.dagen)
				{
					foreach (var z in dag)
					{
						if (z.datum == datum)
						{
							if (z.naam == zaalNaam)
							{

								row = z.zitplekken / 10;
								foreach (var tijd in z.tijden)
								{

									if (tijd.tijd == tijdNaam)
									{
										tijd.beschikbaar = jsonBeschikbaar;
									}
								}
							}
						}
					}


				}

			}
		}

		string changedLocatie = JsonConvert.SerializeObject(locatieList, Formatting.Indented);
		//verander de hele file met de nieuwe json informatie
		File.WriteAllText(url, changedLocatie);
		return input;
	}

	private void stoelVrijMaken(int bioscoopID, string zaalNaam, string tijdNaam, string stoel, string datum)
	{//pak alle json informatie
		string url = "..\\..\\..\\locatie.json";
		string locatieJson = File.ReadAllText(url);
		List<Cinema_adress> locatieList = JsonConvert.DeserializeObject<List<Cinema_adress>>(locatieJson);

		//maak een nieuwe dictionary om alle informatie op te slaan voor later
		Dictionary<int, string> stoelen = new Dictionary<int, string>();

		// vraag de input
		bool wrong = true;
		int row = 0;
		string jsonBeschikbaar = "";

		/*
		 * maak een forloop om te checken welke stoelen genomen, gebroken en beschikbaar zijn
		 * voor elke locatie in de lijst van locaties check je of het overeenkomt met de bioscoopNaam
		 * voor elke zaal in zalen van de locatie check je of het overeenkomt met de zaalNaam
		 * voor elke tijd in tijden check je of het overeenkomt met tijdNaam
		 * en nu pak je alle informatie en zet je het in de stoelen dictionary
		 * 
		 */
		bool done = false;
		foreach (var locatie in locatieList)
		{
			if (locatie.id == bioscoopID)
			{
				foreach (var dag in locatie.dagen)
				{
					foreach (var z in dag)
					{
						if (z.datum == datum)
						{
							if (z.naam == zaalNaam)
							{

								row = z.zitplekken / 10;
								foreach (var tijd in z.tijden)
								{

									if (tijd.tijd == tijdNaam)
									{
										jsonBeschikbaar = tijd.beschikbaar;
										for (int i = 0; i < tijd.beschikbaar.Length; i++)
										{
											if (tijd.beschikbaar[i] == 'T')
											{
												stoelen.Add(i + 1, "B");
											}
											else
											{
												stoelen.Add(i + 1, "O");
											}
										}
										done = true;
										break;

									}
									if (done)
									{
										break;
									}
								}
								if (done)
								{
									break;
								}
							}
							if (done)
							{
								break;
							}
						}
						if (done)
						{
							break;
						}
					}
					if (done)
					{
						break;
					}


				}
				if (done)
				{
					break;
				}

			}
			if (done)
			{
				break;
			}
		}

		StringBuilder nieuweBeschikbaar = new StringBuilder(jsonBeschikbaar);
		nieuweBeschikbaar[Int32.Parse(stoel) - 1] = 'T';
		jsonBeschikbaar = nieuweBeschikbaar.ToString();

		//verander deze data met genomen in de json file
		foreach (var locatie in locatieList)
		{
			if (locatie.id == bioscoopID)
			{
				foreach (var dag in locatie.dagen)
				{
					foreach (var z in dag)
					{
						if (z.datum == datum)
						{
							if (z.naam == zaalNaam)
							{

								row = z.zitplekken / 10;
								foreach (var tijd in z.tijden)
								{

									if (tijd.tijd == tijdNaam)
									{
										tijd.beschikbaar = jsonBeschikbaar;
									}
								}
							}
						}
					}


				}

			}
		}

		string changedLocatie = JsonConvert.SerializeObject(locatieList, Formatting.Indented);
		//verander de hele file met de nieuwe json informatie
		File.WriteAllText(url, changedLocatie);
	}

	private int aantalVrijeStoelen(int bioscoopNaam, string zaalNaam, string tijdNaam, string datum)
    {
		//pak alle json informatie
		string url = "..\\..\\..\\locatie.json";
		string locatieJson = File.ReadAllText(url);
		List<Cinema_adress> locatieList = JsonConvert.DeserializeObject<List<Cinema_adress>>(locatieJson);

		//maak een nieuwe dictionary om alle informatie op te slaan voor later
		int vrijeStoelen = 0;


		/*
		 * maak een forloop om te checken welke stoelen genomen, gebroken en beschikbaar zijn
		 * voor elke locatie in de lijst van locaties check je of het overeenkomt met de bioscoopNaam
		 * voor elke zaal in zalen van de locatie check je of het overeenkomt met de zaalNaam
		 * voor elke tijd in tijden check je of het overeenkomt met tijdNaam
		 * en nu pak je alle informatie en zet je het in de stoelen dictionary
		 * 
		 */
		bool done = false;
		int stoelencounter = 0;
		foreach (var locatie in locatieList)
		{
			if (locatie.id == bioscoopNaam)
			{
				foreach (var dag in locatie.dagen)
				{
					foreach (var z in dag)
					{
						if (z.datum == datum)
						{
							if (z.naam == zaalNaam)
							{

								foreach (var tijd in z.tijden)
								{

									if (tijd.tijd == tijdNaam)
									{
										for (int i = 0; i < tijd.beschikbaar.Length; i++)
										{
											if (tijd.beschikbaar[i] == 'T')
											{
												vrijeStoelen++;
												stoelencounter++;
											}
											else
											{
												stoelencounter++;
											}
										}
										if (stoelencounter == tijd.beschikbaar.Length)
										{
											done = true;
											break;
										}


									}
									if (done)
									{
										break;
									}
								}
								if (done)
								{
									break;
								}
							}
						}
					}
					if (done)
					{
						break;
					}


				}
				if (done)
				{
					break;
				}

			}
		}
		return vrijeStoelen;
	}

	public void reserveren(string filmIdString, int accountID)
	{

		Console.Clear();
		bool loopBack = true;
		

		// lees de file en zet alles in een string
		string strResultJson = File.ReadAllText("..\\..\\..\\account.json");
		string strResultJson2 = File.ReadAllText("..\\..\\..\\locatie.json");

		// maak een lijst van alle informatie die er is
		List<Account> accounts = JsonConvert.DeserializeObject<List<Account>>(strResultJson);
		List<Cinema_adress> locaties = JsonConvert.DeserializeObject<List<Cinema_adress>>(strResultJson2);

		Console.WriteLine("wat is jouw volledige naam?");
		string fullName = Console.ReadLine();

        while (string.IsNullOrEmpty(fullName) || Locatie.hasSpecialChar(fullName) || fullName.Any(char.IsDigit) || fullName.Length < 7)
        {
			Console.WriteLine("Dit is niet uw volledige naam, voer uw volledige naam in aub.");
			fullName = Console.ReadLine();
		}

		while (loopBack)
		{
			Console.Clear();
			double filmPrijs = 0;
			int bioscopenMetFilm = 0;
			int tijdenMetFilm = 0;
			int zalenMetFilm = 0;
			int dagenCounter = 0;
			int filmId = Int32.Parse(filmIdString);
			bool loopBackChecker = false;

			foreach (var locatie in locaties)
			{
				bool checklocatie = false;
				foreach (var zaal in locatie.dagen)
				{
					foreach (var z in zaal)
					{
						if (z.film_ID == filmId)
						{
							bioscopenMetFilm++;
							checklocatie = true;
							break;
						}

					}
					if (checklocatie)
					{
						break;
					}

				}

			}


			string[] bioscoopNamen = new string[bioscopenMetFilm];
			int[] bioscoopNamenID = new int[bioscopenMetFilm];



			int bioscoopNamenCounter = 0;
			int filmtijdenCounter = 0;
			foreach (var locatie in locaties)
			{
				bool check = false;
				foreach (var zaal in locatie.dagen)
				{
					foreach (var z in zaal)
					{
						if (z.film_ID == filmId)
						{
							foreach (var tijd in z.tijden)
							{
								bioscoopNamen[bioscoopNamenCounter] = locatie.name + " " + locatie.address;
								bioscoopNamenID[bioscoopNamenCounter] = locatie.id;
								check = true;
								bioscoopNamenCounter++;
								break;


							}
						}
						if (check)
						{
							break;
						}
					}
					if (check)
					{
						break;
					}

				}

			}

			bioscoopNamenCounter = 0;
			filmtijdenCounter = 0;
			string bioscoopKeuze = "-1";
			string bioscoopNaam = "";
			string tijdKeuze = "";
			string zaalKeuze = "";
			string technologie = "";
			string dagKeuze = "";
			string stoelKeuze = "";
            if (loopBackChecker == false)
            {
				while (true)
				{
					Console.Clear();
					Console.WriteLine("Kies de bioscoop waarin u de film wilt bekijken.\n");
					Console.WriteLine("U kunt '/' typen om al uw keuzes opnieuw te maken.");
					Console.WriteLine("U kunt '*' typen om terug te gaan.\n");
					foreach (var item in bioscoopNamen)
					{

						Console.WriteLine("[" + bioscoopNamenCounter + "] : " + item + "\n");
						bioscoopNamenCounter++;
					}
					bioscoopKeuze = Console.ReadLine();
                    if (bioscoopKeuze.Trim() == "/")
                    {
						loopBackChecker = true;
						break;

					}
                    else if (bioscoopKeuze.Trim() == "*")
                    {
						return;
                    }
					else if (!(bioscoopKeuze.All(char.IsDigit)) || String.IsNullOrEmpty(bioscoopKeuze) || Int32.Parse(bioscoopKeuze) >= bioscoopNamen.Length || Int32.Parse(bioscoopKeuze) < 0)
					{
						bioscoopNamenCounter = 0;
						Console.WriteLine("Gebruik alstublieft de bovenstaande keuzes.\n");
						Thread.Sleep(1000);
					}
					else
					{

						bioscoopKeuze = bioscoopNamenID[int.Parse(bioscoopKeuze)].ToString();
						foreach (var item in locaties)
						{
							if (int.Parse(bioscoopKeuze) == item.id)
							{
								bioscoopNaam = item.name;
								break;
							}
						}
						break;
					}
				}
			}

			if (loopBackChecker == false)
			{
				foreach (var locatie in locaties)
				{
					if (int.Parse(bioscoopKeuze) == locatie.id)
					{
						foreach (var zaal in locatie.dagen)
						{
							foreach (var z in zaal)
							{
								if (z.film_ID == filmId)
								{
									dagenCounter++;
									break;
								}
							}


						}
						break;
					}
				}
			}
			string[] datums = new string[dagenCounter];
			if (loopBackChecker == false)
            {
				
				dagenCounter = 0;
				foreach (var locatie in locaties)
				{
					if (int.Parse(bioscoopKeuze) == locatie.id)
					{
						foreach (var zaal in locatie.dagen)
						{
							foreach (var z in zaal)
							{
								if (z.film_ID == filmId)
								{
									datums[dagenCounter] = z.datum;
									dagenCounter++;
									break;
								}
							}

						}
						break;
					}

				}
			}
			string datumKeuze = "";
			if (loopBackChecker == false)
			{


				dagenCounter = 0;
				bool extracheck = true;
				
				while (extracheck)
				{
					Console.Clear();
					Console.WriteLine("Kies de datum waarin u de film wilt bekijken.\n");
					Console.WriteLine("U kunt '/' typen om al uw keuzes opnieuw te maken.");
					Console.WriteLine("U kunt '*' typen om terug te gaan.\n");
					foreach (var datum in datums)
					{
						Console.WriteLine("[" + dagenCounter + "] : " + datum);
						dagenCounter++;
					}
					datumKeuze = Console.ReadLine();
					if (datumKeuze.Trim() == "/")
					{
						loopBackChecker = true;
						break;

                    }
                    else if (datumKeuze.Trim() == "*")
                    {
						return;
                    }
					else if (!(datumKeuze.All(char.IsDigit)) || String.IsNullOrEmpty(datumKeuze) || Int32.Parse(datumKeuze) >= datums.Length || Int32.Parse(datumKeuze) < 0)
					{
						dagenCounter = 0;
						Console.WriteLine("Gebruik alstublieft de bovenstaande keuzes.\n");
						Thread.Sleep(1000);
					}
					else
					{

						datumKeuze = datums[int.Parse(datumKeuze)];
						break;
					}
				}
			}
			if (loopBackChecker == false)
			{
				foreach (var locatie in locaties)
				{
					if (locatie.id == int.Parse(bioscoopKeuze))
					{
						foreach (var zaal in locatie.dagen)
						{
							foreach (var z in zaal)
							{
								if (datumKeuze == z.datum)
								{
									if (filmId == z.film_ID)
									{
										foreach (var tijd in z.tijden)
										{

											zalenMetFilm++;
											break;

										}
									}
								}
							}


						}
					}

				}
			}
			string[] zalenTechnoligien = new string[zalenMetFilm];
			zalenMetFilm = 0;
            if (loopBackChecker == false)
            {
				foreach (var locatie in locaties)
				{
					if (locatie.id == int.Parse(bioscoopKeuze))
					{
						foreach (var zaal in locatie.dagen)
						{
							foreach (var z in zaal)
							{
								if (datumKeuze == z.datum)
								{
									if (filmId == z.film_ID)
									{
										foreach (var tijd in z.tijden)
										{

											zalenTechnoligien[zalenMetFilm] = z.naam + " " + z.type + " : " + z.prijs;
											zalenMetFilm++;
											break;

										}
									}
								}
							}


						}
					}

				}
			}
			
			zalenMetFilm = 0;
			if (loopBackChecker == false)
			{


				while (true)
				{
					Console.Clear();
					Console.WriteLine("Kies de zaal waarin u de film wilt bekijken.\n");
					Console.WriteLine("U kunt '/' typen om al uw keuzes opnieuw te maken.");
					Console.WriteLine("U kunt '*' typen om terug te gaan.\n");
					foreach (var item in zalenTechnoligien)
					{
						Console.WriteLine("[" + zalenMetFilm + "] : " + item + "\n");
						zalenMetFilm++;
					}
					zaalKeuze = Console.ReadLine();
					if (zaalKeuze.Trim() == "/")
					{
						loopBackChecker = true;
						break;

                    }
                    else if (zaalKeuze.Trim() == "*")
                    {
						return;
                    }
					else if (!(zaalKeuze.All(char.IsDigit)) || String.IsNullOrEmpty(zaalKeuze) || Int32.Parse(zaalKeuze) >= zalenTechnoligien.Length || Int32.Parse(zaalKeuze) < 0)
					{
						Console.WriteLine("Gebruik alstublieft de bovenstaande keuzes.\n");
						zalenMetFilm = 0;
						Thread.Sleep(1000);
					}
					else
					{
						zaalKeuze = zalenTechnoligien[Int32.Parse(zaalKeuze)];
                        break;

					}
				}
			}
			tijdenMetFilm = 0;
			string searchZaal = "";
			bool tijdChecker = false;
			if (loopBackChecker == false)
            {

            
			switch (zaalKeuze)
			{
				case "zaal 1 2D : 12.5":
					searchZaal = "zaal 1";
					break;
				case "zaal 2 3D : 15":
					searchZaal = "zaal 2";
					break;
				case "zaal 3 IMAX : 17.5":
					searchZaal = "zaal 3";
					break;
				case "zaal 1 2D : 12,5":
					searchZaal = "zaal 1";
					break;
				case "zaal 3 IMAX : 17,5":
					searchZaal = "zaal 3";
					break;
				}
			
			foreach (var locatie in locaties)
			{
				if (locatie.id == int.Parse(bioscoopKeuze))
				{
					foreach (var zaal in locatie.dagen)
					{
						foreach (var z in zaal)
						{
							if (z.datum == datumKeuze)
							{
								if (searchZaal == z.naam)
								{
									//if (z.film_ID == filmId)
									//{
									foreach (var tijd in z.tijden)
									{


										tijdenMetFilm++;


									}
									//}
									tijdChecker = true;
									break;
								}
								if (tijdChecker)
								{
									break;
								}
							}
							if (tijdChecker)
							{
								break;
							}
							if (tijdChecker)
							{
								break;
							}
						}

						if (tijdChecker)
						{
							break;
						}
					}
					if (tijdChecker)
					{
						break;
					}
				}
			}
			}
			string[] filmtijden = new string[tijdenMetFilm];

			tijdenMetFilm = 0;
			filmtijdenCounter = 0;
			tijdChecker = false;
			if (loopBackChecker == false)
			{
				foreach (var locatie in locaties)
				{
					if (locatie.id == int.Parse(bioscoopKeuze))
					{
						foreach (var zaal in locatie.dagen)
						{
							foreach (var z in zaal)
							{
								if (z.datum == datumKeuze)
								{
									if (searchZaal == z.naam)
									{
										//if (z.film_ID == filmId)
										//{
										foreach (var tijd in z.tijden)
										{


											filmtijden[filmtijdenCounter] = tijd.tijd;
											filmtijdenCounter++;


										}
										//}
										tijdChecker = true;
										break;
									}
									if (tijdChecker)
									{
										break;
									}

								}
								if (tijdChecker)
								{
									break;
								}
							}
							if (tijdChecker)
							{
								break;
							}
						}
						if (tijdChecker)
						{
							break;
						}
					}
				}
			}
			tijdenMetFilm = 0;
			filmtijdenCounter = 0;
			if (loopBackChecker == false)
			{


				while (true)
				{
					Console.Clear();
					Console.WriteLine("Kies de tijd waarin u de film wilt bekijken.\n");
					Console.WriteLine("U kunt '/' typen om al uw keuzes opnieuw te maken.");
					Console.WriteLine("U kunt '*' typen om terug te gaan.\n");
					foreach (var item in filmtijden)
					{
						Console.WriteLine("[" + filmtijdenCounter + "] : " + item + "\n");
						filmtijdenCounter++;
					}
					tijdKeuze = Console.ReadLine();
                    if (tijdKeuze.Trim() == "/")
                    {
						loopBackChecker = true;
						break;
                    }
                    else if (tijdKeuze.Trim() == "*")
                    {
						return;
                    }
					else if (!(tijdKeuze.All(char.IsDigit)) || String.IsNullOrEmpty(tijdKeuze) || Int32.Parse(tijdKeuze) >= filmtijden.Length || Int32.Parse(tijdKeuze) < 0)
					{
						filmtijdenCounter = 0;
						Console.WriteLine("Gebruik alstublieft de bovenstaande keuzes.");
						Thread.Sleep(1000);
					}
					else
					{
						for (int counter = 0; counter < filmtijden.Length; counter++)
						{
							if (counter == Int32.Parse(tijdKeuze))
							{
								tijdKeuze = filmtijden[counter];
								break;
							}
						}
						break;
					}
				}
			}
			if (loopBackChecker == false)
			{
				foreach (var locatie in locaties)
				{
					foreach (var zaal in locatie.dagen)
					{
						foreach (var z in zaal)
						{
							if (z.datum == datumKeuze)
							{
								if (z.film_ID == filmId)
								{
									foreach (var tijd in z.tijden)
									{


										bioscoopNaam = locatie.name;
										break;


									}
								}
							}
						}


					}

				}
			}
			if (loopBackChecker == false)
			{
				bool aantalGeselecteerd = false;
				int aantalStoelenOver = aantalVrijeStoelen(Int32.Parse(bioscoopKeuze), searchZaal, tijdKeuze, datumKeuze);
				int aantalStoelenKeuzeInt = 0;
				Console.Clear();
				while (aantalGeselecteerd == false)
                {
					Console.WriteLine("Selecteer de aantal stoelen die u wilt reserveren:");
					Console.WriteLine("Het aantal moet hoger dan 0 zijn en lager of gelijk aan "+ aantalStoelenOver);
					Console.WriteLine("u kunt '/' typen om al uw keuzes opnieuw te maken");
					Console.WriteLine("u kunt '*' typen om terug te gaan");
					if (aantalStoelenOver == 0)
                    {
						Console.WriteLine("Zaal is vol.");
						Console.WriteLine("Typ iets om al uw keuzes opnieuw te maken.");
						loopBackChecker = true;
						break;
                    }
                    else
                    {
						string aantalStoelenKeuze = Console.ReadLine();
						bool aantalStoelenIsNumber = int.TryParse(aantalStoelenKeuze, out aantalStoelenKeuzeInt);
                        if (aantalStoelenKeuze.Trim() == "/")
                        {
							loopBackChecker = true;
							break;
                        }
                        else if (aantalStoelenKeuze.Trim() == "*")
                        {
							return;
                        }
                        else if (!aantalStoelenIsNumber)
                        {
							Console.WriteLine("Typ een nummer alstublieft.");
                        }
                        else if (aantalStoelenKeuzeInt >0 && aantalStoelenKeuzeInt<= aantalStoelenOver)
                        {
							Console.WriteLine("Er zijn momenteel niet zoveel stoelen vrij.");
							aantalGeselecteerd = true;

						}
						


					}
					Console.WriteLine();

                }
				string[] stoelen = new string[aantalStoelenKeuzeInt];
				int stoelCounter = 0;
				if (aantalGeselecteerd)
                {
					Console.Clear();
					
					
					
					showBeschikbaar(Int32.Parse(bioscoopKeuze), searchZaal, tijdKeuze, datumKeuze);
                    foreach (var stoel in stoelen)
                    {
						stoelKeuze = stoelKiezen(Int32.Parse(bioscoopKeuze), searchZaal, tijdKeuze, datumKeuze);
						if (stoelKeuze.Trim() == "/")
						{

                            for (int i = 0; i < stoelCounter; i++)
                            {
								stoelVrijMaken(Int32.Parse(bioscoopKeuze), searchZaal, tijdKeuze, stoelen[i], datumKeuze);
							}
							

							loopBackChecker = true;
							break;
                        }
                        else if (stoelKeuze.Trim() == "*")
                        {
							for (int i = 0; i < stoelCounter; i++)
							{
								stoelVrijMaken(Int32.Parse(bioscoopKeuze), searchZaal, tijdKeuze, stoelen[i], datumKeuze);
							}
							return;
                        }
						stoelen[stoelCounter] = stoelKeuze;
						stoelCounter++;
					}
					
					
				}
				if (loopBackChecker == false)
				{
					switch (zaalKeuze)
					{
						case "zaal 1 2D : 12.5":
							zaalKeuze = "zaal 1";
							technologie = "2D";
							filmPrijs = 12.5;
							break;
						case "zaal 2 3D : 15":
							zaalKeuze = "zaal 2";
							technologie = "3D";
							filmPrijs = 15.0;
							break;
						case "zaal 3 IMAX : 17.5":
							zaalKeuze = "zaal 3";
							technologie = "IMAX";
							filmPrijs = 17.5;
							break;
						case "zaal 1 2D : 12,5":
							zaalKeuze = "zaal 1";
							technologie = "2D";
							filmPrijs = 12.5;
							break;
						case "zaal 3 IMAX : 17,5":
							zaalKeuze = "zaal 3";
							technologie = "IMAX";
							filmPrijs = 17.5;
							break;

					}
					if (bevestiging(filmId, fullName, filmPrijs, bioscoopKeuze, zaalKeuze, technologie, datumKeuze, tijdKeuze, stoelen, locaties))
					{
						Console.Clear();
						Console.WriteLine("*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*");
						Console.WriteLine("|                                             Betaal opties                                         |");
						Console.WriteLine("*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*" + " \n");
						Console.WriteLine("         [1] iDEAL\n");
						Console.WriteLine("         [2] PayPal\n");
						Console.WriteLine("         [3] CreditCard\n");
						Console.WriteLine("         [*] Annuleren\n");

						string betaalKeuze = Console.ReadLine();

						while (betaalKeuze.Trim() != "1" && betaalKeuze.Trim() != "2" && betaalKeuze.Trim() != "3" && betaalKeuze.Trim() != "*")
						{
							Console.WriteLine("Kies a.u.b. een van de bovenstaande opties.\n");
							betaalKeuze = Console.ReadLine();
						}

						if (betaalKeuze.Trim() == "*")
						{
							for (int i = 0; i < aantalStoelenKeuzeInt; i++)
							{
								stoelVrijMaken(Int32.Parse(bioscoopKeuze), searchZaal, tijdKeuze, stoelen[i], datumKeuze);
							}
							Console.WriteLine("reservatie is geannuleerd!");
							Thread.Sleep(2000);
							Console.Clear();
							return;
						}

						switch (zaalKeuze)
						{
							case "zaal 1":
								zaalKeuze = "1";
								break;
							case "zaal 2":
								zaalKeuze = "2";
								break;
							case "zaal 3":
								zaalKeuze = "3";
								break;
						}

                        foreach (var eenStoel in stoelen)
                        {
							foreach (var item in accounts)
							{
								if (item.id == accountID)
								{
									if (item.tickets == null)
									{
										item.tickets = new Ticket[]
										{
										new Ticket() { id = 0, filmID = filmId.ToString(),name = fullName, prijs = filmPrijs, bioscoopID = Int32.Parse(bioscoopKeuze), zaalID = Int32.Parse(zaalKeuze),filmTechnologie = technologie, dag = datumKeuze, stoel = Int32.Parse(eenStoel),tijd = tijdKeuze}
										};

									}
									else
									{
										var tickets = new Ticket[item.tickets.Length + 1];
										int ticketcounter = 0;
										foreach (var item2 in item.tickets)
										{
											tickets[ticketcounter] = item2;
											ticketcounter++;
										}
										tickets[tickets.Length - 1] = new Ticket() { id = item.tickets.Length, filmID = filmId.ToString(), name = fullName, prijs = filmPrijs, bioscoopID = Int32.Parse(bioscoopKeuze), zaalID = Int32.Parse(zaalKeuze), filmTechnologie = technologie, dag = datumKeuze, tijd = tijdKeuze, stoel = Int32.Parse(eenStoel) };
										item.tickets = tickets;
									}
								}
							}
						}
						
						string convertedJson = JsonConvert.SerializeObject(accounts, Formatting.Indented);
						//verander de hele file met de nieuwe json informatie
						File.WriteAllText("..\\..\\..\\account.json", convertedJson);
						loopBack = false;
					}
					else
					{
						switch (zaalKeuze)
						{
							case "zaal 1":
								zaalKeuze = "zaal 1";
								break;
							case "zaal 2":
								zaalKeuze = "zaal 2";
								break;
							case "zaal 3":
								zaalKeuze = "zaal 3";
								break;
						}
						for (int i = 0; i < aantalStoelenKeuzeInt; i++)
						{
							stoelVrijMaken(Int32.Parse(bioscoopKeuze), searchZaal, tijdKeuze, stoelen[i], datumKeuze);
						}
						loopBack = false;
					}
				}
			}
		}
	}

	private bool bevestiging(int filmId, string fullName, double filmPrijs, string bioscoopKeuze, string zaalKeuze, string technologie, string dagKeuze, string tijdKeuze, string[] stoelKeuze, List<Cinema_adress> bioscopenLijst)
	{
		string bevestiging = "";
		string filmNaam = "";
		string bioscoopNaam = "";
		string strResultJson = File.ReadAllText("..\\..\\..\\movies.json");

		// maak een lijst van alle informatie die er is
		List<movie> films = JsonConvert.DeserializeObject<List<movie>>(strResultJson);

		foreach (var item in films)
		{
			if (item.id == filmId)
			{
				filmNaam = item.name;
			}
		}
		foreach (var item in bioscopenLijst)
		{
			if (item.id == Int32.Parse(bioscoopKeuze))
			{
				bioscoopNaam = item.name;
			}
		}
		switch (zaalKeuze)
		{
			case "zaal 1":
				zaalKeuze = "1";
				break;
			case "zaal 2":
				zaalKeuze = "2";
				break;
			case "zaal 3":
				zaalKeuze = "3";
				break;
		}
		switch (tijdKeuze)
		{
			case "9-12":
				tijdKeuze = "9:00-12:00";
				break;
			case "12-15":
				tijdKeuze = "12:00-15:00";
				break;
			case "15-18":
				tijdKeuze = "15:00-18:00";
				break;
			case "18-21":
				tijdKeuze = "18:00-21:00";
				break;
			case "21-24":
				tijdKeuze = "21:00-24:00";
				break;
		}
		while (true)
		{
			Console.Clear();
			Console.WriteLine("Film ID : " + filmId);
			Console.WriteLine("Film naam : " + filmNaam);
			Console.WriteLine("Volledige naam : " + fullName);
			Console.WriteLine("Prijs : $" + filmPrijs* stoelKeuze.Length);
			Console.WriteLine("Bioscoop naam : " + bioscoopNaam);
			Console.WriteLine("Zaal : " + zaalKeuze);
			Console.WriteLine("Zaal technologie : " + technologie);
			Console.WriteLine("Datum : " + dagKeuze);
			Console.WriteLine("Tijd : " + tijdKeuze);
			Console.WriteLine("Stoel : " + string.Join(",", stoelKeuze) + "\n\n") ;

			Console.WriteLine("Wilt u een bestelling plaatsen met deze informatie?");
			Console.WriteLine("Beantwoordt de vraag met 'ja' of 'nee'.\n");
			bevestiging = Console.ReadLine();
			if (bevestiging == "ja")
			{
				return true;
			}
			else if (bevestiging == "nee")
			{
				return false;
			}
			else
			{
				Console.WriteLine("Gebruik alstublieft de bovenstaande keuzes.");
				Thread.Sleep(2000);
			}
		}
	}
}