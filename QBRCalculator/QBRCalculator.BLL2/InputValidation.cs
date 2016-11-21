using QBRCalculator.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QBRCalculator.BLL
{
    public static class InputValidation
    {
        public static QBRResponse VaidateStats(QBRRequest request)
        {
            QBRResponse response = new QBRResponse();
            if(request.PassesCompleted < 0 || request.PassesAttempted < 0 || request.TouchdownPasses < 0 || request.Interceptions < 0)
            {
                response.Message = "QB stats cannot be negative (with the exception of passing yards).";
                response.ValidInput = false;
                return response;
            }
            if(request.PassesAttempted == 0)
            {
                response.Message = "QB Rating is undefined for players with zero pass attempts.";
                response.ValidInput = false;
                return response;
            }
            if (request.PassesCompleted > request.PassesAttempted)
            {
                response.Message = "Passes completed must be less than or equal to passes attempted.";
                response.ValidInput = false;
                return response;
            }
            if(request.TouchdownPasses > request.PassesCompleted)
            {
                response.Message = "Touchdowns must be less than or equal to completions.";
                response.ValidInput = false;
                return response;
            }
            if(request.Interceptions > request.PassesAttempted - request.PassesCompleted)
            {
                response.Message = "Interceptions must be less than or equal to the number of incompletions";
                response.ValidInput = false;
                return response;
            }
            if (request.TouchdownPasses + request.Interceptions >= request.PassesAttempted)
            {
                response.Message = "Touchdown passes plus interceptions must be less than passes attempted.";
                response.ValidInput = false;
                return response;
            }
            response.ValidInput = true;
            return response;
        }
    }
}
