using Telegram.Bot.Types;

namespace Task_3
{
    public interface ICommand
    {
        Task ExecuteAsync(List<string> args, Update update, CancellationToken cancelToken);
    }
}