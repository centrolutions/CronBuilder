using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CronBuilder
{
    public struct SectionValue
    {
        public int Value { get; }
        public SectionUnitType UnitType { get; }
        public bool IsWeekday { get; private set; }

        public bool IsStar { get { return UnitType == SectionUnitType.Star; } }
        public bool IsQuestion { get { return UnitType == SectionUnitType.Question; } }
        public bool IsAbsolute { get { return UnitType == SectionUnitType.Absolute; } }
        public bool IsLast {  get {  return UnitType == SectionUnitType.Last;} }

        public SectionValue(int value) : this(value, SectionUnitType.Absolute) { }
        private SectionValue(int value, SectionUnitType unitType)
        {
            Value = value;
            UnitType = unitType;
            IsWeekday = false;
        }
        public SectionValue(string value)
        {
            if (value != "*" && value != "?" && !value.Contains("L") && !value.Contains("W"))
                throw new ArgumentException("Invalid characters found in the string.", nameof(value));

            UnitType = SectionUnitType.Absolute;
            Value = 0;
            IsWeekday = false;

            if (value == "*")
                UnitType = SectionUnitType.Star;

            if (value == "?")
                UnitType = SectionUnitType.Question;

            if (value.Contains("L"))
            {
                UnitType = SectionUnitType.Last;
                Value = ParseLastValue(value);
            }

            if (value.Contains("W"))
            {
                IsWeekday = true;
                Value = ParseWeekValue(value);
            }
        }

        private int ParseWeekValue(string value)
        {
            var num = value.Replace("W", "");
            int.TryParse(num, out var intValue);
            return intValue;
        }

        private int ParseLastValue(string value)
        {
            var num = value.Replace("L", "");
            int.TryParse(num, out var intValue);
            return intValue;
        }

        public override string ToString()
        {
            if (IsStar)
                return "*";
            if (IsQuestion)
                return "?";
            
            if (IsLast)
            {
                if (IsWeekday)
                    return "LW";

                if (Value < 0)
                    return $"L{Value}";

                return "L";
            }

            if (IsWeekday)
                return $"{Value}W";

            return Value.ToString();
        }

        public static implicit operator SectionValue(string val)
        {
            return new SectionValue(val);
        }
        public static implicit operator SectionValue(int val)
        {
            return new SectionValue(val);
        }
    }
}
