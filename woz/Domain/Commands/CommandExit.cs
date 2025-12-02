namespace woz.Domain.Commands
{
    using woz.Domain.Story;

    /// <summary>
    /// A Exit Command class for exiting the game which uses the same structure
    /// as the other command classes in the Commands folder, and shows the end scene when executed
    /// </summary>
    class CommandExit : BaseCommand, ICommand
    {
        public CommandExit()
        {
            this.description = "Forlad spillet";
        }

        public void Execute(StoryHandler storyHandler, string command, string[] parameters)
        {
            if (storyHandler.isCurrentSceneOftype<EndScene>())
            {
                storyHandler.UI.DrawError("Spillet er allerede slut");
                return;
            }
            // Finding the end scene object based on name, and showing it
            Scene sceneObj = storyHandler.Story.FindScene<Scene>("Endscene");
            if (sceneObj is EndScene endScene)
            {
                storyHandler.TransitionToScene(endScene);
            }
            else
            {
                storyHandler.UI.DrawError("Endscene not found..."); // Debugging message if end scene is not found
            }
        }
    }

}
