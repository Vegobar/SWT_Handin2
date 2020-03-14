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

        private string DoorOpen;



        public void open()
        {
            if(!_isLocked)
            OnDoorOpenEvent(new OpenDoorEventArgs{DoorOpen="Im open"});
            DoorOpen = "Im open";
        }

        public void close()
        {
            OnClosedDoorEvent(new ClosedDoorEventArgs());
        }

        public event EventHandler<OpenDoorEventArgs> OpenDoorEvent;
        public event EventHandler<ClosedDoorEventArgs> ClosedDoorEvent;

        protected virtual void OnDoorOpenEvent(OpenDoorEventArgs e)
        {
            OpenDoorEvent?.Invoke(this, e);
        }

        protected virtual void OnClosedDoorEvent(ClosedDoorEventArgs e)
        {
            ClosedDoorEvent?.Invoke(this, null);
        }
    }
}
