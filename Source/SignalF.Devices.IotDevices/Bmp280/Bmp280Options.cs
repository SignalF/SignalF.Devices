using Iot.Device.Bmxx80;
using Iot.Device.Bmxx80.PowerMode;
using UnitsNet;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalF.Devices.IotDevices.Bmp280
{
    public partial class Bmp280Options
    {
        public Bmx280PowerMode PowerMode { get; set; } = Bmx280PowerMode.Sleep;
        public Sampling TemperatureSampling { get; set; } = Sampling.UltraLowPower;
        public Sampling PressureSampling { get; set; } = Sampling.UltraLowPower;
        public StandbyTime StandbyTime { get; set; } = StandbyTime.Ms1000;

    }
}
