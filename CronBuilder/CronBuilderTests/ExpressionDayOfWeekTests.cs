using CronBuilder;
using FluentAssertions;
using NUnit.Framework;
using System;

namespace CronBuilderTests
{
    public class ExpressionDayOfWeekTests
    {
        Expression _Sut;

        [SetUp]
        public void Setup()
        {
            _Sut = new Expression();
        }

        [Test]
        public void DayOfWeek_SetsDayOfWeekToSingleNumber_WhenANumberIsPassed()
        {
            int DayOfWeek = 4;
            var expected = "* * * * 4";

            var result = _Sut.DayOfWeek(DayOfWeek).Build();

            result.Should().Be(expected);
        }

        [Test]
        public void DayOfWeek_SetsMultipleDayValues_WhenCalledMoreThanOnce()
        {
            int day1 = 0;
            int day2 = 4;
            var expected = "* * * * 0,4";

            var result = _Sut.DayOfWeek(day1).DayOfWeek(day2).Build();

            result.Should().Be(expected);
        }

        [Test]
        public void DayOfWeek_SetsMultipleDayValues_WhenMixOfNumbersAndStrings()
        {
            var expected = "* * * * 0,1-4/2";

            var result = _Sut.DayOfWeek(0).DayOfWeek("1-4/2").Build();

            result.Should().Be(expected);
        }

        [Test]
        public void DayOfWeek_SetsNthValue_WhenValueUsesPoundSign()
        {
            var expected = "* * * * 6#2";

            var result = _Sut.DayOfWeek("6#2").Build();

            result.Should().Be(expected);
        }

        [Test]
        public void DayOfWeek_ThrowsArgumentOutOfRangeException_WhenDayIsGreaterThan7()
        {
            Assert.Throws(typeof(ArgumentOutOfRangeException), () => { _Sut.DayOfWeek(8); });
        }

        [Test]
        public void DayOfWeek_ThrowsArgumentOutOfRangeException_WhenStepIsGreaterThan7()
        {
            Assert.Throws(typeof(ArgumentOutOfRangeException), () => { _Sut.DayOfWeek("4/8"); });
        }

        [Test]
        public void DayOfWeek_ThrowsArgumentOutOfRangeException_WhenWeekdayIsUsed()
        {
            Assert.Throws(typeof(ArgumentOutOfRangeException), () => { _Sut.DayOfWeek("W"); });
        }

        [Test]
        public void DayOfWeek_ThrowsArgumentOutOfRangeException_WhenNthGreaterThan5()
        {
            Assert.Throws(typeof(ArgumentOutOfRangeException), () => { _Sut.DayOfWeek("3#6"); });
        }
    }
}