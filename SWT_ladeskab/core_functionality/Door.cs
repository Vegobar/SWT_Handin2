using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWT_ladeskab
{
    public class Door : IDoor
    {
        private enum doorState
        {
            Closed, Open
        };


        private doorState _state {get; set; }

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

        public int getDoorState()
        {

            if (_state == doorState.Open)
            {
                return 1;
            }

            else
            {
                return 0;
            }
        }
        public void open()
        {
            if (!_isLocked && _state != doorState.Open)
            {
                _state = doorState.Open;
                OnDoorOpenEvent(new OpenDoorEventArgs {DoorOpen = "Door is open"});
            }
            else if(_isLocked)
            {
                Console.WriteLine("Døren er låst");
            }
            else if(!_isLocked && _state == doorState.Open)
            {
                Console.WriteLine("Døren er allerede åben");
            }
        }

        public void close()
        {
            if (_state != doorState.Closed)
            {
                _state = doorState.Closed;
                OnClosedDoorEvent(new ClosedDoorEventArgs {DoorClosed = "Door is closed"});
            }
            else if(_state == doorState.Closed)
            {
                Console.Write("Døren er allerede lukket");
            }
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
