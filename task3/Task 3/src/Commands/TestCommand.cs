using Telegram.Bot;
using Telegram.Bot.Types;

namespace Task_3
{
    public class TestCommand(TelegramBotClient botClient) : ICommand
    {
        public async Task ExecuteAsync(List<string> args, Update update, CancellationToken cancelToken)
        {
            Message message = await botClient.SendTextMessageAsync(
                chatId: update.Message.Chat.Id,
                text: "Test Message!",
                cancellationToken: cancelToken
            );
        }
    }
}