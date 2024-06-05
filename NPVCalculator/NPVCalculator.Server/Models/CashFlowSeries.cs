namespace NPVCalculator.Server.Models
{
    public class CashFlowSeries
    {
        public int Period { get; set; }
        public decimal CashFlow { get; set; }
        public decimal PresentValue { get; set; }
    }
}
