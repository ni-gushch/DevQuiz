using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using DevQuiz.Libraries.Core.Extensions;
using DevQuiz.Libraries.Core.Repositories;
using DevQuiz.Libraries.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace DevQuiz.Libraries.Data.DbContexts
{
    /// <summary>
    /// Db context for connecting DevQuiz data
    /// </summary>
    public class DevQuizDbContext : DbContext, IUnitOfWork
    {
        private IDbContextTransaction _dbContextTransaction;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options">Options for creating DevQuiz context</param>
        public DevQuizDbContext(DbContextOptions<DevQuizDbContext> options)
            : base(options)
        {
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

        /// <inheritdoc cref="IUnitOfWork.BeginTransaction(IsolationLevel)" />
        public IDisposable BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            _dbContextTransaction = Database.BeginTransaction(isolationLevel);
            return _dbContextTransaction;
        }

        /// <inheritdoc cref="IUnitOfWork.BeginTransactionAsync(IsolationLevel, CancellationToken)" />
        public async Task<IDisposable> BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted, CancellationToken cancellationToken = default)
        {
            _dbContextTransaction = await Database.BeginTransactionAsync(isolationLevel, cancellationToken);
            return _dbContextTransaction;
        }

        /// <inheritdoc cref="IUnitOfWork.CommitTransaction" />
        public void CommitTransaction()
        {
            _dbContextTransaction.Commit();
        }

        /// <inheritdoc cref="IUnitOfWork.CommitTransactionAsync(CancellationToken)" />
        public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            await _dbContextTransaction.CommitAsync(cancellationToken);
        }


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