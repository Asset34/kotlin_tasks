using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Task_4
{
    public class ListAllNotes(TelegramBotClient botClient) : ICommand
    {
        public async Task ExecuteAsync(List<string> args, Update update, CancellationToken cancelToken)
        {
            // Action
            List<Note>? notes = await NotesDbHandler.ReceiveNoteListAsync();
            if (notes is null)
            {
                return;
            }

            // Compile message
            StringBuilder sb = new();
            foreach (var note in notes)
            {
                sb.AppendLine(note.ToStringMeta());
            }

            // Send Message
            Message message = await botClient.SendTextMessageAsync(
                chatId: update.Message.Chat.Id,
                text: sb.ToString(),
                // parseMode: ParseMode.MarkdownV2,
                cancellationToken: cancelToken
            );
        }
    }
}