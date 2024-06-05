namespace NPVCalculator.Server.Models
{
    public class NPVRequest
    {
        public decimal InitialInvestments { get; set; }
        public decimal InterestRate { get; set; }
        public List<decimal> CashFlows { get; set; }
        public DiscountRateRange? DiscountRateRange { get; set; }
    }

    public class DiscountRateRange
    {

        public decimal LowerDiscountRate { get; set; }
        public decimal UpperDiscountRate { get; set; }
        public decimal DiscountRateIncrement { get; set; }
    }  
}