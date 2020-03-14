using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Smtp;
using NSubstitute;
using NUnit.Framework;
using SWT_ladeskab;


namespace Ladeskab_unit_test
{
    public class unit_test_stationcontrol
    {
        [TestFixture]
        class Sub_tester
        {

            private StationControl _stationControl;
            private IRFIDReader _rfidReader;
            private IChargeControl _chargeControl;
            private IDoor _door;
            private IDisplay _display;


            [SetUp]
            public void Setup()
            {
                _rfidReader = Substitute.For<IRFIDReader>();
                _chargeControl = Substitute.For<IChargeControl>();
                _door = Substitute.For<IDoor>();
                _display = Substitute.For<IDisplay>();

                _stationControl = new SWT_ladeskab.StationControl(_door, _display, _rfidReader,_chargeControl);
            }

            [Test]
            public void testDoorOpenEventHandler()
            {
                _door.OpenDoorEvent += Raise.EventWith(new OpenDoorEventArgs());
                _display.Received(1).display("Tilslut telefon",1);
            }

            [Test]
            public void testCloseOpenEventHandler()
            {
                _door.ClosedDoorEvent += Raise.EventWith(new ClosedDoorEventArgs());
                _display.Received(1).display("Indlæs RFID",1);
            }

            [Test]
            public void testCheckID_true()
            {
                var result = _stationControl.CheckId(30, 30);
                Assert.IsTrue(result);
            }
            
            [Test]
            public void testCheckID_false()
            {
                var result = _stationControl.CheckId(30, 35);
                Assert.IsFalse(result);
            }

            [Test]
            public void testRFIDDetectedEvent()
            {
                var wasCalled = false;
                _rfidReader.RfidDetectedEvent += (sender, args) => wasCalled = true;

                _rfidReader.RfidDetectedEvent += Raise.EventWith(new RfidDetectedEventArgs());
                Assert.True(wasCalled);
            }

            [Test]
            public void test_connected_phone_true()
            {
                _chargeControl.isConnected().Returns(true);

                _rfidReader.RfidDetectedEvent += Raise.EventWith(new RfidDetectedEventArgs());
                
                _display.Received(1).display("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.",1);
            }
        }
    }
}