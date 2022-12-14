using System.Data.SqlTypes;
using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure.Repository;

namespace HostelBot.Domain.Infrastructure.Services;

public class ResidentService
{
    private readonly CoreRepository coreRepository;

    public ResidentService(CoreRepository coreRepository)
    {
        this.coreRepository = coreRepository;
    }

    public async Task<bool> CreateAsync(Resident resident)
    {
        var createSuccessful = await coreRepository.Residents.CreateAsync(resident);

        if (!createSuccessful)
            throw new Exception("Данным житель уже внесён в базу данных");

        return createSuccessful;
    }

    public async Task<Resident> GetAsync(int id)
    {
        var resident = await coreRepository.Residents.GetAsync(id);

        if (resident == null)
            throw new Exception("Житель с данным ID не найден в базе данных");

        return resident;
    }

    public async Task<bool> UpdateAsync(Resident resident)
    {
        var updateSuccessful = await coreRepository.Residents.UpdateAsync(resident);

        if (!updateSuccessful)
            throw new Exception("Данный житель не найден в базе данных");

        return updateSuccessful;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var deleteSuccessful = await coreRepository.Residents.DeleteAsync(id);
        
        if (!deleteSuccessful)
            throw new Exception("Данный житель не найден в базе данных");

        return deleteSuccessful;
    }

}