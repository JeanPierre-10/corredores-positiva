using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LP.Core.BLCorredores.Beans
{
    public class BSAvanceCoaseguro
    {
        public String Date { get; set; }
        public String Year { get; set; }
        public String Month { get; set; }
        public DataAmount AnnualData { get; set; }
        public DataAmount MonthlyData { get; set; }
    }
}
