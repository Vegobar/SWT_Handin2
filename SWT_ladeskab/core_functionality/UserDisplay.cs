using core_functionality;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWT_ladeskab
{
    public class UserDisplay : IConsoleOutput
    {
        public void displayText(string text)
        {
            Console.WriteLine(text);
        }
    }
}