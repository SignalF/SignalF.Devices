using Iot.Device.Bmxx80;
using Iot.Device.Bmxx80.PowerMode;
using UnitsNet;

namespace SignalF.Devices.IotDevices.Bme280;

public partial class Bme280Options
{
    public Bmx280PowerMode PowerMode { get; set; } = Bmx280PowerMode.Sleep;
    public Sampling TemperatureSampling { get; set; } = Sampling.UltraLowPower;
    public Sampling HumiditySampling { get; set; } = Sampling.UltraLowPower;
    public Sampling PressureSampling { get; set; } = Sampling.UltraLowPower;
    public StandbyTime StandbyTime { get; set; } = StandbyTime.Ms1000;
}