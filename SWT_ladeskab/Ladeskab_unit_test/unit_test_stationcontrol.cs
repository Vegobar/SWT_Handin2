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
    public class unit_test_stationcontrol
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

                _stationControl = new SWT_ladeskab.StationControl(_door, _display, _rfidReader);
            }

            [Test]
            public void testDoorOpenEventHandler()
            {
                _door.OpenDoorEvent += Raise.EventWith(new OpenDoorEventArgs());
                _display.Received(1).display("Tilslut telefon");
            }

            [Test]
            public void testCloseOpenEventHandler()
            {
                _door.ClosedDoorEvent += Raise.EventWith(new ClosedDoorEventArgs());
                _display.Received(1).display("Indlæs RFID");
            }
        }
    }
}