using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs.Commands
{
    class CommandMove : BaseCommand, ICommand
    {
        public void Execute(StoryHandler StoryHandler, string command, string[] parameters)
        {
            if (GuardEq(parameters, 1))
            {
                Console.WriteLine("Hmmm....Jeg ved ikke hvor det er");
                return;

            }
            else
            {
                StoryHandler.SwitchScene(parameters[0]);
            }
        }
    }
}
