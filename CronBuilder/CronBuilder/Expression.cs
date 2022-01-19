using System;

namespace CronBuilder
{
    public class Expression : IExpression
    {
        int _Minutes;
        int _Hours;

        public IExpression Minutes(int minutes)
        {
            _Minutes = minutes;
            return this;
        }

        public IExpression Hours(int hours)
        {
            _Hours = hours;
            return this;
        }

        public string Build()
        {
            var minutesSection = (_Minutes == 0) ? "*" : _Minutes.ToString();
            var hoursSection = (_Hours == 0) ? "*" : _Hours.ToString();
            return $"{minutesSection} {hoursSection} * * *";
        }
    }
}
