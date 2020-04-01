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

            [SetUp]
            public void Setup()
            {
                _rfidReader = Substitute.For<IRFIDReader>();
                _chargeControl = Substitute.For<IChargeControl>();
                _door = Substitute.For<IDoor>();
                _display = new Display();

                _stationControl = Substitute.For<StationControl>(_door, _display, _rfidReader, _chargeControl);
            }


            [Test]
            public void testDisplayOpenDoor()
            {

                //Act
                 _door.OpenDoorEvent += Raise.EventWith(new OpenDoorEventArgs { DoorOpen = "Door is open"});

                //Assert
                Assert.That(_display.ReceivedString, Is.EqualTo("Tilslut telefon"));
            }


            [Test]
            public void testDisplayCloseDoor()
            {
                //Act
                _door.ClosedDoorEvent += Raise.EventWith(new ClosedDoorEventArgs { DoorClosed = "Door is closed" });

                //Assert
                Assert.That(_display.ReceivedString, Is.EqualTo("Indlæs RFID"));
            }


            [Test]
            public void testDisplayConnected()
            {
                //Act
                _door.OpenDoorEvent += Raise.EventWith(new OpenDoorEventArgs { DoorOpen = "Door is open" });
                _door.ClosedDoorEvent += Raise.EventWith(new ClosedDoorEventArgs { DoorClosed = "Door is closed" });
                _chargeControl.isConnected().Returns(true);
                _rfidReader.RfidDetectedEvent += Raise.EventWith(new RfidDetectedEventArgs { id = 123 });

                //Assert
                Assert.That(_display.ReceivedString, Is.EqualTo("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op."));
            }


            [Test]
            public void testDisplay_wrongTag()
            {
                //Act
                _door.OpenDoorEvent += Raise.EventWith(new OpenDoorEventArgs { DoorOpen = "Door is open" });
                _door.ClosedDoorEvent += Raise.EventWith(new ClosedDoorEventArgs { DoorClosed = "Door is closed" });
                _chargeControl.isConnected().Returns(true);
                _rfidReader.RfidDetectedEvent += Raise.EventWith(new RfidDetectedEventArgs { id = 123 });
                _rfidReader.RfidDetectedEvent += Raise.EventWith(new RfidDetectedEventArgs { id = 125 });

                //Assert
                Assert.That(_display.ReceivedString, Is.EqualTo("Forkert RFID tag"));
            }

            [Test]
            public void testChargeDisplay()
            {
                //Act
                _door.OpenDoorEvent += Raise.EventWith(new OpenDoorEventArgs { DoorOpen = "Door is open" });
                _door.ClosedDoorEvent += Raise.EventWith(new ClosedDoorEventArgs { DoorClosed = "Door is closed" });
                _rfidReader.RfidDetectedEvent += Raise.EventWith(new RfidDetectedEventArgs { id = 123 });
                _chargeControl.ChargeDisplayEvent += Raise.EventWith(new ChargeDisplayEventArgs { msg = "Phone charging." });
         
                //Assert
                Assert.That(_display.ReceivedString, Is.EqualTo("Phone charging."));
            }
        }
    }
}
