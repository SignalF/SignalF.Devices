using Iot.Device.Bmxx80;
using Iot.Device.Bmxx80.FilteringMode;
using Iot.Device.Bmxx80.PowerMode;
using UnitsNet;

namespace SignalF.Devices.IotDevices.Bme680;

public partial class Bme680Options
{
    public Bme680PowerMode PowerMode { get; set; } = Bme680PowerMode.Sleep;
    
    public Sampling TemperatureSampling { get; set; } = Sampling.UltraLowPower;
    
    public Sampling HumiditySampling { get; set; } = Sampling.UltraLowPower;
    
    public Sampling PressureSampling { get; set; } = Sampling.UltraLowPower;
    
    public bool HeaterIsEnabled { get; set; } = false;

    public bool GasConversionIsEnabled { get; set; } = false;
}