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
    public class unit_test_door
    {
        [TestFixture]
        class NUnit_test_door_display
        {

            private IStationControl _stationControl;
            private Door _door;
            private IDisplay _display;
            private IChargeControl _chargeControl = Substitute.For<ChargeControl>();
            private IRFIDReader _rfidReader = Substitute.For<RFIDReader>();
            private OpenDoorEventArgs _receivedDoorArgs;
            private ClosedDoorEventArgs _receivedClosedDoorArgs;

            [SetUp]
            public void Setup()
            {
                _rfidReader = Substitute.For<IRFIDReader>();
                _chargeControl = Substitute.For<IChargeControl>();
                _door = new Door();
                _display = Substitute.For<IDisplay>();

                _stationControl = Substitute.For<StationControl>(_door, _display, _rfidReader, _chargeControl);
            }


            [Test]
            public void getDoorStateOpen()
            {
                _door.close();
                _door.unlockDoor();
                _door.open();
                Assert.That(_door.getDoorState(), Is.EqualTo(1));
            }

            [Test]
            public void getDoorStateClosed()
            {
                _door.close();
                Assert.That(_door.getDoorState(), Is.EqualTo(0));
            }

            [Test]
            public void getDoorStatusOnOpenedDoor()
            {
                _door.open();
                _door.open();
                Assert.That(_door.getDoorState(), Is.EqualTo(1));
            }

            [Test]
            public void getDoorStatusOnClosedDoor()
            {
                _door.close();
                _door.close();
                Assert.That(_door.getDoorState(), Is.EqualTo(0));
            }

            [Test]
             public void testingOpenDoorInvokation()
             {

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
                _door.open();
                _door.close();
                _stationControl.RfidDetected(123);

                //Assert
                Assert.IsTrue(_door.IsLocked);
            }

            [Test]
            public void testDoorUnlocked()
            {
                //Ac
                _stationControl.RfidDetected(123);
                _stationControl.RfidDetected(123);

                Assert.IsFalse(_door.IsLocked);

            }
           
            [Test]
            public void check_EventFiredOpenDoor()
            {
                //Arrange
                _receivedDoorArgs = null;

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
                _door.open();
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

                _door.open();
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
                _door.lockDoor();
                _door.open();

                Assert.IsTrue(_door.IsLocked);
            }
        }
    }
}
