using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CronBuilder
{
    public class ExpressionFactory
    {
        public IExpression EveryXMinutes(int minutes)
        {
            var result = new Expression();
            result.Minutes($"0/{minutes}");
            return result;
        }

        public IExpression EveryXHours(int hours)
        {
            var result = new Expression();
            result.Hours($"0/{hours}");
            return result;
        }

        public IExpression DailyAtTime(int hour, int minutes)
        {
            var result = new Expression();
            result.Minutes(minutes).Hours(hour).DayOfMonth("1/1");
            return result;
        }

        public IExpression WeekDaysAtTime(int hour, int minutes)
        {
            var result = new Expression();
            result.Minutes(minutes).Hours(hour).DayOfWeek("1-5");
            return result;
        }

        public IExpression MonthlyOnDay(int dayOfMonth, int hour, int minutes)
        {
            var result = new Expression();
            result.Minutes(minutes).Hours(hour).DayOfMonth(dayOfMonth).Months("1/1");
            return result;
        }

        public IExpression YearlyOnDay(int month, int dayOfMonth, int hour, int minutes)
        {
            var result = new Expression();
            result.Minutes(minutes).Hours(hour).DayOfMonth(dayOfMonth).Months(month);
            return result;
        }
    }
}
