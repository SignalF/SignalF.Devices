using Microsoft.Extensions.Logging;
using SignalF.Controller.Hardware.Channels.I2c;
using SignalF.Controller.Signals;
using SignalF.Datamodel.Hardware;

namespace SignalF.Devices.IotDevices;

public abstract class IotI2cDevice : I2cDevice<IDeviceConfiguration>

{
    protected IotI2cDevice(ISignalHub signalHub, ILogger<IotI2cDevice> logger) : base(signalHub,
        logger)
    {
    }
}