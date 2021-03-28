using System;
using System.Linq;
using System.Threading.Tasks;
using DevQuiz.Libraries.Data.DbContexts;
using DevQuiz.Libraries.Data.Models;
using DevQuiz.Libraries.Data.Repositories;
using DevQuiz.Libraries.Data.Tests.Helpers;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DevQuiz.Libraries.Data.Tests
{
    public class UserRepositoryTests : DevQuizContextSeedDataHelper
    {

        public UserRepositoryTests()
        {
            
        }

        [Fact]
        public async Task UserRepository_CreateUser()
        {
            //Arrange
            //create context
            await using var devQuizContext = new DevQuizDbContext(this.ContextOptions);
            //Create several entities
            await devQuizContext.SeedUsers(3)
                            .CommitAsync();

            //create user repo instance
            var userRepository = new UserRepository(devQuizContext);

            //prepare user entity
            var userToAdd = new User
            {
                FirstName = "FirstName",
                LastName = "LastName",
                UserName = "UserName"
            };

            //Act
            var createResult = await userRepository.CreateAsync(userToAdd);
            var findUserBeforeSave = await userRepository.GetAll().SingleOrDefaultAsync(it => it.Id.Equals(createResult.Id));

            await userRepository.UnitOfWork.SaveChangesAsync();

            var findUserAfterSave = await userRepository.GetAll().SingleOrDefaultAsync(it => it.Id.Equals(createResult.Id));

            //Assert
            Assert.NotNull(createResult);
            Assert.Equal(userToAdd.UserName, createResult.UserName);
            Assert.Equal(userToAdd.FirstName, createResult.FirstName);
            Assert.Equal(userToAdd.LastName, createResult.LastName);
            Assert.NotEqual(Guid.Empty, createResult.Id);
            Assert.Equal(DateTime.Now.Date, createResult.CreatedTime.Date);
        }

        [Fact]
        public void UserRepository_GetAllUsers()
        {

        }
    }
}
