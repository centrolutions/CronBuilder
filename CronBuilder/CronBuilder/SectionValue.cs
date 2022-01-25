using System;
using System.Collections.Generic;

namespace CronBuilder
{
    public struct SectionValue
    {
        private static readonly Dictionary<string, int> _Days = new Dictionary<string, int>()
        {
            { "SUN", 0 },
            { "MON", 1 },
            { "TUE", 2 },
            { "WED", 3 },
            { "THU", 4 },
            { "FRI", 5 },
            { "SAT", 6 },
        };

        private static readonly Dictionary<string, int> _Months = new Dictionary<string, int>()
        {
            { "JAN", 1 }, { "FEB", 2 },
            { "MAR", 3 }, { "APR", 4 },
            { "MAY", 5 }, { "JUN", 6 },
            { "JUL", 7 }, { "AUG", 8 },
            { "SEP", 9 }, { "OCT", 10 },
            { "NOV", 11 }, { "DEC", 12 }
        };

        public int Value { get; }
        public SectionUnitType UnitType { get; }
        public bool IsWeekday { get; private set; }
        public bool IsRange { get; private set; }
        public int Low { get; }
        public int High { get; }
        public bool HasStep { get; }
        public int Step { get; }
        public int Nth { get; }

        public bool IsStar { get { return UnitType == SectionUnitType.Star; } }
        public bool IsQuestion { get { return UnitType == SectionUnitType.Question; } }
        public bool IsAbsolute { get { return UnitType == SectionUnitType.Absolute; } }
        public bool IsLast { get { return UnitType == SectionUnitType.Last; } }

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
            Nth = 0;
        }
        public SectionValue(string value)
        {
            if (value != "*" && value != "?" && !value.Contains("L")
                    && !value.Contains("W") && !value.Contains("-")
                    && !value.Contains("/") && !value.Contains("#")
                    && !_Days.ContainsKey(value) && !_Months.ContainsKey(value))
                throw new ArgumentException("Invalid characters found in the string.", nameof(value));

            UnitType = SectionUnitType.Absolute;
            Value = 0;
            IsWeekday = false;
            IsRange = false;
            Low = 0;
            High = 0;
            HasStep = false;
            Step = 0;
            Nth = 0;

            if (value.Contains("/"))
            {
                if (!IsValidStep(value))
                    throw new ArgumentException("Invalid step expression.", nameof(value));
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
                if (!IsValidRange(value))
                    throw new ArgumentException("Invalid range expression.", nameof(value));
                IsRange = true;
                (Low, High) = ParseRange(value);
            }

            if (value.Contains("#"))
            {
                if (!IsValidNth(value))
                    throw new ArgumentException("Invalid Nth expression", nameof(value));
                Nth = ParseNthValue(value);
                value = value.Split('#')[0];
            }

            if (int.TryParse(value, out var numVal))
                Value = numVal;
            else if (_Days.ContainsKey(value))
                Value = _Days[value];
            else if (_Months.ContainsKey(value))
                Value = _Months[value];
        }

        private bool IsValidNth(string value)
        {
            var values = value.Split('#');
            if (values.Length < 2 || values.Length > 2 || string.IsNullOrWhiteSpace(values[0]) || string.IsNullOrWhiteSpace(values[1]))
                return false;

            return true;
        }

        private int ParseNthValue(string value)
        {
            var values = value.Split('#');
            int.TryParse(values[1], out var val);
            return val;
        }

        private bool IsValidStep(string value)
        {
            var values = value.Split('/');
            if (values.Length < 2 || values.Length > 2 || string.IsNullOrWhiteSpace(values[0]) || string.IsNullOrWhiteSpace(values[1]))
                return false;

            return true;
        }
        private int ParseStep(string value)
        {
            var values = value.Split('/');
            int.TryParse(values[1], out var val);
            return val;
        }

        private bool IsValidRange(string value)
        {
            var values = value.Split('-');
            if (values.Length < 2 || values.Length > 2 || string.IsNullOrWhiteSpace(values[0]) || string.IsNullOrWhiteSpace(values[1]))
                return false;

            return true;
        }

        private (int, int) ParseRange(string value)
        {
            var values = value.Split('-');
            if (!int.TryParse(values[0], out var first))
            {
                if (_Days.ContainsKey(values[0]))
                    first = _Days[values[0]];

                if (_Months.ContainsKey(values[0]))
                    first = _Months[values[0]];
            }

            if (!int.TryParse(values[1], out var second))
            {
                if (_Days.ContainsKey(values[1]))
                    second = _Days[values[1]];

                if (_Months.ContainsKey(values[1]))
                    second = _Months[values[1]];
            }

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
