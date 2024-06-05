namespace NPVCalculator.Server.Models
{
    public class NPVResponse
    {
        public decimal CalculatedNPV { get; set; }

       public List<CashFlowSeries> CashFlowSeries { get; set; }
    }
}
