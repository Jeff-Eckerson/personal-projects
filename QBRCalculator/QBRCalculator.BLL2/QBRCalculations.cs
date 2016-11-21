using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QBRCalculator.BLL
{
    public static class QBRCalculations
    {
        public static QBRResponse CalculateQBR(QBRRequest request)
        {
            QBRResponse response = InputValidation.VaidateStats(request);
            if (response.ValidInput)
            {
                response.Name = request.Name;
                response.Rating = SumValues(request);
                response.Message = $"{response.Name}'s QB Rating is: {response.Rating}";
                return response;
            }
            else
            {
                return response;
            }
        }

        public static decimal SumValues(QBRRequest request)
        {
            decimal passAttempts = request.PassesAttempted;
            decimal first = WeightedValueCpA(request.PassesCompleted, passAttempts);
            decimal second = WeightedValueAYGpA(request.PassingYards, passAttempts);
            decimal third = WeightedValueTDP(request.TouchdownPasses, passAttempts);
            decimal fourth = WeightedValueInterceptions(request.Interceptions, passAttempts);
            decimal QBR = (first + second + third + fourth) / 6 * 100;
            return Math.Round(QBR, 1);
        }

        public static decimal WeightedValueCpA(decimal completions, decimal passAttempts)
        {
            decimal percentage = completions / passAttempts * 100;
            decimal result = (percentage - 30) * .05M;
            return result > 0 ? Math.Min(2.375M, result) : Math.Max(0, result);
        }

        public static decimal WeightedValueAYGpA(decimal passingYards, decimal passAttempts)
        {
            decimal ratio = passingYards / passAttempts;
            decimal result = (ratio - 3) * .25M;
            return result > 0 ? Math.Min(2.375M, result) : Math.Max(0, result);
        }

        public static decimal WeightedValueTDP(decimal touchdownPasses, decimal passAttempts)
        {
            decimal percentage = touchdownPasses / passAttempts * 100;
            decimal result = percentage * .2M;
            return Math.Min(result, 2.375M);
        }

        public static decimal WeightedValueInterceptions(decimal interceptions, decimal passAttempts)
        {
            decimal percentage = interceptions / passAttempts * 100;
            decimal result = 2.375M - (percentage * .25M);
            return Math.Max(result, 0);
        }
    }
}
