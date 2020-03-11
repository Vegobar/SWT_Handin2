﻿using System;
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
        //you wrong! :((((((
        private void OpenDoorEventHandler(object sender, EventArgs e)
        {
            _state = LadeSkabsState.DoorOpen;
            _display.display("Tilslut telefon");
            _door.unlockDoor();
        }

        private void CloseDoorEventHandler(object sender, EventArgs e)
        {
            _state = LadeSkabsState.Locked;
            _display.display("Indlæs RFID");
            _door.lockDoor();
        }

        private void RfidDetectedEventHandler(object sender, RfidDetectedEventArgs e)
        {
            RfidDetected(e.id);
        }
    }
}
