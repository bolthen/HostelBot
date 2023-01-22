using HostelBot.Domain.Domain;

namespace HostelBot.Domain.Infrastructure.Repository;

public class AppealRepository : EntityRepository<Appeal>
{
    public AppealRepository(IMainDbContext context) : base(context) { }

    public async void AddAnswer(long id, string answer)
    {
        var appeal = GetAsync(id).Result;
        appeal.Answer = answer;
        var addResult = UpdateAsync(appeal).Result;
        
        if (addResult == false)
            throw new Exception("This Appeal don't exist");
    }

    
}