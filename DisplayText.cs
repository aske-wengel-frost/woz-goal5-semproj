namespace cs
{

    using System;
    using System.Data;
    using System.Diagnostics;
    using System.Formats.Asn1;
    using System.Net.NetworkInformation;
    using System.Runtime.InteropServices;
    using System.Runtime.InteropServices.Marshalling;
    using System.Text;
    using System.Threading;

    using static System.Runtime.InteropServices.JavaScript.JSType;

    class Program
    {
        private static readonly object consoleLock = new();
        static bool stopAnimation = false;
        static Dictionary<Style, int> lowertypeDictionary = new Dictionary<Style, int> { { Style.Normal, 0 }, { Style.Bold, 0x1D41A }, { Style.Italic, 0x1D44E }, { Style.BoldItalic, 0x1D482 }, { Style.Script, 0x1D4B6 }, { Style.BoldScript, 0x1D4EA }, { Style.Fraktur, 0x1D51E }, { Style.DoubleStruck, 0x1D552 }, { Style.BoldFraktur, 0x1D586 }, { Style.SansSerif, 0x1D5BA }, { Style.BoldSansSerif, 0x1D5EE }, { Style.ItalicSansSerif, 0x1D622 }, { Style.BoldItalicSansSerif, 0x1D656 }, { Style.Monospace, 0x1D68A } };
        static Dictionary<Style, int> uppertypeDictionary = new Dictionary<Style, int> { { Style.Normal, 0 }, { Style.Bold, 0x1D400 }, { Style.Italic, 0x1D434 }, { Style.BoldItalic, 0x1D468 }, { Style.Script, 0x1D49C }, { Style.BoldScript, 0x1D4D0 }, { Style.Fraktur, 0x1D504 }, { Style.DoubleStruck, 0x1D538 }, { Style.BoldFraktur, 0x1D56C }, { Style.SansSerif, 0x1D5A0 }, { Style.BoldSansSerif, 0x1D5D4 }, { Style.ItalicSansSerif, 0x1D608 }, { Style.BoldItalicSansSerif, 0x1D63C }, { Style.Monospace, 0x1D670 } };
        static Style[] array = { Style.Normal, Style.BoldItalic, Style.Bold, Style.Italic, Style.BoldItalic, Style.Script, Style.BoldScript, Style.Fraktur, Style.DoubleStruck, Style.BoldFraktur, Style.SansSerif, Style.BoldSansSerif, Style.ItalicSansSerif, Style.BoldItalicSansSerif, Style.Monospace };
        static List<string> TextList = new List<string>();
        static List<int> LeftList = new List<int>();
        static List<int> TopList = new List<int>();
        static List<ConsoleColor> ColorList = new List<ConsoleColor>();
        static List<bool> Animation1List = new List<bool>();
        static List<bool> Animation2List = new List<bool>();
        static List<int> AnsiList = new List<int>();
        static List<int> NumberList = new List<int>();
        static Random random = new Random();
        static int number = 0;
        static int count = 0;
        static int sAnsi = 10;
        static int left;
        static int top;
        static bool thread1;
        static bool thread2;

        enum Style { Normal, Bold, Italic, BoldItalic, Script, BoldScript, Fraktur, DoubleStruck, BoldFraktur, SansSerif, BoldSansSerif, ItalicSansSerif, BoldItalicSansSerif, Monospace }

        static void print(string text, bool enter = true, int charDelay = 100, int punctDelay = 1, ConsoleColor color = ConsoleColor.White, ConsoleColor backgroundColor = ConsoleColor.Black, int Ansi = 10, bool Animation1 = false, Style style = Style.Normal, bool Animation2 = false)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;
            Console.CursorVisible = false;
            count = 0;
            int sLeft = Console.CursorLeft;
            int sTop = Console.CursorTop;
            int multiplier = 1;
            number = Array.IndexOf(array, style);
            sAnsi = Ansi;
            Console.BackgroundColor = backgroundColor;
            foreach (char c in text)
            {
                do
                {
                    if (!thread2)
                    {
                        thread1 = true;
                        if (lowertypeDictionary.ContainsKey(style) || uppertypeDictionary.ContainsKey(style) || Animation2 || Animation1)
                        {
                            if (Animation1 || Animation2)
                            {
                                if (Animation1)
                                {
                                    sAnsi = random.Next(1, 11);
                                }
                                if (Animation2)
                                {
                                    number = random.Next(0, 14);
                                }
                                Console.SetCursorPosition(sLeft, sTop);
                            }
                            for (int i = 0; i < count; i++)
                            {
                                output(text[i], array[number], sAnsi, color);

                            }
                            output(c, array[number], sAnsi, color);

                            if (Animation1 || Animation2)
                            {
                                count++;
                            }
                            if (count == text.Length)
                            {
                                stopAnimation = true;
                                TextList.Add(text);
                                LeftList.Add(sLeft);
                                TopList.Add(sTop);
                                ColorList.Add(color);
                                Animation1List.Add(Animation1);
                                Animation2List.Add(Animation2);
                                AnsiList.Add(sAnsi);
                                NumberList.Add(number);
                                if (TextList.Count == 1)
                                {
                                    Thread animationThread = new Thread(Animation);
                                    animationThread.Start();
                                }
                            }
                        }
                        left = Console.CursorLeft;
                        top = Console.CursorTop;
                        thread1 = false;
                        if (punctDelay != 1)
                        {
                            if (c == '.' || c == '!' || c == '?')
                            {
                                multiplier = punctDelay;
                            }
                        }
                        Task.Delay(charDelay * multiplier).Wait();
                        multiplier = 1;
                        break;
                    }
                }
                while (true);
            }
            Console.Write(enter ? "\n" : " ");
            left = Console.CursorLeft;
            top = Console.CursorTop;
        }

        static void output(char c, Style type, int Ansi, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            if (type != Style.Normal)
            {
                if (c >= 'a' && c <= 'z')
                {
                    int offset = c - 'a';
                    Console.Write("\x1b[" + Ansi + "m" + char.ConvertFromUtf32(offset + lowertypeDictionary[type]) + "\x1b[0m");
                }

                else if (c >= 'A' && c <= 'Z')
                {
                    int offset = c - 'A';
                    Console.Write("\x1b[" + Ansi + "m" + char.ConvertFromUtf32(offset + uppertypeDictionary[type]) + "\x1b[0m");
                }

                else
                {
                    Console.Write(c);
                }
            }
            else
            {
                Console.Write("\x1b[" + Ansi + "m" + c + "\x1b[0m");
            }
        }

        static string ReadInput()
        {
            string inputBuffer = "";
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    thread1 = true;
                    var key = Console.ReadKey(true);
                    do
                    {
                        if (!thread2)
                        {
                            if (key.Key == ConsoleKey.Enter)
                            {
                                thread1 = false;
                                return inputBuffer;
                            }
                            else
                            {
                                inputBuffer += key.Key;
                                Console.SetCursorPosition(left, top);
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.Write(key.KeyChar);
                                if (left == Console.WindowWidth - 1)
                                {
                                    top++;
                                    left = 0;
                                }
                                else
                                {
                                    left++;
                                }
                                thread1 = false;
                            }
                            break;
                        }
                    } while (true);
                }
            }
        }

        static void Animation()
        {
            do
            {
                if (!thread1)
                {
                    thread2 = true;
                    for (int i = 0; i < TextList.Count; i++)
                    {
                        int lAnsi = 0;
                        int lNumber = 0;
                        if (Animation1List[i])
                        {
                            lAnsi = random.Next(1, 11);
                        }
                        else if (Animation2List[i])
                        {
                            lNumber = random.Next(0, 14);
                        }
                        else
                        {
                            lAnsi = AnsiList[i];
                            lNumber = NumberList[i];
                        }
                        int lLeft = LeftList[i];
                        int lTop = TopList[i];
                        for (int j = 0; j < TextList[i].Length; j++)
                        {
                            while (true)
                            {
                                Console.SetCursorPosition(lLeft, lTop);
                                output(TextList[i][j], array[lNumber], lAnsi, ColorList[i]);
                                lLeft++;
                                break;
                            }

                        }
                    }
                    Console.SetCursorPosition(left, top);
                    thread2 = false;
                    Console.ForegroundColor = ConsoleColor.White;
                    Task.Delay(200).Wait();
                }
            }
            while (stopAnimation);
            TextList = new List<string>();
            LeftList = new List<int>();
            TopList = new List<int>();
            ColorList = new List<ConsoleColor>();
            Animation1List = new List<bool>();
            Animation2List = new List<bool>();
            AnsiList = new List<int>();
            NumberList = new List<int>();
        }
    }
}
