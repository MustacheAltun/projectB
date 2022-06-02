using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
public class Omzet
{
    public static void ShowOmzet()
    {
        string keuze = "";
        bool toonAlles = true;
        while (keuze != "1")
        {
            Console.Clear();
            int i = 1;
            string s = "";
            string strOmzet = File.ReadAllText("..\\..\\..\\omzet.json");
            List<WeeklyEarning> OmzetList = JsonConvert.DeserializeObject<List<WeeklyEarning>>(strOmzet);


            foreach(var Weeklies in OmzetList)
            {
                if (toonAlles)
                {
                    int j = 1;
                    s += "------------------ Week" + i + " ----------------------------------------------------\n";
                    s += "| " + (Weeklies.dailyEarnings[0].date) + " tot " + (Weeklies.dailyEarnings[6].date) + "\n";
                    s += "| Volledige omzet: " + Weeklies.amountEarned + "\n|\n";
                    foreach (var dailies in Weeklies.dailyEarnings)
                    {
                        s += "| Dag " + j + ": " + dailies.date + "\n";
                        s += "| Verdient: " + dailies.earned + " Euro" + "\n|\n";
                        j++;
                    }
                  
                }
                else
                {
                    int j = 1;
                    if (Weeklies.amountEarned > 0.0)
                    {
                        s += "------------------ Week" + i + " ----------------------------------------------------\n";
                        s += "| " + (Weeklies.dailyEarnings[0].date) + " tot " + (Weeklies.dailyEarnings[6].date);
                        s += "| Volledige omzet: " + Weeklies.amountEarned + "\n|\n";
                        foreach (var dailies in Weeklies.dailyEarnings)
                        {
                            s += "| Dag " + j + ": " + dailies.date + "\n";
                            s += "| Verdient: " + dailies.earned + " Euro" + "\n|\n";
                            j++;
                        }
  
                    }
                }
                i++;
            }
            Console.WriteLine(s);
            Console.WriteLine("----------------------------------------------------------------");
            Console.WriteLine("| [1] Terug | [2] Alle Weken Tonen | [3] Weken met omzet tonen |");
            Console.WriteLine("---------------------------------------------------------------- ");
            Console.WriteLine("Voer de nummer van uw actie in:");
            keuze = Console.ReadLine();
            while(keuze != "2" && keuze != "3" && keuze != "1")
            {
                Console.WriteLine("Onjuiste invoer!");
                Console.WriteLine("Voer de nummer van uw actie in:");
                keuze = Console.ReadLine();
            }
            if(keuze == "1")
            {
                return;
            }
            else if(keuze == "2")
            {
                toonAlles = true;
                keuze = "";
            }
            else if(keuze == "3")
            {
                toonAlles = false;
                keuze = "";
            }
        }
        return;
    }
    public static void UpdateOmzet()
    {
        string strOmzet = File.ReadAllText("..\\..\\..\\omzet.json");
        List<WeeklyEarning> OmzetList = JsonConvert.DeserializeObject<List<WeeklyEarning>>(strOmzet);
        DateTime today = DateTime.Now;

        if (OmzetList.Count == 0)
        {
            dailyEarnings[] dailyEarning = new dailyEarnings[7];
            for(int i = 0; i < 7; i++)
            {
                dailyEarnings D1 = new dailyEarnings()
                {
                    date = today.AddDays(i).ToString("dd-MM-yyyy"),
                    earned = 0.0
            };
                dailyEarning[i] = D1;
            }
            OmzetList.Add(new WeeklyEarning()
            {
                weekendDate = (today.AddDays(7)).ToString("dd-MM-yyyy"),
                amountEarned = 0,
                dailyEarnings = dailyEarning
            });
            string OmzetJson = JsonConvert.SerializeObject(OmzetList, Formatting.Indented);
            //verander de hele file met de nieuwe json informatie
            File.WriteAllText("..\\..\\..\\omzet.json", OmzetJson);
        }
        else
        {
            int i = 1;
            foreach (var WekenlijkseOmzet in OmzetList)
            {
                if (i == OmzetList.Count)
                {
                    DateTime WekenlijkseConvert = Convert.ToDateTime(WekenlijkseOmzet.weekendDate);
                    today = today;
                    TimeSpan ts = today - WekenlijkseConvert;
                    int differenceInDays = ts.Days;

                    if (differenceInDays > 0)
                    {
                        dailyEarnings[] dailyEarning = new dailyEarnings[7];
                        for (int j = 0; j < 7; j++)
                        {
                            dailyEarnings D1 = new dailyEarnings()
                            {
                                date = WekenlijkseConvert.AddDays(j).ToString("dd-MM-yyyy"),
                                earned = 0.0
                            };
                            dailyEarning[j] = D1;
                        }

                        OmzetList.Add(new WeeklyEarning()
                        {
                            weekendDate = (WekenlijkseConvert.AddDays(7)).ToString("dd-MM-yyyy"),
                            amountEarned = 0,
                            dailyEarnings = dailyEarning
                        });
                        string OmzetJson = JsonConvert.SerializeObject(OmzetList, Formatting.Indented);
                        //verander de hele file met de nieuwe json informatie
                        File.WriteAllText("..\\..\\..\\omzet.json", OmzetJson);
                        break;
                    }
                }
                i++;
            }
        }
    }
    public static void AddOmzet(double add)
    {
        string strOmzet = File.ReadAllText("..\\..\\..\\omzet.json");
        List<WeeklyEarning> OmzetList = JsonConvert.DeserializeObject<List<WeeklyEarning>>(strOmzet);
        DateTime today = DateTime.Now;
        string dateT = today.ToString("dd-MM-yyyy");
        foreach (var Weeklies in OmzetList)
        {
            foreach (var dailies in Weeklies.dailyEarnings)
            {
                if(dailies.date == dateT)
                {
                    dailies.earned += add;
                    Weeklies.amountEarned += add;
                    string OmzetJson = JsonConvert.SerializeObject(OmzetList, Formatting.Indented);
                    //verander de hele file met de nieuwe json informatie
                    File.WriteAllText("..\\..\\..\\omzet.json", OmzetJson);
                    break;
                }
            }
        }
    }
}