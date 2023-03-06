using ATM_App.Domain.Entities;
using System;


namespace ATM_App.UI
{
    public class AppScreen
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

        internal static int SelectAmount()
        {
            Console.WriteLine("");
            Console.WriteLine(":1.{0}500      5.{0}10,000", cur);
            Console.WriteLine(":2.{0}1000     6.{0}15,000", cur);
            Console.WriteLine(":3.{0}2000     7.{0}20,000", cur);
            Console.WriteLine(":4.{0}5000     8.{0}40,000", cur);
            Console.WriteLine(":0.Other");
            Console.WriteLine("");

            int selectedAmount = Validator.Convert<int>("option:");
            switch (selectedAmount)
            {
                case 1:
                    return 500;
                case 2:
                    return 1000;
                case 3:
                    return 2000;
                case 4:
                    return 5000;
                case 5:
                    return 10000;
                case 6:
                    return 15000;
                case 7:
                    return 20000;
                case 8:
                    return 40000;
                case 0:
                    return 0;
                default:
                    Utility.PrintMessage("Invalid input. Try again.", false);
                    return -1;
            }
        }
        internal InternalTransfer InternalTransferForm()
        {
            var internalTransfer = new InternalTransfer();
            internalTransfer.ReciepeintBankAccountNumber = Validator.Convert<long>("recipient's account number:");
            internalTransfer.TransferAmount = Validator.Convert<decimal>($"amount {cur}");
            internalTransfer.RecipientBankAccountName = Utility.GetUserInput("recipient's name:");
            return internalTransfer;
        } 
    }
}
