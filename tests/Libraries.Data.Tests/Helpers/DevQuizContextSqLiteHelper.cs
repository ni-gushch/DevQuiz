using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DevQuiz.Libraries.Data.DbContexts;
using DevQuiz.Libraries.Data.Models;

namespace DevQuiz.Libraries.Data.Tests.Helpers
{
    public static class DevQuizContextSqLiteHelper
    {
        private static List<string> QuestionCategoryNames => new() { ".Net", "Java", "C++", "Python" };
        private static List<string> QuestionTagNames => new() { "Tag1", "Tag2", "Tag3", "Tag4" };
        private static List<string> QuestionAnswers => new() { "Ans1", "Ans2", "Ans3", "Ans4" };

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
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                };
                usersDbSet.Add(tempUserEntity);
            }

            return context;
        }

        public static DevQuizDbContext SeedQuestions(this DevQuizDbContext context, int countQuestion,
            bool includeAnswers,
            bool includeCategories,
            bool includeTags)
        {
            var questionsDbSet = context.Questions;
            var newCategoriesList = new List<Category>();
            var newTagsList = new List<Tag>();

            if (includeCategories)
            {
                newCategoriesList = QuestionCategoryNames.Select(cat => new Category() { Name = cat })
                    .ToList();
                newCategoriesList.ForEach(it => context.Categories.Add(it));
                context.SaveChanges();
                context.ChangeTracker.Clear();
            }

            if (includeTags)
            {
                newTagsList = QuestionTagNames.Select(cat => new Tag() { Name = cat })
                    .ToList();
                newTagsList.ForEach(it => context.Tags.Add(it));
                context.SaveChanges();
                context.ChangeTracker.Clear();
            }

            for (var i = 1; i <= countQuestion; i++)
            {
                var tempQuestion = new Question()
                {
                    Text = $"Question number {i} Text",
                    CreatedDate = DateTime.Now,
                    CategoryId = i
                };
                context.Questions.Add(tempQuestion);
                context.SaveChanges();
                context.ChangeTracker.Clear();

                var newAnswers = new List<Answer>();
                if (includeAnswers)
                {
                    newAnswers = QuestionAnswers.Select(it => new Answer() {QuestionId = tempQuestion.Id, Text = it}).ToList();
                    newAnswers.ForEach(it => context.Answers.Add(it));
                    context.SaveChanges();
                    context.ChangeTracker.Clear();

                    var randomIndex = new Random().Next(QuestionAnswers.Count);
                    tempQuestion.RightAnswerId = newAnswers.Select(it => it.Id).ElementAt(randomIndex);
                    tempQuestion.RightAnswerExplanation = Path.GetRandomFileName();
                    context.Questions.Update(tempQuestion);
                    context.SaveChanges();
                    context.ChangeTracker.Clear();
                }

                if (includeCategories)
                {
                    var randomIndex = new Random().Next(QuestionCategoryNames.Count);
                    tempQuestion.CategoryId = newCategoriesList.Select(it => it.Id).ElementAt(randomIndex);
                    context.Questions.Update(tempQuestion);
                    context.SaveChanges();
                    context.ChangeTracker.Clear();
                }               
            }

            return context;
        }

        public static void Commit(this DevQuizDbContext context)
        {
            context.SaveChanges();
        }

        public static async Task CommitAsync(this DevQuizDbContext context)
        {
            await context.SaveChangesAsync();
        }
    }
}