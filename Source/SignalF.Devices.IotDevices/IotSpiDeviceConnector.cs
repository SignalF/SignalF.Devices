using System.Device.Spi;
using SignalF.Controller.Hardware.Channels.Spi;
using SpiDevice = System.Device.Spi.SpiDevice;

namespace SignalF.Devices.IotDevices;

public class IotSpiDeviceConnector : SpiDevice
{
    private readonly IList<ISpiChannel> _channels;

    public IotSpiDeviceConnector(IList<ISpiChannel> channels)
    {
        _channels = channels;
    }

    public override SpiConnectionSettings ConnectionSettings => null!;

    public override void Read(Span<byte> buffer)
    {
        foreach (var channel in _channels) channel.Read(buffer);
    }

    public override void Write(ReadOnlySpan<byte> buffer)
    {
        foreach (var channel in _channels) channel.Write(buffer);
    }

    public override void TransferFullDuplex(ReadOnlySpan<byte> writeBuffer, Span<byte> readBuffer)
    {
        foreach (var channel in _channels) channel.TransferFullDuplex(writeBuffer, readBuffer);
    }
}