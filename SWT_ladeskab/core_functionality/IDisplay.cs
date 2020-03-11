using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWT_ladeskab
{
    public interface IDisplay
    {
         void display(string text, int id);
         void display(string text); 
    }
}
