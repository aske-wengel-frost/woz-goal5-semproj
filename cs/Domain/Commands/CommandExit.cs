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
<<<<<<< HEAD
            storyHandler.ShowEndScene();
=======
            Scene sceneObj = StoryHandler.story.FindSceneByName("Endscene");
            if (sceneObj is EndScene endScene)
            {
                StoryHandler.ShowEndScene(endScene.EndSceneContent);
            }
            else
            {
                StoryHandler._UIHandler.DrawError("Endscene not found...");
            }
>>>>>>> Aske
        }
    }

}
