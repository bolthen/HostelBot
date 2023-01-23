using HostelBot.Domain.Domain;

namespace HostelBot.Domain.Infrastructure.Repository;

public class AppealRepository : EntityRepository<Appeal>
{
    public AppealRepository(IMainDbContext context) : base(context) { }

    public async Task AddAnswer(long id, string answer)
    {
        var appeal = await GetAsync(id);
        appeal.Answer = answer;
        var addResult = await UpdateAsync(appeal);
        
        if (addResult == false)
            throw new Exception("This Appeal don't exist");
    }
}