using core_functionality;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;
using SWT_ladeskab;

namespace Ladeskab_unit_test
{
    public class unit_test_stationcontrol
    {
        [TestFixture]
        private class Sub_tester
        {
            private StationControl _stationControl;
            private IRFIDReader _rfidReader;
            private IChargeControl _chargeControl;
            private IDoor _door;
            private IDisplay _display;
            private ILog _log;


            [SetUp]
            public void Setup()
            {
                _rfidReader = Substitute.For<IRFIDReader>();
                _chargeControl = Substitute.For<IChargeControl>();
                _door = Substitute.For<IDoor>();
                _display = Substitute.For<IDisplay>();
                _log = Substitute.For<ILog>();

                _stationControl = new StationControl(_door, _display, _rfidReader, _chargeControl, _log);
            }

            //Test in this region is a bit better... Maybe.

            #region All test cases:

            #region Test for switch case avaiable & locked

            [Test]
            public void test_LadeSkabsStateLocked_false_id_connected_false()
            {
                _chargeControl.isConnected().Returns(false);

                _rfidReader.RfidDetectedEvent += Raise.EventWith(new RfidDetectedEventArgs {id = 123});
                _rfidReader.RfidDetectedEvent += Raise.EventWith(new RfidDetectedEventArgs {id = 123});

                _display.Received(2).display("Din telefon er ikke ordentlig tilsluttet. Prøv igen",
                    1);
            }

            [Test]
            public void test_LadeSkabsStateLocked_false_id_locked()
            {
                _chargeControl.isConnected().Returns(true);

                _rfidReader.RfidDetectedEvent += Raise.EventWith(new RfidDetectedEventArgs {id = 123});

                _display.Received(1).display("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.",
                    1);
            }

            [Test]
            public void test_LadeSkabsStateLocked_false_id_and_wrong_tag()
            {
                _chargeControl.isConnected().Returns(true);

                _rfidReader.RfidDetectedEvent += Raise.EventWith(new RfidDetectedEventArgs {id = 123});
                _rfidReader.RfidDetectedEvent += Raise.EventWith(new RfidDetectedEventArgs {id = 124});

                _display.Received(1).display("Forkert RFID tag", 1);
            }

            [Test]
            public void test_LadeSkabsStateLocked_false_id_both_combined()
            {
                _chargeControl.isConnected().Returns(true);

                _rfidReader.RfidDetectedEvent += Raise.EventWith(new RfidDetectedEventArgs {id = 123});
                _rfidReader.RfidDetectedEvent += Raise.EventWith(new RfidDetectedEventArgs {id = 124});

                _display.Received(1).display("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.",
                    1);
                _display.Received(1).display("Forkert RFID tag", 1);
            }

            [Test]
            public void test_LadeSkabsStateLocked_true_id_lock_phoneChargeing()
            {
                _chargeControl.isConnected().Returns(true);

                _rfidReader.RfidDetectedEvent += Raise.EventWith(new RfidDetectedEventArgs {id = 123});

                _display.Received(1).display("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.",
                    1);
            }

            [Test]
            public void test_LadeSkabsStateLocked_true_id_lock_takePhoneOut()
            {
                _chargeControl.isConnected().Returns(true);

                _rfidReader.RfidDetectedEvent += Raise.EventWith(new RfidDetectedEventArgs {id = 123});
                _rfidReader.RfidDetectedEvent += Raise.EventWith(new RfidDetectedEventArgs {id = 123});

                _display.Received(1).display("Tag din telefon ud af skabet og luk døren", 1);
            }

            [Test]
            public void test_LadeSkabsStateLocked_true_id_full_route_complete()
            {
                _chargeControl.isConnected().Returns(true);

                _rfidReader.RfidDetectedEvent += Raise.EventWith(new RfidDetectedEventArgs {id = 123});
                _rfidReader.RfidDetectedEvent += Raise.EventWith(new RfidDetectedEventArgs {id = 123});


                _display.Received(1).display("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.",
                    1);
                _display.Received(1).display("Tag din telefon ud af skabet og luk døren", 1);
            }

