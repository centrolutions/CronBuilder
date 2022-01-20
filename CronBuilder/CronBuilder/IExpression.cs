namespace CronBuilder
{
    public interface IExpression
    {
        IExpression Minutes(int minutes);
        IExpression Hours(int hours);

        string Build();
    }
}
