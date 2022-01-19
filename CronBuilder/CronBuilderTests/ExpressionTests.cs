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
        public void Hours_SetsHoursToSingleNumber_WhenANumberIsPassed()
        {
            int hours = 10;
            var expected = "* 10 * * *";

            var result = _Sut.Hours(hours).Build();

            result.Should().Be(expected);
        }
    }
}