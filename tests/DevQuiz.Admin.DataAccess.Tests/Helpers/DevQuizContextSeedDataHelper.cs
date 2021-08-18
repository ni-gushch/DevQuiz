using System;
using System.Data.Common;
using DevQuiz.Admin.DataAccess.DbContexts;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DevQuiz.Admin.DataAccess.Tests.Helpers
{
    public class DevQuizContextSeedDataHelper : IDisposable
    {
        protected readonly DbConnection _connection;

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

        protected DbContextOptions<DevQuizDbContext> ContextOptions { get; }

        public void Dispose()
        {
            _connection.Dispose();
        }

        protected static DbConnection CreateInMemoryDatabase()
        {
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();
            return connection;
        }
    }
}