namespace cs.Domain
{
    using cs.Domain.Commands;
    using cs.Domain.Player;
    using cs.Domain.Story;
    using cs.Persistance;
    using cs.Presentation;

    using System;

    public static class Game
    {
        static public StoryHandler? StoryHandler { get; set; }
        static ICommand fallback = new CommandUnknown();
        static Registry? Registry { get; set; }


        /// <summary>
        /// This class initializes all classes used in the game. It also loads the story and areas from a json file.
        /// </summary>
        private static void InitGame()
        {
            // Initializes an instance of Terminal UI
            UITerminal UIHandler = new UITerminal();

            // Initializes an instance of JsonDataProvider 
            // ONLY INIT ONE INSTANCE OF A DATAPROVIDER AS IT WILL FUCK UP IDS IF NOT!!
            TestDataProvider tdp = new TestDataProvider(); // TDP = The class with the TestDataProvider
            JsonDataProvider jsondp = new JsonDataProvider(); // JsonDP = The Class with the Json Data (And the one being used in the game)
            
            // Inits and sets the instance of StoryHandler
            StoryHandler = new StoryHandler(UIHandler, jsondp);

            // Inits and sets the instance of Registry 
            Registry = new Registry(StoryHandler, fallback);
             
            // Inits the map with the mapelements defined in the story loaded.
            UIHandler.InitMap(StoryHandler.Story.Areas);

            // We call the InitRegistry method
            InitRegistry();

            //tdp.exportTestStory();
        }

        static void Main(string[] args)
        {
            InitGame();

            // Welcome message
            Console.WriteLine("---------=======================================================================================---------");
            Console.WriteLine("Detté spil er udviklet for at skabe opmærksomhed omkring psykisk vold mod kvinder.");
            Console.WriteLine("Gennem fortællinger, valg og scenarier får du mulighed for at opleve, hvordan psykisk vold kan opstå,");
            Console.WriteLine("udvikle sig og påvirke et menneske - både synligt og usynligt.");
            Console.WriteLine();
            Console.WriteLine("Du er ikke alene, der findes hjælp - både i og uden for spillet. ");
            Console.WriteLine("Lev Uden Volds Hotline: 1888");
            Console.WriteLine();
            Console.WriteLine("Hvis du bliver i tvivl om, hvordan spillet fungerer, kan du altid skrive kommandoen 'hjælp' ");
            Console.WriteLine("for at se en liste over alle kommandoer med deres beskrivelse");
            Console.WriteLine("---------=======================================================================================---------");
            Console.WriteLine();
            Console.Write("Indtast dit navn: ");
            string? playerName = Console.ReadLine();
            StoryHandler.Player = new Player.Player (playerName); //Create the player in storyHandler. 
            //storyHandler.player.Name = Environment.UserName;

            // We start the story
            StoryHandler.StartStory();

            // Game loop  
            while (StoryHandler.IsDone() == false)
            {
                Console.Write("> ");
                string? line = Console.ReadLine();

                Registry.Dispatch(line);
            }
            Console.WriteLine($"Spillet er nu slut, tak fordi du spillede {playerName}");

        }

        
        /// Responsible for invoking commands possible in registry.
        private static void InitRegistry()
        {
            Registry.Register(new [] {"hjælp"}, new CommandHelp(Registry));
            Registry.Register(new [] {"afslut", "gg"}, new CommandExit()); 
            Registry.Register(new [] {"bevæg", "go", "gå"}, new CommandMove());
            Registry.Register(new [] {"se"}, new CommandLook());
            Registry.Register(new [] {"inventar", "inv"}, new CommandInventory());
            Registry.Register(new [] {"tag"}, new CommandTake());
            Registry.Register(new [] {"smid"}, new CommandDrop());
            Registry.Register(new [] {"brug"}, new CommandUse());
            Registry.Register(new [] {"kort"}, new CommandMap());
            Registry.Register(new [] {"status"}, new CommandStatus());
            Registry.Register(new [] {"ja", "j"}, new CommandRestartGame());
            Registry.Register(new [] {"nej", "n"}, new CommandExitGame());
        }
    }
}
