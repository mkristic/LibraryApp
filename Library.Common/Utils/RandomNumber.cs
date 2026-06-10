using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Common.Utils
{
    public class RandomNumber
    {
        public static int GetRandomNumber()
        {
            var randomNumber = new Random();

            return randomNumber.Next(1, 1000);
        }
    }
}
