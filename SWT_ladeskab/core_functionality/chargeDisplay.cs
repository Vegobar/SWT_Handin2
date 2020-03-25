using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using core_functionality;

namespace SWT_ladeskab
{
    public class chargeDisplay : IConsoleOutput
    {
        public void displayText(string text)
        {
            Console.WriteLine(text);
        }
    }
}