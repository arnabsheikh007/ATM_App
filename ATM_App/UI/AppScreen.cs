using ATM_App.Domain.Entities;
using System;


namespace ATM_App.UI
{
    public static class AppScreen
    {
        internal static string cur = "BDT ";
        internal static void Welcome()
        {
            Console.Clear();
            Console.Title = "My ATM App";
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("\n\n-----------Welcome to my ATM App----------\n\n");
            Console.WriteLine("Please Enter your ATM Card");
            Console.WriteLine("Actual ATM Machine will read the number validate your card and accept it");
            Utility.PressEnterToContinue();
        }


        internal static UserAccount UserLoginForm()
        {
            UserAccount tempUserAccount = new UserAccount();

            tempUserAccount.CardNumber = Validator.Convert<long>("Your card number");
            tempUserAccount.CardPin = Convert.ToInt32(Utility.GetSecretInput("Enter your card pin"));

            return tempUserAccount;
        }

        internal static void LoginProgress()
        {
            Console.WriteLine("\n\nChecking Card Number and PIN...");
            Utility.PrintDotAnimation(10);
        }

        internal static void PrintLockMsg()
        {
            Console.Clear();
            Utility.PrintMessage("Your Account is locked. Please go to the nearest branch", false);
            Utility.PressEnterToContinue();
            Environment.Exit(1);
        }

        internal static void WelcomeCustomer(string FullName)
        {
            Console.WriteLine($"Welcome {FullName}");
            Utility.PressEnterToContinue();
        }

        internal static void DisplayAppMenu()
        {
            Console.Clear();
            Console.WriteLine("----------App Menu----------");
            Console.WriteLine(":                          :");
            Console.WriteLine("1. Account Balance         :");
            Console.WriteLine("2. Cash Deposit            :");
            Console.WriteLine("3. Withdrawal              :");
            Console.WriteLine("4. Transfer Balance        :");
            Console.WriteLine("5. Transections            :");
            Console.WriteLine("6. Log Out                 :");
        }

        internal static void LogOutProgress()
        {
            Console.WriteLine("Good Bye");
            Utility.PrintDotAnimation();
            Console.Clear();
        }

    }
}
