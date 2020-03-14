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


            [Test]
             public void testingOpenDoorInvokation()
             {
                //Assert
                _door = Substitute.For<IDoor>();
                _stationControl = new StationControl(_door, _display, _rfidReader, _chargeControl);

                var wasCalled = false;
                 _door.OpenDoorEvent += (sender, args) => wasCalled = true;
                 _door.OpenDoorEvent += Raise.EventWith(new OpenDoorEventArgs());

                 Assert.True(wasCalled);
             }

             [Test]
             public void testingCloseDoorInvokation()
             {
                // Assert
                _door = Substitute.For<IDoor>();
                _stationControl = new StationControl(_door, _display, _rfidReader, _chargeControl);

                //Act
                var wasCalledClose = false;
                 _door.ClosedDoorEvent += (sender, args) => wasCalledClose = true;
                 _door.ClosedDoorEvent += Raise.EventWith(new ClosedDoorEventArgs());

                 Assert.True(wasCalledClose);
             }


        }
    }
}
