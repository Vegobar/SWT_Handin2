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
            RfidDetectedEvent?.Invoke(this,new RfidDetectedEventArgs(){id = RfidId});
        }


    }
}