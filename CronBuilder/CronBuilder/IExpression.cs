using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CronBuilder
{
    public interface IExpression
    {
        IExpression Minutes(int minutes);
        IExpression Hours(int hours);

        string Build();
    }
}
