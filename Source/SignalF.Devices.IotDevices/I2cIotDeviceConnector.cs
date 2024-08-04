using System.Device.I2c;
using SignalF.Controller.Hardware.Channels.I2c;
using I2cDevice = System.Device.I2c.I2cDevice;

namespace SignalF.Devices.IotDevices;

public class I2cIotDeviceConnector : I2cDevice
{
    private readonly IList<II2cChannel> _channels;

    public I2cIotDeviceConnector(IList<II2cChannel> channels)
    {
        _channels = channels;
    }

    public override I2cConnectionSettings ConnectionSettings => null!;

    public override void Read(Span<byte> buffer)
    {
        foreach (var channel in _channels)
        {
            channel.Read(buffer);
        }
    }

    public override void Write(ReadOnlySpan<byte> buffer)
    {
        foreach (var channel in _channels)
        {
            channel.Write(buffer);
        }
    }

    public override void WriteRead(ReadOnlySpan<byte> writeBuffer, Span<byte> readBuffer)
    {
        foreach (var channel in _channels)
        {
            channel.WriteRead(writeBuffer, readBuffer);
        }
    }
}
