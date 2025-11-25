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
                storyHandler._UIHandler.DrawError("FEJL: Spilleren kunne ikke findes.");
                return;
            }

            storyHandler._UIHandler.DrawInfo("--- Player Info ---");

            storyHandler._UIHandler.DrawInfo($"Name: {player.Name}");
            storyHandler._UIHandler.DrawInfo($"Score: {player.Score}");

            storyHandler._UIHandler.DrawInfo("Inventar: (Brug 'inventar' kommandoen for at se)");

            storyHandler._UIHandler.DrawInfo("---------------------");
        }
    }
}
