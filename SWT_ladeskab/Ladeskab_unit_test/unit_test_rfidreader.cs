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
        class Sub_tester
        {
            private IRFIDReader _rfidReader;
            private RFIDReader uut;
            private RfidDetectedEventArgs _receivedRFIDargs;

            [SetUp]
            public void Setup()
            {
                _receivedRFIDargs = null;
                _rfidReader = Substitute.For<IRFIDReader>();
                uut = Substitute.For<RFIDReader>();
                _rfidReader.RfidDetectedEvent += (o, args) => { _receivedRFIDargs = args; };
            }

            [TestCase(5)]
            [TestCase(2)]
            [TestCase(293673)]
            [TestCase(-52)]
            
            public void uut_readerEventargs_test(int a)
            {
                _rfidReader.RfidDetectedEvent += Raise.EventWith(new RfidDetectedEventArgs(){id = a});
                Assert.That(_receivedRFIDargs.id,Is.EqualTo(a));
            }

            [Test]
            public void uut_onRfid_test()
            {
                bool wasCalled = false;
                
                _rfidReader.RfidDetectedEvent += (sender, args) => wasCalled = true;
                _rfidReader.RfidDetectedEvent += Raise.EventWith(new RfidDetectedEventArgs());
                Assert.That(wasCalled == true);
            }

            [Test]
            public void uut_onRFIDEvent()
            {
                int value = 0;
                uut.RfidDetectedEvent += (sender, args) => value = args.id;
                uut.onRfidDetectedEvent(25);
                Assert.That(value == 25);
                
            }
        }
    }
}