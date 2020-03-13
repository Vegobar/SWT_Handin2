using System;
using core_functionality;
using SWT_ladeskab;

class Program
{
    static void Main(string[] args)
    {
        // Assemble your system here from all the classes

        //Make objects, so that we can call door methods and rfidreader.
        IRFIDReader rfidReader = new RFIDReader();
        IDoor door = new Door();
        IDisplay display = new Display();
        IChargeControl chargeControl = new ChargeControl();

        IStationControl stationControl = new StationControl(door, display, rfidReader,chargeControl);

        bool finish = false;
        do
        {
            string input;
            System.Console.WriteLine("Indtast E, O, C, R: ");
            input = Console.ReadLine();
            if (string.IsNullOrEmpty(input)) continue;

            switch (input[0])
            {
                case 'E': 
                case 'e':
                    finish = true;
                    break;

                case 'O':
                case 'o':
                    door.open();
                    break;

                case 'C':
                case 'c':
                    door.close();
                    break;

                case 'R':
                case 'r':
                   /* System.Console.WriteLine("Indtast RFID id: ");
                    string idString = System.Console.ReadLine();

                    int id = Convert.ToInt32(idString);*/
                    rfidReader.onRfidDetectedEvent();
                    
                    break;

                case 'P':
                case 'p':
                    break;

                default:
                    break;
            }

        } while (!finish);
    }
}