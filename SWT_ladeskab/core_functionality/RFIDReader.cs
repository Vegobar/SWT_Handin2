using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWT_ladeskab
{
    public class RFIDReader: IRFIDReader
    {
        private IRFIDReader _irfidReaderImplementation;

        public event EventHandler<RfidDetectedEventArgs> RfidDetectedEvent;

        public void onRfidDetectedEvent(int RfidId)
        {
            try
            {
                if (RfidId > 0)
                {
                    RfidDetectedEvent?.Invoke(this, new RfidDetectedEventArgs() {id = RfidId});
                }
                else
                {
                    throw new Exception("ugyldig RFID kode");
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

    }
}