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
            this.ClearScreen();
            //for (int i = 0; i < 80; i++)
            //{
            //    Console.Write("=");
            //}
            Console.WriteLine($"---------=========[ {scene.Name} ]=========---------");
            Console.WriteLine($"{scene.DialogueText}");

            Console.WriteLine("\nWhat are you gonna do now?:");
            foreach (SceneChoice sceneChoice in scene.Choices)
            { 
                Console.WriteLine($" -> {sceneChoice.Description} : [{sceneChoice.SceneId}]"); 
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
            Console.WriteLine("Der er opstået en fejl");
        }

        public void DrawError(string errorMsg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(errorMsg);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
