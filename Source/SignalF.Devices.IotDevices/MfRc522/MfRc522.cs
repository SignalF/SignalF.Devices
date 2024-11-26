using Microsoft.Extensions.Logging;
using SignalF.Configuration.Integration;
using SignalF.Controller.Hardware.Channels;
using SignalF.Controller.Hardware.Channels.I2c;
using SignalF.Controller.Hardware.Channels.Spi;
using SignalF.Controller.Signals;
using SignalF.Controller.Signals.Devices;
using SignalF.Datamodel.Hardware;

namespace SignalF.Devices.IotDevices.MfRc522;

[Device]
public class MfRc522 : Device<IDeviceConfiguration>
{
    /// <summary>
    ///     PinReset for the <see cref="Iot.Device.Mfrc522.MfRc522" /> is always set to -1.
    ///     We handle the pin reset within this class.
    /// </summary>
    private const int InternalPinReset = -1;

    private Iot.Device.Mfrc522.MfRc522? _mfrc522;


    //TODO: Add serial channel
    private IotSpiDeviceConnector? _spiDeviceConnector;
    private IotI2cDeviceConnector? _i2cDeviceConnector;

    public MfRc522(ISignalHub signalHub, ILogger<Device<IDeviceConfiguration>> logger) : base(signalHub, logger)
    {
    }

    public override void AssignChannels(IList<IChannel> channels)
    {
        _spiDeviceConnector = new IotSpiDeviceConnector(channels.OfType<ISpiChannel>().ToList());
        _i2cDeviceConnector = new IotI2cDeviceConnector(channels.OfType<II2cChannel>().ToList());
    }

    protected override void OnInitialize()
    {
        base.OnInitialize();

        if (_spiDeviceConnector == null)
        {
            throw new NullReferenceException(nameof(_spiDeviceConnector));
        }

        var options = GetOptions<Mfrc522Options>();
        if (options != null)
        {
        }

        _mfrc522 = new Iot.Device.Mfrc522.MfRc522(_spiDeviceConnector, InternalPinReset, null, false);

    }
}
