﻿using System;
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
    public class unit_test_ladeskab
    {
        [TestFixture]
        class Sub_tester
        {

            private IStationControl _stationControl;
            private IRFIDReader _rfidReader;
            private IChargeControl _chargeControl;
            private IDoor _door;
            private IDisplay _display;
            private IUsbCharger _usbCharger;
            private OpenDoorEventArgs _receivedDoorArgs;


            [SetUp]
            public void Setup()
            {
                _stationControl = Substitute.For<IStationControl>();
                _door = Substitute.For<Door>();
                _display = Substitute.For<IDisplay>();
          

            }


           /* [Test]
            public void testingOpenDoorInvokation()
            {
                var wasCalled = false;
                _door.OpenDoorEvent += (sender, args) => wasCalled = true;
                _door.OpenDoorEvent += Raise.EventWith(new OpenDoorEventArgs());

                Assert.True(wasCalled);
            }

            [Test]
            public void testingCloseDoorInvokation()
            {
                var wasCalledClose = false;
                _door.ClosedDoorEvent += (sender, args) => wasCalledClose = true;
                _door.ClosedDoorEvent += Raise.EventWith(new ClosedDoorEventArgs());

                Assert.True(wasCalledClose);
            }*/

            [Test]
            public void testlockDoor()
            {
                _door.lockDoor();
               // Assert.That(_door.getDoorState(), Is.True);
            }



        }
    }
}
