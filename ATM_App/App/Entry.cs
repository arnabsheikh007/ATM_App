using ATM_App.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_App.App
{
    class Entry
    {
        static void Main(string[] args)
        {
            AppScreen.Welcome();

            ATM_App atmApp = new ATM_App();

            atmApp.InitializeData();

            atmApp.CheckUserCardNumAndPassword();
            atmApp.Welcome();
            
            Utility.PressEnterToContinue();
        }
    }
}
