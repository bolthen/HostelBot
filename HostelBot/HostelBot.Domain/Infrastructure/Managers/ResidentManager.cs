using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure.Services;

namespace HostelBot.Domain.Infrastructure.Managers;

public class ResidentManager : Manager<ResidentFiller>
{
    private readonly ResidentRepository residentRepository;
    private readonly HostelRepository hostelRepository;

    public ResidentManager(ResidentRepository residentRepository, HostelRepository hostelRepository)
    {
        this.residentRepository = residentRepository;
        this.hostelRepository = hostelRepository;
    }
    
    protected override void Handle(ResidentFiller value)
    {
        var hostel = hostelRepository.GetByName($"№{value.HostelNumber}").Result;
        var room = new Room(value.RoomNumber, hostel.Name);
        hostel.AddRoom(room);
        hostelRepository.UpdateAsync(hostel);
        var resident = new Resident(value.ResidentId, value.Name, value.Surname, hostel, room);
        residentRepository.CreateAsync(resident);
    }
}