using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWT_ladeskab
{
    class Door : IDoor
    {
        public void unlockDoor()
        {
            throw new NotImplementedException();
        }

        public void lockDoor()
        {
            throw new NotImplementedException();
        }

        public event EventHandler<OpenDoorEventArgs> OpenDoorEvent;
        public event EventHandler<ClosedDoorEventArgs> ClosedDoorEvent;
    }
}
