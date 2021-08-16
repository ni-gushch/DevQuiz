using Microsoft.EntityFrameworkCore;

namespace DevQuiz.Admin.DataAccess.DbContexts.Factories
{
    /// <summary>
    /// DevQuiz db context factory
    /// </summary>
    public class DevQuizDesignTimeDbContextFactory : DesignTimeDbContextFactory<DevQuizDbContext>
    {
        /// <summary>
        /// Creating new object
        /// </summary>
        /// <param name="options">Db context options</param>
        /// <returns>Db context instance</returns>
        protected override DevQuizDbContext CreateNewInstance(DbContextOptions<DevQuizDbContext> options)
        {
            return new DevQuizDbContext(options);
        }
    }
}