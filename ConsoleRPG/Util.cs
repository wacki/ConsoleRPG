using System;
using System.Text.RegularExpressions;

namespace ConsoleRPG {

    class Util {

        public delegate bool CustomValidateFuncCallback<T>(string input, out T output);

        static public int GetIntegerInput()
        {
            while(true) {
                int result;

                if(!int.TryParse(GetInput(), out result))
                    Console.WriteLine("Please enter a valid numerical value");
                else
                    return result;
            }
        }

        static public int GetIntegerInput(int min, int max)
        {
            while(true) {
                int result = GetIntegerInput();

                if(result < min || max < result)
                    Console.WriteLine("Please enter a number between " + min + " or " + max);
                else
                    return result;
            }
        }

        // generic input function with custom validate callback
        static public T GetInput<T>(CustomValidateFuncCallback<T> validate, string errorMsg)
        {
            T result;
            while(true) {
                if(!validate(GetInput(), out result)) {
                    Console.WriteLine(errorMsg);
                }
                else
                    break;
            }

            return result;
        }

        static public string GetInputRegex(string pattern, string errorMsg)
        {
            string result = GetInput<string>((string input, out string output) => {
                // match every string without special characters or digits in them
                Regex r = new Regex(pattern, RegexOptions.IgnoreCase);
                Match m = r.Match(input);
                output = input;
                return m.Success;
            }, errorMsg);

            return result;
        }



        static public bool YesNoPrompt()
        {
            return YesNoPrompt("Please enter yes or no.");
        }

        static public bool YesNoPrompt(string errorMsg = "")
        {
            return GetInput<bool>((string input, out bool output) => {
                output = false;

                if(input.Equals("yes", StringComparison.OrdinalIgnoreCase) ||
                input.Equals("y", StringComparison.OrdinalIgnoreCase)) {
                    output = true;
                    return true;
                }
                else if(input.Equals("no", StringComparison.OrdinalIgnoreCase) ||
                input.Equals("n", StringComparison.OrdinalIgnoreCase)) {
                    output = false;
                    return true;
                }

                return false;
            }, errorMsg);
        }

        static public string GetInput()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine();
            string result = Console.ReadLine();
            Console.WriteLine();
            Console.ResetColor();
            return result;
        }

        public static T Clamp<T>(T value, T min, T max) where T : System.IComparable<T>
        {
            T result = value;
            if(value.CompareTo(max) > 0)
                result = max;
            if(value.CompareTo(min) < 0)
                result = min;
            return result;
        }



        public static void ConsoleWriteCol(ConsoleColor fgColor, string text)
        {
            ConsoleWriteCol(fgColor, ConsoleColor.Black, text);
        }
        public static void ConsoleWriteCol(ConsoleColor fgColor, ConsoleColor bgColor, string text)
        {
            ConsoleColor fgColorOrig = Console.ForegroundColor;
            ConsoleColor bgColorOrig = Console.BackgroundColor;

            Console.ForegroundColor = fgColor;
            Console.BackgroundColor = bgColor;

            Console.Write(text);

            Console.ForegroundColor = fgColorOrig;
            Console.BackgroundColor = bgColorOrig;
        }

    }


    class Vector2i {
        public int x;
        public int y;

        public static Vector2i operator+(Vector2i v1, Vector2i v2)
        {
            return new Vector2i(v1.x + v2.x, v1.y + v2.y);
        }

        public Vector2i(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

}
