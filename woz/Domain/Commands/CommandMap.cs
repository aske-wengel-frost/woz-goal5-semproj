namespace woz.Domain.Commands
{
    using System;

    using woz.Domain.Story;

    /// <summary>
    /// A Map command that shows the map to the player.
    /// In a visual way in a text-based way with ASCII art.
    /// </summary>
    public class CommandMap : BaseCommand, ICommand
    {
        public CommandMap()
        {
             this.description = "Viser kortet";
        }

        // Execute the DrawMap method from the UI handler
        public void Execute(StoryHandler storyHandler, string command, string[] parameters)
        {
            storyHandler._UI.DrawMap();
        }
    }
}
