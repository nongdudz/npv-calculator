using NPVCalculator.Server.Models;

namespace NPVCalculator.Server.Services.Calculator
{
    /// <summary>
    /// The calculator service interface class.
    /// </summary>
    public interface ICalculatorService
    {
        /// <summary>
        /// Calculates the net present value asynchronously.
        /// </summary>
        /// <param name="npvRequest">The net present value request.</param>
        /// <returns></returns>
        Task<NPVResponse> CalculateNPVAsync(NPVRequest npvRequest);

        /// <summary>
        /// Calculates the NPV with discount rate range asynchronously.
        /// </summary>
        /// <param name="npvRequest">The NPV request.</param>
        /// <returns></returns>
        Task<List<NPVRangeResponse>> CalculateNPVWithDiscountRateRangeAsync(NPVRequest npvRequest);
    }
}