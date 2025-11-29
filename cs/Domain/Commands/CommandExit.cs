/* Command for exiting program
 */

namespace cs.Domain.Commands
{
    using cs.Domain.Story;

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

            // Instantiating the end scene and showing it
            Scene sceneObj = storyHandler.story.FindScene<Scene>("Endscene");
            if (sceneObj is EndScene endScene)
            {
                storyHandler.HandleEndScene(endScene);
            }
            else
            {
                storyHandler._UI.DrawError("Endscene not found..."); // Debugging message if end scene is not found
            }
        }
    }

}
