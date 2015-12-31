using System;
using System.Globalization;
using System.Collections.Generic;

namespace Ex03.ConsoleUI
{
    public static class ConsoleUtilities
    {
        private const string k_YesStr = "Y";
        private const string k_NoStr = "N";
        private const int k_MaxNumOfDigitsInInput = 10;
        private const int k_MaxNumOfAllowedChars = 30;
        private const bool k_OnlyIntegerAllowed = true;

        // clear screen and prompt message to user
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

        // Overload of getting non empty string from user, with predefined max length
        public static string GetInputString(string i_MessageToUser)
        {
            return GetInputString(i_MessageToUser, k_MaxNumOfAllowedChars);
        }

        // Overload of getting non empty string from user, with given max length
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

        // getting positive number from user.if i_IntegerOnly == true then only integers allowed, otherwise decimal point allowed
        public static decimal GetPosNumFromUser(string i_MessageToUser, bool i_IntegerOnly)
        {
            bool numIsPositive = false;
            decimal inputNumber;
            do
            {
                inputNumber = GetNonNegativeNumFromUser(i_MessageToUser, i_IntegerOnly);
                if (inputNumber == 0)
                {
                    ShowBadInputMessage("Zero value is not allowed");
                }
                else
                {
                    numIsPositive = true;
                }
            }
            while (!numIsPositive);

            return inputNumber;
        }

        // overload of getting non-negative number from user, with an option to define maximum value
        public static decimal GetNonNegativeNumFromUser(string i_MessageToUser, bool i_IntegerOnly)
        {
            decimal decimalParseRes;
            NumberStyles style;

            // updating parsing arguments
            if (i_IntegerOnly)
            {
                style = NumberStyles.Integer;
            }
            else
            {
                style = NumberStyles.AllowDecimalPoint;
            }

            string inputString;
            bool parsingSuccess;
            do
            {
                inputString = GetInputString(i_MessageToUser, k_MaxNumOfDigitsInInput);
                parsingSuccess = decimal.TryParse(inputString, style, CultureInfo.CurrentCulture, out decimalParseRes);
                if (!parsingSuccess)
                {
                    ShowBadInputMessage("Only digits allowed");
                }
            }
            while (!parsingSuccess);

            return decimalParseRes;
        }

        public static void ShowBadInputMessage(string i_Reason)
        {
            Console.WriteLine("Input Error; " + i_Reason);
        }

        // getting enum type, asking user to choose one of the option, showing all listing possible using reflection
        public static int ChooseEnumValue(Type i_EnumType)
        {
            List<string> valuesDesc = new List<string>();

            foreach (object enumValue in Enum.GetValues(i_EnumType))
            {
                valuesDesc.Add(enumValue.ToString());
            }

            return ChooseEnumValue(i_EnumType, valuesDesc);
        }

        // overloadto ChooseEnumValue, here showing all possible values using i_ValuesDesc, that must contains all possible enum description respectively
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

        // showing a list of of strings, each with unique index
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
