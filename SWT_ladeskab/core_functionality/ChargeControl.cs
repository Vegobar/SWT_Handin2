using System;
using UsbSimulator;

namespace SWT_ladeskab
{
    public class ChargeControl : IChargeControl
    {
        private IUsbCharger Usb;

        public bool connected { get; set; }
        public double CurrentCharge { get; set; }

        public event EventHandler<ChargeDisplayEventArgs> ChargeDisplayEvent;

        public ChargeControl(IUsbCharger UsbCharger)
        {
            Usb = UsbCharger;
            Usb.CurrentValueEvent += ChargeChangedevent;
        }

        public void updateDisplayPower(double charge)
        {
            var chargeState = "";
            if (charge == 0.0)
            {
                chargeState = "";
                Usb.StopCharge();
            }
            else if (0 < charge && charge <= 5)
            {
                chargeState = "Phone fully charged.";
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
            connected = Usb.Connected;
            return connected;
        }

        //Requests UsbCharger to begin charging.
        public void startCharge()
        {
            if (isConnected())
                Usb.StartCharge();
            else
                Usb.StopCharge();
        }

        public void stopCharge()
        {
            Usb.StopCharge();
        }

        //Gets charge value from UsbCharger
        private void ChargeChangedevent(object sender, CurrentEventArgs e)
        {
            if (CurrentCharge != e.Current)
            {
                CurrentCharge = e.Current;
                updateDisplayPower(CurrentCharge);
            }
        }

        private void onChargeDisplayEvent(string message)
        {
            ChargeDisplayEvent?.Invoke(this, new ChargeDisplayEventArgs() { msg = message });
        }
    }
}