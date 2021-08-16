using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevQuiz.TelegramBot.Models.ApiResults
{
    /// <summary>
    /// Set web hook api result model
    /// </summary>
    public class SetWebHookApiResult
    {
        /// <summary>
        /// Set web hook status
        /// </summary>
        public bool Ok { get; set; }
        /// <summary>
        /// Error code
        /// </summary>
        public int Error_code { get; set; }
        /// <summary>
        /// Description message
        /// </summary>
        public string Description { get; set; }
    }
}
