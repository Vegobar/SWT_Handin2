using System;

namespace SWT_ladeskab
{
    public interface IChargeControl
    {
        event EventHandler<ChargeDisplayEventArgs> ChargeDisplayEvent;
        void updateDisplayPower(double value);
        bool isConnected();
        void startCharge();
        void stopCharge();

        double CurrentCharge { get; set; }
    }

    public class ChargeDisplayEventArgs : EventArgs
    {
        public string msg { get; set; }
    }
}