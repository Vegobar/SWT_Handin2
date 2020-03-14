using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Castle.Core.Smtp;
using NSubstitute;
using NUnit.Framework;
using SWT_ladeskab;
using UsbSimulator;

namespace Ladeskab_unit_test
{
    public class unit_test_door_display
    {
        [TestFixture]
        class Sub_tester
        {

            private IStationControl _stationControl;
            private IDoor _door;
            private IDisplay _display = Substitute.For<Display>();
            private IChargeControl _chargeControl = Substitute.For<ChargeControl>();
            private IRFIDReader _rfidReader = Substitute.For<RFIDReader>();
            private OpenDoorEventArgs _receivedDoorArgs;
            private ClosedDoorEventArgs _receivedClosedDoorArgs;


            [Test]
             public void testingOpenDoorInvokation()
             {
                //Arrange
                _door = Substitute.For<IDoor>();
                _stationControl = new StationControl(_door, _display, _rfidReader, _chargeControl);

                //Act
                var wasCalled = false;
                 _door.OpenDoorEvent += (sender, args) => wasCalled = true;
                 _door.OpenDoorEvent += Raise.EventWith(new OpenDoorEventArgs());

                //Assert
                 Assert.True(wasCalled);
             }

             [Test]
             public void testingCloseDoorInvokation()
             {
                // Arrange
                _door = Substitute.For<IDoor>();
                _stationControl = new StationControl(_door, _display, _rfidReader, _chargeControl);

                //Act
                var wasCalledClose = false;
                 _door.ClosedDoorEvent += (sender, args) => wasCalledClose = true;
                 _door.ClosedDoorEvent += Raise.EventWith(new ClosedDoorEventArgs());

                //Assert
                 Assert.True(wasCalledClose);
             }

            [Test]
            public void testingDoorLocked()
            {
                // Arrange
                Door doorTest = new Door();
                _stationControl = Substitute.For<StationControl>(doorTest, _display, _rfidReader, _chargeControl);

                // Act
                _stationControl.RfidDetected(123);

                //Assert
                Assert.IsTrue(doorTest.IsLocked);
            }

            [Test]
            public void testDoorUnlocked()
            {
                //Arrange
                Door doorTest = new Door();
                _stationControl = Substitute.For<StationControl>(doorTest, _display, _rfidReader, _chargeControl);

                //Ac
                _stationControl.RfidDetected(123);
                _stationControl.RfidDetected(123);

                Assert.IsFalse(doorTest.IsLocked);

            }

            [Test]
            public void check_EventFiredOpenDoor()
            {
                //Arrange
                _receivedDoorArgs = null;
                _door = new Door();

                _door.OpenDoorEvent +=
                    (o, args) =>
                    {
                        _receivedDoorArgs = args;
                    };


                //Act
                _door.open();

                //Assert
                Assert.That(_receivedDoorArgs, Is.Not.Null);

                }

            [Test]
            public void check_OpenDoorStringReceived()
            {
                //Arrange 
                _receivedDoorArgs = null;
                _door = new Door();

                _door.OpenDoorEvent +=
                    (o, args) =>
                    {
                        _receivedDoorArgs = args;
                    };

                //Act
                _door.open();

                //Assert
                Assert.AreEqual(_receivedDoorArgs.DoorOpen, "Door is open");

            }


            [Test]
            public void check_EventFiredCloseDoor()
            {
                //Arrange
                _receivedClosedDoorArgs = null;
                _door = new Door();

                _door.ClosedDoorEvent +=
                    (o, args) =>
                    {
                        _receivedClosedDoorArgs = args;
                    };


                //Act
                _door.close();

                //Assert
                Assert.That(_receivedClosedDoorArgs, Is.Not.Null);

            }

            [Test]
            public void ClosedDoorStringReceived()
            {
                //Arrange
                _receivedClosedDoorArgs = null;
                _door = new Door();

                _door.ClosedDoorEvent +=
                    (o, args) =>
                    {
                        _receivedClosedDoorArgs = args;
                    };


                //Act
                _door.close();

                //Assert
                Assert.AreEqual(_receivedClosedDoorArgs.DoorClosed, "Door is closed");

            }


            [Test]
            public void testDisplayOpenDoor()
            {
                _door = new Door();
                _stationControl = Substitute.For<StationControl>(_door, _display, _rfidReader, _chargeControl);

                _door.open();

                _display.Received(1).display("Tilslut telefon", 1);
            }

            [Test]
            public void testDisplayCloseDoor()
            {
                _door = new Door();
                _stationControl = Substitute.For<StationControl>(_door, _display, _rfidReader, _chargeControl);
                _door.close();
                _display.Received(1).display("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.", 1);
            }

            [Test]
            public void testDisplay_takePhoneOut()
            {
                _display = new Display();
                _door = Substitute.For<IDoor>();
                _stationControl = Substitute.For<StationControl>(_door, _display, _rfidReader, _chargeControl);

                _door.open();
                _rfidReader.onRfidDetectedEvent(123);
                _rfidReader.onRfidDetectedEvent(125);

                _display.Received(1).display("Phone is charging", 1);
            }




        }

    }
}
