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

        public void Execute(StoryHandler StoryHandler, string command, string[] parameters)
        {
            StoryHandler._UI.DrawMap();
        }
    }
}
