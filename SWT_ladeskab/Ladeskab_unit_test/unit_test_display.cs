using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using core_functionality;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;
using SWT_ladeskab;

namespace Ladeskab_unit_test
{
    class unit_test_display
    {
        [TestFixture]
        private class Nunit_test_display
        {
            private IStationControl _stationControl;
            private IRFIDReader _rfidReader;
            private IChargeControl _chargeControl;
            private IDoor _door;
            private Display _display;
            private ILog _log;
            private OpenDoorEventArgs _receivedDoorArgs;

            [SetUp]
            public void Setup()
            {
                _rfidReader = Substitute.For<IRFIDReader>();
                _chargeControl = Substitute.For<IChargeControl>();
                _door = Substitute.For<IDoor>();
                _display = new Display();
                _log = Substitute.For<ILog>();

                _stationControl = Substitute.For<StationControl>(_door, _display, _rfidReader, _chargeControl, _log);
            }


            [Test]
            public void testDisplayOpenDoor()
            {

                //Act
                _door.OpenDoorEvent += Raise.EventWith(new OpenDoorEventArgs());

                //Assert
                Assert.That(_display.ReceivedString, Is.EqualTo("Tilslut telefon"));
            }


            [Test]
            public void testDisplayCloseDoor()
            {
                //Act
                _stationControl.RfidDetected(123);

                //Assert
                Assert.That(_display.ReceivedString, Is.EqualTo("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op."));
            }

            [Test]
            public void testDisplay_wrongTag()
            {
                //Act
                _door.open();
                _rfidReader.onRfidDetectedEvent(123);
                _rfidReader.onRfidDetectedEvent(125);

                //Assert
                Assert.That(_display.ReceivedString, Is.EqualTo("Forkert RFID tag"));
            }

            [Test]
            public void testChargeDisplay()
            {
                //Act
                _door.close();
                _rfidReader.onRfidDetectedEvent(123);
                _chargeControl.stopCharge();
                _chargeControl.startCharge();


                //Assert
                Assert.That(_display.ReceivedString, Is.EqualTo("Phone charging."));
            }
        }
    }
}
