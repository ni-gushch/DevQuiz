using DevQuiz.Libraries.Core.Extensions;
using System.Threading.Tasks;
using Xunit;

namespace DevQuiz.Libraries.Core.Tests.Extensions
{
    public class StringExtensionsTests
    {
        [Fact]
        public void ToSnakeCase_NullString_ReturnNull()
        {
            string inputString = null;

            var outputString = inputString.ToSnakeCase();

            Assert.Null(outputString);
        }

        [Fact]
        public void ToSnakeCase_EmptyString_ReturnEmptyString()
        {
            string inputString = string.Empty;

            var outputString = inputString.ToSnakeCase();

            Assert.Equal(string.Empty, outputString);
        }

        [Fact]
        public void ToSnakeCase_CamelCaseString_ReturnSnakeCaseString()
        {
            var inputString = "OneTwoThree";

            var outputString = inputString.ToSnakeCase();

            Assert.Equal("one_two_three", outputString);
        }

        [Fact]
        public void ToSnakeCase_SmallCamelCaseString_ReturnSnakeCaseString()
        {
            var inputString = "oneTwoThree";

            var outputString = inputString.ToSnakeCase();

            Assert.Equal("one_two_three", outputString);
        }

        [Fact]
        public void ToSnakeCase_SnakeCaseString_ReturnSnakeCaseString()
        {
            var inputString = "one_two_three";

            var outputString = inputString.ToSnakeCase();

            Assert.Equal(outputString, outputString);
        }

        [Fact]
        public void ToSnakeCase_SnakeCaseStringWithDigitsBeforeUpperCase_ReturnSnakeCaseString()
        {
            var inputString = "One1Two2Three3";

            var outputString = inputString.ToSnakeCase();

            Assert.Equal("one1_two2_three3", outputString);
        }

        [Fact]
        public void ToSnakeCase_SnakeCaseStringWithDigitsInWordMiddle_ReturnSnakeCaseString()
        {
            var inputString = "On1eTw2oThre3e";

            var outputString = inputString.ToSnakeCase();

            Assert.Equal("on1e_tw2o_thre3e", outputString);
        }
    }
}
