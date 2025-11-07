using System;
using System.Threading.Tasks;

namespace cs
{
    public static class textDisplay
    {
        static Dictionary<Style, int> lowertypeDictionary = new Dictionary<Style, int> { { Style.Normal, 0 }, { Style.Bold, 0x1D41A }, { Style.Italic, 0x1D44E }, { Style.BoldItalic, 0x1D482 }, { Style.Script, 0x1D4B6 }, { Style.BoldScript, 0x1D4EA }, { Style.Fraktur, 0x1D51E }, { Style.DoubleStruck, 0x1D552 }, { Style.BoldFraktur, 0x1D586 }, { Style.SansSerif, 0x1D5BA }, { Style.BoldSansSerif, 0x1D5EE }, { Style.ItalicSansSerif, 0x1D622 }, { Style.BoldItalicSansSerif, 0x1D656 }, { Style.Monospace, 0x1D68A } };
        static Dictionary<Style, int> uppertypeDictionary = new Dictionary<Style, int> { { Style.Normal, 0 }, { Style.Bold, 0x1D400 }, { Style.Italic, 0x1D434 }, { Style.BoldItalic, 0x1D468 }, { Style.Script, 0x1D49C }, { Style.BoldScript, 0x1D4D0 }, { Style.Fraktur, 0x1D504 }, { Style.DoubleStruck, 0x1D538 }, { Style.BoldFraktur, 0x1D56C }, { Style.SansSerif, 0x1D5A0 }, { Style.BoldSansSerif, 0x1D5D4 }, { Style.ItalicSansSerif, 0x1D608 }, { Style.BoldItalicSansSerif, 0x1D63C }, { Style.Monospace, 0x1D670 } };
        public enum Style { Normal, Bold, Italic, BoldItalic, Script, BoldScript, Fraktur, DoubleStruck, BoldFraktur, SansSerif, BoldSansSerif, ItalicSansSerif, BoldItalicSansSerif, Monospace }
        // The initialization of the Enum types of the text styles and the Dictionaries that hold the corresponding unicode hexadecimal for each text styles.  

        /// <summary>
        /// A class that is capable of displaying text in the terminal in a more Storywise fashion
        /// </summary>
        /// <param name="text">The text that to be displayed onto the terminal</param>
        /// <param name="newLine">A flag that determines if the next text should be displayed on the same line or a new</param>
        /// <param name="charDelay">The delay between the display of each character</param>
        /// <param name="punctDelay">The multiplier that multiplies the charDelay when a punctuation marks is met</param>
        /// <param name="color">An enum type that determines the color of the text</param>
        /// <param name="backgroundColor">An enum type that determines the background color of the text</param>
        /// <param name="ANSI">An integer that determines how the text should be displayed in ANSI espace code</param>
        /// <param name="TextStyle">An enum type that determines the text style of the text to be displayed on the terminal</param>

        public static void Display(string text, string text2 = null, int split = 0, bool newLine = true, int charDelay = 100, int punctDelay = 1, ConsoleColor color = ConsoleColor.White, ConsoleColor backgroundColor = ConsoleColor.Black, int ANSI = 10, Style TextStyle = Style.Normal)
        {
            // The "Console.OutputEncoding = System.Text.Encoding.UTF8;" basically makes the Consoles STDIN able to interpret the unicode of the text styles.
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            int count = 0;

            //Basically a for loop that iterates between eách character in the text 
            foreach (char character in text)
            {
                // Code that tells the Console class the color and background color of the text, that was given by the parameter of the method
                Console.ForegroundColor = color;
                Console.BackgroundColor = backgroundColor;
                
                if (count >= split && split >= 1)
                {
                    Console.WriteLine("");
                    count = 0;
                }

                //Checks if a key has been pressed and writes the whole text to the terminal at once.
                if (Console.KeyAvailable)
                {
                    Console.ReadKey(true);
                    charDelay = 0;
                }
                //An if statement that checks if the given Enum text style type is present in the Dictionary and an else statement that will display an error message.
                if (lowertypeDictionary.ContainsKey(TextStyle) || uppertypeDictionary.ContainsKey(TextStyle))
                {
                    //An if statement that check if the character is a character of the lowercase english alphabet and makes sure to skip if the text style is Normal.
                    if (character >= 'a' && character <= 'z' && TextStyle != Style.Normal)
                    {
                        //The offset essentially converts the character into an integer which is between 1 and 25. It then offsets it with 1 represented with the character 'a'. This is beacuse the text style is repsented in an integer form or in this case an hexadecimal, which you can see in the dictionaries. Each letter in the alphabet in the given text style is represented by the hexadecimal plus the the integer form of the letter, which corresponds to the number in its place in the alphabetic sequence. For example the text style Monospace of the letter m is 0x1D68A + 13, because m is the 13 letter in the alphabet. But 0x1D68A actually is the representation of the letter a, which is the reason why we need the offset of 1, because otherwise the letter a would become the text style b 0x1D68A(the letter a) + 1 = 0x1D68B = letter b. It also includes the other letters.  
                        int offset = character - 'a';
                        //The char.ConvertFromUtf32 converts the given calculated hexadecimal into type char. The added content of the string "\x1b + Ansi + "m" + (The character) + \x1b[0m" is the method of converting the text into the corresponding ANSI escape code. In more details the "\1x1b(Ansi)m" is what actually determines the ANSi escape code and has 10 different ANSI types and is represented through 0 to 9. In short the ANSI escape code just tells the terminal of how to display the text" 
                        Console.Write("\x1b[" + ANSI + "m" + char.ConvertFromUtf32(offset + lowertypeDictionary[TextStyle]) + "\x1b[0m");
                    }

                    //This is the else if statement of the uppercases of the english alphabet and works exactly the same as the lowercases, but has different unicode hexadecimals
                    else if (character >= 'A' && character <= 'Z' && TextStyle != Style.Normal)
                    {
                        int offset = character - 'A';
                        Console.Write("\x1b[" + ANSI + "m" + char.ConvertFromUtf32(offset + uppertypeDictionary[TextStyle]) + "\x1b[0m");
                    }

                    //This else statemenmt only fires if the text style is Normal, because we don't have to through the unicode hexadecimal calculation.
                    else
                    {
                        Console.Write("\x1b[" + ANSI + "m" + character + "\x1b[0m");
                    }
                }
                else
                {
                    Console.Write("Invalid text style type");
                    break;
                }

                if (text2 != null)
                {
                    int x = Console.CursorLeft;
                    int y = Console.CursorTop;
                    Console.Write(text2);
                    Console.SetCursorPosition(x,y);
                }

                //This is the code that gives the delay between each display of each character to the terminal. It consist of a sort of "if statement". If the character is a punctuation mark then have the delay to be the charDelay multiplied by the punctDelay. Otherwise have the delay to be just the charDelay
                Task.Delay((character == '.' || character == '?' || character == '!' || character == ':') ? (charDelay * punctDelay) : (charDelay)).Wait();
                count++;
            }

            //These if and else statement just determines if the next text should be displayed on the same line or on a new 
            if (newLine)
            {
                Console.WriteLine("");
            }
            else
            {
                Console.Write(" ");
            }
        }
    }
}
