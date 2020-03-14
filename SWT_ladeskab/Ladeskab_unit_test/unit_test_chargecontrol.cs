using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Castle.Core.Smtp;
using NSubstitute;
using NSubstitute.Core.Arguments;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;
using SWT_ladeskab;
using UsbSimulator;

namespace Ladeskab_unit_test
{
    public class unit_test_chargecontrol
    {
        [TestFixture]
        class Sub_tester
        {
            private IChargeControl _chargeControl;
            private ChargeControl uut;
            private IUsbCharger _usbCharger;
            private CurrentEventArgs _currentArgs;
            private ChargeDisplayEventArgs _chargeDisplayArgs;

            [SetUp]
            public void Setup()
            {
               
                _chargeControl = Substitute.For<IChargeControl>();
                _usbCharger = Substitute.For<IUsbCharger>();
                uut = Substitute.For<ChargeControl>();
                _usbCharger.CurrentValueEvent += (o, args) => { _currentArgs = args; };
                uut.ChargeDisplayEvent += (o, args) => { _chargeDisplayArgs = args; };
            }

            [Test]
            public void uut_chargeChangedEvent_test()
            {
                _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() {Current = 25.0});
                uut.Received(1).updateDisplayPower(uut.CurrentCharge);
            }

            [TestCase(false)]
            [TestCase(true)]
            public void uut_isConnected_test(bool a)
            {
                uut.connected = a;
                uut.startCharge();
                uut.Received(1).isConnected();
            }

            [Test]
            public void uut_stopCharge_test()
            {
                uut.stopCharge();
                _usbCharger.ReceivedCalls();
            }

            [TestCase(0, "")]
            [TestCase(3, "Phone fully charged.")]
            [TestCase(200, "Phone charging.")]
            [TestCase(501, "Warning: short circuit, disabling charge mode")]
            public void uut_chargeDisplay_test(double a, string b)
            {
                uut.updateDisplayPower(a);
                Assert.That(_chargeDisplayArgs.msg == b);
            }

        }
    }
}