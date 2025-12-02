/* Command registry
 */

namespace woz.Domain
{
    using Commands;

    using woz.Domain.Story;

    public class Registry
    {
        private StoryHandler StoryHandler;
        private ICommand fallback;
        private Dictionary<string, ICommand> commands = new Dictionary<string, ICommand>();

        public Registry(StoryHandler StoryHandler, ICommand fallback)
        {
            this.StoryHandler = StoryHandler;
            this.fallback = fallback;
        }

        public void Register(IEnumerable<string> names, ICommand command)
        {
            // adds support for multiple "aliases" for same command.
            foreach (string name in names)
            {
                commands.Add(name, command);
            }
        }

        public void Dispatch(string line)
        {
            // Check if the line is empty, null, or just whitespace
            if (string.IsNullOrWhiteSpace(line))
            {
                return;
            }

            // Convert the line to lowercase
            string processedLine = line.ToLower();

            string[] elements = processedLine.Split(" ");
            string command = elements[0];
            string[] parameters = GetParameters(elements);
            (commands.ContainsKey(command) ? GetCommand(command) : fallback).Execute(StoryHandler, command, parameters);
        }

        // Interface that Gets a command by name and returns command object
        public ICommand GetCommand(string commandName)
        {
            return commands[commandName];
        }

        // Method that returns all registered command names
        public string[] GetCommandNames()
        {
            return commands.Keys.ToArray();
        }

        public Dictionary<string, ICommand> GetCommands()
        {
            return commands;
        }
        // helpers

        private string[] GetParameters(string[] input)
        {
            //New string-array excludes command name
            string[] output = new string[input.Length - 1];
            for (int i = 0; i < output.Length; i++)
            {
                output[i] = input[i + 1];
            }
            return output;
        }
    }
}

