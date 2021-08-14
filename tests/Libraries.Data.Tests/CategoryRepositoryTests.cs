using System;
using DevQuiz.Libraries.Core;
using DevQuiz.Libraries.Core.Models.Entities;
using DevQuiz.Libraries.Core.Repositories;
using DevQuiz.Libraries.Data.DbContexts;
using DevQuiz.Libraries.Data.Repositories;
using DevQuiz.Libraries.Data.Tests.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace DevQuiz.Libraries.Data.Tests
{
    public class CategoryRepositoryTests : DevQuizContextSeedDataHelper
    {
        private readonly DevQuizDbContext _dbContext;
        private readonly IDevQuizUnitOfWork<User, Question, Answer, Category, Tag, Guid> _unitOfWork;

        public CategoryRepositoryTests()
        {
            var serviceCollection = new ServiceCollection()
                .AddScoped(_ => new DevQuizDbContext(this.ContextOptions))
                .AddScoped<IGenericRepository<Question>, GenericRepository<DevQuizDbContext, Question>>()
                .AddScoped<IGenericRepository<Answer>, GenericRepository<DevQuizDbContext, Answer>>()
                .AddScoped<IGenericRepository<Category>, GenericRepository<DevQuizDbContext, Category>>()
                .AddScoped<IGenericRepository<Tag>, GenericRepository<DevQuizDbContext, Tag>>()
                .AddScoped<IDevQuizUnitOfWork<User, Question, Answer, Category, Tag, Guid>, DevQuizUnitOfWork<DevQuizDbContext, User, Question, Answer, Category, Tag, Guid>>();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            _dbContext = serviceProvider.GetRequiredService<DevQuizDbContext>();
            _unitOfWork = serviceProvider.GetRequiredService<IDevQuizUnitOfWork<User, Question, Answer, Category, Tag, Guid>>();
        }
    }
}
