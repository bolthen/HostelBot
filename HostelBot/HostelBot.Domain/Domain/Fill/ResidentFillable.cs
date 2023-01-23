using System.ComponentModel.DataAnnotations;
using System.Reflection;
using HostelBot.Domain.Domain;
using HostelBot.Domain.Infrastructure.Managers;

namespace HostelBot.Domain.Infrastructure;

public class ResidentFillable : Fillable<ResidentFillable>
{
      public override async Task OnFilled()
     {
           foreach (var observer in observers.ToArray())
                 await observer.OnCompleted(this);
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