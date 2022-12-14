using System.Reflection;
using HostelBot.Domain.Infrastructure;

namespace HostelBot.Domain.Domain;

public class Status : ValueType<Status>, IFillable
{
    public bool Filled { get; set; }

    public void OnFilled()
    {
        throw new NotImplementedException();
    }

    public IReadOnlyCollection<PropertyInfo> GetFields()
    {
        //класс затычка, хз нужен ли будет
        throw new NotImplementedException();
    }
}