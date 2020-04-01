using System;

namespace SWT_ladeskab
{
    public class RFIDReader : IRFIDReader
    {

        public event EventHandler<RfidDetectedEventArgs> RfidDetectedEvent;

        public void onRfidDetectedEvent(int RfidId)
        {
            try
            {
                if (RfidId > 0)
                    RfidDetectedEvent?.Invoke(this, new RfidDetectedEventArgs() { id = RfidId });
                else
                    throw new Exception("ugyldig RFID kode");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}