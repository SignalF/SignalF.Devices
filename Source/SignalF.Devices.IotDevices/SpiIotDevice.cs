using Microsoft.Extensions.Logging;
using SignalF.Controller.Hardware.Channels.Spi;
using SignalF.Controller.Signals;
using SignalF.Datamodel.Hardware;

namespace SignalF.Devices.IotDevices;

public abstract class SpiIotDevice : SpiDevice<IDeviceConfiguration>

{
    protected SpiIotDevice(ISignalHub signalHub, ILogger<SpiIotDevice> logger) : base(signalHub,
        logger)
    {
    }
}