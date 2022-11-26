using System.Text.Json;
using HostelBot.Domain.Domain;
using System.Text.Json.Serialization;

namespace HostelBot.App
{
    public class ResidentRegistrationCommand : ICommand
    {
        public void HandleCommand(string message)
        {
            var resident = JsonSerializer.Deserialize<Resident>(message);
        }

        public ICanFill GetObjectToFill()
        {
            return new Resident();
        }
    }
}