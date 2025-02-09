﻿using System.ComponentModel;
using HostelBot.Domain.Infrastructure;

namespace HostelBot.Domain.Domain
{
    public class Resident : Entity<Resident>, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        
        public Resident(long telegramId, string name, string surname, Hostel hostel, Room room, 
            RepositoryChangesParser repositoryChangesParser)
        {
            Id = telegramId;
            Name = name;
            Surname = surname;
            Hostel = hostel;
            Room = room;
            Hostel = hostel;
            PropertyChanged += repositoryChangesParser.ParseRepositoryChanges;
        }

        public Resident(){}

        public string Name { get; set; }
        
        public string Surname { get; set; }

        public Room? Room { get; set; }
        
        public Hostel Hostel { get; set; }

        public List<Utility> Utilities { get; set; } = new ();

        public List<Appeal> Appeals { get; set; } = new();

        private bool isAccepted;
        public bool IsAccepted { 
            get => isAccepted;
            set
            {
                isAccepted = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsAccepted)));
            } 
        }

        public void AddUtility(Utility utility)
        {
            Utilities.Add(utility);
        }
        
        public void AddAppeal(Appeal appeal)
        {
            Appeals.Add(appeal);
        }

        public override string ToString()
        {
            return $"{Name} {Surname}";
        }
    }
}