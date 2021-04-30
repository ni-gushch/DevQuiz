using System.ComponentModel.DataAnnotations;

namespace DevQuiz.TelegramBot.Models.InputModels
{
    /// <summary>
    /// Input model for set web hook action
    /// </summary>
    public class SetWebHookInputModel
    {
        /// <summary>
        /// Which address will be set as webhook
        /// </summary>
        [Required]
        [Url]
        public string WebHookUri { get; set; }
    }
}
