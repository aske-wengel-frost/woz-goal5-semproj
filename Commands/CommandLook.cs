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
                    Console.WriteLine($"Du befinder dig i: {ctx.Area.Name}");
                    Console.Write($"Genstande:");
                    foreach (Item it in ctx.Area.Items)
                    {
                        Console.Write($" {it.Name},");
                    }
                    Console.WriteLine();
                }

            }
            else { Console.Write("Command not accessable in this context."); } // Do we need to handle other cases ? 
        }
    }
}
