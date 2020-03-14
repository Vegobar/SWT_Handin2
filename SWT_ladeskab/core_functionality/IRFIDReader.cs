﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
