using CronBuilder;
using FluentAssertions;
using NUnit.Framework;
using System;

namespace CronBuilderTests
{
    public class ExpressionMonthsTests
    {
        Expression _Sut;

        [SetUp]
        public void Setup()
        {
            _Sut = new Expression();
        }

        [Test]
        public void Months_SetsMonthsToSingleNumber_WhenANumberIsPassed()
        {
            int months = 10;
            var expected = "* * * 10 *";

            var result = _Sut.Months(months).Build();

            result.Should().Be(expected);
        }

        [Test]
        public void Months_SetsMultipleMonthValues_WhenCalledMoreThanOnce()
        {
            int month1 = 10;
            int month2 = 12;
            var expected = "* * * 10,12 *";

            var result = _Sut.Months(month1).Months(month2).Build();

            result.Should().Be(expected);
        }

        [Test]
        public void Months_SetsMultipleMonthValues_WhenMixOfNumbersAndStrings()
        {
            var expected = "* * * 4,8-11/2 *";

            var result = _Sut.Months(4).Months("8-11/2").Build();

            result.Should().Be(expected);
        }

        [Test]
        public void Months_ThrowsArgumentOutOfRangeException_WhenMonthIsGreaterThan12()
        {
            Assert.Throws(typeof(ArgumentOutOfRangeException), () => { _Sut.Months(13); });
        }

        [Test]
        public void Months_ThrowsArgumentOutOfRangeException_WhenStepIsGreaterThan12()
        {
            Assert.Throws(typeof(ArgumentOutOfRangeException), () => { _Sut.Months("4/13"); });
        }

        [Test]
        public void Months_ThrowsArgumentOutOfRangeException_WhenNthOrLastOrWeekdayOrQuestionIsUsed()
        {
            Assert.Throws(typeof(ArgumentOutOfRangeException), () => { _Sut.Months("4#1"); });
            Assert.Throws(typeof(ArgumentOutOfRangeException), () => { _Sut.Months("L"); });
            Assert.Throws(typeof(ArgumentOutOfRangeException), () => { _Sut.Months("W"); });
            Assert.Throws(typeof(ArgumentOutOfRangeException), () => { _Sut.Months("?"); });
        }
    }
}