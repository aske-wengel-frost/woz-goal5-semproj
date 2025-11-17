namespace cs
{
    public class EndScene
    {
        private StoryHandler storyHandler;

        public EndScene(StoryHandler storyHandler)
        {
            this.storyHandler = storyHandler;
        }

        /// <summary>
        /// ¨Show end scene with player's score and option to play again
        /// </summary>
        public void ShowEndScene()
        {
            Console.Clear();
            Console.WriteLine("---------=======================================================================================---------");
            Console.WriteLine("Tak fordi du spillede!");
            Console.WriteLine();
            Console.WriteLine("Dette spil er skabt for at skabe opmærksomhed omkring psykisk vold mod kvinder.");
            Console.WriteLine("Vi håber, at du gennem disse valg og scenarier har fået indblik i,");
            Console.WriteLine("hvordan psykisk vold kan påvirke et menneske - både synligt og usynligt.");
            Console.WriteLine();
            Console.WriteLine("Husk: Du er ikke alene. Der findes altid hjælp.");
            Console.WriteLine("Hvis du kender nogle eller har selv oplevet lignende situationer,");
            Console.WriteLine("så tøv ikke med at række ud for støtte og hjælp.");
            Console.WriteLine("Lev Uden Volds Hotline: 1888");
            Console.WriteLine("---------=======================================================================================---------");
            Console.WriteLine();

            // Show players total score
            ShowPlayerScore();

            Console.WriteLine();
            Console.WriteLine("Vil du gerne prøve igen? (ja/nej)");
            Console.Write("> ");

            string? input = Console.ReadLine()?.ToLowerInvariant();

            if (input == "ja" || input == "j")
            {
                RestartGame();
            }
        }

        /// <summary>
        /// Show player's total score
        /// </summary>
        private void ShowPlayerScore()
        {
            Player player = storyHandler.GetPlayer();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"═══════════════════════════════");
            Console.WriteLine($"  {player.Name}'s Total Score: {player.Score}");
            Console.WriteLine($"═══════════════════════════════");
            Console.ResetColor();
        }

        /// <summary>
        /// Restart game from the beginning
        /// </summary>
        private void RestartGame()
        {
            Console.Clear();

            // Reset players score and inventory
            storyHandler.GetPlayer().Score = 0;
            storyHandler.GetPlayer().Inventory = new Inventory();

            // Start the game from the beginning
            storyHandler.Start();
        }
    }
}