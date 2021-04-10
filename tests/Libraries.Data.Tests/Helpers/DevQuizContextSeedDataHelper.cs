using System;
using System.Data.Common;
using DevQuiz.Libraries.Data.DbContexts;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DevQuiz.Libraries.Data.Tests.Helpers
{
    public class DevQuizContextSeedDataHelper : IDisposable
    {
        protected readonly DbConnection _connection;

        protected DbContextOptions<DevQuizDbContext> ContextOptions { get; }

        public DevQuizContextSeedDataHelper()
        {
            ContextOptions = new DbContextOptionsBuilder<DevQuizDbContext>()
                .UseSqlite(CreateInMemoryDatabase())
                .Options;

            _connection = RelationalOptionsExtension
                .Extract(ContextOptions)
                .Connection;

            using var context = new DevQuizDbContext(ContextOptions);
            context.EnsureDb();
        }

        protected static DbConnection CreateInMemoryDatabase()
        {
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();
            return connection;
        }

        public void Dispose() => _connection.Dispose();
    }
}