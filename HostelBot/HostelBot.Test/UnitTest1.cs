using HostelBot.Domain.Infrastructure.Repository;
using FakeItEasy;
using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace HostelBot.Test;

public class Tests
{
    private IMainDbContext context;
    private ResidentRepository repository;
    [SetUp]
    public void Setup()
    {
        context = A.Fake<IMainDbContext>();
    }

    [Test]
    public void Test1()
    {
        var resident = new Resident(1, 
            "Егор", "Лопарев", A.Fake<Hostel>(), A.Fake<Room>(),
            A.Fake<RepositoryChangesParser>());
        var result = repository.CreateAsync(resident).Result;
        Assert.Equals(result, resident);
    }
}

public class FakeContext : IMainDbContext
{
    public FakeContext(IMainDbContext context)
    {
    }
}