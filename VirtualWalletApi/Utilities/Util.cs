using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualWalletApi.Utilities
{
    public class Util
    {
        public static string NewWalletLabel()
        {
            string[] words = { "Amazing", "Golden", "Silver", "Dope", "Diamond", "Sparkling", "Precious", "Apache", "Newton", "Einstein", "Johnny", "Billy", "Hommy" };
            string choosenWord = words[new Random().Next(0, words.Length - 1)];
            return ($"{choosenWord}-wallet-{ Guid.NewGuid().ToString().Substring(0, 4) }");
        }

        public static string NewWalletAccountNumber()
        {
            string accNumber = "";
            for (int i = 1; i <= 10; i++)
            {
                accNumber += new Random().Next(0, 9);
            }

            return accNumber;
        }
    }
}
