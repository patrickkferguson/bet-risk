namespace BetRisk.Domain
{
    public class Customer
    {
        public int Id { get; set; }

        public CustomerRiskStatus CustomerRiskStatus { get; set; }

        public string RiskReason { get; set; }
    }
}
