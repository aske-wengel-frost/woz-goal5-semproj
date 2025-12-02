namespace woz.Presentation
{
    using woz.Domain;
    using woz.Domain.Player;
    using woz.Domain.Story;
    using woz.Presentation.MapTerminal;

    using MapTerminal;

    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Dynamic;
    using System.Linq;
    using System.Reflection.Emit;
    using System.Runtime.ExceptionServices;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Transactions;
    using System.Xml;

    using static System.Formats.Asn1.AsnWriter;

    class UITerminal : IUIHandler
    {
        private TerminalMap map { get; set; } = new TerminalMap();
        int ConsoleViewCharLength { get; set; } = 120;
        int OuterLineLength { get; set; } = 10;
        int CharDelay { get; set; } = 10;

        /// <summary>
        /// Draws the scene in the terminal
        /// </summary>
        /// <param name="scene">The scene you want to have drawn</param>
        public void DrawScene(Scene scene, Player player)
        { 
            // Set scope-specific charDelay for anmiations.
            textDisplay.charDelay = 10;

            // Clear the terminal when a new scene is drawn
            ClearScreen();

            // Draw the top statusbar
            DrawStatusBar(player);

            // Draw scene depending on type
            if (scene is CutScene cutScene)
            {
                this.DrawCutScene(cutScene);
            }
            else if (scene is ContextScene contextScene)
            {
                this.DrawContextScene(contextScene);
            } 
            else if (scene is EndScene endScene)
            {
                this.DrawEndScene(endScene, player);
            }

        }

        /// <summary>
        /// Clears the terminal screen
        /// </summary>
        public void ClearScreen()
        {
            Console.Clear();
        }

        /// <summary>
        /// Draw error overload method for non descriptive error
        /// </summary>
        public void DrawError()
        {
            DrawError("Der er opstået en fejl");
        }

        /// <summary>
        /// Draw error method for printing errors in red
        /// </summary>
        /// <param name="errorMsg"></param>
        public void DrawError(string errorMsg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(errorMsg);
            Console.ResetColor();
        }

        /// <summary>
        /// Draws in green text
        /// </summary>
        /// <param name="infoMsg"></param>
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

                mapElements.Add(new MapRoomElement(area.Id, area.Frame.X, area.Frame.Y, area.Frame.Height, area.Frame.Width, area.Name));
            }
            map.Elements = mapElements;
        }

        /// <summary>
        /// Highlights a specefic area on the map, based on an id
        /// </summary>
        /// <param name="id"></param>
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

        // Helpers

        /// <summary>
        /// Draws a contextscene
        /// </summary>
        /// <param name="contextScene"></param>
        private void DrawContextScene(ContextScene contextScene)
        {
            Console.WriteLine(ConstructLine(contextScene.Area.Name));

            textDisplay.Display(contextScene.DialogueText, split: ConsoleViewCharLength, punctDelay: 7);

            //Console.WriteLine(LineConstructor(contextScene.Area.Name, false));
            Console.WriteLine(ConstructLine());

            Console.WriteLine();
            textDisplay.Display("Her er dine valgmuligheder:", punctDelay: 4);

            // Uses the index of the array to display the options ascendingly
            for (int i = 1; i < contextScene.Choices.Count() + 1; i++)
            {
                textDisplay.Display($"[{i}] > {contextScene.Choices[i - 1].Description}", punctDelay: 5);
            }


            Console.WriteLine("");
            textDisplay.Display("[hjælp] Hvis du er i tvivl", punctDelay: 5);
        }

        /// <summary>
        /// Draws a Cut Scene
        /// </summary>
        /// <param name="cutScene">the object of the CutScene to draw</param>
        private void DrawCutScene(CutScene cutScene)
        {
            Console.WriteLine(ConstructLine());
            textDisplay.Display(cutScene.ConditionInfo, split: ConsoleViewCharLength);
        }

        private void DrawEndScene(EndScene endScene, Player player)
        {
            textDisplay.Display(endScene.EndSceneContent);

            DrawInfo($"═════════════════════════════════════");
            DrawInfo($"  {player.Name}'s Totale score: {player.Score}");
            DrawInfo($"  Partnerens Aggressionsniveau: {player.PartnerAggression}%");
            DrawInfo($"═════════════════════════════════════");
        }

        /// <summary>
        /// Draws a horizontal line in the terminal
        /// </summary>
        /// <returns></returns>
        private string ConstructLine()
        {
            string output = "";
            output += ConstructCharLine(OuterLineLength, '-');
            output += ConstructCharLine(ConsoleViewCharLength - (2 * OuterLineLength), '=');
            output += ConstructCharLine(OuterLineLength, '-');
            return output;
        }

        /// <summary>
        /// Draws a horizontal line in the terminal with a text title
        /// </summary>
        /// <param name="title"></param>
        /// <param name="displayName"></param>
        /// <returns></returns>
        private string ConstructLine(string title)
        {
            string output = "";

            // initiates a new stringbuilder, and passes in the result of the ConstructLine method to which this method is a overload
            StringBuilder sb = new StringBuilder(ConstructLine());

            // Added the brackets to each side of the title
            string newTitle = $"[ {title} ]";

            // Gets the index within the string which represents the line, of where to place the title to get it to be roughly in the center
            int titleStartIndexInLine = (ConsoleViewCharLength - newTitle.Length) / 2 ;

            // adds the title to the line
            for(int i = 0; i < newTitle.Length; i++)
            {
                sb[i + titleStartIndexInLine] = newTitle[i];
            }
            
            // sets the output string and returns
            output = sb.ToString();
            return output;
        }
        
        private string ConstructCharLine(int length, char ch)
        {
            string retStr = "";
            for (int i = 0; i < length; i++)
            {
                retStr += ch;   
            }
            return retStr;
        }

        private void DrawStatusBar(Player player)
        {
            string angerTxt = "Partners Aggression";
            string scoreTxt = "Point";
            string tmpS = player.Score.ToString();
            string tmpA = player.PartnerAggression.ToString();

            int angerBarCharLength = 15 + angerTxt.Length + tmpA.Length;
            int scoreBarCharLength = 1 + scoreTxt.Length + tmpS.Length;
            int betweenBarsSpace = ConsoleViewCharLength - (angerBarCharLength + scoreBarCharLength) - 2;

            Console.Write($"{scoreTxt}: {player.Score.ToString("0")}");
            for (int i = 0; i < betweenBarsSpace; i++) { Console.Write(" "); }
            DrawProgressBar(10, player.PartnerAggression, player.MAX_AGRESSION, angerTxt);

            Console.WriteLine();
        }

        private void DrawProgressBar(int BarCharLength, int curVal, int maxVal, string title = "Bar")
        {
            // Is not allowed, so we set curVal to 0
            if (curVal > maxVal)
            {
                curVal = maxVal;
            }

            // percent the value is
            double Percent = (double)curVal / (double)maxVal;

            int numOfblockChars = (int)(BarCharLength * Percent);

            double num = Percent * 100;
            Console.Write($"{title}: [{num.ToString("0")}%|");

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
