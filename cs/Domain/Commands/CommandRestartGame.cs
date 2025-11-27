namespace cs.Domain.Commands
{
    using cs.Domain.Story;
    class CommandRestartGame : BaseCommand, ICommand
    {
        public CommandRestartGame()
        {
            this.description = "Restarter spillet.";
        }

        public void Execute(StoryHandler storyHandler, string command, string[] parameters)
        {

            if (!storyHandler.isEndScene)
            {
                storyHandler._UI.DrawError("Du kan kun genstarte spillet såfremt du er ved slutningen.");
                return; 
            }
            storyHandler.RestartGame();

        }
    }
}
