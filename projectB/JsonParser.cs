using System;


public class Rootobject
{
    public Account[] accountList { get; set; }
}

public class Account
{
    public string username { get; set; }
    public string password { get; set; }
    public string security { get; set; }
    public Ticket[]? tickets { get; set; }
}

public class Ticket
{
    public int? id { get; set; }
    public string? name { get; set; }
}




