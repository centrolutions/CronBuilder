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
    public class ExpressionFactoryTests
    {
        ExpressionFactory _Sut;

        [SetUp]
        public void Setup()
        {
            _Sut = new ExpressionFactory();
        }

        [Test]
        public void EveryXMinutes_ReturnsExpressionWithValueOf5_When5IsPassed()
        {
            var expression = _Sut.EveryXMinutes(5);

            expression.Build().Should().Be("0/5 * * * *");
        }

        [Test]
        public void EveryXHours_ReturnsExpressionWithValueOf2_When2IsPassed()
        {
            var expression = _Sut.EveryXHours(2);

            expression.Build().Should().Be("* 0/2 * * *");
        }

        [Test]
        public void DailyAtTime_ReturnsExpressionWithHourMinuteAndDay_WhenCalled()
        {
            var expression = _Sut.DailyAtTime(15, 10);

            expression.Build().Should().Be("10 15 1/1 * *");
        }

        [Test]
        public void WeekDaysAtTime_ReturnsExpressionWithHourMinuteAndWeekday_WhenCalled()
        {
            var expression = _Sut.WeekDaysAtTime(14, 30);

            expression.Build().Should().Be("30 14 * * 1-5");
        }

        [Test]
        public void MonthlyOnDay_ReturnsExpressionWithHourMinuteDayAndMonth_WhenCalled()
        {
            var expression = _Sut.MonthlyOnDay(21, 13, 45);

            expression.Build().Should().Be("45 13 21 1/1 *");
        }

        [Test]
        public void YearlyOnDay_ReturnsExpressionWithMonthDayHourAndMinute_WhenCalled()
        {
            var expression = _Sut.YearlyOnDay(5, 21, 20, 15);

            expression.Build().Should().Be("15 20 21 5 *");
        }
    }
}
