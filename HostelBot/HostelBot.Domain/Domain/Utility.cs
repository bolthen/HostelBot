﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Text.Json.Serialization;
using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure;

namespace HostelBot.Domain;

public class Utility : Entity<Utility>
{
    public Utility()
    {
    }
    
    public Utility(string name, Resident resident)
    {
        Name = name;
        Resident = resident;
        CreationDateTime = DateTime.Now;
        HostelId = Resident.Hostel.Id;
    }

    public Utility(string name, string content, Resident resident)
    {
        Name = name;
        Content = content;
        Resident = resident;
        CreationDateTime = DateTime.Now;
        HostelId = Resident.Hostel.Id;
    } 
    
    public string Name { get; set; }
    
    [Question("Опишите Вашу проблему", ViewType.TextEnter)]
    public string Content { get; set; }

    public Resident Resident { get; set; }
    
    public DateTime CreationDateTime { get; set; }
    
    public long HostelId { get; set; }
    
    public IReadOnlyCollection<PropertyInfo> GetFields() => Properties;
    private readonly List<Infrastructure.IObserver<Utility>> observers = new();
}