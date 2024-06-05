namespace NPVCalculator.Server.Models
{
    /// <summary>
    /// The net present value class.
    /// </summary>
    public class NetPresentValue(
        decimal initialInvestments,
        decimal interestRate,
        List<decimal> cashFlows)
    {
        /// <summary>
        /// The initial investments
        /// </summary>
        private readonly decimal _initialInvestments = initialInvestments > 0 ? initialInvestments : throw new ArgumentNullException(nameof(initialInvestments), "Initial investment should be greater than zero.");

        /// <summary>
        /// The interest rate
        /// </summary>
        private readonly decimal _interestRate = interestRate > 0 ? (interestRate / 100) : throw new ArgumentNullException(nameof(interestRate), "Interest rate should be greater than zero.");

        /// <summary>
        /// The cash flows
        /// </summary>
        private readonly List<decimal> _cashFlows = cashFlows != null && cashFlows.Count > 0 ? cashFlows : throw new ArgumentNullException(nameof(cashFlows), "Cash flows cannot be null or empty.");

        /// <summary>
        /// Calculates the net present value.
        /// </summary>
        /// <returns>The calculated net present values and it's cash flow streams.</returns>
        public (decimal NPV, Dictionary<int, (decimal CashFlow, decimal Value)> CashFlowStream) Calculate()
        {
            return CalculateWithCashFlowStream(_interestRate);
        }

        /// <summary>
        /// Calculates the net present value with discount rate range.
        /// </summary>
        /// <param name="lowerDiscountRate">The lower discount rate.</param>
        /// <param name="upperDiscountRate">The upper discount rate.</param>
        /// <param name="discountRateIncrement">The discount rate increment.</param>
        /// <returns>The calculated net present values and it's cash flow streams.</returns>
        /// <exception cref="System.ArgumentException">
        /// Lower discount rate must be less than upper discount rate.
        /// or
        /// Discount rate increment must be greater than zero.
        /// </exception>
        public List<(decimal Rate, decimal NPV, Dictionary<int, (decimal CashFlow, decimal Value)> CashFlowStream)> CalculateWithRange(decimal lowerDiscountRate, decimal upperDiscountRate, decimal discountRateIncrement)
        {
            if (lowerDiscountRate >= upperDiscountRate)
            {
                throw new ArgumentException("Lower discount rate must be less than upper discount rate.");
            }
            if (discountRateIncrement <= 0)
            {
                throw new ArgumentException("Discount rate increment must be greater than zero.");
            }

            lowerDiscountRate /= 100;
            upperDiscountRate /= 100;
            discountRateIncrement /= 100;

            var results = new List<(decimal Rate, decimal NPV, Dictionary<int, (decimal CashFlow, decimal Value)> CashFlowStream)>();

            for (decimal rate = lowerDiscountRate; rate <= upperDiscountRate; rate += discountRateIncrement)
            {
                var (npv, cashFlowStream) = CalculateWithCashFlowStream(rate);
                results.Add((rate * 100, npv, cashFlowStream));
            }

            return results;
        }

        /// <summary>
        /// Calculates net present value.
        /// </summary>
        /// <param name="rate">The rate.</param>
        /// <returns>The calculated net present values and it's cash flow streams.</returns>
        private (decimal NPV, Dictionary<int, (decimal CashFlow, decimal Value)> CashFlowStream) CalculateWithCashFlowStream(decimal rate)
        {
            decimal npv = -_initialInvestments;
            Dictionary<int, (decimal CashFlow, decimal Value)> discountedCashFlows = new Dictionary<int, (decimal CashFlow, decimal Value)>();

            for (int t = 0; t < _cashFlows.Count; t++)
            {
                decimal discountedCashFlow = _cashFlows[t] / (decimal)Math.Pow((double)(1 + rate), t + 1);
                discountedCashFlows.Add(t + 1, (_cashFlows[t], Math.Round(discountedCashFlow, 2)));
                npv += discountedCashFlow;
            }

            npv = Math.Round(npv, 2);
            return (npv, discountedCashFlows);
        }
    }
}