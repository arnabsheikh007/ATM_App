using ATM_App.Domain.Entities;
using ATM_App.Domain.Interfaces;
using ATM_App.UI;
using System;
using System.Collections.Generic;

namespace ATM_App
{
    public class ATM_App : IUserLogin

    {
        private List<UserAccount> userAccountList;
        private UserAccount selectedAccount;

        public void CheckUserCardNumAndPassword()
        {
            bool isCorrectLogin = false;

            UserAccount tempUserAccount = new UserAccount();

            tempUserAccount.CardNumber = Validator.Convert<long>("Your card number");
            tempUserAccount.CardPin = Convert.ToInt32(Utility.GetSecretInput("Enter your card pin")); 
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
                    CardPin = 1010,
                    AccountBalance = 100000.00m,
                    IsLocked = false
                },
                new UserAccount
                {
                    Id = 2,
                    FullName = "Sheikh Arnab",
                    AccountNumber = 123123124,
                    CardNumber = 321321322,
                    CardPin = 1212,
                    AccountBalance = 150000.00m,
                    IsLocked = false
                },
                new UserAccount
                {
                    Id = 3,
                    FullName = "Sheikh Bahauddin Arnab",
                    AccountNumber = 123123125,
                    CardNumber = 321321323,
                    CardPin = 2121,
                    AccountBalance = 50000.00m,
                    IsLocked = false
                },
            };
        }

    }
}
