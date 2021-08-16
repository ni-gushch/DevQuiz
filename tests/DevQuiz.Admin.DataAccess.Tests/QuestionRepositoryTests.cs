using System.Linq;
using System.Threading.Tasks;
using DevQuiz.Admin.Core;
using DevQuiz.Admin.Core.Models.Entities;
using DevQuiz.Admin.Core.Repositories;
using DevQuiz.Admin.DataAccess.DbContexts;
using DevQuiz.Admin.DataAccess.Repositories;
using DevQuiz.Admin.DataAccess.Tests.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace DevQuiz.Admin.DataAccess.Tests
{
    public class QuestionRepositoryTests : DevQuizContextSeedDataHelper
    {
        private readonly DevQuizDbContext _dbContext;
        private readonly IDevQuizUnitOfWork _unitOfWork;

        public QuestionRepositoryTests()
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

        [Fact]
        public async Task CreateQuestion()
        {
            //Arrange
            await _dbContext
                .SeedQuestions(4, true, true, true)
                .CommitAsync();

            var newQuestion = new Question()
            {
                Text = "NewQuestionText",
                CategoryId = (int)_dbContext.Categories.FirstOrDefault()?.Id
            };

            //Act
            await _unitOfWork.QuestionRepository.CreateAsync(newQuestion).ConfigureAwait(false);
            var commitStatus = await _unitOfWork.CommitAsync().ConfigureAwait(false);


            //Assert
            Assert.Equal(1, commitStatus);
        }
    }
}
