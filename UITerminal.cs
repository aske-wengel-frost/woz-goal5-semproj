namespace cs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class UITerminal : IUIHandler
    {
        /// <summary>
        /// Draws the scene in the terminal
        /// </summary>
        /// <param name="scene">The scene you want to have drawn</param>
        public void DrawScene(Scene scene)
        {
            for (int i = 0; i < 80; i++)
            {
                Console.Write("#");
            }
            Console.WriteLine();
            Console.WriteLine($"Du er gået til: {scene.Name}");
            Console.WriteLine($" {scene.DialogueText}");
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
            Console.WriteLine("Der er opstået en fejl");
        }


    }
}
