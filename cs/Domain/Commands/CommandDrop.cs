namespace cs.Domain.Commands
{
    using cs.Domain.Player;
    using cs.Domain.Story;
    class CommandDrop : BaseCommand, ICommand 
    {
        public CommandDrop()
        {
            this.description = "Smid en genstand";
        }
        public void Execute(StoryHandler storyHandler, string command, string[] parameters)
        {
            if (parameters.Length == 0)
            {
                storyHandler._UIHandler.DrawInfo("Brug: smid [genstand navn]");
                return;
            }
            // get name of item
            string itemName = parameters[0];
            
            // find the ID of item
            Player player = storyHandler.GetPlayer();
            Inventory inventory = player.inventory;
            
            Item? item = inventory.GetItemName(itemName);

            if (item == null)
            {
                storyHandler._UIHandler.DrawError("Hov... Du har ikke denne genstand");
            }
            
            // check if user has item
            if (item != null)
            {
                // add the item to the area the user is in
                storyHandler.GetCurrentScene().Area.AddItem(item);
                
                // Remove the item from the players inventory
                storyHandler.player.inventory.RemoveItem(item);
                
                storyHandler._UIHandler.DrawInfo($"Du smed: {itemName} [{item.Description}]");
            }

        }
    } 
}