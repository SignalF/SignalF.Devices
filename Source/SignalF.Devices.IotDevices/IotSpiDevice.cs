using Microsoft.Extensions.Logging;
using SignalF.Controller.Hardware.Channels.Spi;
using SignalF.Controller.Signals;
using SignalF.Datamodel.Hardware;

namespace SignalF.Devices.IotDevices;

public abstract class IotSpiDevice : SpiDevice<IDeviceConfiguration>

{
    protected IotSpiDevice(ISignalHub signalHub, ILogger<IotSpiDevice> logger) : base(signalHub,
        logger)
    {
    }
}