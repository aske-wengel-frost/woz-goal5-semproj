namespace cs.Domain.Commands
{
    using cs.Domain.Player;
    using cs.Domain.Story;

    using System;

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
                if (ctx != null)
                {
                    storyHandler._UIHandler.DrawInfo($"Du befinder dig i: {ctx.Area.Name}");
                    storyHandler._UIHandler.DrawInfo($"Genstande:");
                    foreach (Item it in ctx.Area.Items.Values)
                    {
                        storyHandler._UIHandler.DrawInfo($" {it.Name},");
                    }
                    storyHandler._UIHandler.DrawInfo("");

                   
                }

            }
            else { storyHandler._UIHandler.DrawError("Command not accessable in this context."); } // Do we need to handle other cases ? 
        }
    }
}
