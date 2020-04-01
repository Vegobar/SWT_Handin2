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
        private class ChargeConrolTests
        {
            private ChargeControl _uut;
            private IUsbCharger _usbCharger;
            private ChargeDisplayEventArgs _chargeDisplayArgs;

            [SetUp]
            public void Setup()
            {
                _chargeDisplayArgs = null;

                _usbCharger = Substitute.For<IUsbCharger>();
                _uut = new ChargeControl(_usbCharger);

                _uut.ChargeDisplayEvent += (o, args) => { _chargeDisplayArgs = args; };
            }

            [Test]
            public void uut_chargeChangedEvent_test()
            {
                _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() {Current = 25.0});
                Assert.That(_uut.CurrentCharge, Is.EqualTo(25.0));
            }

            [TestCase(false)]
            [TestCase(true)]
            public void uut_isConnected_test(bool a)
            {
                _usbCharger.Connected.Returns(a);
                _uut.startCharge();
                Assert.That(_uut.isConnected(), Is.EqualTo(a));
            }

            [Test]
            public void uut_startCharge_and_Connected_Test()
            {
                _usbCharger.Connected.Returns(true);
                _uut.startCharge();
                _usbCharger.Received(1).StartCharge();
            }

            [Test]
            public void uut_startCharge_and_notConnected_test()
            {
                _usbCharger.Connected.Returns(false);
                _uut.startCharge();
                _usbCharger.Received(1).StopCharge();
            }

            [Test]
            public void uut_stopCharge_test()
            {
                _uut.stopCharge();
                _usbCharger.Received(1).StopCharge();
            }

            [TestCase(0)]
            public void uut_chargeDisplay_NotConnected_test(double a)
            {
                _uut.updateDisplayPower(a);
                Assert.That(_chargeDisplayArgs.msg == "");
            }

            [TestCase(1)]
            [TestCase(2)]
            [TestCase(3)]
            [TestCase(5)]
            public void uut_chargeDisplay_doneCharging_test(double a)
            {
                _uut.updateDisplayPower(a);
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
                _uut.updateDisplayPower(a);
                Assert.That(_chargeDisplayArgs.msg == "Phone charging.");
            }

            [TestCase(501)]
            [TestCase(550)]
            [TestCase(1000)]
            public void uut_chargeDisplay_Overloaded_test(double a)
            {
                _uut.updateDisplayPower(a);
                Assert.That(_chargeDisplayArgs.msg == "Warning: short circuit, disabling charge mode");
            }

            [TestCase(501)]
            [TestCase(550)]
            [TestCase(750)]

            public void uut_charge_overload_test(double a)
            {
                _usbCharger.Connected.Returns(true);
                _uut.startCharge();
                _uut.updateDisplayPower(a);
                _usbCharger.Received(1).StopCharge();
            }

            [Test]
            public void uut_charge_not_connected()
            {
                _usbCharger.Connected.Returns(true);
                _uut.startCharge();
                _uut.updateDisplayPower(0);
                _usbCharger.Received(1).StopCharge();
            }

            [TestCase(1)]
            [TestCase(2)]
            [TestCase(4)]
            [TestCase(5)]
            public void uut_charge_fully_charged(double a)
            {
                _usbCharger.Connected.Returns(true);
                _uut.startCharge();
                _uut.updateDisplayPower(a);
                _usbCharger.DidNotReceive().StopCharge();
            }

            [TestCase(5.1)]
            [TestCase(50)]
            [TestCase(100)]
            [TestCase(300)]
            [TestCase(500)]
            public void uut_charge_Charging(double a)
            {
                _usbCharger.Connected.Returns(true);
                _uut.startCharge();
                _uut.updateDisplayPower(a);
                _usbCharger.DidNotReceive().StopCharge();
            }
        }
    }
}