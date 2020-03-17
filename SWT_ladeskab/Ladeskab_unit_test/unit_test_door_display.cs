using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Castle.Core.Smtp;
using core_functionality;
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
            private IDisplay _display;
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
            public void OpenWhenCLocked()
            {
                //Arrange
                Door doorTest = new Door();
                _display = Substitute.For<IDisplay>();
                _stationControl = Substitute.For<StationControl>(doorTest, _display, _rfidReader, _chargeControl);
               

                doorTest.lockDoor();
                doorTest.open();

                Assert.IsTrue(doorTest.IsLocked);
            }



            [Test]
            public void testDisplayOpenDoor()
            {
                //Arrange
                _door = Substitute.For<IDoor>();
                _display = new Display();
                _stationControl = Substitute.For<StationControl>(_door, _display, _rfidReader, _chargeControl);

                //Act
                _door.OpenDoorEvent += Raise.EventWith(new OpenDoorEventArgs());

                //Assert
                Assert.That(_display.ReceivedString, Is.EqualTo("Tilslut telefon"));
            }

            [Test]
            public void testDisplayCloseDoor()
            {
                //Arrange
                _door = Substitute.For<IDoor>();
                _display =new Display();
                _stationControl = Substitute.For<StationControl>(_door, _display, _rfidReader, _chargeControl);

                //Act
                _stationControl.RfidDetected(123);

                //Assert
                Assert.That(_display.ReceivedString, Is.EqualTo("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op."));
            }

            [Test]
            public void testDisplay_wrongTag()
            {
                //Arrange
                _door = Substitute.For<IDoor>();
                _display = new Display();
                _stationControl = Substitute.For<StationControl>(_door, _display, _rfidReader, _chargeControl);

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
                //Arrange
                _door = Substitute.For<IDoor>();
                _chargeControl = Substitute.For<ChargeControl>();
                _display = new Display();
                _stationControl = Substitute.For<StationControl>(_door, _display, _rfidReader, _chargeControl);

                _chargeControl.CurrentCharge = 20;



            }

            private void ChargeDisplayEventHandler(object sender, ChargeDisplayEventArgs e)
            {

                _display.display(e.msg, 2);
            }
        }

    }
}
