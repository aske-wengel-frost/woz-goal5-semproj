namespace cs
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Dynamic;
    using System.Linq;
    using System.Runtime.ExceptionServices;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Xml;
    using MapTerminal;

    class UITerminal : IUIHandler
    {
        private TerminalMap map { get; set; } = new TerminalMap();

        int EffectDelay { get; set; }
        public static Dictionary<int, int> SceneChoiceAsc = new Dictionary<int, int> { };
        
        public int dashLength = 9;
        public int anger = 0;
        int angerBar;

        /// <summary>
        /// Draws the scene in the terminal
        /// </summary>
        /// <param name="scene">The scene you want to have drawn</param>
        public void DrawScene(Scene scene, int score)
        {
            // Set scope-specific charDelay for anmiations.
            textDisplay.charDelay = 10;

            if (scene is CutScene cutScene)
            {
                ClearScreen();
                textDisplay.Display(cutScene.ConditionInfo);
            }
            else if (scene is ContextScene ctx)
            {
                ClearScreen();
                if (anger>10)
                {
                    angerBar = 10-(10%(anger/10));
                }
                else
                {
                    angerBar = 0;
                }
                string angerChars = anger.ToString();
                string scoreChars = score.ToString();
                int spaceLength = 62+ctx.Area.Name.Length-(37+angerChars.Length+scoreChars.Length);
                
                Console.Write($"Score: {score}");
                for (int i = 0; i < spaceLength; i++)
                {
                    Console.Write(" ");
                }
                Console.Write("Partners vrede: [" + anger + "%|");
                for(int i = 0; i < 10; i++)
                {
                    if (i < angerBar)
                    {
                        Console.Write("█");
                    }
                    else
                    {
                        Console.Write("-");
                    }
                }
                Console.WriteLine("]");

                Console.Write($"---------====================[ ");
                textDisplay.Display(ctx.Area.Name, " ]====================---------");
                textDisplay.Display(ctx.DialogueText, split: 60 + ctx.Name.Length, punctDelay: 7);
                //Console.WriteLine($"{ctx.DialogueText}");
                Console.Write($"---------=====================");
                foreach (char c in ctx.Name)
                {
                    Console.Write("=");
                }
                Console.WriteLine("=====================---------");
                Console.WriteLine("");
                textDisplay.Display("Her er dine valgmuligheder:", punctDelay: 4);

                int num = 1;
                foreach (SceneChoice sceneChoice in ctx.Choices)
                {
                    SceneChoiceAsc[num] = sceneChoice.SceneId;
                    textDisplay.Display($"[{num}] > {sceneChoice.Description}", punctDelay: 5);
                    num++;
                }
                Console.WriteLine("");
                textDisplay.Display("[hjælp] Hvis du er i tvivl", punctDelay: 5);
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

        public void DrawMap()
        {
            map.DrawMap();
        }

        public void InitMap(List<MapElement> elements)
        {
            map.Elements = elements;
        }

        public void HighlightArea(int id)
        {
            map.HighlightElement(id);
        }

        /// <summary>
        /// Waits for user input. Not key-specific. 
        /// </summary>
        public void WaitForKeypress()
        {
            textDisplay.Display("\nTryk [enter] for at fortsætte...");
            Console.ReadLine();
        }

    }
}
