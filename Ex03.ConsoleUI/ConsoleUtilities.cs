using System;
using System.Globalization;
using System.Collections.Generic;

namespace Ex03.ConsoleUI
{
    public class ConsoleUtilities
    {
        private const string k_YesStr = "Y";
        private const string k_NoStr = "N";
        private const int k_MaxNumOfDigitsInInput = 10;
        private const int k_MaxNumOfAllowedChars = 30;
        private const bool k_OnlyIntegerAllowed = true;

        public static void PromptMessage(string i_Message)
        {
            Console.Clear();
            Console.WriteLine(i_Message);
            Console.WriteLine("Press any key to continue..");
            Console.ReadKey();
            Console.Clear();
        }
        
        public static bool AskUserBooleanQuestion(string i_QuestionString)
        {
            bool? resultValue = null;

            Console.WriteLine("{0}, ({1}/{2})", i_QuestionString, k_YesStr, k_NoStr);

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
            string inputString = Console.ReadLine();
            inputString = inputString.Trim();

            while (string.IsNullOrEmpty(inputString) || inputString.Length > i_MaxNumOfAllowedChars)
            {
                ShowBadInputMessage("Empty Input is not allowed");
                inputString = Console.ReadLine();
            }

            return inputString;
        }

        public static decimal GetPosNumFromUser(string i_MessageToUser, bool i_OnlyInteger)
        {
            return GetPosNumFromUser(i_MessageToUser, i_OnlyInteger, decimal.MaxValue);
        }

        public static decimal GetPosNumFromUser(string i_MessageToUser, bool i_OnlyInteger, decimal i_MaxValue)
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

            string inputString;
            bool parsingSuccess;
            bool parseResInRange;
            bool inputIsValid;
            do
            {
                inputString = GetInputString(i_MessageToUser, k_MaxNumOfDigitsInInput);
                parsingSuccess = decimal.TryParse(inputString, style, CultureInfo.CurrentCulture, out decimalParseRes);
                parseResInRange = decimalParseRes > 0 && decimalParseRes <= i_MaxValue;
                inputIsValid = parsingSuccess && parseResInRange;

                if (!parsingSuccess)
                {
                    ShowBadInputMessage("Only digits allowed");
                }
                else if (parsingSuccess && decimalParseRes == 0)
                {
                    ShowBadInputMessage("Zero value not allowed");
                }
                else if (parsingSuccess && !parseResInRange)
                {
                    string userMessage = string.Format("Input Out Of Range. Max value: {0}", i_MaxValue);
                    ShowBadInputMessage(userMessage);
                }
            }
            while (!inputIsValid);

            return decimalParseRes;
        }

        public static void ShowBadInputMessage(string i_Reason)
        {
            Console.WriteLine("Input Error; " + i_Reason);
        }

        public static int ChooseEnumValue(Type i_EnumType)
        {
            List<string> valuesDesc = new List<string>();

            foreach (object enumValue in Enum.GetValues(i_EnumType))
            {
                valuesDesc.Add(enumValue.ToString());
            }

            return ChooseEnumValue(i_EnumType, valuesDesc);
        }

        public static int ChooseEnumValue(Type i_EnumType, List<string> i_ValuesDesc)
        {
            if (i_ValuesDesc == null)
            {
                throw new ArgumentNullException("i_ValuesDesc");
            }

            showListingChoices(i_ValuesDesc);
            int choice;
            bool choiceValid;
            do
            {
                choice = (int)GetPosNumFromUser("Please choose one of the above", k_OnlyIntegerAllowed);
                choiceValid = Enum.IsDefined(i_EnumType, choice - 1);

                if (!choiceValid)
                {
                    ShowBadInputMessage("Invalid choice");
                }
            }
            while (!choiceValid);

            return choice - 1;
        }

        private static void showListingChoices(List<string> i_ValuesDesc)
        {
            string userMessage = string.Empty;
            byte listCounter = 0;

            foreach (string valueDesc in i_ValuesDesc)
            {
                listCounter++;
                userMessage += listCounter + " : " + valueDesc + Environment.NewLine;
            }

            Console.Write(userMessage);
        }
    }
}
