using System;
using SWT_ladeskab;

class Program
{
    static void Main(string[] args)
    {
        // Assemble your system here from all the classes

        //Make objects, so that we can call door methods and rfidreader.
        IRFIDReader rfidReader;

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
                    //door.OnDoorOpen();
                    break;

                case 'C':
                case 'c':
                    //door.OnDoorClose();
                    break;

                case 'R':
                case 'r':
                    System.Console.WriteLine("Indtast RFID id: ");
                    string idString = System.Console.ReadLine();

                    int id = Convert.ToInt32(idString);
                    rfidReader.RfidDetectedEvent(id);
                    
                    break;

                default:
                    break;
            }

        } while (!finish);
    }
}