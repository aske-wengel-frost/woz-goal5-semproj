namespace cs
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Dynamic;
    using System.Linq;
    using System.Runtime.ExceptionServices;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    using MapTerminal;

    class UITerminal : IUIHandler
    {
        private TerminalMap map { get; set; } = new TerminalMap();

        int EffectDelay { get; set; }
        public static Dictionary<int, int> SceneChoiceAsc = new Dictionary<int, int> { };

        /// <summary>
        /// Draws the scene in the terminal
        /// </summary>
        /// <param name="scene">The scene you want to have drawn</param>
        public void DrawScene(Scene scene)
        {
            ClearScreen();
            textDisplay.charDelay = 10;

            Console.Write($"---------==================[ ");
            textDisplay.Display(scene.Area.Name, " ]====================---------");
            textDisplay.Display(scene.DialogueText, split: (60 + scene.Name.Length), punctDelay: 7);
            //Console.WriteLine($"{scene.DialogueText}");
            Console.Write($"---------=====================");
            foreach (char c in scene.Name)
            {
                Console.Write("=");
            }
            Console.WriteLine("=====================---------");
            Console.WriteLine("");
            textDisplay.Display("Her er dine valgmuligheder:", punctDelay: 4);

            int num = 1;
            foreach (SceneChoice sceneChoice in scene.Choices)
            {
                SceneChoiceAsc[num] = sceneChoice.SceneId;
                textDisplay.Display($"[{num}] > {sceneChoice.Description}", punctDelay: 5);
                num++;
            }
            Console.WriteLine("");
            textDisplay.Display("[Hjælp] Hvis du er i tvivl", punctDelay: 5);
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
            Console.ResetColor();
        }
        public void DrawInfo(string infoMsg)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(infoMsg);
            Console.ResetColor();
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

        public void RefreshMap(Dictionary<int, Area> areas)
        {
            map.DrawMap(areas);
        }
    }
}
