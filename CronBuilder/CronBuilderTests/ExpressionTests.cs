using CronBuilder;
using FluentAssertions;
using NUnit.Framework;

namespace CronBuilderTests
{
    public class ExpressionTests
    {
        Expression _Sut;

        [SetUp]
        public void Setup()
        {
            _Sut = new Expression();
        }

        [Test]
        public void Minutes_SetsMinutesToSingleNumber_WhenANumberIsPassed()
        {
            int minutes = 30;
            var expected = "30 * * * *";

            var result = _Sut.Minutes(minutes).Build();

            result.Should().Be(expected);
        }

        [Test]
        public void Minutes_SetsMultipleMinuteValues_WhenCalledMoreThanOnce()
        {
            int min1 = 15;
            int min2 = 45;
            var expected = "15,45 * * * *";

            var result = _Sut.Minutes(min1).Minutes(min2).Build();

            result.Should().Be(expected);
        }

        [Test]
        public void Minutes_SetsMultipleMinuteValues_WhenMixOfNumbersAndStrings()
        {
            var expected = "30,15-45/2 * * * *";

            var result = _Sut.Minutes(30).Minutes("45-15/2").Build();

            result.Should().Be(expected);
        }

        [Test]
        public void Hours_SetsHoursToSingleNumber_WhenANumberIsPassed()
        {
            int hours = 10;
            var expected = "* 10 * * *";

            var result = _Sut.Hours(hours).Build();

            result.Should().Be(expected);
        }
    }
}