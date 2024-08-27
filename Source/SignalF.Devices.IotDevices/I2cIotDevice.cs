using Microsoft.Extensions.Logging;
using SignalF.Controller.Hardware.Channels.I2c;
using SignalF.Controller.Signals;
using SignalF.Datamodel.Hardware;

namespace SignalF.Devices.IotDevices;

public abstract class I2cIotDevice : I2cDevice<IDeviceConfiguration>

{
    protected I2cIotDevice(ISignalHub signalHub, ILogger<I2cDevice<IDeviceConfiguration>> logger) : base(signalHub,
        logger)
    {
    }
}