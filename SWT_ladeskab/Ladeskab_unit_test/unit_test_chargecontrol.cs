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

            [TestCase(0)]
            public void uut_chargeDisplay_NotConnected_test(double a)
            {
                uut.updateDisplayPower(a);
                Assert.That(_chargeDisplayArgs.msg == "");
            }
            
            [TestCase(1)]
            [TestCase(2)]
            [TestCase(3)]
            [TestCase(5)]
            public void uut_chargeDisplay_doneCharging_test(double a)
            {
                uut.updateDisplayPower(a);
                Assert.That(_chargeDisplayArgs.msg == "Phone fully charged.");
            }

            [TestCase(6)]
            [TestCase(50)]
            [TestCase(100)]
            [TestCase(200)]
            [TestCase(400)]
            [TestCase(500)]
            public void uut_chargeDisplay_isCharging_test(double a)
            {
                uut.updateDisplayPower(a);
                Assert.That(_chargeDisplayArgs.msg == "Phone charging.");
            }

            [TestCase(501)]
            [TestCase(550)]
            [TestCase(1000)]
            public void uut_chargeDisplay_Overloaded_test(double a)
            {
                uut.updateDisplayPower(a);
                Assert.That(_chargeDisplayArgs.msg == "Warning: short circuit, disabling charge mode");
            }

        }
    }
}