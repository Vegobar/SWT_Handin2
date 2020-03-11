using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UsbSimulator;

namespace SWT_ladeskab
{

    class ChargeControl : IChargeControl
    {
        UsbChargerSimulator Usb = new UsbChargerSimulator();
        private bool connected { get; set; }
        static private double CurrentCharge { get; set; }
        public double updateDisplayPower()
        {
            return CurrentCharge;
        }

        public bool isConnected()
        {
            return connected;
        }
        //Requests UsbCharger to begin charging.
        public void startCharge()
        {
            if (connected)
            {
                Usb.StartCharge();
            }
            else
            {
                Usb.StopCharge();
            }
        }
        //Gets charge value from UsbCharger
        static void ChargeChanged(object sender, CurrentEventArgs e)
        {
            CurrentCharge = e.Current;
        }
    }
}
