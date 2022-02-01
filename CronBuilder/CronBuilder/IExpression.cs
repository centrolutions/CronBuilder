namespace CronBuilder
{
    public interface IExpression
    {
        IExpression Minutes(int minutes);
        IExpression Minutes(SectionValue value);
        IExpression Hours(int hours);
        IExpression Hours(SectionValue value);
        IExpression DayOfMonth(int day);
        IExpression DayOfMonth(SectionValue value);
        IExpression Months(int month);
        IExpression Months(SectionValue value);
        IExpression DayOfWeek(int day);
        IExpression DayOfWeek(SectionValue value);

        string Build();
    }
}
