using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWT_ladeskab
{
    class RFIDReader: IRFIDReader
    {
        private IRFIDReader _irfidReaderImplementation;

        public event EventHandler<RfidDetectedEventArgs> RfidDetectedEvent;

        protected virtual void onRfidDetectedEvent(RfidDetectedEventArgs e)
        {
            RfidDetectedEvent?.Invoke(this,e);
        }
    }
}