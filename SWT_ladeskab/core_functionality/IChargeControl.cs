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
        double updateDisplayPower();
        bool isConnected();
        void startCharge();

    }
}
