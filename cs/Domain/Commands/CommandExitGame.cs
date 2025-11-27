namespace cs.Domain.Commands
{
    using cs.Domain.Story;
    class CommandExitGame : BaseCommand, ICommand
    {
        public CommandExitGame()
        {
            this.description = "Slukker spillet.";
        }

        public void Execute(StoryHandler storyHandler, string command, string[] parameters)
        {

            if (!storyHandler.isEndScene)
            {
                storyHandler._UI.DrawError("Du kan kun afslutte spillet såfremt du er ved slutningen.");
                return;
            }
            storyHandler.MakeDone();
        }
    }
}
