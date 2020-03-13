using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWT_ladeskab
{
    public class Display : IDisplay
    {
        private string _userString = "";
        private string _chargeString = "";

        UserDisplay _uDisplay = new UserDisplay();
        chargeDisplay _cDisplay = new chargeDisplay();

        public void display(string text1, int display_num)
        {
            switch (display_num)
            {
                case 1:
                    _userString = text1;
                    _uDisplay.display(_userString);
                    _cDisplay.display(_chargeString);
                    break;

                case 2:
                    if (string.Compare(_chargeString, text1) != 0)
                    {
                        _chargeString = text1;
                        _uDisplay.display(_userString);
                        _cDisplay.display(_chargeString);
                    }

                    break;
            }
        }

        public void display(string text1)
        { // Not implemented
        }
    }
}
