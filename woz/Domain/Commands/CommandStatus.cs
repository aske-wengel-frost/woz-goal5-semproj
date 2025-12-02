namespace woz.Domain.Commands
{
    using woz.Domain.Story;
    using woz.Domain.Player;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// The Status command shows the player's current status, including name, score, and inventory.
    /// and shows the information in a structured format.
    /// </summary>
    public class CommandStatus : BaseCommand, ICommand
    {
        public CommandStatus() 
        {
            this.description = "Viser Player status";
        }
        public void Execute(StoryHandler storyHandler, string command, string[] parameters)
        {
            Player player = storyHandler.GetPlayer();
           
            storyHandler._UI.DrawInfo("--- Player Info ---");

            storyHandler._UI.DrawInfo($"Name: {player.Name}");
            storyHandler._UI.DrawInfo($"Score: {player.Score}");
            storyHandler._UI.DrawInfo($"Partner agression: {player.PartnerAggression}");

            storyHandler._UI.DrawInfo("Inventar: (Brug 'inventar' kommandoen for at se)");

            storyHandler._UI.DrawInfo("---------------------");
        }
    }
}
