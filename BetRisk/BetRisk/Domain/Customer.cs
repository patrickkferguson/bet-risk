namespace BetRisk.Domain
{
    public class Customer
    {
        public int Id { get; set; }

        public int TotalNumberOfBets { get; set; }

        public int NumberOfSettledBets { get; set; }

        public int NumberOfUnsettledBets { get; set; }

        public int NumberOfWinningBets { get; set; }

        public decimal TotalSettledStake { get; set; }

        public decimal TotalSettledWin { get; set; }

        public decimal TotalUnsettledStake { get; set; }

        public decimal TotalUnsettledWin { get; set; }

        public CustomerRiskStatus CustomerRiskStatus { get; set; }

        public string CustomerRiskStatusLabel { get { return CustomerRiskStatus.ToString(); } }

        public string RiskReason { get; set; }
    }
}
