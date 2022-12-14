using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure;

namespace HostelBot.App;

public class ResidentRegistrationCommand : FillCommand<Resident>
{
    public ResidentRegistrationCommand(string name, IEnumerable<Manager<Resident>> managers) : base(name, managers, new Resident())
    {
    }
}