using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure.Repository;

namespace HostelBot.Domain.Infrastructure.Services;

public class HostelRepository : EntityRepository<Hostel>
{
    public HostelRepository(MainDbContext context) : base(context) { }

    public Task<List<UtilityName>> GetUtilityNames(int id)
    {
        var hostel = GetAsync(id);
        return GetUtilityNames(hostel.Result?.Name);
    }

    public async Task<List<UtilityName>> GetUtilityNames(string hostelName)
    {
        return context.UtilityNames.Where(x => x.HostelName == hostelName).ToList();
    }
}