namespace CronBuilder
{
    public interface IExpression
    {
        IExpression Minutes(int minutes);
        IExpression Minutes(SectionValue value);
        IExpression Hours(int hours);
        IExpression Hours(SectionValue value);

        string Build();
    }
}
