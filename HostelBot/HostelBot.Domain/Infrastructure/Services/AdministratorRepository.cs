using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure.Repository;

namespace HostelBot.Domain.Infrastructure.Services;

public class AdministratorRepository : EntityRepository<Administrator>
{
    public AdministratorRepository(MainDbContext context) : base(context) { }

    public async Task<Administrator> GetByLogin(string login)
    {
        return context.Administrators.FirstOrDefault(x => x.Login == login);
    }

    public async Task<string> GetPasswordHash(string login)
    {
        return GetByLogin(login).Result.HashPassword;
    }
}