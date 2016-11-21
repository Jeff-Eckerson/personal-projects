using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QBRCalculator.BLL
{
    public class QBRRequest
    {
        public string Name { get; set; }
        public decimal PassesAttempted { get; set; }
        public decimal PassesCompleted { get; set; }
        public decimal PassingYards { get; set; }
        public decimal TouchdownPasses { get; set; }
        public decimal Interceptions { get; set; }
    }
}
