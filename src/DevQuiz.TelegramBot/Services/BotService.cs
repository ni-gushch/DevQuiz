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
            var configurationValue = botConfiguration.Value;

            Client = string.IsNullOrWhiteSpace(configurationValue.Socks5Host)
                ? new TelegramBotClient(configurationValue.AccessToken)
                : new TelegramBotClient(configurationValue.AccessToken, 
                    new HttpToSocks5Proxy(configurationValue.Socks5Host, configurationValue.Socks5Port));
        }
    }
}