using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LP.Core.ADCorredores.Entity
{
    public class BEAvanceCoaseguro
    {
        public String Date { get; set; }
        public String Year { get; set; }
        public String Month { get; set; }
        public DataAvance AnnualData { get; set; }
        public DataAvance MonthlyData { get; set; }

    }
}
