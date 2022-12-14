using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure.Repository;

namespace HostelBot.Domain.Infrastructure.Services;

public class UtilityService
{
    private readonly CoreRepository coreRepository;

    public UtilityService(CoreRepository coreRepository)
    {
        this.coreRepository = coreRepository;
    }

    public async Task<bool> CreateAsync(Utility utility)
    {
        var createSuccessful = await coreRepository.Utilities.CreateAsync(utility);

        if (!createSuccessful)
            throw new Exception("Данным житель уже внесён в базу данных");

        return createSuccessful;
    }

    public async Task<Utility> GetAsync(int id)
    {
        var utility = await coreRepository.Utilities.GetAsync(id);

        if (utility == null)
            throw new Exception("Житель с данным ID не найден в базе данных");

        return utility;
    }

    public async Task<bool> UpdateAsync(Utility utility)
    {
        var updateSuccessful = await coreRepository.Utilities.UpdateAsync(utility);

        if (!updateSuccessful)
            throw new Exception("Данный житель не найден в базе данных");

        return updateSuccessful;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var deleteSuccessful = await coreRepository.Utilities.DeleteAsync(id);
        
        if (!deleteSuccessful)
            throw new Exception("Данный житель не найден в базе данных");

        return deleteSuccessful;
    }
}