﻿using SQLite;

namespace StampCollectorApp.Models;

public class Category
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; }
}