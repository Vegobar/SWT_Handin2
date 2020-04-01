using core_functionality;
using System;

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