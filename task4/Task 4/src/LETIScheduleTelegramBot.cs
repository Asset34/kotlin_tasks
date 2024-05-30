using Telegram.Bot;

namespace Task_4
{
    public class LETIScheduleTelegramBot
    {
        private TelegramBotClient _botClient = new("7259762732:AAGzSsswYizbH8pg1_LbHJ4Thlmgx49l3Pg");
        private CancellationTokenSource _cancelTokenSrc = new();

        private CommandHandler _commandHandler;

        public LETIScheduleTelegramBot()
        {
            _commandHandler = new(_botClient);
        }

        public void Run()
        {
            try
            {
                _commandHandler.ReceiveCommandAsync(_cancelTokenSrc.Token).Wait();
            }
            catch (System.Exception)
            {
                _cancelTokenSrc.Cancel();
                throw;
            }
        }
    }
}