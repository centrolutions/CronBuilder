using CronBuilder;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CronBuilderTests
{
    public class ExpressionMinutesTests
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
        public void Minutes_ThrowsArgumentOutOfRangeException_WhenResultingValueIsGreaterThan60()
        {
            Assert.Throws(typeof(ArgumentOutOfRangeException), () => { _Sut.Minutes(61); });
        }

        [Test]
        public void Minutes_ThrowsArgumentOutOfRangeException_WhenStepIsGreaterThan60()
        {
            Assert.Throws(typeof(ArgumentOutOfRangeException), () => { _Sut.Minutes("30/65"); });
        }

        [Test]
        public void Minutes_ThrowsArgumentOutOfRangeException_WhenNthOrLastOrWeekdayOrQuestionIsUsed()
        {
            Assert.Throws(typeof(ArgumentOutOfRangeException), () => { _Sut.Minutes("30#1"); });
            Assert.Throws(typeof(ArgumentOutOfRangeException), () => { _Sut.Minutes("L"); });
            Assert.Throws(typeof(ArgumentOutOfRangeException), () => { _Sut.Minutes("W"); });
            Assert.Throws(typeof(ArgumentOutOfRangeException), () => { _Sut.Minutes("?"); });
        }
    }
}
