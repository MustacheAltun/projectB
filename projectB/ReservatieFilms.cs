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
		string chairResult = "Rij 1 |		";
		int huidigeRij = 1;
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
            if (row == 10)
            {
				if (counter % 10 == 0 && huidigeRij < 10)
				{
					huidigeRij++;
                    if (huidigeRij <10)
                    {
						chairResult += "\nRij " + huidigeRij + " |		";
					}
                    else
                    {
						chairResult += "\nRij " + huidigeRij + "|		";
					}
					
				}
            }
            else if (row == 15)
            {
				if (counter % 10 == 0 && huidigeRij < 15)
				{
					huidigeRij++;
					if (huidigeRij < 10)
					{
						chairResult += "\nRij " + huidigeRij + " |		";
					}
					else
					{
						chairResult += "\nRij " + huidigeRij + "|		";
					}
				}
            }
            else
            {
				if (counter % 10 == 0 && huidigeRij < 20)
				{
					huidigeRij++;
					if (huidigeRij < 10)
					{
						chairResult += "\nRij " + huidigeRij + " |		";
					}
					else
					{
						chairResult += "\nRij " + huidigeRij + "|		";
					}
				}
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

	private void Logo()
    {

		Console.WriteLine("\n*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*");
		Console.WriteLine("|                                              Reservering                                              |");
		Console.WriteLine("*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*" + " \n");
	}
	public void reserveren(string filmIdString, int accountID)
	{

		Console.Clear();
		Logo();
		bool loopBack = true;
		

		// lees de file en zet alles in een string
		string strResultJson = File.ReadAllText("..\\..\\..\\account.json");
		string strResultJson2 = File.ReadAllText("..\\..\\..\\locatie.json");

		// maak een lijst van alle informatie die er is
		List<Account> accounts = JsonConvert.DeserializeObject<List<Account>>(strResultJson);
		List<Cinema_adress> locaties = JsonConvert.DeserializeObject<List<Cinema_adress>>(strResultJson2);

		Console.WriteLine("Wat is jouw volledige naam?");
		string fullName = Console.ReadLine();

        while (string.IsNullOrEmpty(fullName) || Locatie.hasSpecialChar(fullName) || fullName.Any(char.IsDigit) || fullName.Length < 5)
        {
			Console.WriteLine("Dit is niet uw volledige naam, voer uw volledige naam in aub.");
			fullName = Console.ReadLine();
		}
		// als loopback true blijft tot de einde dan doet hij alles opnieuw
		// oftewel of iemand "/" typt tijdens een input
		while (loopBack)
		{
			//alle variabelen maken/resetten
			Console.Clear();
			double filmPrijs = 0;
			int bioscopenMetFilm = 0;
			int tijdenMetFilm = 0;
			int zalenMetFilm = 0;
			int dagenCounter = 0;
			int filmId = Int32.Parse(filmIdString);
			bool loopBackChecker = false;

			//In deze nested for loop wordt er gekeken of er film in de bioscoop staat met filmID variabel. 
			//Dus de filmID die je in de parameters van de functie geeft.
			//elke keer dat hij een bioscoop tegen komt met de film, dan dooet hij +1 bij bioscopenMetFilm variabel.
			//bioscopenMetFilm wordt later gebruikt om een array te maken.
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

			//maak de arrays voor bioscopen
			string[] bioscoopNamen = new string[bioscopenMetFilm];
			int[] bioscoopNamenID = new int[bioscopenMetFilm];


			int bioscoopNamenCounter = 0;
			int filmtijdenCounter = 0;
			//in deze nested foreach loop wordt de bioscoopNamen gevuld met de naam en adres met dezelfde loops als hierboven.
			//ook de bioscoopNamenID worden gevuld met de ID's van de bioscoop in dezelfde index als de namen
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
				//hier wordt de bioscoop gekozen en als de input niet goed is dan loopt hij opnieuw.
				while (true)
				{
					Console.Clear();
					Logo();
					Console.WriteLine("Kies de bioscoop waarin u de film wilt bekijken.\n");
					Console.WriteLine("U kunt '/' typen om al uw keuzes opnieuw te maken.");
					Console.WriteLine("U kunt '*' typen om terug te gaan.\n");
					Console.WriteLine("U kunt hier de beschikbare bioscopen zien:");
					foreach (var item in bioscoopNamen)
					{

						Console.WriteLine("	[" + bioscoopNamenCounter + "] : " + item + "\n");
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
			//Er wordt hier gekeken welke op welke dagen de film wordt afgespeeld, de aantal wordt opgeslagen in een variabel
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
			//met de aantal die is opgeslagen wordt een array aangemaakt
			string[] datums = new string[dagenCounter];
			if (loopBackChecker == false)
            {
				
				dagenCounter = 0;
				//nu zetten wij de data van de dagen die zijn gevonden in de array
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
				//dit wordt telkens herhaald totdat er een goede keuze is gemaakt
				//hier wordt dus een datum gekozen
				while (extracheck)
				{
					Console.Clear();
					Logo();
					Console.WriteLine("Kies de datum waarin u de film wilt bekijken.\n");
					Console.WriteLine("U kunt '/' typen om al uw keuzes opnieuw te maken.");
					Console.WriteLine("U kunt '*' typen om terug te gaan.\n");
					Console.WriteLine("U kunt hier de beschikbare datums zien:");
					foreach (var datum in datums)
					{
						Console.WriteLine("	[" + dagenCounter + "] : " + datum);
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
			//de aantal zalen waar de film draait wordt opgeslagen in een variabel voor later. 
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
			//nu wordt er een variabel gemaakt met het opgeslagen aantal van zalen
			string[] zalenTechnoligien = new string[zalenMetFilm];
			zalenMetFilm = 0;
            if (loopBackChecker == false)
            {
				//hier worden de zalen met de type en prijs opgeslagen in de array zalenTechnoligien
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
											string tijdelijkePrijs= "";
                                            if (z.prijs == 12.5)
                                            {
												tijdelijkePrijs = "12,50 Euro";

											}
                                            else if (z.prijs == 15.0)
                                            {
												tijdelijkePrijs = "15,00 Euro";
											}
                                            else if (z.prijs == 17.5)
                                            {
												tijdelijkePrijs = "17,50 Euro";
											}
                                            zalenTechnoligien[zalenMetFilm] = z.naam + " " + z.type + " : " + tijdelijkePrijs;
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

				//dit wordt telkens gedaan totdat een goede keuze is gemaakt.
				//hier wordt een zaal gekozen
				while (true)
				{
					Console.Clear();
					Logo();
					Console.WriteLine("Kies de zaal waarin u de film wilt bekijken.\n");
					Console.WriteLine("U kunt '/' typen om al uw keuzes opnieuw te maken.");
					Console.WriteLine("U kunt '*' typen om terug te gaan.\n");
					Console.WriteLine("U kunt hier de beschikbare zalen zien:");
					foreach (var item in zalenTechnoligien)
					{
						Console.WriteLine("	[" + zalenMetFilm + "] : " + item + "\n");
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

				//als de zaal keuze een van deze keuzes zijn dan wordt searchZaal aangepast
				switch (zaalKeuze)
			{
				case "zaal 1 2D : 12,50 Euro":
					searchZaal = "zaal 1";
					break;
				case "zaal 2 3D : 15,00 Euro":
					searchZaal = "zaal 2";
					break;
				case "zaal 3 IMAX : 17,50 Euro":
					searchZaal = "zaal 3";
					break;
				}
			
			//hier wordt de aantal tijden opgeslagen zodat we later een array kunnen maken van de tijden
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
									foreach (var tijd in z.tijden)
									{


										tijdenMetFilm++;


									}
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
			//nu maken wij een array met de aantal tijden die zijn opgeslagen
			string[] filmtijden = new string[tijdenMetFilm];

			tijdenMetFilm = 0;
			filmtijdenCounter = 0;
			tijdChecker = false;
			if (loopBackChecker == false)
			{
				// hier wordt alle tijden date in de array toegevoegd
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
										foreach (var tijd in z.tijden)
										{


											filmtijden[filmtijdenCounter] = tijd.tijd;
											filmtijdenCounter++;


										}
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

				// hier wordt de tijd gekozen en dit gaat door totdat er een juist input is.
				while (true)
				{
					Console.Clear();
					Logo();
					Console.WriteLine("Kies de tijd waarin u de film wilt bekijken.\n");
					Console.WriteLine("U kunt '/' typen om al uw keuzes opnieuw te maken.");
					Console.WriteLine("U kunt '*' typen om terug te gaan.\n");
					Console.WriteLine("U kunt hier de beschikbare tijden zien:");
					foreach (var item in filmtijden)
					{
						string TijdelijkeTijd = "";
						switch (item)
						{
							case "9-12":
								TijdelijkeTijd = "9:00-12:00";
								break;
							case "12-15":
								TijdelijkeTijd = "12:00-15:00";
								break;
							case "15-18":
								TijdelijkeTijd = "15:00-18:00";
								break;
							case "18-21":
								TijdelijkeTijd = "18:00-21:00";
								break;
							case "21-24":
								TijdelijkeTijd = "21:00-00:00";
								break;
						}
						Console.WriteLine("	[" + filmtijdenCounter + "] : " + TijdelijkeTijd + "\n");
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
				//hier wordt de bioscoopnaam genoteerd van de tijd.
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
				Logo();
				//hier wordt moet er een keuze gemaakt worden over hoeveel stoelen er gereserveerd moet worden.
				//als er geen goed aantal is gegeven dan wordt het opnieuw gelooped
				while (aantalGeselecteerd == false)
                {
					Console.WriteLine("Selecteer de aantal stoelen die u wilt reserveren:");
					Console.WriteLine("Het aantal moet hoger dan 0 zijn en lager of gelijk aan "+ aantalStoelenOver);
					Console.WriteLine("\nU kunt '/' typen om al uw keuzes opnieuw te maken.");
					Console.WriteLine("U kunt '*' typen om terug te gaan.");
					if (aantalStoelenOver == 0)
                    {
						Console.WriteLine("\nZAAL IS VOL!");
						Console.WriteLine("Typ iets om al uw keuzes opnieuw te maken.");
						Console.ReadLine();
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
				//er wordt een string array gemaakt van de stoelen aantal.
				string[] stoelen = new string[aantalStoelenKeuzeInt];
				int stoelCounter = 0;
				if (aantalGeselecteerd)
                {
					Console.Clear();

					Logo();
					//de stoelen worden door deze functie geprint.
					showBeschikbaar(Int32.Parse(bioscoopKeuze), searchZaal, tijdKeuze, datumKeuze);
					//Nu worden de stoelen gevraagd voor de aantal stoelen die de klant wilt.
                    foreach (var stoel in stoelen)
                    {
						//in de stoelkiezen zijn er al checks vandaar dat er niet hier veel gechecked wordt.
						stoelKeuze = stoelKiezen(Int32.Parse(bioscoopKeuze), searchZaal, tijdKeuze, datumKeuze);
						//als de input '/' is dan maakt hij eerst elke stoel vrij
						//en daarna gaat hij naar de begin van de loop zodat hij al zijn keuzes opnieuw kan maken
						if (stoelKeuze.Trim() == "/")
						{

                            for (int i = 0; i < stoelCounter; i++)
                            {
								stoelVrijMaken(Int32.Parse(bioscoopKeuze), searchZaal, tijdKeuze, stoelen[i], datumKeuze);
							}
							

							loopBackChecker = true;
							break;
                        }
						//als de input '*' is dan maakt hij eerst elke stoel vrij
						//en daarna gaat hij uit de functie
						else if (stoelKeuze.Trim() == "*")
                        {
							for (int i = 0; i < stoelCounter; i++)
							{
								stoelVrijMaken(Int32.Parse(bioscoopKeuze), searchZaal, tijdKeuze, stoelen[i], datumKeuze);
							}
							return;
                        }
                        if (stoelCounter < stoelen.Length)
                        {
							Console.WriteLine("U heeft uw stoel gekozen.");
							Console.WriteLine("Kies nog een stoel alstublieft.");
                        }
                        else
                        {
							Console.WriteLine("U heeft al uw stoelen gekozen");
                        }
						
						stoelen[stoelCounter] = stoelKeuze;
						stoelCounter++;
					}
				}
				if (loopBackChecker == false)
				{
					//hier wordt de informatie opgesplitst in meerdere variabelen zodat er meerdere checks uitgvoerd kunnen worden.
					switch (zaalKeuze)
					{
						case "zaal 2 3D : 15,00 Euro":
							zaalKeuze = "zaal 2";
							technologie = "3D";
							filmPrijs = 15.0;
							break;
						case "zaal 1 2D : 12,50 Euro":
							zaalKeuze = "zaal 1";
							technologie = "2D";
							filmPrijs = 12.5;
							break;
						case "zaal 3 IMAX : 17,50 Euro":
							zaalKeuze = "zaal 3";
							technologie = "IMAX";
							filmPrijs = 17.5;
							break;

					}
					//als de bevestiging true is dan gaat hij door naar de betaal pagina
					if (bevestiging(filmId, fullName, filmPrijs, bioscoopKeuze, zaalKeuze, technologie, datumKeuze, tijdKeuze, stoelen, locaties))
					{
						//hier vind de betaal pagina plaats
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
						//voor elke stoel wordt er hier een ticket gemaakt in de acount ticket lijst
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
										Omzet.AddOrRemoveOmzet(filmPrijs);

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
										Omzet.AddOrRemoveOmzet(filmPrijs);
									}
								}
							}
						}
						string convertedJson = JsonConvert.SerializeObject(accounts, Formatting.Indented);
						//verander de hele file met de nieuwe json informatie
						File.WriteAllText("..\\..\\..\\account.json", convertedJson);
						Console.WriteLine("Bedankt voor uw reservering!");
						Thread.Sleep(2000);
						//en nu is de loop false dus hij is klaar met de functie
						loopBack = false;
					}
					else
					{
						//hier heeft de gebruiker nee gezegd bij bevestiging dus worden de stoelen weer vrij gemaakt en dan is de functie afgelopen.
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
						//en nu is de loop false dus hij is klaar met de functie
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
				tijdKeuze = "21:00-00:00";
				break;
		}
		while (true)
		{
			//hier worden alle gegevens weergeven van de keuzes die zijn gemaakt
			Console.Clear();
			Logo();
			Console.WriteLine("	Film ID : " + filmId);
			Console.WriteLine("	Film naam : " + filmNaam);
			Console.WriteLine("	Volledige naam : " + fullName);
			Console.WriteLine("	Prijs : " + String.Format("{0:0.00}", filmPrijs * stoelKeuze.Length) + " Euro");
			Console.WriteLine("	Bioscoop naam : " + bioscoopNaam);
			Console.WriteLine("	Zaal : " + zaalKeuze);
			Console.WriteLine("	Zaal technologie : " + technologie);
			Console.WriteLine("	Datum : " + dagKeuze);
			Console.WriteLine("	Tijd : " + tijdKeuze);
			Console.WriteLine("	Stoel : " + string.Join(",", stoelKeuze) + "\n\n") ;

			Console.WriteLine("Wilt u een bestelling plaatsen met deze informatie?");
			Console.WriteLine("Beantwoordt de vraag met 'ja' of 'nee'.\n");
			bevestiging = Console.ReadLine();
			//enige input die geaccepteerd wordt is ja of nee, anders loopt hij opnieuw.
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