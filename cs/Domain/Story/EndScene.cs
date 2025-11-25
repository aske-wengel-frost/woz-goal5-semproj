namespace cs.Domain.Story
{
    using cs.Presentation;
    using cs.Domain.Player;

    public class EndScene: Scene
    {
        private StoryHandler storyHandler;

        public EndScene(string name, StoryHandler storyHandler) 
            : base (name)
        {
            this.storyHandler = storyHandler;
            this.Name = name;
        }

        /// <summary>
        /// Show end scene with player's score and option to play again
        /// </summary>
        public void ShowEndScene()
        {
            storyHandler._UIHandler.ClearScreen();
            textDisplay.charDelay = 5;
            
            textDisplay.Display("---------=======================================================================================---------");
            textDisplay.Display("Tak fordi du spillede!");
            Console.WriteLine("");
            textDisplay.Display("Dette spil er skabt for at skabe opmærksomhed omkring psykisk vold mod kvinder.");
            textDisplay.Display("Vi håber, at du gennem disse valg og scenarier har fået indblik i,");
            textDisplay.Display("hvordan psykisk vold kan påvirke et menneske - både synligt og usynligt.");
            Console.WriteLine("");
            textDisplay.Display("Husk: Du er ikke alene. Der findes altid hjælp.");
            textDisplay.Display("Hvis du kender nogle eller har selv oplevet lignende situationer,");
            textDisplay.Display("så tøv ikke med at række ud for støtte og hjælp.");
            textDisplay.Display("Lev Uden Volds Hotline: 1888");
            textDisplay.Display("---------=======================================================================================---------");
            Console.WriteLine("");

            // Show players total score
            storyHandler.ShowPlayerScore();

            Console.WriteLine("");
            textDisplay.Display("Vil du gerne prøve igen? (ja/nej)");
            Console.Write("> ");
            //
            // string? input = Console.ReadLine()?.ToLowerInvariant();
            //
            // if (input == "ja" || input == "j")
            // {
            //     storyHandler.RestartGame();
            // }
            // else if (input == "nej" || input == "n")
            // {
            //     storyHandler.MakeDone();
            // }
            // else 
            // {
            //    storyHandler._UIHandler.DrawError("Vælg validt udtryk");
            // }

        }

        /// <summary>
        /// Show player's total score
        /// </summary>
        // private void ShowPlayerScore()
        // {
        //     Player player = storyHandler.GetPlayer();
        //     Console.WriteLine();
        //     storyHandler._UIHandler.DrawInfo($"═══════════════════════════════");
        //     storyHandler._UIHandler.DrawInfo($"  {player.Name}'s Total Score: {player.Score}");
        //     storyHandler._UIHandler.DrawInfo($"═══════════════════════════════");
        // }
        //
        // /// <summary>
        // /// Restart game from the beginning
        // /// </summary>
        // private void RestartGame()
        // {
        //     storyHandler._UIHandler.ClearScreen();
        //
        //     // Reset players score and inventory
        //     storyHandler.GetPlayer().Score = 0;
        //
        //     // storyHandler.GetPlayer().Inventory = new Inventory();
        //     // Remove all items in inventory instead of just making a new one
        //     storyHandler.GetPlayer().Inventory.RemoveAllItems();
        //
        //     // Start the game from the beginning
        //     storyHandler.StartStory();
        // }
    }
}
