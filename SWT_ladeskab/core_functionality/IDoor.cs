using System;

namespace SWT_ladeskab
{
    public interface IDoor
    {
        void open();
        void close();
        void lockDoor();
        void unlockDoor();

        int getDoorState();

        event EventHandler<OpenDoorEventArgs> OpenDoorEvent;
        event EventHandler<ClosedDoorEventArgs> ClosedDoorEvent;
    }

    public class OpenDoorEventArgs : EventArgs
    {
        public string DoorOpen { get; set; }
    }

    public class ClosedDoorEventArgs : EventArgs
    {
        public string DoorClosed { get; set; }
    }
}