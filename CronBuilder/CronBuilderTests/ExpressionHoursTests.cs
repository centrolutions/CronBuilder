using CronBuilder;
using FluentAssertions;
using NUnit.Framework;
using System;

namespace CronBuilderTests
{
    public class ExpressionHoursTests
    {
        Expression _Sut;

        [SetUp]
        public void Setup()
        {
            _Sut = new Expression();
        }

        [Test]
        public void Hours_SetsHoursToSingleNumber_WhenANumberIsPassed()
        {
            int hours = 10;
            var expected = "* 10 * * *";

            var result = _Sut.Hours(hours).Build();

            result.Should().Be(expected);
        }

        [Test]
        public void Hours_SetsMultipleHourValues_WhenCalledMoreThanOnce()
        {
            int hour1 = 10;
            int hour2 = 12;
            var expected = "* 10,12 * * *";

            var result = _Sut.Hours(hour1).Hours(hour2).Build();

            result.Should().Be(expected);
        }

        [Test]
        public void Hours_SetsMultipleHourValues_WhenMixOfNumbersAndStrings()
        {
            var expected = "* 4,8-11/2 * * *";

            var result = _Sut.Hours(4).Hours("8-11/2").Build();

            result.Should().Be(expected);
        }

        [Test]
        public void Hours_ThrowsArgumentOutOfRangeException_WhenHourIsGreaterThan23()
        {
            Assert.Throws(typeof(ArgumentOutOfRangeException), () => { _Sut.Hours(24); });
        }

        [Test]
        public void Hours_ThrowsArgumentOutOfRangeException_WhenStepIsGreaterThan23()
        {
            Assert.Throws(typeof(ArgumentOutOfRangeException), () => { _Sut.Hours("4/24"); });
        }

        [Test]
        public void Hours_ThrowsArgumentOutOfRangeException_WhenNthOrLastOrWeekdayOrQuestionIsUsed()
        {
            Assert.Throws(typeof(ArgumentOutOfRangeException), () => { _Sut.Hours("30#1"); });
            Assert.Throws(typeof(ArgumentOutOfRangeException), () => { _Sut.Hours("L"); });
            Assert.Throws(typeof(ArgumentOutOfRangeException), () => { _Sut.Hours("W"); });
            Assert.Throws(typeof(ArgumentOutOfRangeException), () => { _Sut.Hours("?"); });
        }
    }
}