            [Test]
            public void test_logger_called_lock_with_rfid()
            {
                _chargeControl.isConnected().Returns(true);

                _rfidReader.RfidDetectedEvent += Raise.EventWith(new RfidDetectedEventArgs {id = 123});

                _log.Received(1).PrintToFile(": Skab låst med RFID: ", 123);
            }

            [Test]
            public void test_logger_called_unlock_with_rfid()
            {
                _chargeControl.isConnected().Returns(true);

                _rfidReader.RfidDetectedEvent += Raise.EventWith(new RfidDetectedEventArgs {id = 123});
                _rfidReader.RfidDetectedEvent += Raise.EventWith(new RfidDetectedEventArgs {id = 123});

                _log.Received(1).PrintToFile(": Skab låst op med RFID: ", 123);
            }

            [Test]
            public void test_logger_called_testing_log_combined()
            {
                _chargeControl.isConnected().Returns(true);

                _rfidReader.RfidDetectedEvent += Raise.EventWith(new RfidDetectedEventArgs {id = 123});
                _rfidReader.RfidDetectedEvent += Raise.EventWith(new RfidDetectedEventArgs {id = 123});

                _log.Received(1).PrintToFile(": Skab låst med RFID: ", 123);
                _log.Received(1).PrintToFile(": Skab låst op med RFID: ", 123);
            }

            #endregion


            #region Tests for connected = false & true

            #endregion

            #endregion


            //Tests under this are not done yet. 

            [Test]
            public void test_conncted_phone_false()
            {
                _chargeControl.isConnected().Returns(false);

                _rfidReader.RfidDetectedEvent += Raise.EventWith(new RfidDetectedEventArgs() {id = 123});

                _display.Received(1).display("Din telefon er ikke ordentlig tilsluttet. Prøv igen", 1);
            }

            [Test]
            public void test_connected_phone_true()
            {
                _chargeControl.isConnected().Returns(true);

                _rfidReader.RfidDetectedEvent += Raise.EventWith(new RfidDetectedEventArgs() {id = 123});

                _display.Received(1).display("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.",
                    1);
            }


            [Test]
            public void test_LadeSkabsStateLocked_false_id_connected_false_thenAfter_true()
            {
                _chargeControl.isConnected().Returns(false);

                _rfidReader.RfidDetectedEvent += Raise.EventWith(new RfidDetectedEventArgs {id = 123});
                _chargeControl.isConnected().Returns(true);
                _rfidReader.RfidDetectedEvent += Raise.EventWith(new RfidDetectedEventArgs {id = 123});

                _display.Received(1).display("Din telefon er ikke ordentlig tilsluttet. Prøv igen",
                    1);
                _display.Received(1).display("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.",
                    1);
            }

            [Test]
            public void testCheckID_false()
            {
                var result = _stationControl.CheckId(30, 35);
                Assert.IsFalse(result);
            }

            [Test]
            public void testCheckID_true()
            {
                var result = _stationControl.CheckId(30, 30);
                Assert.IsTrue(result);
            }

            [Test]
            public void testCloseOpenEventHandler()
            {
                _door.ClosedDoorEvent += Raise.EventWith(new ClosedDoorEventArgs());
                _display.Received(1).display("Indlæs RFID", 1);
            }

            [Test]
            public void testDoorOpenEventHandler()
            {
                _door.OpenDoorEvent += Raise.EventWith(new OpenDoorEventArgs());
                _display.Received(1).display("Tilslut telefon", 1);
            }

            [Test]
            public void testRFIDDetectedEvent()
            {
                var wasCalled = false;
                _rfidReader.RfidDetectedEvent += (sender, args) => wasCalled = true;

                _rfidReader.RfidDetectedEvent += Raise.EventWith(new RfidDetectedEventArgs());
                Assert.True(wasCalled);
            }


            [Test]
            public void test_logger_called_wrong_id()
            {
                _chargeControl.isConnected().Returns(true);

                _rfidReader.RfidDetectedEvent += Raise.EventWith(new RfidDetectedEventArgs {id = 123});
                _rfidReader.RfidDetectedEvent += Raise.EventWith(new RfidDetectedEventArgs {id = 124});

                _log.Received(1).PrintToFile(": Skab låst med RFID: ", 123);
                _log.Received(0).PrintToFile(": Skab låst op med RFID: ", 123);
            }
        }
    }
}