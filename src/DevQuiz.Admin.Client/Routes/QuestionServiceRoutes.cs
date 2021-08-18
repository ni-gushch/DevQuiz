namespace DevQuiz.Admin.Client.Routes
{
    /// <summary>
    /// Routes collection for Question service
    /// </summary>
    public static class QuestionServiceRoutes
    {
        /// <summary>
        /// Route for GetAll method
        /// </summary>
        public const string GetAll = "getall";

        /// <summary>
        /// Route for GetById method
        /// </summary>
        public const string GetById = "get/{id:int}";
    }
}