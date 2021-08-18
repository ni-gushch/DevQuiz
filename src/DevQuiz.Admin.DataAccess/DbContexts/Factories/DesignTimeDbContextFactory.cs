using System;
using System.IO;
using DevQuiz.Admin.Core.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DevQuiz.Admin.DataAccess.DbContexts.Factories
{
    /// <summary>
    ///     Custom db context abstract factory
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    public abstract class DesignTimeDbContextFactory<TContext> : IDesignTimeDbContextFactory<TContext>
        where TContext : DbContext
    {
        /// <summary>
        ///     Создать объект БД
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public TContext CreateDbContext(string[] args)
        {
            return Create(Directory.GetCurrentDirectory(),
                Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));
        }

        /// <summary>
        ///     Создание нового объекта
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        protected abstract TContext CreateNewInstance(DbContextOptions<TContext> options);

        /// <summary>
        ///     Создание объекта фабрики
        /// </summary>
        /// <returns></returns>
        public TContext Create()
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var basePath = AppContext.BaseDirectory;
            Console.WriteLine($"AppContext base directory - {basePath}");
            return Create(basePath, environmentName);
        }

        private TContext Create(string basePath, string environmentName)
        {
            Console.WriteLine($"Base path - {basePath}");
            var builder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environmentName}.json", true)
                .AddEnvironmentVariables();

            var config = builder.Build();
            var dbConfiguration = config.GetSection(nameof(DataBaseConfiguration)).Get<DataBaseConfiguration>();
            if (string.IsNullOrWhiteSpace(dbConfiguration.ConnectionString))
                throw new InvalidOperationException("Could not find a connection string named 'default'.");
            Console.WriteLine($"ConnectionString - {dbConfiguration.ConnectionString}");
            return Create(dbConfiguration.ConnectionString);
        }

        private TContext Create(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException($"{nameof(connectionString)} is null or empty.", nameof(connectionString));
            var optionsBuilder = new DbContextOptionsBuilder<TContext>();
            Console.WriteLine($"MyDesignTimeDbContextFactory.Create(string): Connection string: {connectionString}");
            optionsBuilder.UseNpgsql(connectionString,
                s => s.CommandTimeout((int) TimeSpan.FromMinutes(10).TotalSeconds));
            var options = optionsBuilder.Options;
            return CreateNewInstance(options);
        }
    }
}