using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure.Services;

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
        var hostel = hostelRepository.GetByName($"№{value.HostelNumber}").Result;
        var room = new Room(value.RoomNumber, hostel);
        hostel.AddRoom(room);
        hostelRepository.UpdateAsync(hostel);
        var resident = new Resident(value.ResidentId, value.Name, value.Surname, hostel, room);
        residentRepository.CreateAsync(resident);
    }
}