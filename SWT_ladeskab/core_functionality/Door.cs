using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWT_ladeskab
{
    public class Door : IDoor
    {
        private bool _isLocked = false;
        public void lockDoor()
        {
            _isLocked = true;
        }

        public void unlockDoor()
        {
            _isLocked = false;
        }

        public bool IsLocked
        {
            get { return _isLocked; }
        }


        public void open()
        {
            if(!_isLocked)
            OnDoorOpenEvent(new OpenDoorEventArgs{DoorOpen="Door is open"});
        }

        public void close()
        {
            OnClosedDoorEvent(new ClosedDoorEventArgs { DoorClosed = "Door is closed" });
        }

        public event EventHandler<OpenDoorEventArgs> OpenDoorEvent;
        public event EventHandler<ClosedDoorEventArgs> ClosedDoorEvent;

        protected virtual void OnDoorOpenEvent(OpenDoorEventArgs e)
        {
            OpenDoorEvent?.Invoke(this, e);
        }

        protected virtual void OnClosedDoorEvent(ClosedDoorEventArgs e)
        {
            ClosedDoorEvent?.Invoke(this, e);
        }
    }
}
