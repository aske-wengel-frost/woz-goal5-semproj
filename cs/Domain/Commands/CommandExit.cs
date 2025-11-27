/* Command for exiting program
 */

namespace cs.Domain.Commands
{
    using cs.Domain.Story;

    class CommandExit : BaseCommand, ICommand
    {
        public CommandExit()
        {
            this.description = "Forlad spillet";
        }

        public void Execute(StoryHandler storyHandler, string command, string[] parameters)
        {

            Scene sceneObj = storyHandler.story.FindScene<Scene>("Endscene");
            if (sceneObj is EndScene endScene)
            {
                storyHandler.ShowEndScene(endScene.EndSceneContent);
            }
            else
            {
                storyHandler._UI.DrawError("Endscene not found...");
            }
        }
    }

}
