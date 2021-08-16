using System;
using System.Linq;
using System.Threading.Tasks;
using DevQuiz.Admin.Core;
using DevQuiz.Admin.Core.Models.Entities;
using DevQuiz.Admin.Core.Repositories;
using DevQuiz.Admin.DataAccess.DbContexts;
using DevQuiz.Admin.DataAccess.Repositories;
using DevQuiz.Admin.DataAccess.Tests.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace DevQuiz.Admin.DataAccess.Tests
{
    public class UserRepositoryTests : DevQuizContextSeedDataHelper
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DevQuizDbContext _dbContext;

        public UserRepositoryTests()
        {
            var serviceCollection = new ServiceCollection()
                .AddScoped(opt => new DevQuizDbContext(ContextOptions))
                .AddScoped<IUnitOfWork, DevQuizUserUnitOfWork<DevQuizDbContext>>()
                .AddScoped<IGenericRepository<User>, GenericRepository<DevQuizDbContext, User>>();

            var serviceProvider = serviceCollection.BuildServiceProvider();
            
            _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
            _dbContext = serviceProvider.GetRequiredService<DevQuizDbContext>();
        }

        [Fact]
        public async Task CreateUser()
        {
            //Arrange
            //Create several entities
            await _dbContext
                .SeedUsers(3)
                .CommitAsync();

            //prepare user entity
            var userToAdd = new User
            {
                FirstName = "FirstName",
                LastName = "LastName",
                UserName = "UserName",
                CreatedDate = DateTime.Now
            };
            var userRepository = _unitOfWork.GetRepository<IGenericRepository<User>, User>();

            //Act
            await userRepository.CreateAsync(userToAdd);
            var findUserBeforeSave = await userRepository.GetOneAsync(user => user.Id.Equals(userToAdd.Id));

            var createCommitResult = await _unitOfWork.CommitAsync();

            var findUserAfterSave = await userRepository.GetOneAsync(user => user.Id.Equals(userToAdd.Id));

            //Assert
            Assert.NotNull(findUserAfterSave);
            Assert.Equal(userToAdd.UserName, findUserAfterSave.UserName);
            Assert.Equal(userToAdd.FirstName, findUserAfterSave.FirstName);
            Assert.Equal(userToAdd.LastName, findUserAfterSave.LastName);
            Assert.NotEqual(Guid.Empty, findUserAfterSave.Id);
            Assert.Equal(DateTime.Now.Date, findUserAfterSave.CreatedDate.Date);
        }

        [Fact]
        public async Task UpdateUser()
        {
            //Arrange
            //Create several entities
            await _dbContext
                .SeedUsers(3)
                .CommitAsync();

            //prepare user entity
            var userToAdd = new User
            {
                FirstName = "FirstName",
                LastName = "LastName",
                UserName = "UserName",
                CreatedDate = DateTime.Now
            };
            var userRepository = _unitOfWork.GetRepository<IGenericRepository<User>, User>();

            await userRepository.CreateAsync(userToAdd);
            var createCommitResult = await _unitOfWork.CommitAsync();
            var findUserAfterSave = await userRepository.GetOneAsync(user => user.Id.Equals(userToAdd.Id));
            findUserAfterSave.FirstName = "UpdatedFirstName";
            findUserAfterSave.LastName = "UpdatedLastName";
            findUserAfterSave.UserName = "UpdatedUserName";
            findUserAfterSave.UpdatedDate = DateTime.Now;

            //Act
            userRepository.Update(findUserAfterSave);
            var saveResult = await _unitOfWork.CommitAsync();

            var findUserAfterUpdate = await userRepository.GetOneAsync(user => user.Id.Equals(findUserAfterSave.Id));

            //Assert
            Assert.NotNull(findUserAfterUpdate);
            Assert.Equal(findUserAfterSave.UserName, findUserAfterUpdate.UserName);
            Assert.Equal(findUserAfterSave.FirstName, findUserAfterUpdate.FirstName);
            Assert.Equal(findUserAfterSave.LastName, findUserAfterUpdate.LastName);
            Assert.Equal(findUserAfterSave.Id, findUserAfterUpdate.Id);
            Assert.Equal(DateTime.Now.Date, findUserAfterUpdate.CreatedDate.Date);
            Assert.Equal(DateTime.Now.Date, findUserAfterUpdate.CreatedDate.Date);
        }

        [Fact]
        public async Task DeleteUser_NullAfterDeleteSuccess_ThrowDbUpdateConcurrencyExceptionWhileRepeatDelete()
        {
            //Arrange
            //Create several entities
            await _dbContext
                .SeedUsers(3)
                .CommitAsync();

            //prepare user entity
            var userToAdd = new User
            {
                FirstName = "FirstName",
                LastName = "LastName",
                UserName = "UserName"
            };
            var userRepository = _unitOfWork.GetRepository<IGenericRepository<User>, User>();

            await userRepository.CreateAsync(userToAdd);
            var createCommitResult = await _unitOfWork.CommitAsync();
            var findUserAfterSave = await userRepository.GetOneAsync(user => user.Id.Equals(userToAdd.Id));

            //Act
            userRepository.Delete(findUserAfterSave);
            await _unitOfWork.CommitAsync();
            var findDeletedUser = await userRepository.GetOneAsync(user => user.Id.Equals(findUserAfterSave.Id));

            userRepository.Delete(findUserAfterSave);
            await Assert.ThrowsAsync<DbUpdateConcurrencyException>(() => _unitOfWork.CommitAsync());

            //Assert
            Assert.Null(findDeletedUser);
        }

        [Fact]
        public async Task GetAll()
        {
            //Arrange
            var usersCount = 3;
            //create context
            await using var devQuizContext = _dbContext;
            //Create several entities
            await devQuizContext
                .SeedUsers(usersCount)
                .CommitAsync();

            //create user repo instance
            var userRepository = _unitOfWork.GetRepository<IGenericRepository<User>, User>();

            var query = await userRepository.ListAsync().ConfigureAwait(false);

            //Act
            var users = await userRepository.ListAsync()
                .ConfigureAwait(false);

            //Assert
            Assert.Equal(usersCount, users.Count());
            Assert.NotNull(users.FirstOrDefault());
        }
    }
}
