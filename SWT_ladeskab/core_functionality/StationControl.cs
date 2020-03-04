using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWT_ladeskab
{
    class StationControl : IStationControl
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

        private int _oldId;

        public void LogDoorLocked(int id)
        {
            throw new NotImplementedException();
        }

        public bool CheckId(int OldID, int id)
        {
            throw new NotImplementedException();
        }

        public void RfidDetected_handler(int id)
        {
            throw new NotImplementedException();
        }
    }
}
