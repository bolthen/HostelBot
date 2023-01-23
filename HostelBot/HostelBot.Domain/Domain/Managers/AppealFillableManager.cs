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
    
    protected override async Task Handle(AppealFillable value)
    {
        var resident =  await residentRepository.GetAsync(value.ResidentId);
        var appeal = new Appeal(resident, value.Content, repositoryChangesParser);
        resident.AddAppeal(appeal);
        await residentRepository.UpdateAsync(resident);
    }
}