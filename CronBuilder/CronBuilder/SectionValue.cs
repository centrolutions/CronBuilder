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
        public bool IsRange { get; private set; }
        public int Low { get; }
        public int High { get; }
        public bool HasStep { get; }
        public int Step { get; }

        public bool IsStar { get { return UnitType == SectionUnitType.Star; } }
        public bool IsQuestion { get { return UnitType == SectionUnitType.Question; } }
        public bool IsAbsolute { get { return UnitType == SectionUnitType.Absolute; } }
        public bool IsLast {  get {  return UnitType == SectionUnitType.Last;} }

        public SectionValue(int value) : this(value, SectionUnitType.Absolute) { }
        private SectionValue(int value, SectionUnitType unitType)
        {
            if (value < 0) throw new ArgumentOutOfRangeException(nameof(value), "Cannot be negative");

            Value = value;
            UnitType = unitType;
            IsWeekday = false;
            IsRange = false;
            Low = 0;
            High = 0;
            HasStep = false;
            Step = 0;
        }
        public SectionValue(string value)
        {
            if (value != "*" && value != "?" && !value.Contains("L") && !value.Contains("W") && !value.Contains("-") && !value.Contains("/"))
                throw new ArgumentException("Invalid characters found in the string.", nameof(value));

            UnitType = SectionUnitType.Absolute;
            Value = 0;
            IsWeekday = false;
            IsRange = false;
            Low = 0;
            High= 0;
            HasStep = false;
            Step = 0;

            if (value.Contains("/"))
            {
                HasStep = true;
                Step = ParseStep(value);
                value = value.Split('/')[0];
            }

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

            if (value.Contains("-") && !value.StartsWith("L"))
            {
                IsRange = true;
                (Low, High) = ParseRange(value);
            }

            if (int.TryParse(value, out var numVal))
                Value = numVal;
        }

        private int ParseStep(string value)
        {
            var values = value.Split('/');
            int.TryParse(values[1], out var val);
            return val;
        }

        private (int, int) ParseRange(string value)
        {
            var values = value.Split('-');
            int.TryParse(values[0], out var first);
            int.TryParse(values[1], out var second);

            if (first < second)
                return (first, second);
            else
                return (second, first);
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
            string result = null;
            if (IsStar)
                result = "*";

            if (IsQuestion)
                result = "?";
            
            if (IsLast)
            {
                if (IsWeekday)
                    result = "LW";

                if (Value < 0)
                    result = $"L{Value}";

                if (string.IsNullOrWhiteSpace(result))
                    result = "L";
            }

            if (IsWeekday && !IsLast)
                result = $"{Value}W";

            if (IsRange)
                result = $"{Low}-{High}";

            if (string.IsNullOrWhiteSpace(result))
                result = Value.ToString();

            if (HasStep)
                result += $"/{Step}";

            return result;
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
