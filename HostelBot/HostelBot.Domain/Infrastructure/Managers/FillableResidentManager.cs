﻿using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure.Repository;

namespace HostelBot.Domain.Infrastructure.Managers;

public class FillableResidentManager : Manager<ResidentFillable>
{
    private readonly HostelRepository hostelRepository;
    private readonly RepositoryChangesParser repositoryChangesParser;

    public FillableResidentManager(ResidentRepository residentRepository, HostelRepository hostelRepository,
        RepositoryChangesParser repositoryChangesParser) 
        : base(residentRepository)
    {
        this.hostelRepository = hostelRepository;
        this.repositoryChangesParser = repositoryChangesParser;
    }
    
    protected override void Handle(ResidentFillable value)
    {
        var room = hostelRepository.FindOrCreateRoom($"№{value.HostelNumber}", value.RoomNumber).Result;
        var hostel = hostelRepository.GetByName($"№{value.HostelNumber}").Result;
        var resident = new Resident(value.ResidentId, value.Name, value.Surname, hostel, room, repositoryChangesParser);
        residentRepository.CreateAsync(resident);
    }
}