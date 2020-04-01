using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Castle.Core.Smtp;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;
using SWT_ladeskab;
using UsbSimulator;

namespace Ladeskab_unit_test
{
    public class unit_test_rfidreader
    {
        [TestFixture]
        private class RfidTester
        {
            private RFIDReader _uut;
            private RfidDetectedEventArgs _receivedRFIDargs;

            [SetUp]
            public void Setup()
            {
                _receivedRFIDargs = null;
                _uut = new RFIDReader();
                _uut.RfidDetectedEvent += (o, args) => { _receivedRFIDargs = args; };
            }

            [TestCase(5)]
            [TestCase(2)]
            [TestCase(293673)]
            public void uut_readerEventargs_test(int a)
            {
                _uut.onRfidDetectedEvent(a);
                Assert.That(_receivedRFIDargs.id, Is.EqualTo(a));
            }

            [Test]
            public void uut_onRfid_test()
            {

                var wasCalled = false;
                _uut.RfidDetectedEvent += (sender, args) => wasCalled = true;
                
                //Rasing an event.
                _uut.onRfidDetectedEvent(20);

                //Asserting.
                Assert.True(wasCalled);
            }

            [TestCase(25, 25)]
            [TestCase(293673, 293673)]
            [TestCase(1000, 1000)]
            public void uut_onRfidDetectedEvent_test(int a, int expected)
            {
                _uut.onRfidDetectedEvent(a);
                Assert.That(_receivedRFIDargs.id, Is.EqualTo(expected));
            }

            [TestCase(-10)]
            [TestCase(0)]
            [TestCase(-100)]
            public void uut_onRfidDetectedEvent_Exception_test(int a)
            {
                _uut.onRfidDetectedEvent(a);
                Assert.That(_receivedRFIDargs, Is.Null);
            }

            [Test]
            public void uut_onRFIDEvent()
            {
                var value = 0;
                _uut.RfidDetectedEvent += (sender, args) => value = args.id;
                _uut.onRfidDetectedEvent(25);
                Assert.That(value == 25);
            }
        }
    }
}