using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Task_3
{
    public class CommandHandler(TelegramBotClient botClient) : IUpdateHandler
    {
        private SortedList<string, ICommand> _commands = new(){
            { "/test"       , new TestCommand(botClient)             },
            { "/day"        , new DayScheduleCommand(botClient)      },
            { "/today"      , new TodayScheduleCommand(botClient)    },
            { "/tomorrow"   , new TomorrowScheduleCommand(botClient) },
            { "/near_lesson", new ClosestScheduleCommand(botClient)  },
            { "/all"        , new WeekScheduleCommand(botClient)     }
        };

        public async Task ReceiveCommandAsync(CancellationToken cancelToken)
        {
            // Receive command
            await botClient.ReceiveAsync(
                updateHandler: HandleUpdateAsync,
                pollingErrorHandler: HandlePollingErrorAsync,
                cancellationToken: cancelToken
            );
        }

        public async Task HandleUpdateAsync(ITelegramBotClient _, Update update, CancellationToken cancellationToken)
        {
            // Get Text message
            if (update.Message is not { } || update.Message.Text is not { })
            {
                return;
            }
            var messageText = update.Message.Text;

            // Validate message
            if (String.IsNullOrWhiteSpace(messageText))
            {
                return;
            }

            // Split into Command and Args
            var args = messageText.Split(' ').ToList<string>();
            string command = args[0];
            args.RemoveAt(0);

            // Validate Command
            if (!_commands.ContainsKey(command))
            {
                return;
            }

            // Process Command
            await _commands[command].ExecuteAsync(args, update, cancellationToken);
        }

        public Task HandlePollingErrorAsync(ITelegramBotClient _, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);

            return Task.CompletedTask;   
        }
    }
}