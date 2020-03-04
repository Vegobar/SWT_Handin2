using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWT_ladeskab
{
    class RFIDReader: IRFIDReader
    {

        public event EventHandler<RfidDetectedEventArgs> RfidDetected_Event;
    }
}
