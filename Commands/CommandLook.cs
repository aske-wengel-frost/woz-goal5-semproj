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
                Console.WriteLine($"Du befinder dig i: {currentScene.Area.Name}");
                Console.Write($"Genstande:");
                foreach(Item it in StoryHandler.GetCurrentScene().Area.Items )
                {
                    Console.Write($" {it.Name},");
                }
                Console.WriteLine();
            }
        }
    }
}