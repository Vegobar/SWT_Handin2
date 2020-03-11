using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWT_ladeskab
{
    public class Door : IDoor
    {
        public void unlockDoor()
        {
            OnDoorOpenEvent(new OpenDoorEventArgs());
        }

        public void lockDoor()
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
            ClosedDoorEvent?.Invoke(this, e);
        }
    }
}
