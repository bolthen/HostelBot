using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure.Repository;

namespace HostelBot.Domain.Infrastructure.Services;

public class HostelService
{
    private readonly CoreRepository coreRepository;
    
    public HostelService(CoreRepository coreRepository)
    {
        this.coreRepository = coreRepository;
    }
    
    public async Task<bool> CreateAsync(Hostel hostel)
    {
        var createSuccessful = await coreRepository.Hostels.CreateAsync(hostel);

        if (!createSuccessful)
            throw new Exception("Данным житель уже внесён в базу данных");

        return createSuccessful;
    }

    public async Task<Hostel> GetAsync(int id)
    {
        var hostel = await coreRepository.Hostels.GetAsync(id);

        if (hostel == null)
            throw new Exception("Житель с данным ID не найден в базе данных");

        return hostel;
    }

    public async Task<bool> UpdateAsync(Hostel hostel)
    {
        var updateSuccessful = await coreRepository.Hostels.UpdateAsync(hostel);

        if (!updateSuccessful)
            throw new Exception("Данный житель не найден в базе данных");

        return updateSuccessful;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var deleteSuccessful = await coreRepository.Hostels.DeleteAsync(id);
        
        if (!deleteSuccessful)
            throw new Exception("Данный житель не найден в базе данных");

        return deleteSuccessful;
    }
}