using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Task_4
{
    public class NewNoteCommand(TelegramBotClient botClient) : ICommand
    {
        public async Task ExecuteAsync(List<string> args, Update update, CancellationToken cancelToken)
        {
            // Validate and Parse arguments

            if (args.Count < 2)
            {
                return;
            }

            string title = args[0];
            string text = args[1];

            // Action
            await NotesDbHandler.AddNote(title, text);

            // Send Message
            Message message = await botClient.SendTextMessageAsync(
                chatId: update.Message.Chat.Id,
                text: "Saved!",
                // parseMode: ParseMode.MarkdownV2,
                cancellationToken: cancelToken
            );
        }
    }
}