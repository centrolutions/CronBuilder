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
    public class SectionValueTests
    {
        [Test]
        public void OperatorFromString_CreatesStar_WhenStringIsAstrisk()
        {
            SectionValue value = "*";

            value.IsStar.Should().BeTrue();
        }

        [Test]
        public void OperatorFromString_CreatesQuestion_WhenValueIsQuestionMark()
        {
            SectionValue value = "?";

            value.IsQuestion.Should().BeTrue();
        }

        [Test]
        public void OperatorFromString_CreatesLast_WhenValueContainsL()
        {
            SectionValue value = "L-1";

            value.IsLast.Should().BeTrue();
        }

        [Test]
        public void OperatorFromString_CreatesLastWithValue_WhenValueIsNumeric()
        {
            SectionValue value = "4L";

            value.Value.Should().Be(4);
        }

        [Test]
        public void OperatorFromString_CreatesWeekValue_WhenValueContainsW()
        {
            SectionValue value = "3W";

            value.Value.Should().Be(3);
            value.IsWeekday.Should().Be(true);
        }

        [Test]
        public void OperatorFromString_CreatesLastWeekValue_WhenValueIsLW()
        {
            SectionValue value = "LW";

            value.IsLast.Should().BeTrue();
            value.IsWeekday.Should().BeTrue();
        }

        [Test]
        public void OperatorFromString_ThrowsException_WhenValueIsNotQuestionMarkOrAstrisk()
        {
            Assert.Throws(typeof(ArgumentException), () => { SectionValue value = "a"; });
        }

        

        [Test]
        public void OperatorFromInt_CreatesAbsolute_WhenValueIsGreaterThanZero()
        {
            SectionValue value = 2;

            value.IsAbsolute.Should().BeTrue();
        }

        [Test]
        public void Constructor_CreatesAbsoluteWithValue_WhenValuesIsGreaterThanZero()
        {
            int expectedValue = 20;
            var sut = new SectionValue(expectedValue);

            sut.Value.Should().Be(expectedValue);
        }


        [Test]
        public void ToString_ReturnsNumber_WhenValueIsSetToInteger()
        {
            var sut = new SectionValue(10);

            sut.ToString().Should().Be("10");
        }

        [Test]
        public void ToString_ReturnsAstrisk_WhenIsStar()
        {
            var sut = new SectionValue("*");

            sut.ToString().Should().Be("*");
        }

        [Test]
        public void ToString_ReturnsQuestionMark_WhenIsQuestion()
        {
            var sut = new SectionValue("?");

            sut.ToString().Should().Be("?");
        }

        [Test]
        public void ToString_ReturnsL_WhenIsLast()
        {
            var sut = new SectionValue("L");

            sut.ToString().Should().Be("L");
        }

        [Test]
        public void ToString_ReturnsLW_WhenIsLastAndIsWeek()
        {
            var sut = new SectionValue("LW");

            sut.ToString().Should().Be("LW");
        }

        [Test]
        public void ToString_ReturnsLAndValue_WhenLastAndNegativeValue()
        {
            var sut = new SectionValue("L-2");

            sut.ToString().Should().Be("L-2");
        }

        [Test]
        public void ToString_ReturnsValueAndW_WhenIsWeekNumber()
        {
            var sut = new SectionValue("3W");

            sut.ToString().Should().Be("3W");
        }
    }
}
