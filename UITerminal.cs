using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs
{
    internal class UITerminal : IUIHandler
    {
        public void DrawScene(Scene scene)
        {
            for (int i = 0; i < 80; i++)
            {
                Console.Write("#");
            }
        }

        public string GetUserInput(String input)
        {
            return input;
        }

        public void ClearScreen()
        {
            Console.Clear();
        }

        public void DrawError()
        {
            Console.WriteLine("An error has occurred. The game will restart.");
        }
    }
}
