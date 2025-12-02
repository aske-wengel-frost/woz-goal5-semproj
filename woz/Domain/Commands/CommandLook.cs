namespace woz.Domain.Commands
{
    using woz.Domain.Player;
    using woz.Domain.Story;

    using System;

    /// <summary>
    /// CommandLook class which implements the ICommand interface
    /// and provides functionality to look around in the current scene.
    /// </summary>
    class CommandLook : BaseCommand, ICommand
    {
        public CommandLook()
        {
            this.description = "Kig rundt i din current scene";
        }

        public void Execute(StoryHandler storyHandler, string command, string[] parameters)
        {
            // Method to look at the player's current location (scene)
            Scene currentScene = storyHandler.GetCurrentScene();
            if (currentScene is ContextScene ctx)
            {
                // Checks location and lists items present within the area
                if (ctx != null)
                {
                    storyHandler._UI.DrawInfo($"====[ Genstande ]====");
                    foreach (Item it in ctx.Area.Items.Values)
                    {
                        storyHandler._UI.DrawInfo($"* {it.ToString()}");
                    }
                    storyHandler._UI.DrawInfo("");

                }
            }
        }
    }
}
