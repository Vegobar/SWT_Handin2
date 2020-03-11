using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SWT_ladeskab
{
    public class StationControl : IStationControl
    {
        private enum LadeSkabsState
        {
            Available,
            Locked,
            DoorOpen
        };

        private LadeSkabsState _state;
        private IChargeControl _chargeControl;
        private IDoor _door;
        private IDisplay _display;
        private IRFIDReader _rfid;

        public string currentDisplay { get; set; }

        private int _oldId;

        public StationControl(IDoor door, IDisplay display, IRFIDReader rfid)
        {
            _display = display;
            _door = door;
            _rfid = rfid;

            //Events
            _door.OpenDoorEvent += OpenDoorEventHandler;
            _door.ClosedDoorEvent += CloseDoorEventHandler;
            _rfid.RfidDetectedEvent += RfidDetectedEventHandler;
            _chargeControl.ChargeDisplayEvent += ChargeDisplayEventHandler;

        }

        public void LogDoorLocked(int id)
        {
            throw new NotImplementedException();
        }

        public bool CheckId(int OldID, int id)
        {
            throw new NotImplementedException();
        }

        public void RfidDetected(int id)
        {
            throw new NotImplementedException();
        }

        private void OpenDoorEventHandler(object sender, EventArgs e)
        {
            _state = LadeSkabsState.DoorOpen;
            _display.display("Tilslut telefon",currentDisplay);
           // _door.unlockDoor();
        }

        private void CloseDoorEventHandler(object sender, EventArgs e)
        {
            _state = LadeSkabsState.Locked;
            //_display.display("Indlæs RFID");
            _door.lockDoor();
        }

        private void RfidDetectedEventHandler(object sender, RfidDetectedEventArgs e)
        {
            RfidDetected(e.id);
        }

        private void ChargeDisplayEventHandler(object sender, ChargeDisplayEventArgs e)
        {
            currentDisplay = e.msg;
            _display.display(fudge, currentDisplay);
        }
    }
}
