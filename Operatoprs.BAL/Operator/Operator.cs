using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operatoprs.BAL.Operator
{
    public class Operator : IOperator
    {
        public string logo
        {
            get;
            set;
        }

        public string nameOperator
        {
            get;
            set;
        }

        public double percent
        {
            get;
            set;
        }

        public List<short> pref
        {
            get;
            set;
        }
        public Operator()
        {
            pref = new List<short>();
        }
    }
}
