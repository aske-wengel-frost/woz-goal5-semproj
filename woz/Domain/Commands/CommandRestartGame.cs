namespace woz.Domain.Commands
{
    using woz.Domain.Story;

    /// <summary>
    /// Restart method that restarts the game if the player is at the end scene.
    /// And if the player chooses to restart the game.
    /// </summary>
    class CommandRestartGame : BaseCommand, ICommand
    {
        public CommandRestartGame()
        {
            this.description = "Restarter spillet.";
        }

        public void Execute(StoryHandler storyHandler, string command, string[] parameters)
        {

            if (!storyHandler.IsEndScene)
            {
                storyHandler._UI.DrawError("Du kan kun genstarte spillet såfremt du er ved slutningen.");
                return; 
            }
            storyHandler.RestartGame();

        }
    }
}
