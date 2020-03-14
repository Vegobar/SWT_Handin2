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
    public class unit_test_chargecontrol
    {
        [TestFixture]
        class Sub_tester
        {
            private CurrentEventArgs _currentEvent;
            private ChargeControl uut;

            [SetUp]
            public void Setup()
            {

            }

            [TestCase()]
            void uut_currentEvent_test(double a, double b)
            {
                _currentEvent.Current = a;
            }
        }
    }
}