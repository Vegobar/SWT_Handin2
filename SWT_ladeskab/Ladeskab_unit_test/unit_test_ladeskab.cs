﻿using System;
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
            // private IHeater _heater;
            // private ISensor _sensor;
            // private IWindow _window;
            // private RefactoredECS.ECS _uut;

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
            public void Skabelon_test()
            {
                //_sensor.GetTemp().Returns(_uut.LowerTemperatureThreshold - 20);
                //_uut.Regulate();
                //_heater.Received(1).TurnOn();
            }
        }
    }
}
