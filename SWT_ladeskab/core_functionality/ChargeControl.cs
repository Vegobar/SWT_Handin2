using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWT_ladeskab
{

    class ChargeControl : IChargeControl
    {
        private bool connected { get; set; }
        public int updateDisplayPower()
        {
            int power = 0;
            return power;
        }

        public bool isConnected()
        {

            return connected;
        }

        public void startCharge()
        {

        }
    }
}
