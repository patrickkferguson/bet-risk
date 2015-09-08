namespace BetRisk.Domain
{
    public class Bet
    {
        public int CustomerId { get; set; }

        public int Event { get; set; }

        public int Participant { get; set; }

        public decimal Stake { get; set; }

        public decimal Win { get; set; }

        public BetStatus BetStatus { get; set; }

        public string BetStatusLabel { get { return BetStatus.ToString(); } }

        public BetRiskStatus BetRiskStatus { get; set; }

        public string BetRiskStatusLabel { get { return BetRiskStatus.ToString(); } }

        public string RiskReason { get; set; }
    }
}
