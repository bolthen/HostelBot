using System.ComponentModel.DataAnnotations;
using System.Reflection;
using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure.Managers;
using HostelBot.Domain.Infrastructure.Services;

namespace HostelBot.Domain.Infrastructure;

public class ResidentFiller : Filler<ResidentFiller>
{
     /*public void OnFilled()
     {
         foreach (var observer in observers.ToArray())
             observer.OnCompleted(this);
     }
 
     public IReadOnlyCollection<PropertyInfo> GetFields() => Properties;
 
     public long ResidentId { get; set; }*/
     /*public override Resident GetFilledEntity(Manager<Resident> hostelRepository)
     {
          var hostel = hostelRepository.GetByName($"№{HostelNumber}").Result;
          var room = new Room(RoomNumber, hostel.Name);
          hostel.AddRoom(room);
          return new Resident(ResidentId, Name, Surname, hostel, room);
     }*/
      
     public override void OnFilled()
     {
           foreach (var observer in observers.ToArray())
                 observer.OnCompleted(this);
     }
     
     [Question("Имя", ViewType.TextEnter)]
     [Required, RegularExpression(@"^([А-ЩЭ-Я][а-яё]+-?)+$",
           ErrorMessage = "Имя должно начинаться с заглавной буквы, не иметь пробелов")]
     public string Name { get; set; }

     [Question("Фамилия", ViewType.TextEnter)]
     [Required, RegularExpression(@"^([А-ЩЭ-Я][а-яё]+-?)+$",
           ErrorMessage = "Фамилия должна начинаться с заглавной буквы, не иметь пробелов")]
     public string Surname { get; set; }

     [Question("Номер комнаты", ViewType.TextEnter)]
     [Required, RegularExpression(@"^([0-9]+-?)+$",
           ErrorMessage = "Номер комнаты должен состоять только из цифр")]
     public int RoomNumber { get; set; }

     [Question("Номер общежития", ViewType.TextEnter)]
     [Required, RegularExpression(@"^([0-9]+-?)+$",
           ErrorMessage = "Номер общежития должен состоять только из цифр")]
     public string HostelNumber { get; set; }
}