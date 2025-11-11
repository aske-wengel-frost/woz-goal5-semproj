namespace cs
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Dynamic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using cs;

    class UITerminal : IUIHandler
    {
        int EffectDelay { get; set; }

        /// <summary>
        /// Draws the scene in the terminal
        /// </summary>
        /// <param name="scene">The scene you want to have drawn</param>
        public void DrawScene(Scene scene)
        {
            this.EffectDelay = 10;
            ClearScreen();

            Console.Write($"---------===================[");
            textDisplay.Display(scene.Name, "]=====================---------", charDelay: EffectDelay);
            textDisplay.Display(scene.DialogueText, split: (60 + scene.Name.Length), punctDelay: 7, charDelay: EffectDelay);
            //Console.WriteLine($"{scene.DialogueText}");
            Console.Write($"---------=====================");
            foreach (char c in scene.Name)
            {
                Console.Write("=");
            }
            Console.WriteLine("=====================---------");
            Console.WriteLine("");
            textDisplay.Display("Her er dine valgmuligheder:", punctDelay: 4, charDelay: EffectDelay);
            foreach (SceneChoice sceneChoice in scene.Choices)
            {
                textDisplay.Display($"[{sceneChoice.SceneId}] > {sceneChoice.Description}", punctDelay: 5, charDelay: EffectDelay);
            }
        }

        public string GetUserInput(String input)
        {
            return input;
        }

        /// <summary>
        /// Clears the terminal screen
        /// </summary>
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

        /// <summary>
        /// Splices text into multiple lines based on a given line length
        /// </summary>
        /// <param name="text">The given dialogue text we want spliced</param>
        /// <param name="lineLength">The amount of chars we want every line to be</param>
        /// <returns>Returns the spliced text</returns>
        private string SpliceText(string text, int lineLength)
        {
            return Regex.Replace(text, "(.{" + lineLength + "})", "$1" + Environment.NewLine);
        }
    }
}
