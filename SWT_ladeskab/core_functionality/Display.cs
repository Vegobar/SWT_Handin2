using System;

namespace SWT_ladeskab
{
    public class Display : IDisplay
    {
        private string _userString = "";
        private string _chargeString = "";
        private string _receivedString = "";

        private UserDisplay _uDisplay = new UserDisplay();
        private chargeDisplay _cDisplay = new chargeDisplay();

        public void display(string text1, int display_num)
        {
            switch (display_num)
            {
                case 1:
                    _userString = text1;
                    _receivedString = text1;
                    Console.WriteLine("**************************************");
                    _uDisplay.displayText(_userString);
                    Console.WriteLine("**************************************");
                    Console.WriteLine("**************************************");
                    _cDisplay.displayText(_chargeString);
                    Console.WriteLine("**************************************");

                    break;

                case 2:
                    if (string.Compare(_chargeString, text1) != 0)
                    {
                        _chargeString = text1;
                        _receivedString = text1;
                        Console.WriteLine("**************************************");
                        _uDisplay.displayText(_userString);
                        Console.WriteLine("**************************************");
                        Console.WriteLine("**************************************");
                        _cDisplay.displayText(_chargeString);
                        Console.WriteLine("**************************************");
                    }

                    break;
            }
        }


        public string ReceivedString => _receivedString;
    }
}