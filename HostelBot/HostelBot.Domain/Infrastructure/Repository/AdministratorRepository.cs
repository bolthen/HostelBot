using HostelBot.Domain.Domain;

namespace HostelBot.Domain.Infrastructure.Repository;

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