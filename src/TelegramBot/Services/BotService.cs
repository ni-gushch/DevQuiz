using DevQuiz.TelegramBot.Configurations;
using Microsoft.Extensions.Options;
using MihaZupan;
using Telegram.Bot;
using DevQuiz.TelegramBot.Interfaces;

namespace DevQuiz.TelegramBot.Services
{
    /// <inheritdoc cref="IBotService" />
    public class BotService : IBotService
    {
        /// <inheritdoc cref="IBotService.Client" />
        public TelegramBotClient Client { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="botConfiguration">IOptions instance of botConfiguration</param>
        public BotService(IOptions<BotConfiguration> botConfiguration)
        {
            var _botConfiguration = botConfiguration.Value;

            Client = string.IsNullOrWhiteSpace(_botConfiguration.Socks5Host)
                ? new TelegramBotClient(_botConfiguration.AccessToken)
                : new TelegramBotClient(_botConfiguration.AccessToken, 
                    new HttpToSocks5Proxy(_botConfiguration.Socks5Host, _botConfiguration.Socks5Port));
        }
    }
}