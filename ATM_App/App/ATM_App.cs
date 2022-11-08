using ATM_App.UI;
using System;

namespace ATM_App
{
    class ATM_App
    {
        static void Main(string[] args)
        {
            AppScreen.Welcome();

            long cardNmber = Validator.Convert<long>("Your Card Number");
            Console.WriteLine($"Your Card Number is {cardNmber}");

            Utility.PressEnterToContinue();
        }
    }
}
