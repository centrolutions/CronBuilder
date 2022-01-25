using System.Collections.Generic;
using System.Linq;

namespace CronBuilder
{
    public class Expression : IExpression
    {
        List<SectionValue> _Minutes = new List<SectionValue>() { "*" };
        int _Hours;

        public IExpression Minutes(int minutes)
        {
            return Minutes(new SectionValue(minutes));
        }

        public IExpression Minutes(SectionValue value)
        {
            if (_Minutes.Count == 1 && _Minutes[0].IsStar)
                _Minutes.Clear();

            _Minutes.Add(value);
            return this;
        }

        public IExpression Hours(int hours)
        {
            _Hours = hours;
            return this;
        }

        public string Build()
        {
            var minutesSection = string.Join(",", _Minutes.Select(x => x.ToString()));
            var hoursSection = (_Hours == 0) ? "*" : _Hours.ToString();
            return $"{minutesSection} {hoursSection} * * *";
        }
    }
}
