using NPVCalculator.Server.Models;
using NPVCalculator.Server.Services.Helpers;

namespace NPVCalculator.Server.Services.Calculator
{
    /// <summary>
    /// The calculator service.
    /// </summary>
    /// <seealso cref="NPVCalculator.Server.Services.Calculator.ICalculatorService" />
    public class CalculatorService : ICalculatorService
    {

        /// <summary>
        /// Calculates the net present value asynchronously.
        /// </summary>
        /// <param name="npvRequest">The net present value request.</param>
        /// <returns></returns>
        public async Task<NPVResponse> CalculateNPVAsync(NPVRequest npvRequest)
        {

            ValidateNPVRequest(npvRequest);

            var npv = new NetPresentValue(
                npvRequest.InitialInvestments,
                npvRequest.InterestRate,
                npvRequest.CashFlows);

            var result = await Task.Run(() => npv.Calculate());

            return result.CreateNPVResponse(npvRequest.InitialInvestments);
        }

        /// <summary>
        /// Calculates the NPV with discount rate range asynchronously.
        /// </summary>
        /// <param name="npvRequest">The NPV request.</param>
        /// <returns></returns>
        public async Task<List<NPVRangeResponse>> CalculateNPVWithDiscountRateRangeAsync(NPVRequest npvRequest)
        {

            ValidateNPVRequest(npvRequest);

            var npv = new NetPresentValue(
                npvRequest.InitialInvestments,
                npvRequest.InterestRate,
                npvRequest.CashFlows);

            var results = await Task.Run(() => npv.CalculateWithRange(npvRequest.DiscountRateRange.LowerDiscountRate,
                                                                 npvRequest.DiscountRateRange.UpperDiscountRate,
                                                                 npvRequest.DiscountRateRange.DiscountRateIncrement));

            return results.CreateNPVRangeResponse(npvRequest.InitialInvestments);
        }

        /// <summary>
        /// Validates the NPV request.
        /// </summary>
        /// <param name="nPVRequest">The n pv request.</param>
        /// <exception cref="System.ArgumentException">
        /// Initial investments must be greater than zero. - InitialInvestments
        /// or
        /// Cash flows cannot be null or empty. - CashFlows
        /// or
        /// Lower discount rate must be less than upper discount rate. - LowerDiscountRate
        /// or
        /// Discount rate increment must be greater than zero. - DiscountRateIncrement
        /// </exception>
        private void ValidateNPVRequest(NPVRequest nPVRequest)
        {
            if (nPVRequest.InitialInvestments <= 0)
            {
                throw new ArgumentException("Initial investments must be greater than zero.", nameof(nPVRequest.InitialInvestments));
            }
            if (nPVRequest.CashFlows == null || !nPVRequest.CashFlows.Any())
            {
                throw new ArgumentException("Cash flows cannot be null or empty.", nameof(nPVRequest.CashFlows));
            }
            if (nPVRequest.DiscountRateRange != null)
            {
                if (nPVRequest.DiscountRateRange.LowerDiscountRate >= nPVRequest.DiscountRateRange.UpperDiscountRate)
                {
                    throw new ArgumentException("Lower discount rate must be less than upper discount rate.", nameof(nPVRequest.DiscountRateRange.LowerDiscountRate));
                }
                if (nPVRequest.DiscountRateRange.DiscountRateIncrement <= 0)
                {
                    throw new ArgumentException("Discount rate increment must be greater than zero.", nameof(nPVRequest.DiscountRateRange.DiscountRateIncrement));
                }
            }
        }
    }
}