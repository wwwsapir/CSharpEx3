using System;
using System.Globalization;

namespace Ex03.ConsoleUI
{
    public class ConsoleUtilities
    {
        private const string k_YesStr = "Y";
        private const string k_NoStr = "N";
        private const int k_MaxNumOfDigitsInInput = 10;
        private const int k_MaxNumOfAllowedChars = 30;

        public static void PromptMessage(string i_Message)
        {
            Console.Clear();
            Console.WriteLine(i_Message);
            Console.WriteLine("Press Enter key to continue");
            Console.ReadLine();
        }

        public static bool AskUserBooleanQuestion(string i_QuestionString)
        {
            bool? resultValue = null;

            Console.WriteLine("{0}, ({1}/{2})?", i_QuestionString, k_YesStr, k_NoStr);

            while (resultValue == null)
            {
                string userInputStr = Console.ReadLine().Trim().ToUpper();
                switch (userInputStr)
                {
                    case k_YesStr:
                        resultValue = true;
                        break;
                    case k_NoStr:
                        resultValue = false;
                        break;
                    default:
                        ShowBadInputMessage("Input must be in the given format above");
                        break;
                }
            }

            return resultValue.Value;
        }

        public static string GetInputString(string i_MessageToUser)
        {
            return GetInputString(i_MessageToUser, k_MaxNumOfAllowedChars);
        }

        public static string GetInputString(string i_MessageToUser, int i_MaxNumOfAllowedChars)
        {
            Console.WriteLine(i_MessageToUser);
            string strnextAction = Console.ReadLine();
            strnextAction = strnextAction.Trim();

            while (string.IsNullOrEmpty(strnextAction) || strnextAction.Length > i_MaxNumOfAllowedChars)
            {
                ShowBadInputMessage("Empty Input is not allowed");
                strnextAction = Console.ReadLine();
            }

            return strnextAction;
        }

        public static decimal GetPosNumFromUser(string i_MessageToUser, bool i_OnlyInteger)
        {
            decimal decimalParseRes;
            NumberStyles style;

            if (i_OnlyInteger)
            {
                style = NumberStyles.Integer;
            }
            else
            {
                style = NumberStyles.AllowDecimalPoint;
            }

            string strnextAction = GetInputString(i_MessageToUser, k_MaxNumOfDigitsInInput);
            bool inputIsValid = decimal.TryParse(strnextAction, style, CultureInfo.CurrentCulture, out decimalParseRes) && decimalParseRes > 0;     // Checking if a positive valid number entered
            while (!inputIsValid)
            {
                ShowBadInputMessage("Input may only contains digits(zero value is not allowed) try again..");
                strnextAction = GetInputString(i_MessageToUser, k_MaxNumOfDigitsInInput);
                inputIsValid = decimal.TryParse(strnextAction, style, CultureInfo.CurrentCulture, out decimalParseRes) && decimalParseRes > 0;     // Checking if a positive valid number entered
            }

            return decimalParseRes;
        }

        public static void ShowBadInputMessage(string i_Reason)
        {
            Console.WriteLine("Input is not valid; " + i_Reason);
        }
    }
}
