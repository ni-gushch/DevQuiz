namespace DevQuiz.TelegramBot.Constants
{
    /// <summary>
    /// Names of typed https clients
    /// </summary>
    internal static class TypedHttpClients
    {
        /// <summary>
        /// Telegram api http client name
        /// </summary>
        public static TypedHttpClientInformation TelegramApi = new ("TelegramApi", "https://api.telegram.org");
    }

    /// <summary>
    /// Record that represent a typed http client
    /// </summary>
    internal record TypedHttpClientInformation(string ClientName, string Address)
    {
        /// <summary>
        /// Get string from client
        /// </summary>
        /// <returns>Http client name</returns>
        public override string ToString()
        {
            return ClientName;
        }
    };
}
