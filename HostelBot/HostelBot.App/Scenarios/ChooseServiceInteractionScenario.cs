﻿using System.Windows.Input;
using HostelBot.Domain;
using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure;

namespace HostelBot.App;

public class ChooseServiceInteractionScenario : IInteractionScenario
{
    public List<ICommand> GetSubcommands()
    {
        return new List<ICommand> {new ServiceCommand(new Service("Клининг"))};
    }

    public object[] GetStaticInfo()
    {
        return Array.Empty<object>();
    }

    public IFiller GetFiller()
    {
        throw new NotImplementedException();
    }
}