namespace woz.Domain.Commands
{
    using woz.Domain.Story;

    /// <summary>
    /// A ExitGame Command class for exiting the game which uses the same structure
    /// as the other command classes in the Commands folder, and ends the game when executed
    /// </summary>
    class CommandExitGame : BaseCommand, ICommand
    {
        public CommandExitGame()
        {
            this.description = "Slukker spillet.";
        }

        public void Execute(StoryHandler storyHandler, string command, string[] parameters)
        {
            // If-statement to check if the player is at the end scene before allowing them to exit the game
            if (storyHandler.isCurrentSceneOftype<EndScene>())
            {
                storyHandler.UI.DrawError("Du kan kun afslutte spillet såfremt du er ved slutningen.");
                return;
            }
            storyHandler.MakeDone();
        }
    }
}
