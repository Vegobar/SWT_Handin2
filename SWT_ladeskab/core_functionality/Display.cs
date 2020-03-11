using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWT_ladeskab
{
    public class Display : IDisplay
    {
        string _userString;
        string _chargeString;

        UserDisplay _uDisplay;
        chargeDisplay _cDisplay;

        public void display(string text1, int id)
        {
            switch (id)
            {
                case 1:
                    _userString = text1;
                    _uDisplay.display(_userString);
                    _cDisplay.display(_chargeString);
                    break;

                case 2:
                    _chargeString = text1;
                    _uDisplay.display(_userString);
                    _cDisplay.display(_chargeString);
                    break;
            }
        }

        public void display(string text1)
        { // Not implemented
        }
    }
}
