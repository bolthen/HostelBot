using HostelBot.Domain.Infrastructure;
using HostelBot.Domain.Infrastructure.Repository;

namespace HostelBot.App;

public class InformationCommand : Command
{
    private readonly ResidentRepository residentRepository;
    private readonly AdministratorRepository administratorRepository;

    public InformationCommand(ResidentRepository residentRepository, AdministratorRepository administratorRepository) 
        : base("Информация")
    {
        this.residentRepository = residentRepository;
        this.administratorRepository = administratorRepository;
    }

    public override string GetInformation(long residentId)
    {
        var resident = residentRepository.GetAsync(residentId).Result;
        var administrator = administratorRepository.GetByHostel(resident.Hostel).Result;
        return $"Имя: {resident.Name}\n" +
               $"Фамилия: {resident.Surname}\n" +
               $"Общежитие: {resident.Hostel.Name}\n" +
               $"Заведующая: {administrator.Surname} {administrator.Name} {administrator.MiddleName}\n" +
               $"Номер комнаты: {resident.Room?.Number}\n" + 
               $"Количество необработанных обращений: {resident.Appeals.Count(x => x.Answer is null)}";
    }
}