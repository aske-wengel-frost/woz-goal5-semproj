namespace woz.Domain.Player
{
    using System.ComponentModel.DataAnnotations;

    public class Player
    {
        public readonly int MAX_AGRESSION = 100;

        public string Name { get; private set; }
        public int Score { get; private set; }
        public int PartnerAggression { get; private set; }
        public Inventory Inventory { get; private set; }

        // We initialize score=0, creates new Inventory for Player, assigns Player a name
        public Player(string name)
        {
            Name = name;
            Score = 0;
            PartnerAggression = 30;
            Inventory = new Inventory();
        }

        // Change value of score based on the specific amount given
        public void ModifyScore(int amount)
        {
            // Make sure the score never goes below 0.
            if (Score + amount < 0)
            {
                Score = 0;
                return;
            }
            Score += amount;
        }

        /// <summary>
        /// Modifies the partner's aggresion level by the specified amount.
        /// The level is clamped to a minimum of 0 and "MAX_PARTNER_AGGRESSION" (100)
        /// </summary>
        public void ModifyPartnerAgression(int amount)
        {
            // Make sure the score never goes below 0.
            if (PartnerAggression + amount < 0)
            {
                PartnerAggression = 0;
                return;
            }
            if(PartnerAggression + amount > MAX_AGRESSION)
            {
                PartnerAggression = MAX_AGRESSION;
            }
            PartnerAggression += amount;
        }

        // Simple reset methods used to reset player stats when restarting the game
        public void ResetPlayerScore()
        {
            Score = 0;
        }

        // Used when restarting the game to put partner aggression back to 0
        public void ResetParterAggression()
        {
            PartnerAggression = 0;
        }
    }
}