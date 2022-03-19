﻿using System;

public class Account
{
    public int id { get; set; }
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

public class movie
{
    public string id { get; set; }
    public string name { get; set; }
    public string year { get; set; }
    public string[] categories { get; set; }
    public string releasedate { get; set; }
    public string director { get; set; }
    public string storyline { get; set; }
}




