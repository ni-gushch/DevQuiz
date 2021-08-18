namespace DevQuiz.TelegramBot.Configurations
{
    /// <summary>
    ///     Configurations for bot
    /// </summary>
    public class BotConfiguration
    {
        /// <summary>
        ///     Access token for connecting to a telegram bot
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        ///     Socks 5 host address
        /// </summary>
        public string Socks5Host { get; set; }

        /// <summary>
        ///     Socks 5 port
        /// </summary>
        public int Socks5Port { get; set; }
    }
}