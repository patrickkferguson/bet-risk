using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using BetRisk.Domain;

namespace BetRisk.Data
{
    public class BetDataAccess
    {
        public IEnumerable<Bet> GetForCustomer(int customerId)
        {
            return GetSettledBets().Concat(GetUnsettledBets()).Where(bet => bet.CustomerId == customerId);
        }

        private List<Bet> GetSettledBets()
        {
            return GetBetData("Settled");
        }

        private List<Bet> GetUnsettledBets()
        {
            return GetBetData("Unsettled");
        }

        private List<Bet> GetBetData(string filename)
        {
            string resourceName = string.Format("BetRisk.Data.{0}.csv", filename);
            Stream dataStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
            if (dataStream == null)
            {
                throw new InvalidOperationException(string.Format("Unable to locate a resource called '{0}'", resourceName));
            }

            string dataString;
            using (StreamReader reader = new StreamReader(dataStream))
            {
                dataString = reader.ReadToEnd();
            }

            if (string.IsNullOrEmpty(dataString))
            {
                return new List<Bet>();
            }

            string[] dataLines = dataString.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);

            return dataLines.Select(BuildBet).ToList();
        }

        private Bet BuildBet(string dataLine)
        {
            string[] properties = dataLine.Split(',');

            Bet bet = new Bet();

            bet.CustomerId = int.Parse(properties[0]);
            bet.Event = int.Parse(properties[1]);
            bet.Participant = int.Parse(properties[2]);
            bet.Stake = int.Parse(properties[3]);
            bet.Win = int.Parse(properties[4]);

            return bet;
        }
    }
}
