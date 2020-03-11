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
        IUsbCharger Usb = new UsbChargerSimulator();
        IDisplay chargeDisplay = new Display();

        private bool connected { get; set; }
        static private double CurrentCharge { get; set; }

        ChargeControl()
        {
            Usb.CurrentValueEvent += ChargeChangedevent;
        }
        public string updateDisplayPower(double charge)
        {
            string chargeState = "";
            if (charge == 0)
            {
                chargeState = "Something went wrong, phone not connected, or not charging.";
                Usb.StopCharge();
            }
            else if (charge > 0 && charge <= 5)
            {
                chargeState = "Phone fully charged.";
                Usb.StopCharge();
            }
            else if (charge > 5 && charge <= 500)
            {
                chargeState = "Phone charging.";
            }
            else if (charge > 500)
            {
                chargeState = "Warning: short circuit, disabling charge mode";
                Usb.StopCharge();
            }

            return chargeState;
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
        static void ChargeChangedevent(object sender, CurrentEventArgs e)
        {
            CurrentCharge = e.Current;
        }
    }
}
