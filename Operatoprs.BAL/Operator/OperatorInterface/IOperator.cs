using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operatoprs.BAL.Operator
{
    public interface IOperator
    {
        List<short> pref { get; set; }
        string logo { get; set; }
        string nameOperator { get; set; }
        double percent { get; set; }
    }
}
