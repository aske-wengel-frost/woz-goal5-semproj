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

        static public StoryHandler storyHandler { get; set; }
        static public IUIHandler UIHandler { get; set; }

        static ICommand fallback = new CommandUnknown();
        static Registry registry {get; set;}

        private static void InitRegistry()
        {
            ICommand cmdExit = new CommandExit();
            registry.Register("help", new CommandHelp(registry));
            registry.Register("exit", cmdExit);
            registry.Register("move", new CommandMove());
            registry.Register("quit", cmdExit);

            // New Commands for the help() function
            registry.Register("back", new CommandBack());
            registry.Register("look", new CommandLook());
            registry.Register("inventory", new CommandInventory());
            registry.Register("inv", new CommandInventory()); // Just a shorter version for inventory (Alias)
            registry.Register("take", new CommandTake());
            registry.Register("use", new CommandUse());
        }

        static void Main(string[] args)
        {
            UIHandler = new UITerminal();
            storyHandler = new StoryHandler(UIHandler);
            registry = new Registry(storyHandler, fallback);

            // Welcome message
            Console.WriteLine("---------=======================================================================================---------");
            Console.WriteLine("Detté spil er udviklet for at skabe opmærksomhed omkring psykisk vold mod kvinder.");
            Console.WriteLine("Gennem fortællinger, valg og scenarier får du mulighed for at opleve, hvordan psykisk vold kan opstå,");
            Console.WriteLine("udvikle sig og påvirke et menneske - både synligt og usynligt.");
            Console.WriteLine();
            Console.WriteLine("Du er ikke alene, der findes hjælp - både i og uden for spillet. ");
            Console.WriteLine("Lev Uden Volds Hotline: 1888");
            Console.WriteLine();
            Console.WriteLine("---------=======================================================================================---------");
            Console.WriteLine();
            Console.Write("Indtast dit navn: ");
            string? playerName = Console.ReadLine();
            storyHandler.player = new Player (playerName); //Create the player in storyHandler. 
            Console.WriteLine($"Hej {playerName}, tak fordi du vælger at engagere dig i et vigtigt emne.");

            // We call the InitRegistry method
            InitRegistry();

            storyHandler.Start();

            // Game loop  
            while (storyHandler.IsDone() == false)
            {
                Console.Write("> ");
                string? line = Console.ReadLine();
                if (line != null) registry.Dispatch(line);
            }
            Console.WriteLine($"Spillet er nu slut, tak fordi du spillede {playerName}");

        }
    }
}
