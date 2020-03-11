using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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


            [SetUp]
            public void Setup()
            {
                _stationControl = Substitute.For<IStationControl>();
                _rfidReader = Substitute.For<IRFIDReader>();
                _chargeControl = Substitute.For<IChargeControl>();
                _door = Substitute.For<IDoor>();
                _display = Substitute.For<IDisplay>();
                _usbCharger = Substitute.For<IUsbCharger>();

            }

            [Test]
            public void Skabelon_test()
            {
                
            }

            [Test]
            public void testingOpenDoor()
            {
                var wasCalled = false;
                _door.OpenDoorEvent += (sender, args) => wasCalled = true;
                _door.OpenDoorEvent += Raise.EventWith(new OpenDoorEventArgs());

                Assert.True(wasCalled);
            }

        }
    }
}