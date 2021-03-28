using System;
using System.IO;
using DevQuiz.Libraries.Data.DbContexts;
using DevQuiz.Libraries.Data.Models;

namespace DevQuiz.Libraries.Data.Tests.Helpers
{
    public static class DevQuizContextSqLiteHelper
    {
        public static DevQuizDbContext EnsureDb(this DevQuizDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            return context;
        }

        public static DevQuizDbContext SeedUsers(this DevQuizDbContext context, int usersCount)
        {
            var usersDbSet = context.Users;
            for (var i = 1; i <= usersCount; i++)
            {
                var tempUserId = Guid.NewGuid();
                var tempUserEntity = new User
                {
                    Id = tempUserId,
                    UserName = $"UserName_{tempUserId}",
                    FirstName = $"FirstName_{tempUserId}",
                    LastName = $"LastName_{tempUserId}",
                    CreatedTime = DateTime.Now,
                    UpdatedTime = DateTime.Now
                };
                usersDbSet.Add(tempUserEntity);
            }

            return context;
        }
    }
}