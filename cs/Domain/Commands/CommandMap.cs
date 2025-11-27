namespace cs.Domain.Commands
{
    using System;

    using cs.Domain.Story;

    public class CommandMap : BaseCommand, ICommand
    {
        public CommandMap()
        {
             this.description = "Viser kortet";
        }

        public void Execute(StoryHandler storyHandler, string command, string[] parameters)
        {
            storyHandler._UI.DrawMap();
        }
    }
}
