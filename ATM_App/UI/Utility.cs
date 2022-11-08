using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_App.UI
{
    public static class Utility
    {
        public static string GetUserInput(String prompt)
        {
            Console.WriteLine($"Enter {prompt}");
            return Console.ReadLine();
        }
        public static void PressEnterToContinue()
        {
            Console.WriteLine("\nPress Enter to continue...\n");
            Console.ReadLine();
        }
    }
}
