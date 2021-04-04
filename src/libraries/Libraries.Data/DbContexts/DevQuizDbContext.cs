using DevQuiz.Libraries.Core.Extensions;
using DevQuiz.Libraries.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace DevQuiz.Libraries.Data.DbContexts
{
    /// <summary>
    /// Db context for connecting DevQuiz data
    /// </summary>
    public class DevQuizDbContext : DbContext
    {
        private string _dbConnectionString;

        /// <summary>
        /// Constructor
        /// </summary>
        public DevQuizDbContext(IConfiguration config)
        {
            _dbConnectionString = config.GetSection("DB:ConnectionString").Value;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options">Optins for creating DevQuiz context</param>
        public DevQuizDbContext(DbContextOptions<DevQuizDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(_dbConnectionString);
            }
        }

        /// <summary>
        /// DevQuiz Users
        /// </summary>
        public DbSet<User> Users { get; set; }
        /// <summary>
        /// DevQuiz Questions
        /// </summary>
        public DbSet<Question> Questions { get; set; }
        /// <summary>
        /// DevQuiz Categories
        /// </summary>
        public DbSet<Category> Categories { get; set; }
        /// <summary>
        /// DevQuiz Tags
        /// </summary>
        public DbSet<Tag> Tags { get; set; }
        /// <summary>
        /// DevQuiz Answers
        /// </summary>
        public DbSet<Answer> Answers { get; set; }

        /// <summary>
        /// Method executing while models creating
        /// </summary>
        /// <param name="modelBuilder">Model builder</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {

                // Replace table names
                entity.SetTableName(entity.GetTableName().ToSnakeCase());

                // Replace column names
                foreach (var property in entity.GetProperties())
                {
                    property.SetColumnName(property.Name.ToSnakeCase());
                }

                foreach (var key in entity.GetKeys())
                {
                    key.SetName(key.GetName().ToSnakeCase());
                }

                foreach (var key in entity.GetForeignKeys())
                {
                    key.PrincipalKey.SetName(key.PrincipalKey.GetName().ToSnakeCase());
                }

                foreach (var index in entity.GetIndexes())
                {
                    index.SetDatabaseName(index.GetDatabaseName().ToSnakeCase());
                }
            }
        }
    }
}