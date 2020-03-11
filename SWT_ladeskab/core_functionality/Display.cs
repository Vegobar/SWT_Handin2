using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWT_ladeskab
{
    public class Display : IDisplay
    {
        public void display(string text)
        {
            Console.WriteLine(text);
        }
    }
}
