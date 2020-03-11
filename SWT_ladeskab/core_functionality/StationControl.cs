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

        private int _oldId;

        public StationControl(IDoor door, IDisplay display)
        {
            _display = display;
            _door = door;
            _door.OpenDoorEvent += OpenDoorEventHandler;
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
            _display.display("Tilslut telefon");
        }
    }
}
