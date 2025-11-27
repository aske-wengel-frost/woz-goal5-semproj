namespace cs.Domain.Commands
{
    using cs.Domain.Story;
    using cs.Domain.Player;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class CommandStatus : BaseCommand, ICommand
    {
        public CommandStatus() 
        {
            this.description = "Viser Player status";
        }
        public void Execute(StoryHandler storyHandler, string command, string[] parameters)
        {
            Player player = storyHandler.GetPlayer();
            if (player == null)
            {
                storyHandler._UI.DrawError("FEJL: Spilleren kunne ikke findes.");
                return;
            }

            storyHandler._UI.DrawInfo("--- Player Info ---");

            storyHandler._UI.DrawInfo($"Name: {player.Name}");
            storyHandler._UI.DrawInfo($"Score: {player.Score}");

            storyHandler._UI.DrawInfo("Inventar: (Brug 'inventar' kommandoen for at se)");

            storyHandler._UI.DrawInfo("---------------------");
        }
    }
}
