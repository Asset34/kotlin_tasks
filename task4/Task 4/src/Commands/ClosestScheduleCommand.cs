using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Task_4
{
    public class ClosestScheduleCommand(TelegramBotClient botClient) : ICommand
    {
        public async Task ExecuteAsync(List<string> args, Update update, CancellationToken cancelToken)
        {
            // Validate and Parse arguments

            if (args.Count < 1)
            {
                return;
            }

            string group = args[0];

            // Action
            Subject? scheduleDay = await LETIScheduleReceiver.ReceiveClosestSubjectAsync(group);
            if (scheduleDay is null)
            {
                return;
            }

            // Compile message
            string text = "```\n" + scheduleDay.ToString() + "```\n";

            // Send Message
            Message message = await botClient.SendTextMessageAsync(
                chatId: update.Message.Chat.Id,
                text: text,
                parseMode: ParseMode.MarkdownV2,
                cancellationToken: cancelToken
            );
        }
    }
}