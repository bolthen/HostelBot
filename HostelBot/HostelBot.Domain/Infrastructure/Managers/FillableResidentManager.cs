using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure.Repository;

namespace HostelBot.Domain.Infrastructure.Managers;

public class FillableResidentManager : Manager<ResidentFillable>
{
    private readonly ResidentRepository residentRepository;
    private readonly HostelRepository hostelRepository;

    public FillableResidentManager(ResidentRepository residentRepository, HostelRepository hostelRepository)
    {
        this.residentRepository = residentRepository;
        this.hostelRepository = hostelRepository;
    }
    
    protected override void Handle(ResidentFillable value)
    {
        var room = hostelRepository.FindOrCreateRoom($"№{value.HostelNumber}", value.RoomNumber).Result;
        var hostel = hostelRepository.GetByName($"№{value.HostelNumber}").Result;
        var resident = new Resident(value.ResidentId, value.Name, value.Surname, hostel, room);
        residentRepository.CreateAsync(resident);
    }
}