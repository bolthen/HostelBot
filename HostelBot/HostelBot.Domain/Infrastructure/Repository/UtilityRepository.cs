﻿using HostelBot.Domain.Domain;
namespace HostelBot.Domain.Infrastructure.Repository;

public class UtilityRepository : EntityRepository<HostelBot.Domain.Utility>
{
    public UtilityRepository(IMainDbContext context) : base(context) { }
}