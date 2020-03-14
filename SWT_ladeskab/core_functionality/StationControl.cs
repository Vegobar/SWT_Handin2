using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using core_functionality;


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
        private ILog _log = new Log();
        
        private int _oldId;

        public StationControl(IDoor door, IDisplay display, IRFIDReader rfid,IChargeControl chargeControl,ILog log)
        {
            _display = display;
            _door = door;
            _rfid = rfid;
            _chargeControl = chargeControl;
            _log = log;
        
            //Events
            _door.OpenDoorEvent += OpenDoorEventHandler;
            _door.ClosedDoorEvent += CloseDoorEventHandler;
            _rfid.RfidDetectedEvent += RfidDetectedEventHandler;
            _chargeControl.ChargeDisplayEvent += ChargeDisplayEventHandler;
        
        }
        
        public StationControl(IDoor door, IDisplay display, IRFIDReader rfid,IChargeControl chargeControl)
        {
            _display = display;
            _door = door;
            _rfid = rfid;
            _chargeControl = chargeControl;

            //Events
            _door.OpenDoorEvent += OpenDoorEventHandler;
            _door.ClosedDoorEvent += CloseDoorEventHandler;
            _rfid.RfidDetectedEvent += RfidDetectedEventHandler;
            _chargeControl.ChargeDisplayEvent += ChargeDisplayEventHandler;

        }

        //public void LogDoorLocked(int id_rfid)
        //{
        //   throw new NotImplementedException();
        //}

        public bool CheckId(int OldID_rfid, int id_rfid)
        {
            if (OldID_rfid == id_rfid)
                return true;
                else
                return false;
        }

        public void RfidDetected(int id_rfid)
        {
            switch(_state)
            {
                case LadeSkabsState.Locked:
                    if (CheckId(_oldId, id_rfid))
                    {
                        _chargeControl.stopCharge();
                        _door.unlockDoor();

                        _log.PrintToFile(": Skab låst op med RFID: ", id_rfid);

                        _display.display("Tag din telefon ud af skabet og luk døren",1);
                        _state = LadeSkabsState.Available;
                    }
                    else
                    {
                        _display.display("Forkert RFID tag",1);
                    }

                   
                    break;

                case LadeSkabsState.DoorOpen:
                    break;

                case LadeSkabsState.Available:
                    if (_chargeControl.isConnected())
                    {
                        _door.lockDoor();
                        _chargeControl.startCharge();
                        _oldId = id_rfid;

                        _log.PrintToFile(": Skab låst med RFID: ", id_rfid);

                        _display.display("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.",1);
                        _state = LadeSkabsState.Locked;
                    }
                    else
                    {
                        _display.display("Din telefon er ikke ordentlig tilsluttet. Prøv igen",1);
                    }
                    break;
            }
        }
        
        private void OpenDoorEventHandler(object sender, EventArgs e)
        {
            //_state = LadeSkabsState.DoorOpen;
            _display.display("Tilslut telefon",1);
        }

        private void CloseDoorEventHandler(object sender, EventArgs e)
        {
            //_state = LadeSkabsState.Available;
            _display.display("Indlæs RFID",1);
        }

        private void RfidDetectedEventHandler(object sender, RfidDetectedEventArgs e)
        {
            RfidDetected(e.id);
        }

        private void ChargeDisplayEventHandler(object sender, ChargeDisplayEventArgs e)
        {
            _display.display(e.msg,2);
        }
    }
}
