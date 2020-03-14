using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWT_ladeskab
{
    public interface IStationControl
    { 
        //void LogDoorLocked(int id);
        bool CheckId(int OldID, int id);
        void RfidDetected(int id);
    }
}
