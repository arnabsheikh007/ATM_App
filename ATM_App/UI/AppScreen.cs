using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_App.UI
{
    public static class AppScreen
    {
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

        
    }
}
