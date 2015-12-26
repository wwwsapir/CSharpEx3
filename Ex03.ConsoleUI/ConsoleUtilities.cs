using System;

namespace Ex03.ConsoleUI
{
    public class ConsoleUtilities
    {
        private const string k_YesStr = "Y";
        private const string k_NoStr = "N";
        private const int k_MaxNumOfDigitsInInput = 10;

        internal static void PromptMessage(string i_Message)
        {
            Console.Clear();
            Console.WriteLine(i_Message);
            Console.WriteLine("Press Enter key to continue");
            Console.ReadLine();
        }

        internal static bool AskUserBooleanQuestion(string i_QuestionString)
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
                        ShowBadInputMessage();
                        break;
                }
            }

            return resultValue.Value;
        }

        internal static string GetInputString(string i_MessageToUser, int i_MaxNumOfAllowedChars)
        {
            Console.WriteLine(i_MessageToUser);
            string strnextAction = Console.ReadLine();
            strnextAction = strnextAction.Trim();

            while (string.IsNullOrEmpty(strnextAction) || strnextAction.Length > i_MaxNumOfAllowedChars)
            {
                ShowBadInputMessage();
                strnextAction = Console.ReadLine();
            }

            return strnextAction;
        }

        internal static int GetPosNumFromUser(string i_MessageToUser)
        {
            int intParseRes;

            string strnextAction = GetInputString(i_MessageToUser, k_MaxNumOfDigitsInInput);
            bool strIsPosNumber = int.TryParse(strnextAction, out intParseRes) && intParseRes > 0;     // Checking if a positive valid number entered
            while (!strIsPosNumber)
            {
                ShowBadInputMessage();
                strnextAction = GetInputString(i_MessageToUser, k_MaxNumOfDigitsInInput);
                strIsPosNumber = int.TryParse(strnextAction, out intParseRes) && intParseRes > 0;     // Checking if a positive valid number entered
            }

            return intParseRes;
        }

        internal static void ShowBadInputMessage()
        {
            Console.WriteLine("Input is not Legal, try again");
        }
    }
}
