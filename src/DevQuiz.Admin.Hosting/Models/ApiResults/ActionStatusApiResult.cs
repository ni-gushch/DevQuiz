namespace DevQuiz.Admin.Hosting.Models.ApiResults
{
    /// <summary>
    ///     Api result with action complete status
    /// </summary>
    public class ActionStatusApiResult
    {
        /// <summary>
        ///     Constructor without parameters
        /// </summary>
        public ActionStatusApiResult()
        {
        }

        /// <summary>
        ///     Constructor with parameter
        /// </summary>
        /// <param name="status">Action status</param>
        public ActionStatusApiResult(bool status)
        {
            ActionStatus = status;
        }

        /// <summary>
        ///     Complete status
        /// </summary>
        public bool ActionStatus { get; set; }
    }
}