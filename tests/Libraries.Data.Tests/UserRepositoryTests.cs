using System;
using System.Linq;
using System.Threading.Tasks;
using DevQuiz.Libraries.Core.Repositories;
using DevQuiz.Libraries.Data.DbContexts;
using DevQuiz.Libraries.Data.Models;
using DevQuiz.Libraries.Data.Repositories;
using DevQuiz.Libraries.Data.Tests.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace DevQuiz.Libraries.Data.Tests
{
    public class UserRepositoryTests : DevQuizContextSeedDataHelper
    {
        private readonly DbFactory<DevQuizDbContext> _dbContextFactory;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository<User, Guid> _userRepository;

        public UserRepositoryTests()
        {
            var serviceCollection = new ServiceCollection()
                .AddScoped(opt => new DevQuizDbContext(this.ContextOptions))
                .AddScoped<Func<DevQuizDbContext>>((sp) => () => sp.GetService<DevQuizDbContext>())
                .AddScoped<DbFactory<DevQuizDbContext>>()
                .AddScoped<IUnitOfWork, UnitOfWork<DevQuizDbContext>>()
                .AddTransient<IUserRepository<User, Guid>, UserRepository<DevQuizDbContext, User, Guid>>();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            _dbContextFactory = serviceProvider.GetRequiredService<DbFactory<DevQuizDbContext>>();
            _userRepository = serviceProvider.GetRequiredService<IUserRepository<User, Guid>>();
            _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
        }

        [Fact]
        public async Task UserRepository_CreateUser()
        {
            //Arrange
            //create context
            await using var devQuizContext = _dbContextFactory.DbContext;
            //Create several entities
            await devQuizContext.SeedUsers(3)
                            .CommitAsync();

            //prepare user entity
            var userToAdd = new User
            {
                FirstName = "FirstName",
                LastName = "LastName",
                UserName = "UserName"
            };

            //Act
            await _userRepository.CreateAsync(userToAdd);
            var findUserBeforeSave = await _userRepository.GetByIdAsync(userToAdd.Id);

            var createCommitResult = await _unitOfWork.CommitAsync();

            var findUserAfterSave = await _userRepository.GetByIdAsync(userToAdd.Id);

            //Assert
            Assert.NotNull(findUserAfterSave);
            Assert.Equal(userToAdd.UserName, findUserAfterSave.UserName);
            Assert.Equal(userToAdd.FirstName, findUserAfterSave.FirstName);
            Assert.Equal(userToAdd.LastName, findUserAfterSave.LastName);
            Assert.NotEqual(Guid.Empty, findUserAfterSave.Id);
            Assert.Equal(DateTime.Now.Date, findUserAfterSave.CreatedDate.Date);
        }

        [Fact]
        public async Task UserRepository_UpdateUser()
        {
            //Arrange
            //create context
            await using var devQuizContext = _dbContextFactory.DbContext;
            //Create several entities
            await devQuizContext.SeedUsers(3)
                            .CommitAsync();

            //prepare user entity
            var userToAdd = new User
            {
                FirstName = "FirstName",
                LastName = "LastName",
                UserName = "UserName"
            };

            await _userRepository.CreateAsync(userToAdd);
            var createCommitResult = await _unitOfWork.CommitAsync();
            var findUserAfterSave = await _userRepository.GetByIdAsync(userToAdd.Id);
            findUserAfterSave.FirstName = "UpdatetFirstName";
            findUserAfterSave.LastName = "UpdatetLastName";
            findUserAfterSave.UserName = "UpdatetUserName";

            //Act
            _userRepository.Update(findUserAfterSave);
            var saveResult = await _unitOfWork.CommitAsync();

            var findUserAfterUpdate = await _userRepository.GetByIdAsync(findUserAfterSave.Id);
            
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
        public async Task UserRepository_DeleteUser_NullAfterDeleteSuccess_ThrowDbUpdateConcurrencyExceptionWhiteRepeatDelete()
        {
            //Arrange
            //create context
            await using var devQuizContext = _dbContextFactory.DbContext;
            //Create several entities
            await devQuizContext.SeedUsers(3)
                            .CommitAsync();

            //prepare user entity
            var userToAdd = new User
            {
                FirstName = "FirstName",
                LastName = "LastName",
                UserName = "UserName"
            };

            await _userRepository.CreateAsync(userToAdd);
            var createCommitResult = await _unitOfWork.CommitAsync();
            var findUserAfterSave = await _userRepository.GetByIdAsync(userToAdd.Id);

            //Act
            _userRepository.Delete(findUserAfterSave);
            await _unitOfWork.CommitAsync();

            var fingDeletedUser = await _userRepository.GetByIdAsync(findUserAfterSave.Id);

            _userRepository.Delete(findUserAfterSave);
            await Assert.ThrowsAsync<DbUpdateConcurrencyException>(() => _unitOfWork.CommitAsync());

            //Assert
            Assert.Null(fingDeletedUser);
        }

        [Fact]
        public void UserRepository_GetAllUsers()
        {

        }
    }
}
