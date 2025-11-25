namespace cs.Domain.Commands
{
    using cs.Domain.Story;
    class CommandExitGame: BaseCommand, ICommand
    {
        public CommandExitGame()
        {
            this.description = "Slukker spillet.";
        }

        public void Execute(StoryHandler storyHandler, string command, string[] parameters)
        {
            storyHandler.MakeDone();

        }
    }
}
