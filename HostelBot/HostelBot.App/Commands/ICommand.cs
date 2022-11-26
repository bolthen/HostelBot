using HostelBot.Domain.Domain;

namespace HostelBot.App
{
    public interface ICommand
    {
        void HandleCommand(string message);
        ICanFill GetObjectToFill();
    }
}