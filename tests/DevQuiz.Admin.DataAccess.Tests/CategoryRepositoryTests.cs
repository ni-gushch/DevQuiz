using DevQuiz.Admin.Core;
using DevQuiz.Admin.Core.Models.Entities;
using DevQuiz.Admin.Core.Repositories;
using DevQuiz.Admin.DataAccess.DbContexts;
using DevQuiz.Admin.DataAccess.Repositories;
using DevQuiz.Admin.DataAccess.Tests.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace DevQuiz.Admin.DataAccess.Tests
{
    public class CategoryRepositoryTests : DevQuizContextSeedDataHelper
    {
        private readonly DevQuizDbContext _dbContext;
        private readonly IDevQuizUnitOfWork _unitOfWork;

        public CategoryRepositoryTests()
        {
            var serviceCollection = new ServiceCollection()
                .AddScoped(_ => new DevQuizDbContext(this.ContextOptions))
                .AddScoped<IGenericRepository<Question>, GenericRepository<DevQuizDbContext, Question>>()
                .AddScoped<IGenericRepository<Answer>, GenericRepository<DevQuizDbContext, Answer>>()
                .AddScoped<IGenericRepository<Category>, GenericRepository<DevQuizDbContext, Category>>()
                .AddScoped<IGenericRepository<Tag>, GenericRepository<DevQuizDbContext, Tag>>()
                .AddScoped<IDevQuizUnitOfWork, DevQuizUnitOfWork<DevQuizDbContext>>();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            _dbContext = serviceProvider.GetRequiredService<DevQuizDbContext>();
            _unitOfWork = serviceProvider.GetRequiredService<IDevQuizUnitOfWork>();
        }
    }
}
