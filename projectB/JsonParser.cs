using System;
public class Account
{
    public int id { get; set; }
    public string username { get; set; }
    public string password { get; set; }
    public string security { get; set; }
    public string rol { get; set; }
#nullable enable
    public Ticket[]? tickets { get; set; }
#nullable disable
}

public class Ticket
{
#nullable enable
    public int id { get; set; }
    public string filmID { get; set; }
    public string name { get; set; }
    public double prijs { get; set; }
    public int bioscoopID { get; set; }
    public int zaalID { get; set; }
    public string filmTechnologie { get; set; }
    public string dag { get; set; }
    public string tijd { get; set; }
    public int stoel { get; set; }
#nullable disable
}

public class movie
{
    public int id { get; set; }
    public string name { get; set; }
    public string year { get; set; }
    public string[] categories { get; set; }
    public string releasedate { get; set; }
    public string director { get; set; }
    public string storyline { get; set; }
    public bool showing { get; set; }
}


public class Cinema_adress
{
    public int id { get; set; }
    public string name { get; set; }
    public string address { get; set; }
    public string street { get; set; }
    public string streetNr { get; set; }
    public string zipcode { get; set; }
    public string city { get; set; }
    public string telNr { get; set; }
    public Dag[] dagen { get; set; }
}
//public class Dagen
//{
//    public Dag[] dag1 { get; set; }
//    public Dag[] dag2 { get; set; }
//    public Dag[] dag3 { get; set; }
//    public Dag[] dag4 { get; set; }
//    public Dag[] dag5 { get; set; }
//    public Dag[] dag6 { get; set; }
//    public Dag[] dag7 { get; set; }
//    public Dag[] dag8 { get; set; }
//    public Dag[] dag9 { get; set; }
//    public Dag[] dag10 { get; set; }
//    public Dag[] dag11 { get; set; }
//    public Dag[] dag12 { get; set; }
//    public Dag[] dag13 { get; set; }
//    public Dag[] dag14 { get; set; }

//}

public class Dag
{
    public string naam { get; set; }
    public string type { get; set; }
    public int zitplekken { get; set; }
    public Tijden[] tijden { get; set; }
    public string Create_Date { get; set; }
    public string datum { get; set; }
    public object film_ID { get; set; }
    public string prijs { get; set; }

}

public class Tijden
{
    public string tijd { get; set; }
    public string beschikbaar { get; set; }

}






