using System;
using System.Linq;
using System.Threading.Tasks;
using DevQuiz.Libraries.Core;
using DevQuiz.Libraries.Core.Repositories;
using DevQuiz.Libraries.Data.DbContexts;
using DevQuiz.Libraries.Data.Models;
using DevQuiz.Libraries.Data.Repositories;
using DevQuiz.Libraries.Data.Tests.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace DevQuiz.Libraries.Data.Tests
{
    public class QuestionRepositoryTests : DevQuizContextSeedDataHelper
    {
        private readonly DevQuizDbContext _dbContext;
        private readonly IDevQuizUnitOfWork<User, Question, Answer, Category, Tag, Guid> _unitOfWork;

        public QuestionRepositoryTests()
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
