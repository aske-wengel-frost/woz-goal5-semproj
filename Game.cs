/* Main class for launching the game
 */
using cs;
using cs.Commands;
using System.Transactions;

namespace cs
{
    using Commands;

    public class Game
    {
        static public StoryHandler? storyHandler { get; set; }
        static public IUIHandler? UIHandler { get; set; }

        static ICommand fallback = new CommandUnknown();
        static Registry? registry {get; set;}


        /// <summary>
        /// This class initializes all classes used in the game. It also loads the story and areas from a json file.
        /// </summary>
        private static void InitGame()
        {
            UIHandler = new UITerminal();
            storyHandler = new StoryHandler(UIHandler);
            registry = new Registry(storyHandler, fallback);

            // Inits the map with the mapelements defined in the story loaded.
            UIHandler.InitMap(storyHandler.story.MapElements);

            // We call the InitRegistry method
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
            storyHandler.player = new Player (playerName); //Create the player in storyHandler. 
            Console.WriteLine($"Hej {playerName}, tak fordi du vælger at engagere dig i et vigtigt emne.");

            // We start the storyhandler and thereby the story
            storyHandler.StartStory();

            // Game loop  
            while (storyHandler.IsDone() == false)
            {
                Console.Write("> ");
                string? line = Console.ReadLine();

                // 1. Check if the line is empty, null, or just whitespace
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }
                // 2. Convert the line to lowercase
                string processedLine = line.ToLowerInvariant();

                // 3. Send the processed line to dispatch system
                registry.Dispatch(processedLine);
            }
            Console.WriteLine($"Spillet er nu slut, tak fordi du spillede {playerName}");

        }


        private static void InitRegistry()
        {
            ICommand cmdExit = new CommandExit();
            registry.Register(new [] {"hjælp"}, new CommandHelp(registry));
            registry.Register(new [] {"afslut"}, cmdExit); // cmdExit x2?
            registry.Register(new [] {"bevæg", "go", "gå"}, new CommandMove());
            registry.Register(new [] {"export"}, new CommandExportStory());

            // New Commands for the help() function
            registry.Register(new [] {"tilbage"}, new CommandBack());
            registry.Register(new [] {"se"}, new CommandLook());
            registry.Register(new [] {"inventar"}, new CommandInventory());
            registry.Register(new [] {"inv"}, new CommandInventory()); // Just a shorter version for inventory (Alias)
            registry.Register(new [] {"tag"}, new CommandTake());
            registry.Register(new [] {"brug"}, new CommandUse());
            registry.Register(new [] {"kort"}, new CommandMap());
            registry.Register(new [] {"status"}, new CommandStatus());
        }
    }
}
