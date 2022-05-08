using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

public class ReservatieFilms
{
	private void showBeschikbaar(string bioscoopNaam, string zaalNaam, string tijdNaam)
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

	private string stoelKiezen(string bioscoopNaam, string zaalNaam, string tijdNaam)
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

	private void stoelVrijMaken(int bioscoopID, string zaalNaam, string tijdNaam,string stoel)
	{
		//pak alle json informatie
		string url = "..\\..\\..\\locatie.json";
		string locatieJson = File.ReadAllText(url);
		List<Cinema_adress> locatieList = JsonConvert.DeserializeObject<List<Cinema_adress>>(locatieJson);

		//verander deze data met genomen in de json file
		locatieList.Single(locatie => locatie.id == bioscoopID)
			.zalen.Single(zaal => zaal.naam == zaalNaam)
			.tijden.Single(tijd => tijd.tijd == tijdNaam)
			.beschikbaar[stoel] = true;

		string changedLocatie = JsonConvert.SerializeObject(locatieList, Formatting.Indented);
		//verander de hele file met de nieuwe json informatie
		File.WriteAllText(url, changedLocatie);
		
	}

	public void reserveren(string filmId, int accountID)
	{
		Console.Clear();
		double filmPrijs = 14.99;
		int bioscopenMetFilm = 0;
		int tijdenMetFilm = 0;
		int zalenMetFilm = 0;

		// lees de file en zet alles in een string
		string strResultJson = File.ReadAllText("..\\..\\..\\account.json");
		string strResultJson2 = File.ReadAllText("..\\..\\..\\locatie.json");

		// maak een lijst van alle informatie die er is
		List<Account> accounts = JsonConvert.DeserializeObject<List<Account>>(strResultJson);
		List<Cinema_adress> locaties = JsonConvert.DeserializeObject<List<Cinema_adress>>(strResultJson2);

		Console.WriteLine("wat is jouw volledige naam?");
		string fullName = Console.ReadLine();

		foreach (var locatie in locaties)
		{
			bool check = false;
			foreach (var zaal in locatie.zalen)
			{

				foreach (var tijd in zaal.tijden)
				{

					if (tijd.film_ID == filmId)
					{
						bioscopenMetFilm++;
						check = true;
						break;
					}

				}
                if (check)
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
			foreach (var zaal in locatie.zalen)
			{

				foreach (var tijd in zaal.tijden)
				{

					if (tijd.film_ID == filmId)
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

		}

		bioscoopNamenCounter = 0;
		filmtijdenCounter = 0;
		string bioscoopKeuze;
		string bioscoopNaam = "";
		string tijdKeuze;
		string zaalKeuze;
		string technologie = "";
		string dagKeuze;
		string stoelKeuze;
		while (true)
		{
			Console.WriteLine("Kies de bioscoop waarin u de film wilt bekijken.\n");
			foreach (var item in bioscoopNamen)
			{

				Console.WriteLine("[" + bioscoopNamenCounter + "] : " + item + "\n");
				bioscoopNamenCounter++;
			}
			bioscoopKeuze = Console.ReadLine();
			if (!(bioscoopKeuze.All(char.IsDigit)) || String.IsNullOrEmpty(bioscoopKeuze) || Int32.Parse(bioscoopKeuze) >= bioscoopNamen.Length || Int32.Parse(bioscoopKeuze) < 0)
			{
				bioscoopNamenCounter = 0;
				Console.WriteLine("Gebruik alstublieft de bovenstaande keuzes.\n");
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



		foreach (var locatie in locaties)
		{
			if (locatie.id == int.Parse(bioscoopKeuze))
			{
				foreach (var zaal in locatie.zalen)
				{

					foreach (var tijd in zaal.tijden)
					{
						if (filmId == tijd.film_ID)
						{
							zalenMetFilm++;
							break;
						}
					}
				}
			}

		}
		string[] zalenTechnoligien = new string[zalenMetFilm];
		zalenMetFilm = 0;
		foreach (var locatie in locaties)
		{
			if (locatie.id == int.Parse(bioscoopKeuze))
			{
				foreach (var zaal in locatie.zalen)
				{

					foreach (var tijd in zaal.tijden)
					{
						if (filmId == tijd.film_ID)
						{
							zalenTechnoligien[zalenMetFilm] = zaal.type;
							zalenMetFilm++;
							break;
						}
					}
				}
			}

		}
		zalenMetFilm = 0;
		while (true)
		{
			Console.WriteLine("Kies de zaal waarin u de film wilt bekijken.\n");
			foreach (var item in zalenTechnoligien)
			{
				Console.WriteLine("[" + zalenMetFilm + "] : zaal " + (zalenMetFilm + 1) + " " + item + "\n");
				zalenMetFilm++;
			}
			zaalKeuze = Console.ReadLine();
			if (!(zaalKeuze.All(char.IsDigit)) || String.IsNullOrEmpty(zaalKeuze) || Int32.Parse(zaalKeuze) >= zalenTechnoligien.Length || Int32.Parse(zaalKeuze) < 0)
			{
				Console.WriteLine("Gebruik alstublieft de bovenstaande keuzes.\n");
				zalenMetFilm = 0;
			}
			else
			{
				zaalKeuze = zalenTechnoligien[Int32.Parse(zaalKeuze)];
				switch (zaalKeuze)
				{
					case "2D":
						zaalKeuze = "zaal1";
						technologie = "2D";
						break;
					case "3D":
						zaalKeuze = "zaal2";
						technologie = "3D";
						break;
					case "IMAX":
						zaalKeuze = "zaal3";
						technologie = "IMAX";
						break;

				}
				break;

			}
		}
		tijdenMetFilm = 0;
		foreach (var locatie in locaties)
		{
			if (locatie.id == int.Parse(bioscoopKeuze))
			{
				foreach (var zaal in locatie.zalen)
				{
					if (zaalKeuze == zaal.naam)
					{
						foreach (var tijd in zaal.tijden)
						{

							if (tijd.film_ID == filmId)
							{
								tijdenMetFilm++;
							}

						}
						break;
					}
				}
			}
		}
		string[] filmtijden = new string[tijdenMetFilm];
		tijdenMetFilm = 0;
		filmtijdenCounter = 0;
		foreach (var locatie in locaties)
		{
			if (locatie.id == int.Parse(bioscoopKeuze))
			{
				foreach (var zaal in locatie.zalen)
				{
					if (zaalKeuze == zaal.naam)
					{
						foreach (var tijd in zaal.tijden)
						{

							if (tijd.film_ID == filmId)
							{
								filmtijden[filmtijdenCounter] = tijd.tijd;
								filmtijdenCounter++;
							}

						}
						break;
					}
				}
			}
		}
		tijdenMetFilm = 0;
		filmtijdenCounter = 0;
		while (true)
		{
			Console.WriteLine("Kies de tijd waarin u de film wilt bekijken.");
			foreach (var item in filmtijden)
			{
				Console.WriteLine("[" + filmtijdenCounter + "] : " + item + "\n");
				filmtijdenCounter++;
			}
			tijdKeuze = Console.ReadLine();
			if (!(tijdKeuze.All(char.IsDigit)) || String.IsNullOrEmpty(tijdKeuze) || Int32.Parse(tijdKeuze) >= bioscoopNamen.Length || Int32.Parse(tijdKeuze) < 0)
			{
				filmtijdenCounter = 0;
				Console.WriteLine("Gebruik alstublieft de bovenstaande keuzes.");
			}
			else
			{
				for (int counter = 0; counter < filmtijden.Length; counter++)
				{
					if (counter == Int32.Parse(tijdKeuze))
					{
						tijdKeuze = filmtijden[counter];
					}
				}
				break;
			}
		}

		while (true)
		{
			Console.WriteLine("Kies de dag waarop u de film wilt bekijken.");
			Console.WriteLine("[1] : maandag\n");
			Console.WriteLine("[2] : dinsdag\n");
			Console.WriteLine("[3] : woensdag\n");
			Console.WriteLine("[4] : donderdag\n");
			Console.WriteLine("[5] : vrijdag\n");
			Console.WriteLine("[6] : zaterdag\n");
			Console.WriteLine("[7] : zondag\n");
			dagKeuze = Console.ReadLine();
			if (dagKeuze == "1" || dagKeuze == "2" || dagKeuze == "3" || dagKeuze == "4" || dagKeuze == "5" || dagKeuze == "6" || dagKeuze == "7")
			{
				switch (dagKeuze)
				{
					case "1":
						dagKeuze = "maandag";
						break;
					case "2":
						dagKeuze = "dinsdag";
						break;
					case "3":
						dagKeuze = "woensdag";
						break;
					case "4":
						dagKeuze = "donderdag";
						break;
					case "5":
						dagKeuze = "vrijdag";
						break;
					case "6":
						dagKeuze = "zaterdag";
						break;
					case "7":
						dagKeuze = "zondag";
						break;

				}
				break;
			}
			else
			{
				Console.WriteLine("Gebruik alstublieft de bovenstaande keuzes.");
			}
		}

		foreach (var locatie in locaties)
		{
			foreach (var zaal in locatie.zalen)
			{

				foreach (var tijd in zaal.tijden)
				{

					if (tijd.film_ID == filmId)
					{
						bioscoopNaam = locatie.name;
						break;
					}

				}

			}

		}

		showBeschikbaar(bioscoopNaam, zaalKeuze, tijdKeuze);
		stoelKeuze = stoelKiezen(bioscoopNaam, zaalKeuze, tijdKeuze);
		if (bevestiging(filmId, fullName, filmPrijs, bioscoopKeuze, zaalKeuze, technologie, dagKeuze, tijdKeuze, stoelKeuze,locaties))
		{

		
		switch (zaalKeuze)
		{
			case "zaal1":
				zaalKeuze = "1";
				break;
			case "zaal2":
				zaalKeuze = "2";
				break;
			case "zaal3":
				zaalKeuze = "3";
				break;
		}

		foreach (var item in accounts)
		{
			if (item.id == accountID)
			{
				if (item.tickets == null)
				{
					item.tickets = new Ticket[]
					{
						new Ticket() { id = 0, filmID = filmId,name = fullName, prijs = filmPrijs, bioscoopID = Int32.Parse(bioscoopKeuze), zaalID = Int32.Parse(zaalKeuze),filmTechnologie = technologie, dag = dagKeuze, stoel = Int32.Parse(stoelKeuze),tijd = tijdKeuze}
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
					tickets[tickets.Length - 1] = new Ticket() { id = item.tickets.Length, filmID = filmId, name = fullName, prijs = filmPrijs, bioscoopID = Int32.Parse(bioscoopKeuze), zaalID = Int32.Parse(zaalKeuze), filmTechnologie = technologie, dag = dagKeuze, tijd = tijdKeuze, stoel = Int32.Parse(stoelKeuze) };
					item.tickets = tickets;
				}
			}
		}
		string convertedJson = JsonConvert.SerializeObject(accounts, Formatting.Indented);
		//verander de hele file met de nieuwe json informatie
		File.WriteAllText("..\\..\\..\\account.json", convertedJson);
        }
        else
        {
			stoelVrijMaken(Int32.Parse(bioscoopKeuze), zaalKeuze,tijdKeuze,stoelKeuze);

		}
	}

    private bool bevestiging(string filmId, string fullName, double filmPrijs, string bioscoopKeuze, string zaalKeuze, string technologie, string dagKeuze, string tijdKeuze, string stoelKeuze, List<Cinema_adress> bioscopenLijst)
    {
		string bevestiging = "";
		string filmNaam = "";
		string bioscoopNaam = "";
		string strResultJson = File.ReadAllText("..\\..\\..\\movies.json");

		// maak een lijst van alle informatie die er is
		List<movie> films = JsonConvert.DeserializeObject<List<movie>>(strResultJson);
		
        foreach (var item in films)
        {
            if (item.id == Int32.Parse(filmId))
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
			case "zaal1":
				zaalKeuze = "1";
				break;
			case "zaal2":
				zaalKeuze = "2";
				break;
			case "zaal3":
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
			Console.WriteLine("Film ID : "+filmId);
			Console.WriteLine("Film naam : " + filmNaam);
			Console.WriteLine("Volledige naam : " + fullName);
			Console.WriteLine("Prijs : " + filmPrijs);
			Console.WriteLine("Bioscoop naam : " + bioscoopNaam );
			Console.WriteLine("Zaal : " + zaalKeuze );
			Console.WriteLine("Zaal technologie : " + technologie );
			Console.WriteLine("Dag : " + dagKeuze );
			Console.WriteLine("Tijd : " + tijdKeuze );
			Console.WriteLine("Stoel : " + stoelKeuze + "\n\n");

			Console.WriteLine("Wilt u een bestelling plaatsen met deze informatie?");
			Console.WriteLine("Beantwoordt de vraag met 'ja' of 'nee'.\n");
			bevestiging = Console.ReadLine();
            if (bevestiging == "ja")
            {
				return true;
            }else if (bevestiging == "nee")
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
