using ATM_App.Domain.Entities;
using ATM_App.Domain.Enums;
using ATM_App.Domain.Interfaces;
using ATM_App.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ATM_App
{
    public class ATM_App : IUserLogin, IUserAccountActions, ITransecton

    {
        private List<UserAccount> userAccountList;
        private UserAccount selectedAccount;
        private List<Transection> _listOfTransections;
        private const decimal minKeptAmount = 500;
        private readonly AppScreen screen;

        public ATM_App()
        {
            screen = new AppScreen();
        }

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
                    PlaceDeposit();
                    break;
                case (int)AppMenu.MakeWithdrawal:
                    MakeWithdrawal();
                    break;
                case (int)AppMenu.InternalTransfer:
                    var internalTransfer = screen.InternalTransferForm();
                    ProcessInternalTransfer(internalTransfer);
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
            _listOfTransections = new List<Transection>();
        }

        public void CheckBalance()
        {
            Utility.PrintMessage($"Your Account Balance is {Utility.FormatAmount(selectedAccount.AccountBalance)}");
        }

        public void PlaceDeposit()
        {
            Console.WriteLine("\nOnly multiples of 500 and 1000 BDT Allowed\n");
            var transection_amount = Validator.Convert<int>($"Amount {AppScreen.cur}");

            //Simulate counting
            Console.WriteLine("\nChecking and Counting bank notes");
            Utility.PrintDotAnimation();
            Console.WriteLine("");

            //Some Guard Clause
            if(transection_amount <= 0)
            {
                Utility.PrintMessage("Amount needs to be greater than zero. Try again", false);
                return;
            }
            if(transection_amount % 500 != 0)
            {
                Utility.PrintMessage("Enter deposit amount multiple of 500 or 1000. Try again", false);
                return;
            }
            if (PreviewBankNotesCount(transection_amount) == false)
            {
                Utility.PrintMessage("You have canceled your action", false);
                return;
            }

            //bind transection details to transection object
            InsertTransection(selectedAccount.Id, TransectionType.Deposite, transection_amount, " ");

            //Update Account balance
            selectedAccount.AccountBalance += transection_amount;

            //print success message
            Utility.PrintMessage($"Your deposit of {Utility.FormatAmount(transection_amount)} was successful", true);
        }

        public void MakeWithdrawal()
        {
            var transection_amount = 0;
            int SelectedAmount = AppScreen.SelectAmount();
            if(SelectedAmount == -1)
            {
                SelectedAmount = AppScreen.SelectAmount();
            }
            else if(SelectedAmount != 0)
            {
                transection_amount = SelectedAmount;
            }
            else
            {
                transection_amount = Validator.Convert<int>($"Amount {AppScreen.cur}");
            }

            //Input validation
            if (transection_amount <= 0)
            {
                Utility.PrintMessage("Amount needs to be greater than zero. Try again", false);
                return;
            }
            if (transection_amount % 500 != 0)
            {
                Utility.PrintMessage("You can only withdraw multiple of 500 or 1000. Try again", false);
                return;
            }
            if(transection_amount > selectedAccount.AccountBalance)
            {
                Utility.PrintMessage("Insufficient Balance", false);
                return;
            }
            if((selectedAccount.AccountBalance - transection_amount) < minKeptAmount)
            {
                Utility.PrintMessage($"Withdrawal failed. You need to keep minimum {Utility.FormatAmount(minKeptAmount)}",false);
            }

            InsertTransection(selectedAccount.Id, TransectionType.Withdrawal, -transection_amount, "");
            selectedAccount.AccountBalance -= transection_amount;
            Utility.PrintMessage($"You have successfully withdrawn {Utility.FormatAmount(transection_amount)}", true);

        }

        private bool PreviewBankNotesCount(int amount)
        {
            int thousandNotesCounts = amount / 1000;
            int fiveHundredNotes = (amount % 1000) / 500;

            Console.WriteLine("\nSummary");
            Console.WriteLine("---------");
            Console.WriteLine($"{AppScreen.cur}1000 X {thousandNotesCounts} = {1000*thousandNotesCounts}");
            Console.WriteLine($"{AppScreen.cur} 500 X {fiveHundredNotes} = {500*fiveHundredNotes}");
            Console.WriteLine($"Total Amount : {Utility.FormatAmount(amount)}\n\n\n");

            int opt = Validator.Convert<int>("1to confirm");
            return opt.Equals(1);

        }

        public void InsertTransection(long _UserBankAccountID, TransectionType _tranType, decimal _tranAmount, string _desc)
        {
            var transection = new Transection()
            {
                TransectionID = Utility.GetTransectionID(),
                UserBankAccount = _UserBankAccountID,
                TransectionDate = DateTime.Now,
                TransectionType = _tranType,
                TransectionAmount = _tranAmount,
                Description = _desc
            };

            _listOfTransections.Add(transection);
        }

        public void ViewTransection()
        {
            throw new NotImplementedException();
        }
        private void ProcessInternalTransfer(InternalTransfer internalTransfer)
        {
            if (internalTransfer.TransferAmount <= 0)
            {
                Utility.PrintMessage("Amount needs to be more than zero. Try again.", false);
                return;
            }
            //check sender's account balance
            if (internalTransfer.TransferAmount > selectedAccount.AccountBalance)
            {
                Utility.PrintMessage($"Transfer failed. You do not hav enough balance" +
                    $" to transfer {Utility.FormatAmount(internalTransfer.TransferAmount)}", false);
                return;
            }
            //check the minimum kept amount 
            if ((selectedAccount.AccountBalance - internalTransfer.TransferAmount) < minKeptAmount)
            {
                Utility.PrintMessage($"Transfer faile. Your account needs to have minimum" +
                    $" {Utility.FormatAmount(minKeptAmount)}", false);
                return;
            }

            //check reciever's account number is valid
            var selectedBankAccountReciever = (from userAcc in userAccountList
                                               where userAcc.AccountNumber == internalTransfer.ReciepeintBankAccountNumber
                                               select userAcc).FirstOrDefault();
            if (selectedBankAccountReciever == null)
            {
                Utility.PrintMessage("Transfer failed. Recieber bank account number is invalid.", false);
                return;
            }
            //check receiver's name
            if (selectedBankAccountReciever.FullName != internalTransfer.RecipientBankAccountName)
            {
                Utility.PrintMessage("Transfer Failed. Recipient's bank account name does not match.", false);
                return;
            }

            //add transaction to transactions record- sender
            InsertTransection(selectedAccount.Id, TransectionType.Transfer, -internalTransfer.TransferAmount, "Transfered " +
                $"to {selectedBankAccountReciever.AccountNumber} ({selectedBankAccountReciever.FullName})");
            //update sender's account balance
            selectedAccount.AccountBalance -= internalTransfer.TransferAmount;

            //add transaction record-reciever
            InsertTransection(selectedBankAccountReciever.Id, TransectionType.Transfer, internalTransfer.TransferAmount, "Transfered from " +
                $"{selectedAccount.AccountNumber}({selectedAccount.FullName})");
            //update reciever's account balance
            selectedBankAccountReciever.AccountBalance += internalTransfer.TransferAmount;
            //print success message
            Utility.PrintMessage($"You have successfully transfered" +
                $" {Utility.FormatAmount(internalTransfer.TransferAmount)} to " +
                $"{internalTransfer.RecipientBankAccountName}", true);

        }
    }
}
