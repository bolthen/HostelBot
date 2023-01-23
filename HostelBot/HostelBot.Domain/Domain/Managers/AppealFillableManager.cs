using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure.Repository;

namespace HostelBot.Domain.Infrastructure;

public class AppealFillableManager : Manager<AppealFillable>
{
    private readonly RepositoryChangesParser repositoryChangesParser;

    public AppealFillableManager(ResidentRepository residentRepository, RepositoryChangesParser repositoryChangesParser) 
        : base(residentRepository)
    {
        this.repositoryChangesParser = repositoryChangesParser;
    }
    
    protected override void Handle(AppealFillable value)
    {
        var resident = residentRepository.GetAsync(value.ResidentId).Result;
        var appeal = new Appeal(resident, value.Content, repositoryChangesParser);
        resident.AddAppeal(appeal);
        residentRepository.UpdateAsync(resident);
    }
}