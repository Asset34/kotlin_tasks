using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Task_4
{
    public class WeekScheduleCommand(TelegramBotClient botClient) : ICommand
    {
        public async Task ExecuteAsync(List<string> args, Update update, CancellationToken cancelToken)
        {
            // Validate and Parse arguments

            if (args.Count < 2)
            {
                return;
            }

            if (!Int32.TryParse(args[0], out int week))
            {
                return;
            }
            string group = args[1];

            // Action
            ScheduleWeek? scheduleWeek = await LETIScheduleReceiver.ReceiveWeekScheduleAsync(group, week);
            if (scheduleWeek is null)
            {
                return;
            }

            // Send Messages

            Message message;
            string text;
            foreach (var daySchedule in scheduleWeek.DaySchedules)
            {
                if (daySchedule.Subjects.Count > 0)
                {
                    text = "```\n" + daySchedule.ToString() +"```\n";

                    message = await botClient.SendTextMessageAsync(
                        chatId: update.Message.Chat.Id,
                        text: text,
                        parseMode: ParseMode.MarkdownV2,
                        cancellationToken: cancelToken
                    );
                }
            }
        }
    }
}