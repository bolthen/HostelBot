﻿using HostelBot.Domain;
using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure;
using HostelBot.Domain.Infrastructure.Services;

namespace HostelBot.App;

public class ChooseServiceCommand : Command
{
    private readonly IEnumerable<Manager<UtilityFillable>> managers;
    private readonly HostelRepository hostelNameRepository;
    private readonly ResidentRepository residentRepository;

    public ChooseServiceCommand(IEnumerable<Manager<UtilityFillable>> managers, HostelRepository hostelNameRepository, ResidentRepository residentRepository)
        : base("Услуги")
    {
        this.managers = managers;
        this.hostelNameRepository = hostelNameRepository;
        this.residentRepository = residentRepository;
    }
    
    public override List<Command> GetSubcommands(long residentId)
    {
        var resident = residentRepository.GetAsync(residentId).Result;
        
        var names = resident.Hostel.UtilityNames; // TODO

        return names.Select(name => new UtilityCommand(name.Name, managers)).Cast<Command>().ToList();
    }
}