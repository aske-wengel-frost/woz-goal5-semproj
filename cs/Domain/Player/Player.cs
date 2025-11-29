namespace cs.Domain.Player
{
    public class Player
    {
        public string Name { get; private set; }
        public int Score { get; private set; }
        public int PartnerAggression { get; private set; }
        public Inventory Inventory { get; private set; }

        // We initialize score=0, creates new Inventory for Player, assigns Player a name
        public Player(string name)
        {
            Name = name;
            Score = 0;
            PartnerAggression = 0;
            Inventory = new Inventory();
        }

        // Change value of score
        public void ModifyScore(int amount)
        {
            // Make sure the score never goes below 0.
            if(Score - amount < 0)
            {
                Score = 0;
                return;
            }
            Score += amount;
        }

        public void ModifyPartnerAgression(int amount)
        {
            // Make sure the score never goes below 0.
            if (PartnerAggression - amount < 0)
            {
                PartnerAggression = 0;
                return;
            }
            PartnerAggression += amount;
        }

        public void ResetPlayerScore()
        {
            Score = 0;
        }

        public void ResetParterAggression()
        {
            PartnerAggression = 0;
        }
    }
}