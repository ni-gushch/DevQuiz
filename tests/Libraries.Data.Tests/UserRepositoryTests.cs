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
            var findUserBeforeSave = await userRepository.GetOneAsync(createResult.Id);

            await userRepository.UnitOfWork.SaveChangesAsync();

            var findUserAfterSave = await userRepository.GetOneAsync(createResult.Id);

            //Assert
            Assert.NotNull(createResult);
            Assert.Equal(userToAdd.UserName, createResult.UserName);
            Assert.Equal(userToAdd.FirstName, createResult.FirstName);
            Assert.Equal(userToAdd.LastName, createResult.LastName);
            Assert.NotEqual(Guid.Empty, createResult.Id);
            Assert.Equal(DateTime.Now.Date, createResult.CreatedTime.Date);
        }

        [Fact]
        public async Task UserRepository_UpdateUser()
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

            var createResult = await userRepository.CreateAsync(userToAdd);
            await userRepository.UnitOfWork.SaveChangesAsync();
            var findUserAfterSave = await userRepository.GetOneAsync(createResult.Id);
            findUserAfterSave.FirstName = "UpdatetFirstName";
            findUserAfterSave.LastName = "UpdatetLastName";
            findUserAfterSave.UserName = "UpdatetUserName";

            //Act
            var updatedUser = userRepository.Update(findUserAfterSave);
            var saveResult = await userRepository.UnitOfWork.SaveChangesAsync();

            var findUserAfterUpdate = await userRepository.GetOneAsync(findUserAfterSave.Id);
            
            //Assert
            Assert.NotNull(updatedUser);
            Assert.Equal(findUserAfterSave.UserName, findUserAfterUpdate.UserName);
            Assert.Equal(findUserAfterSave.FirstName, findUserAfterUpdate.FirstName);
            Assert.Equal(findUserAfterSave.LastName, findUserAfterUpdate.LastName);
            Assert.Equal(findUserAfterSave.Id, findUserAfterUpdate.Id);
            Assert.Equal(DateTime.Now.Date, findUserAfterUpdate.CreatedTime.Date);
            Assert.Equal(DateTime.Now.Date, findUserAfterUpdate.CreatedTime.Date);
        }

        [Fact]
        public async Task UserRepository_DeleteUser_NullAfterDeleteSuccess_ThrowDbUpdateConcurrencyExceptionWhiteRepeatDelete()
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

            var createResult = await userRepository.CreateAsync(userToAdd);
            await userRepository.UnitOfWork.SaveChangesAsync();
            var findUserAfterSave = await userRepository.GetOneAsync(createResult.Id);

            //Act
            userRepository.Delete(findUserAfterSave);
            await userRepository.UnitOfWork.SaveChangesAsync();

            var fingDeletedUser = await userRepository.GetOneAsync(findUserAfterSave.Id);

            userRepository.Delete(findUserAfterSave);
            await Assert.ThrowsAsync<DbUpdateConcurrencyException>(() => userRepository.UnitOfWork.SaveChangesAsync());

            //Assert
            Assert.Null(fingDeletedUser);
        }

        [Fact]
        public void UserRepository_GetAllUsers()
        {

        }
    }
}
