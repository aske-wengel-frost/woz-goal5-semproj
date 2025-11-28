namespace cs.Presentation
{
    using cs.Domain.Story;
    using cs.Presentation.MapTerminal;

    using MapTerminal;

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
    using System.Transactions;
    using System.Xml;

    using static System.Formats.Asn1.AsnWriter;
    using cs.Domain;

    class UITerminal : IUIHandler
    {
        private TerminalMap map { get; set; } = new TerminalMap();

        int LineLength { get; set; } = 60;
        int OuterLineLength { get; set; } = 9;

        /// <summary>
        /// Draws the scene in the terminal
        /// </summary>
        /// <param name="scene">The scene you want to have drawn</param>
        public void DrawScene(Scene scene, int score, int anger)
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

                LineLength = LineLength + ctx.Area.Name.Length;


                DrawStatusBar(score, anger);

                Console.WriteLine(LineConstructor(ctx.Area.Name, true));

                textDisplay.Display(ctx.DialogueText, split: LineLength, punctDelay: 7);

                Console.WriteLine(LineConstructor(ctx.Area.Name, false));

                Console.WriteLine();
                textDisplay.Display("Her er dine valgmuligheder:", punctDelay: 4);

                // Uses the index of the array to display the options ascendingly
                for (int i = 1; i < ctx.Choices.Count() + 1; i++)
                {
                    textDisplay.Display($"[{i}] > {ctx.Choices[i - 1].Description}", punctDelay: 5);
                }


                Console.WriteLine("");
                textDisplay.Display("[hjælp] Hvis du er i tvivl", punctDelay: 5);
            }

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

        public void DrawMap()
        {
            map.DrawMap();
        }

        public void InitMap(Dictionary<int, Area> areas)
        {
            List<MapElement> mapElements = new List<MapElement>();

            foreach (Area area in areas.Values)
            {
                if (area.Frame is null)
                {
                    // We dont add a mapelement
                    continue;
                }

                mapElements.Add(new MapRoomElement(area.ID, area.Frame.X, area.Frame.Y, area.Frame.Height, area.Frame.Width, area.Name));
            }
            map.Elements = mapElements;
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

        public string LineConstructor(string name, bool displayName)
        {
            int drawnLine = LineLength - name.Length;
            string output = "";

            for (int i = 0; i < OuterLineLength; i++)
            {
                output += "-";
            }
            if (displayName)
            {
                for (int i = 0; i < (int)drawnLine/2 - (OuterLineLength+1); i++)
                {
                    output += "═";
                }
                output += $"[ {name} ]";
                for (int i = 0; i < (int)drawnLine/2 - (OuterLineLength+1); i++)
                {
                    output += "═";
                }
            }
            else
            {
                for (int i = 0; i < drawnLine + name.Length + 2 - (2 * OuterLineLength); i++)
                {
                    output += "═";
                }
            }

            for (int i = 0; i < OuterLineLength; i++)
            {
                output += "-";
            }
            return output;
        }

        public void DrawStatusBar(int score, int anger)
        {
            string angerTxt = "Partners Aggression";
            string scoreTxt = "Point";
            string tmpS = score.ToString();
            string tmpA = anger.ToString();

            int angerBarCharLength = 15 + angerTxt.Length + tmpA.Length;
            int scoreBarCharLength = 1 + scoreTxt.Length + tmpS.Length;
            int betweenBarsSpace = LineLength - (angerBarCharLength + scoreBarCharLength);

            Console.Write($"{scoreTxt}: {score}");
            //DrawProgressBar(10, score, 100, scoreTxt);
            for (int i = 0; i < betweenBarsSpace; i++) { Console.Write(" "); }
            DrawProgressBar(10, anger, 100, angerTxt);

            Console.WriteLine();
        }

        public void DrawProgressBar(int BarCharLength, int curVal, int maxVal, string title = "Bar")
        {
            // Is not allowed, so we set curVal to 0
            if (curVal > maxVal)
            {
                curVal = maxVal;
            }

            // percent the value is
            double Percent = (double)curVal / (double)maxVal;

            int numOfblockChars = (int)(BarCharLength * Percent);

            Console.Write($"{title}: [{Percent * 100}%|");

            for (int i = 0; i < BarCharLength; i++)
            {
                if (i < numOfblockChars)
                {
                    Console.Write("█");
                }
                else
                {
                    Console.Write("-");
                }
            }
            Console.Write("]");
        }

    }
}
