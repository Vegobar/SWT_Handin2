using System;

namespace SWT_ladeskab
{
    public interface IRFIDReader
    {
        event EventHandler<RfidDetectedEventArgs> RfidDetectedEvent;
        void onRfidDetectedEvent(int RfidId);
    }

    public class RfidDetectedEventArgs : EventArgs
    {
        public int id { get; set; }
    }
}