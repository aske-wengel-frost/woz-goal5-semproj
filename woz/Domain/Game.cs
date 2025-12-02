namespace woz.Domain
{
    using woz.Domain.Commands;
    using woz.Domain.Player;
    using woz.Domain.Story;
    using woz.Persistance;
    using woz.Presentation;

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
            //TestDataProvider tdp = new TestDataProvider(); // A class that builds and provides a test story
            JsonDataProvider jsondp = new JsonDataProvider(); // A Class which loads the story data from a Json file
            
            // Inits an instance of StoryHandler
            StoryHandler = new StoryHandler(UIHandler, jsondp);

            // Inits an instance of the registry class
            Registry = new Registry(StoryHandler, fallback);
             
            // Inits the map with the mapelements Areas contained in the Story object
            UIHandler.InitMap(StoryHandler.Story.Areas);

            InitRegistry();
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

        
        /// Responsible for intializing Command objects and adding them to registry object
        private static void InitRegistry()
        {
            Registry.Register(new [] {"hjælp"}, new CommandHelp(Registry));
            Registry.Register(new [] {"afslut", "gg"}, new CommandExit()); 
            Registry.Register(new [] {"bevæg", "go", "gå"}, new CommandMove());
            Registry.Register(new [] {"se"}, new CommandLook());
            Registry.Register(new [] {"inventar", "inv"}, new CommandInventory());
            Registry.Register(new [] {"tag"}, new CommandTake());
            Registry.Register(new [] {"smid"}, new CommandDrop());
            Registry.Register(new [] {"kort"}, new CommandMap());
            Registry.Register(new [] {"status"}, new CommandStatus());
            Registry.Register(new [] {"ja", "j"}, new CommandRestartGame());
            Registry.Register(new [] {"nej", "n"}, new CommandExitGame());
        }
    }
}
