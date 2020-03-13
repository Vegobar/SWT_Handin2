using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using SWT_ladeskab;
using UsbSimulator;

namespace SWT_ladeskab
{

    class ChargeControl : IChargeControl
    { 
        IUsbCharger Usb = new UsbChargerSimulator();
        IDisplay chargeDisplay = new Display();

        private bool connected { get; set; }
        static private double CurrentCharge { get; set; }

        public event EventHandler<ChargeDisplayEventArgs> ChargeDisplayEvent;

        ChargeControl()
        {
            Usb.CurrentValueEvent += ChargeChangedevent;
        }

        public void updateDisplayPower(double charge)
        {
            string chargeState = "";
            if (charge == 0)
            {
                chargeState = "";
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

            onChargeDisplayEvent(chargeState);
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

        public void stopCharge()
        {
            throw new NotImplementedException();
        }

        //Gets charge value from UsbCharger
        private void ChargeChangedevent(object sender, CurrentEventArgs e)
        {
            CurrentCharge = e.Current;
            updateDisplayPower(CurrentCharge);
        }

        private void onChargeDisplayEvent(string message)
        {
            ChargeDisplayEvent?.Invoke(this, new ChargeDisplayEventArgs(){msg = message});
        }
    }
}



