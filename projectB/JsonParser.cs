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
    public int? id { get; set; }
    public string? name { get; set; }
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
    public Zalen[] zalen { get; set; }
}

public class Zalen
{
    public string naam { get; set; }
    public string type { get; set; }
    public int zitplekken { get; set; }
    public Tijden[] tijden { get; set; }
    
}

public class Tijden
{
    public string tijd { get; set; }
    public string film_ID  { get; set; }
    public System.Collections.Generic.Dictionary<string, bool> beschikbaar { get; set; }
    public System.Collections.Generic.Dictionary<string, bool> gebroken { get; set; }
}






