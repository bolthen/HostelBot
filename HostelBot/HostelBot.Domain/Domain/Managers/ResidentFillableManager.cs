using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure.Exceptions;
using HostelBot.Domain.Infrastructure.Repository;

namespace HostelBot.Domain.Infrastructure.Managers;

public class ResidentFillableManager : Manager<ResidentFillable>
{
    private readonly HostelRepository hostelRepository;
    private readonly RepositoryChangesParser repositoryChangesParser;

    public ResidentFillableManager(ResidentRepository residentRepository, HostelRepository hostelRepository,
        RepositoryChangesParser repositoryChangesParser) 
        : base(residentRepository)
    {
        this.hostelRepository = hostelRepository;
        this.repositoryChangesParser = repositoryChangesParser;
    }
    
    protected override async Task Handle(ResidentFillable value)
    {
        Room room;
        try
        {
            room = await hostelRepository.FindOrCreateRoom($"№{value.HostelNumber}", value.RoomNumber);
        }
        catch (AggregateException e)
        {
            throw new InvalidHostelNameException(e.InnerException);
        }

        var hostel = await hostelRepository.GetByName($"№{value.HostelNumber}");
        var resident = new Resident(value.ResidentId, value.Name, value.Surname, hostel, room, repositoryChangesParser);
        await residentRepository.CreateAsync(resident);
    }
}