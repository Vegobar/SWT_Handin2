using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using SWT_ladeskab;

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


            [SetUp]
            public void Setup()
            {
                _stationControl = Substitute.For<IStationControl>();
                _rfidReader = Substitute.For<IRFIDReader>();
                _chargeControl = Substitute.For<IChargeControl>();
                _door = Substitute.For<IDoor>();
                _display = Substitute.For<IDisplay>();
            }


            [Test]
            public void testingOpenDoor()
            {
                var wasCalled = false;
                _door.OpenDoorEvent += (sender, args) => wasCalled = true;
                _door.OpenDoorEvent += Raise.EventWith(new OpenDoorEventArgs());

                Assert.True(wasCalled);
            }

            [Test]
            public void testingCloseDoor()
            {
                var wasCalledClose = false;
                _door.ClosedDoorEvent += (sender, args) => wasCalledClose = true;
                _door.ClosedDoorEvent += Raise.EventWith(new ClosedDoorEventArgs());

                Assert.True(wasCalledClose);
            }

            [Test]
            public void testingEventDoorSubscription()
            {
                
            }
        }
    }
}