namespace woz.Domain.Commands
{

    public class BaseCommand
    {
        protected string description = "Undocumented";


        /// <summary>
        /// Helper method to combine an array of command parameters into a single string.
        /// </summary>
        /// <param name="parameters">The array of words typed by the user (after the command word).</param>
        /// <returns>A single string with the words joined by spaces.</returns>
        protected string JoinParameters(string[] parameters)
        {
            return string.Join(" ", parameters);
        }

        protected bool GuardEq(string[] parameters, int bound)
        {
            return parameters.Length != bound;
        }

        public string GetDescription()
        {
            return description;
        }
    }
}
