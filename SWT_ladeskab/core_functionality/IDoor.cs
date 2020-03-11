﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWT_ladeskab
{
    public interface IDoor
    {
        void open();
        void close();

        event EventHandler<OpenDoorEventArgs> OpenDoorEvent;
        event EventHandler<ClosedDoorEventArgs> ClosedDoorEvent;
    }

    public class OpenDoorEventArgs : EventArgs
        {
        
        }

     public class ClosedDoorEventArgs : EventArgs
        {

        }
}
