using System;
using System.Collections.Generic;
using System.Linq;

namespace CronBuilder
{
    public class Expression : IExpression
    {
        List<SectionValue> _Minutes = new List<SectionValue>() { "*" };
        List<SectionValue> _Hours = new List<SectionValue>() { "*" };
        List<SectionValue> _DaysOfMonth = new List<SectionValue> { "*" };
        List<SectionValue> _Months = new List<SectionValue>() { "*" };
        List<SectionValue> _DaysOfWeek = new List<SectionValue>() { "*" };

        public IExpression Minutes(int minutes)
        {
            return Minutes(new SectionValue(minutes));
        }

        public IExpression Minutes(SectionValue value)
        {
            if (!IsValidMinutes(value))
                throw new ArgumentOutOfRangeException(nameof(value));

            if (_Minutes.Count == 1 && _Minutes[0].IsStar)
                _Minutes.Clear();

            _Minutes.Add(value);
            return this;
        }

        public IExpression Hours(int hours)
        {
            return Hours(new SectionValue(hours));            
        }

        public IExpression Hours(SectionValue value)
        {
            if (!IsValidHours(value))
                throw new ArgumentOutOfRangeException(nameof(value));

            if (_Hours.Count == 1 && _Hours[0].IsStar)
                _Hours.Clear();

            _Hours.Add(value);
            return this;
        }

        public IExpression DayOfMonth(int day)
        {
            return DayOfMonth(new SectionValue(day));
        }

        public IExpression DayOfMonth(SectionValue value)
        {
            if (!IsValidDayOfMonth(value))
                throw new ArgumentOutOfRangeException(nameof(value));

            if (_DaysOfMonth.Count == 1 && _DaysOfMonth[0].IsStar)
                _DaysOfMonth.Clear();

            _DaysOfMonth.Add(value);
            return this;
        }

        public IExpression Months(int month)
        {
            return Months(new SectionValue(month));
        }

        public IExpression Months(SectionValue value)
        {
            if (!IsValidMonth(value))
                throw new ArgumentOutOfRangeException(nameof(value));

            if (_Months.Count == 1 && _Months[0].IsStar)
                _Months.Clear();

            _Months.Add(value);
            return this;
        }

        public IExpression DayOfWeek(int day)
        {
            return DayOfWeek(new SectionValue(day));
        }

        public IExpression DayOfWeek(SectionValue value)
        {
            if (!IsValidDayOfWeek(value))
                throw new ArgumentOutOfRangeException(nameof(value));

            if (_DaysOfWeek.Count == 1 && _DaysOfWeek[0].IsStar)
                _DaysOfWeek.Clear();

            _DaysOfWeek.Add(value);
            return this;
        }

        public string Build()
        {
            var minutesSection = string.Join(",", _Minutes.Select(x => x.ToString()));
            var hoursSection = string.Join(",", _Hours.Select(x => x.ToString()));
            var daysOfMonthSection = string.Join(",", _DaysOfMonth.Select(x => x.ToString()));
            var monthsSection = string.Join(",", _Months.Select(x => x.ToString()));
            var daysOfWeekSection = string.Join(",", _DaysOfWeek.Select(x => x.ToString()));
            return $"{minutesSection} {hoursSection} {daysOfMonthSection} {monthsSection} {daysOfWeekSection}";
        }



        private bool IsValidMinutes(SectionValue value)
        {
            return value.Value <= 60 &&
                value.Step <= 60 &&
                value.Nth == 0 &&
                !value.IsLast &&
                !value.IsWeekday &&
                !value.IsQuestion;
        }

        private bool IsValidHours(SectionValue value)
        {
            return value.Value <= 23 &&
                value.Step <= 23 &&
                value.Nth == 0 &&
                !value.IsLast &&
                !value.IsWeekday &&
                !value.IsQuestion;
        }

        private bool IsValidDayOfMonth(SectionValue value)
        {
            return value.Value <= 31 &&
                (value.Value > 0 || value.IsWeekday || (value.Low > 0 && value.High <= 31)) &&
                value.Step <= 31 &&
                value.Nth == 0;
        }

        private bool IsValidMonth(SectionValue value)
        {
            return value.Value <= 12 &&
                value.Step <= 12 &&
                value.Nth == 0 &&
                !value.IsLast &&
                !value.IsWeekday &&
                !value.IsQuestion;
        }

        private bool IsValidDayOfWeek(SectionValue value)
        {
            return value.Value <= 7 &&
                value.Step <= 7 &&
                !value.IsWeekday &&
                value.Nth <= 5;
        }
    }
}
