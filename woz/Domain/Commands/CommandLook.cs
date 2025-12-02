namespace woz.Domain.Commands
{
    using woz.Domain.Player;
    using woz.Domain.Story;

    using System;

    /// <summary>
    /// CommandLook class provides functionality to look around in the current scene
    /// </summary>
    class CommandLook : BaseCommand, ICommand
    {
        public CommandLook()
        {
            this.description = "Kig rundt i området";
        }

        public void Execute(StoryHandler storyHandler, string command, string[] parameters)
        {
            // Method to look at the player's current location (scene)
            Scene currentScene = storyHandler.GetCurrentScene();
            if (currentScene is ContextScene contextScene)
            {
                // Checks location and lists items present within the area
                if (contextScene != null)
                {
                    storyHandler.UI.DrawInfo($"====[ Genstande ]====");
                    foreach (Item it in contextScene.Area.Items.Values)
                    {
                        storyHandler.UI.DrawInfo($"* {it.ToString()}");
                    }
                    storyHandler.UI.DrawInfo("");

                }
            }
        }
    }
}
