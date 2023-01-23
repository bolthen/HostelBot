using HostelBot.Domain.Infrastructure;

namespace HostelBot.Domain.Domain;

public class Administrator : Entity<Administrator>
{
    public Administrator(string name, string middleName, string surname, string login, string hashPassword, int hostelId)
    {
        Name = name;
        MiddleName = middleName;
        Surname = surname;
        Login = login;
        HashPassword = hashPassword;
        HostelId = hostelId;
    }
    
    public Administrator(){}
    
    public string Name { get; set; }
    
    public string MiddleName { get; set; }
        
    public string Surname { get; set; }
    
    public int HostelId { get; set; }
    
    public string Login { get; set; }
    
    public string HashPassword { get; set; }
}