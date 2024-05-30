using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Task_4
{
    public class ShowNoteCommand(TelegramBotClient botClient) : ICommand
    {
        public async Task ExecuteAsync(List<string> args, Update update, CancellationToken cancelToken)
        {
            // Validate and Parse arguments

            if (args.Count < 1)
            {
                return;
            }

            if (!Int32.TryParse(args[0], out int id))
            {
                return;
            }

            // Action
            Note? note = await NotesDbHandler.ReceiveNoteAsync(id);
            if (note is null)
            {
                return;
            }

            // Send Message
            Message message = await botClient.SendTextMessageAsync(
                chatId: update.Message.Chat.Id,
                text: note.ToStringContent(),
                // parseMode: ParseMode.MarkdownV2,
                cancellationToken: cancelToken
            );
        }
    }
}