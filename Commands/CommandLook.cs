namespace cs.Commands
{
    using System;

    class CommandLook : BaseCommand, ICommand
    {
        public CommandLook()
        {
             this.description = "Kig rundt i din current scene";
        }

        public void Execute(StoryHandler StoryHandler, string command, string[] parameters)
        {
            // Method to look at the player's current location (scene)
            Scene currentScene = StoryHandler.GetCurrentScene();
            if (currentScene != null)
            {
                StoryHandler._UIHandler.DrawInfo($"Du befinder dig i: {currentScene.Area.Name}");
                StoryHandler._UIHandler.DrawInfo($"Genstande:");
                foreach(Item it in StoryHandler.GetCurrentScene().Area.Items )
                {
                    StoryHandler._UIHandler.DrawInfo($" {it.Name},");
                }
                StoryHandler._UIHandler.DrawInfo("");
            }
        }
    }
}