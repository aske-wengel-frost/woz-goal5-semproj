namespace cs.Commands
{
    using System;

    class CommandMove : BaseCommand, ICommand
    {
        public CommandMove()
        {
            this.description = "Bevæge til en anden scene ved at skrive scene numemr";
        }

        public void Execute(StoryHandler StoryHandler, string command, string[] parameters)
        {
            if (GuardEq(parameters, 1))
            {
                // We dont have 1 parameter!
                StoryHandler._UIHandler.DrawError("For mange argumenter!");
                return;

            }
            
            // Just a Parse scene ID
            Int32.TryParse(parameters[0], out int sceneID);

            // Finding the scene and selected choice
            Scene currentScene = StoryHandler.GetCurrentScene();
            SceneChoice? selectedChoice = currentScene.Choices.Find(c => c.SceneId == sceneID);

            // Checks if the choice exists
            if (selectedChoice == null)
            {
                StoryHandler._UIHandler.DrawError("Dette valg eksister ikke!");
                return;
            }

            // Checks if the choice requires an item
            if (selectedChoice.RequiredItemId.HasValue)
            { 
                StoryHandler._UIHandler.DrawError("Du mangler en genstand for at vælge dette!");
                return;
            }

            StoryHandler.PerformChoice(parameters[0]);
        }
    }
}
