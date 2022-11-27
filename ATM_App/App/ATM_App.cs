using ATM_App.Domain.Entities;
using ATM_App.Domain.Enums;
using ATM_App.Domain.Interfaces;
using ATM_App.UI;
using System;
using System.Collections.Generic;

namespace ATM_App
{
    public class ATM_App : IUserLogin, IUserAccountActions

    {
        private List<UserAccount> userAccountList;
        private UserAccount selectedAccount;

        public void Run()
        {
            AppScreen.Welcome();
            CheckUserCardNumAndPassword();
            AppScreen.WelcomeCustomer(selectedAccount.FullName);
            AppScreen.DisplayAppMenu();
            ProcessMenuOption();
        }

        public void CheckUserCardNumAndPassword()
        {
            bool isCorrectLogin = false;
            while (isCorrectLogin == false)
            {
                UserAccount inputAccount = AppScreen.UserLoginForm();
                AppScreen.LoginProgress();
                foreach(UserAccount account in userAccountList)
                {
                    selectedAccount = account;
                    if (inputAccount.CardNumber.Equals(selectedAccount.CardNumber))
                    {
                        selectedAccount.TotalLogin++;
                        if (inputAccount.CardPin.Equals(selectedAccount.CardPin))
                        {
                            selectedAccount = account;
                            if(selectedAccount.IsLocked || selectedAccount.TotalLogin > 3)
                            {
                                AppScreen.PrintLockMsg();
                            }
                            else
                            {
                                selectedAccount.TotalLogin = 0;
                                isCorrectLogin = true;
                                break;
                            }
                        }
                    }
                    if(isCorrectLogin == false)
                    {
                        Utility.PrintMessage("\nInvalid Card number or PIN....",false);

                        selectedAccount.IsLocked = selectedAccount.TotalLogin == 3;
                        if (selectedAccount.IsLocked)
                        {
                            AppScreen.PrintLockMsg();
                        }
                    }
                    Console.Clear();
                }
            }
        }


        private void ProcessMenuOption()
        {
            switch(Validator.Convert<int>("an option: "))
            {
                case (int)AppMenu.CheckBalance:
                    CheckBalance();
                    break;
                case (int)AppMenu.PlaceDeposit:
                    Console.WriteLine("placing deposit...");
                    break;
                case (int)AppMenu.MakeWithdrawal:
                    Console.WriteLine("Making Withdrawal...");
                    break;
                case (int)AppMenu.InternalTransfer:
                    Console.WriteLine("Making Internal Transfer...");
                    break;
                case (int)AppMenu.ViewTransaction:
                    Console.WriteLine("Viewing Transection...");
                    break;
                case (int)AppMenu.Logout:
                    AppScreen.LogOutProgress();
                    Utility.PrintMessage("You are successfully logged out. Please collect your ATM Card.");
                    Run();
                    break;
                default:
                    Utility.PrintMessage("Invalid Option",false);
                    break;
               
            }
        }

        


        public void InitializeData()
        {
            userAccountList = new List<UserAccount>
            {
                new UserAccount
                {
                    Id = 1,
                    FullName = "Arnab Sheikh",
                    AccountNumber = 123123123,
                    CardNumber = 321321321,
                    CardPin = 101010,
                    AccountBalance = 100000.00m,
                    IsLocked = false
                },
                new UserAccount
                {
                    Id = 2,
                    FullName = "Sheikh Arnab",
                    AccountNumber = 123123124,
                    CardNumber = 321321322,
                    CardPin = 121212,
                    AccountBalance = 150000.00m,
                    IsLocked = false
                },
                new UserAccount
                {
                    Id = 3,
                    FullName = "Sheikh Bahauddin Arnab",
                    AccountNumber = 123123125,
                    CardNumber = 321321323,
                    CardPin = 212121,
                    AccountBalance = 50000.00m,
                    IsLocked = true
                },
            };
        }

        public void CheckBalance()
        {
            Utility.PrintMessage($"Your Account Balance is {Utility.FormatAmount(selectedAccount.AccountBalance)}");
        }

        public void PlaceDeposit()
        {
            throw new NotImplementedException();
        }

        public void MakeWithdrawal()
        {
            throw new NotImplementedException();
        }
    }
}
