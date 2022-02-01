using CronBuilder;
using FluentAssertions;
using NUnit.Framework;
using System;

namespace CronBuilderTests
{
    public class ExpressionDayOfMonthTests
    {
        Expression _Sut;

        [SetUp]
        public void Setup()
        {
            _Sut = new Expression();
        }

        [Test]
        public void DayOfMonth_SetsDayOfMonthToSingleNumber_WhenANumberIsPassed()
        {
            int DayOfMonth = 10;
            var expected = "* * 10 * *";

            var result = _Sut.DayOfMonth(DayOfMonth).Build();

            result.Should().Be(expected);
        }

        [Test]
        public void DayOfMonth_SetsMultipleHourValues_WhenCalledMoreThanOnce()
        {
            int hour1 = 10;
            int hour2 = 12;
            var expected = "* * 10,12 * *";

            var result = _Sut.DayOfMonth(hour1).DayOfMonth(hour2).Build();

            result.Should().Be(expected);
        }

        [Test]
        public void DayOfMonth_SetsMultipleDayValues_WhenMixOfNumbersAndStrings()
        {
            var expected = "* * 4,8-11/2 * *";

            var result = _Sut.DayOfMonth(4).DayOfMonth("8-11/2").Build();

            result.Should().Be(expected);
        }

        [Test]
        public void DayOfMonth_AcceptsValue_WhenValueIsWeekday()
        {
            var expected = "* * W * *";

            var result = _Sut.DayOfMonth("W").Build();

            result.Should().Be(expected);
        }

        [Test]
        public void DayOfMonth_ThrowsArgumentOutOfRangeException_WhenHourIsGreaterThan31()
        {
            Assert.Throws(typeof(ArgumentOutOfRangeException), () => { _Sut.DayOfMonth(32); });
        }

        [Test]
        public void DayOfMonth_ThrowsArgumentOutOfRangeException_WhenHourIsZero()
        {
            Assert.Throws(typeof(ArgumentOutOfRangeException), () => { _Sut.DayOfMonth(0); });
        }

        [Test]
        public void DayOfMonth_ThrowsArgumentOutOfRangeException_WhenStepIsGreaterThan31()
        {
            Assert.Throws(typeof(ArgumentOutOfRangeException), () => { _Sut.DayOfMonth("4/32"); });
        }

        [Test]
        public void DayOfMonth_ThrowsArgumentOutOfRangeException_WhenNthIsUsed()
        {
            Assert.Throws(typeof(ArgumentOutOfRangeException), () => { _Sut.DayOfMonth("30#1"); });
        }
    }
}