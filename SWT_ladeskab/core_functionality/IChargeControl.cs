using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SWT_ladeskab
{
    public interface IChargeControl
    {
        event EventHandler<ChargeDisplayEventArgs> ChargeDisplayEvent;
        void updateDisplayPower(double value);
        bool isConnected();
        void startCharge();

    }

    public class ChargeDisplayEventArgs : EventArgs
    {
        public string msg { get; set; }
    }
}
