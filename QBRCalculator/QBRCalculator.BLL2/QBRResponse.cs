using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QBRCalculator.BLL
{
    public class QBRResponse
    {
        public decimal Rating { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public bool ValidInput { get; set; }
    }
}
