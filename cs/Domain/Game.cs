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
        static public StoryHandler? storyHandler { get; set; }
        static public IUIHandler? UIHandler { get; set; }

        static ICommand fallback = new CommandUnknown();
        static Registry? registry { get; set; }


        /// <summary>
        /// This class initializes all classes used in the game. It also loads the story and areas from a json file.
        /// </summary>
        private static void InitGame()
        {
            UITerminal UIHandler = new UITerminal();

            TestDataProvider tdp = new TestDataProvider();
            JsonDataProvider jsondp = new JsonDataProvider();

            //storyHandler = new StoryHandler(UIHandler, new JsonDataProvider());
            storyHandler = new StoryHandler(UIHandler, tdp);
            registry = new Registry(storyHandler, fallback);
             
            // Inits the map with the mapelements defined in the story loaded.
            UIHandler.InitMap(storyHandler.story.Areas);

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
            storyHandler.player = new Player.Player (playerName); //Create the player in storyHandler. 
            //storyHandler.player.Name = Environment.UserName;

            Console.WriteLine($"Hej {playerName}, tak fordi du vælger at engagere dig i et vigtigt emne.");

            // We start the storyhandler and thereby the story
            storyHandler.StartStory();

            // Game loop  
            while (storyHandler.IsDone() == false)
            {
                Console.Write("> ");
                string? line = Console.ReadLine();

                registry.Dispatch(line);
            }
            Console.WriteLine($"Spillet er nu slut, tak fordi du spillede {playerName}");

        }

        
        /// Responsible for invoking commands possible in registry.
        private static void InitRegistry()
        {
            registry.Register(new [] {"hjælp"}, new CommandHelp(registry));
            registry.Register(new [] {"afslut", "gg"}, new CommandExit()); 
            registry.Register(new [] {"bevæg", "go", "gå"}, new CommandMove());
            registry.Register(new [] {"export"}, new CommandExportStory());
            registry.Register(new [] {"tilbage"}, new CommandBack());
            registry.Register(new [] {"se"}, new CommandLook());
            registry.Register(new [] {"inventar", "inv"}, new CommandInventory());
            registry.Register(new [] {"tag"}, new CommandTake());
            registry.Register(new [] {"smid"}, new CommandDrop());
            registry.Register(new [] {"brug"}, new CommandUse());
            registry.Register(new [] {"kort"}, new CommandMap());
            registry.Register(new [] {"status"}, new CommandStatus());
            registry.Register(new [] {"ja", "j"}, new CommandRestartGame());
            registry.Register(new [] {"nej", "n"}, new CommandExitGame());
        }
    }
}
