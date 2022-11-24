namespace HostelBot.App;

public interface ICommand
{
    void HandleCommand(string message);
}