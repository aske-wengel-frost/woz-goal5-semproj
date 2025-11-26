using cs.Domain.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    internal class ItemTests
    {
        private Item mobil;
        private Item pandekage;
        private Item toiletNøgle;

        public void Setup()
        {
            // Create dummy items (also the items in game) to use for testing
            mobil = new Item("Mobil", "En smartphone");
            pandekage = new Item("Pandekage", "MMMM mums en pandekage");
            toiletNøgle = new Item("Toilet Nøgle", "En nøgle til toilettet");
        }
    }
}
