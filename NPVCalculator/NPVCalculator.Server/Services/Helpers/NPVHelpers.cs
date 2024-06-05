using NPVCalculator.Server.Models;

namespace NPVCalculator.Server.Services.Helpers
{
    public static class NPVHelpers
    {
        public static NPVResponse CreateNPVResponse(this (decimal NPV, Dictionary<int, (decimal CashFlow, decimal Value)> CashFlowStream) calculationResult, decimal initialInvestment)
        {
            var cashFlowSeries = new List<CashFlowSeries>
        {
            new CashFlowSeries
            {
                Period = 0,
                CashFlow = -initialInvestment,
                PresentValue = -initialInvestment
            }
        };

            cashFlowSeries.AddRange(calculationResult.CashFlowStream.Select(kv => new CashFlowSeries
            {
                Period = kv.Key,
                CashFlow = kv.Value.CashFlow,
                PresentValue = kv.Value.Value
            }));

            return new NPVResponse
            {
                CalculatedNPV = calculationResult.NPV,
                CashFlowSeries = cashFlowSeries
            };
        }

        public static List<NPVRangeResponse> CreateNPVRangeResponse(this List<(decimal Rate, decimal NPV, Dictionary<int, (decimal CashFlow, decimal Value)> CashFlowStream)> results, decimal initialInvestment)
        {
            return results.Select(result => new NPVRangeResponse
            {
                Rate = result.Rate,
                CalculatedNPV = result.NPV,
                CashFlowSeries = new List<CashFlowSeries>
            {
                new CashFlowSeries
                {
                    Period = 0,
                    CashFlow = -initialInvestment,
                    PresentValue = -initialInvestment
                }
            }.Concat(result.CashFlowStream.Select(kv => new CashFlowSeries
            {
                Period = kv.Key,
                CashFlow = kv.Value.CashFlow,
                PresentValue = kv.Value.Value
            })).ToList()
            }).ToList();
        }
    }
}