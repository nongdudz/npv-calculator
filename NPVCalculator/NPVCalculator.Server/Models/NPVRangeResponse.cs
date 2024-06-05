namespace NPVCalculator.Server.Models
{
    public class NPVRangeResponse
    {
        public decimal Rate { get; set; }
        public decimal CalculatedNPV { get; set; }
        public List<CashFlowSeries> CashFlowSeries { get; set; }
    }
}
