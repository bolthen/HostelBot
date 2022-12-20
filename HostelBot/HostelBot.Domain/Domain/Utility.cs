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
    }

    public Utility(string name, string content, Resident resident)
    {
        Name = name;
        Content = content;
        Resident = resident;
    } 
    
    public string Name { get; set; }
    
    [Question("Опишите Вашу проблему", ViewType.TextEnter)]
    public string Content { get; set; }

    public Resident Resident { get; set; }
    
    public IReadOnlyCollection<PropertyInfo> GetFields() => Properties;
    public long ResidentId { get; set; }
    private readonly List<Infrastructure.IObserver<Utility>> observers = new();
    
    /*private bool filled;
    [NotMapped]
    public bool Filled
    {
        get => filled;
        set
        {
            filled = value;
            if (value)
                OnFilled();
        }
    }*/
    
    public IDisposable Subscribe(Infrastructure.IObserver<Utility> observer)
    {
        if (!observers.Contains(observer))
            observers.Add(observer);
        return new Unsubscriber<Utility>(observers, observer);
    }
    
    public void OnFilled()
    {
        foreach (var observer in observers.ToArray())
            observer.OnCompleted(this);
    }

    private void OnNext()
    {
        foreach (var observer in observers)
            observer.OnNext(this);
    }
}