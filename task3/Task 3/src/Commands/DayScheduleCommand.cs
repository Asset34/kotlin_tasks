using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Task_3
{
    public class DayScheduleCommand(TelegramBotClient botClient) : ICommand
    {
        public async Task ExecuteAsync(List<string> args, Update update, CancellationToken cancelToken)
        {
            // Validate and Parse arguments

            if (args.Count < 3)
            {
                return;
            }

            string day = args[0];
            if (!Int32.TryParse(args[1], out int week))
            {
                return;
            }
            string group = args[2];

            // Action
            ScheduleDay? scheduleDay = await LETIScheduleReceiver.ReceiveDayScheduleAsync(group, day, week);
            if (scheduleDay is null)
            {
                return;
            }

            // Compile message
            string text = (scheduleDay.Subjects is []) ? "Free Day!" : "```\n" + scheduleDay.ToString() + "```\n";

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