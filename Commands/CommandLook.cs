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
            if (currentScene is ContextScene ctx)
            {
                if (ctx != null)
                {
                    StoryHandler._UIHandler.DrawInfo($"Du befinder dig i: {ctx.Area.Name}");
                    StoryHandler._UIHandler.DrawInfo($"Genstande:");
                    foreach (Item it in ctx.Area.Items)
                    {
                        StoryHandler._UIHandler.DrawInfo($" {it.Name},");
                    }
                    StoryHandler._UIHandler.DrawInfo("");

                   
                }

            }
            else { StoryHandler._UIHandler.DrawError("Command not accessable in this context."); } // Do we need to handle other cases ? 
        }
    }
}
