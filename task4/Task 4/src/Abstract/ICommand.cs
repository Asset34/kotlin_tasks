using Telegram.Bot.Types;

namespace Task_4
{
    public interface ICommand
    {
        Task ExecuteAsync(List<string> args, Update update, CancellationToken cancelToken);
    }
}