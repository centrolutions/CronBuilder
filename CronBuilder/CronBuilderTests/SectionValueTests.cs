﻿using CronBuilder;
using FluentAssertions;
using NUnit.Framework;
using System;

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
        public void OperatorFromString_CreatesRange_WhenValueIsTwoNumbersDividedByDash()
        {
            SectionValue value = "1-5";

            value.IsRange.Should().BeTrue();
        }

        [Test]
        public void OperatorFromString_CreatesRangeWithLowAndHigh_WhenValueIsTwoNumbersDividedByDash()
        {
            SectionValue value = "10-25";

            value.IsRange.Should().BeTrue();
            value.Low.Should().Be(10);
            value.High.Should().Be(25);
        }

        [Test]
        public void OperatorFromString_CreatesStarExpressionWithStep_WhenValueIncludesAstriskAndSlash()
        {
            SectionValue value = "*/5";

            value.IsStar.Should().BeTrue();
            value.HasStep.Should().BeTrue();
            value.Step.Should().Be(5);
        }

        [Test]
        public void OperatorFromString_CreatesRangeWithStep_WhenValueIncludesDashAndSlash()
        {
            SectionValue value = "0-6/2";

            value.IsRange.Should().BeTrue();
            value.Low.Should().Be(0);
            value.High.Should().Be(6);
            value.HasStep.Should().BeTrue();
            value.Step.Should().Be(2);
        }

        [Test]
        public void OperatorFromString_CreatesValueWithStep_WhenValueIncludesTowNumbersSeparatedBySlash()
        {
            SectionValue value = "10/2";

            value.Value.Should().Be(10);
            value.Step.Should().Be(2);
            value.HasStep.Should().BeTrue();
        }

        [Test]
        public void OperatorFromString_CreatesValueWithStep_WhenValueIncludesMonthNameAndSlash()
        {
            SectionValue value = "JAN/3";

            value.Value.Should().Be(1);
            value.Step.Should().Be(3);
            value.HasStep.Should().BeTrue();
        }

        [Test]
        public void OperatorFromString_CreatesValueWithNth_WhenValueIncludesPoundSign()
        {
            SectionValue value = "6#2"; //second saturday

            value.Value.Should().Be(6);
            value.Nth.Should().Be(2);
        }

        [Test]
        public void OperatorFromString_CreatesValueWithDayNumber_WhenValueIncludesDayName()
        {
            SectionValue value = "MON#3"; //third monday

            value.Value.Should().Be(1);
            value.Nth.Should().Be(3);
        }

        [Test]
        public void OperatorFromString_CreatesRangeWithDayNumber_WhenValueIncludesDayNames()
        {
            SectionValue value = "MON-FRI";

            value.IsRange.Should().BeTrue();
            value.Low.Should().Be(1);
            value.High.Should().Be(5);
        }

        [Test]
        public void OperatorFromString_CreatesNumericValue_WhenValueIncludesDayName()
        {
            SectionValue value = "THU";

            value.Value.Should().Be(4);
        }

        [Test]
        public void OperatorFromString_CreatesNumericValue_WhenValueIncludesMonthName()
        {
            SectionValue value = "SEP";

            value.Value.Should().Be(9);
        }

        [Test]
        public void OperatorFromString_CreatesRange_WhenValueIncludesMonthNames()
        {
            SectionValue value = "JUL-DEC";

            value.IsRange.Should().BeTrue();
            value.Low.Should().Be(7);
            value.High.Should().Be(12);
        }

        [Test]
        public void OperatorFromString_ThrowsArgumentException_WhenValueIsNotQuestionMarkOrAstrisk()
        {
            Assert.Throws(typeof(ArgumentException), () => { SectionValue value = "a"; });
        }

        [Test]
        public void OperatorFromString_ThrowsArgumentException_WhenValueHasSlashButAMisingNumber()
        {
            Assert.Throws(typeof(ArgumentException), () => { SectionValue value = "3/"; });
            Assert.Throws(typeof(ArgumentException), () => { SectionValue value = "/5"; });
            Assert.Throws(typeof(ArgumentException), () => { SectionValue value = "/"; });
        }

        [Test]
        public void OperatorFromString_ThrowsArgumentException_WhenStepIsNotANumber()
        {
            Assert.Throws(typeof(ArgumentException), () => { SectionValue value = "10/a"; });
        }

        [Test]
        public void OperatorFromString_ThrowsArgumentException_WhenStepIsLessThan1()
        {
            Assert.Throws(typeof(ArgumentException), () => { SectionValue value = "10/0"; });
        }

        [Test]
        public void OperatorFromString_ThrowsArgumentException_WhenNthIsMissingNumber()
        {
            Assert.Throws(typeof(ArgumentException), () => { SectionValue value = "6#"; });
            Assert.Throws(typeof(ArgumentException), () => { SectionValue value = "#2"; });
            Assert.Throws(typeof(ArgumentException), () => { SectionValue value = "#"; });
        }

        [Test]
        public void OperatorFromString_ThrowsArgumentException_WhenRangeIsMissingOneOrBothNumbers()
        {
            Assert.Throws(typeof(ArgumentException), () => { SectionValue value = "1-"; });
            Assert.Throws(typeof(ArgumentException), () => { SectionValue value = "-5"; });
            Assert.Throws(typeof(ArgumentException), () => { SectionValue value = "-"; });
        }



        [Test]
        public void OperatorFromInt_CreatesAbsolute_WhenValueIsGreaterThanZero()
        {
            SectionValue value = 2;

            value.IsAbsolute.Should().BeTrue();
        }

        [Test]
        public void OperatorFromInt_ThrowsException_WhenValueIsLessThanZero()
        {
            Assert.Throws(typeof(ArgumentOutOfRangeException), () => { SectionValue value = -1; });
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

        [Test]
        public void ToString_ReturnsW_WhenIsWeekdayAndValueIsZero()
        {
            var sut = new SectionValue("W");

            sut.ToString().Should().Be("W");
        }

        [Test]
        public void ToString_ReturnsTwoNumbersSeparatedByDash_WhenIsRangeInCorrectOrder()
        {
            var sut = new SectionValue("9-12");

            sut.ToString().Should().Be("9-12");
        }

        [Test]
        public void ToString_ReturnsTwoNumbersSeparatedByDash_WhenIsRangeInWrongOrder()
        {
            var sut = new SectionValue("30-7");

            sut.ToString().Should().Be("7-30");
        }

        [Test]
        public void ToString_ReturnsStarSlashAndNumber_WhenIsStarAndHasStep()
        {
            var sut = new SectionValue("*/10");

            sut.ToString().Should().Be("*/10");
        }

        [Test]
        public void ToString_ReturnsThreeNumbersSeparatedByDashAndSlash_WhenIsRangeAndHasStep()
        {
            var sut = new SectionValue("0-6/2");

            sut.ToString().Should().Be("0-6/2");
        }
        
        [Test]
        public void ToString_ReturnsNumbersSeparatedByPoundSign_WhenNthIsUsed()
        {
            var sut = new SectionValue("6#3");

            sut.ToString().Should().Be("6#3");
        }
    }
}
