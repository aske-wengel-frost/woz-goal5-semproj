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
            this.description = "Genstarter spillet.";
        }

        public void Execute(StoryHandler storyHandler, string command, string[] parameters)
        {

            if (storyHandler.isCurrentSceneOftype<EndScene>())
            {
                storyHandler.UI.DrawError("Du kan kun genstarte spillet såfremt du er ved slutningen.");
                return; 
            }
            storyHandler.RestartGame();

        }
    }
}
