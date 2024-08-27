using Microsoft.Extensions.Logging;
using SignalF.Configuration.Integration;
using SignalF.Controller.Hardware.Channels;
using SignalF.Controller.Hardware.Channels.I2c;
using SignalF.Controller.Signals;
using SignalF.Datamodel.Hardware;

namespace SignalF.Devices.IotDevices.Bmp280;

[Device]
public class Bmp280 : I2cIotDevice
{
    private const int TemperatureIndex = 0;
    private const int PressureIndex = 1;

    private readonly int[] _signalIndices = new int[2];

    private Iot.Device.Bmxx80.Bmp280? _bmp280;

    public Bmp280(ISignalHub signalHub, ILogger<I2cIotDevice> logger) : base(signalHub, logger)
    {
    }

    public override void AssignChannels(IList<IChannel> channels)
    {
        _bmp280 = new Iot.Device.Bmxx80.Bmp280(new I2cIotDeviceConnector(channels.OfType<II2cChannel>().ToList()));
    }

    protected override void OnConfigure(IDeviceConfiguration configuration)
    {
        base.OnConfigure(configuration);

        Array.Fill(_signalIndices, -1);
        foreach (var signalDefinition in configuration.Definition.Template.SignalSourceDefinitions)
        {
            var signalConfiguration = configuration.SignalSources.Single(s => s.Definition.Id == signalDefinition.Id);

            switch (signalDefinition.Name)
            {
                case "Temperature":
                {
                    _signalIndices[TemperatureIndex] = GetSignalIndex(signalConfiguration);
                    break;
                }
                case "Pressure":
                {
                    _signalIndices[PressureIndex] = GetSignalIndex(signalConfiguration);
                    break;
                }
                default:
                {
                    throw new Exception(
                        $"Configuration of device BMP280 is wrong! Invalid signal definition name '{signalDefinition.Name}'.");
                }
            }
        }
    }

    protected override void OnRead()
    {
        var timestamp = SignalHub.GetTimestamp();

        if (_bmp280 == null)
        {
            return;
        }

        if (_signalIndices[TemperatureIndex] != -1)
        {
            _bmp280.TryReadTemperature(out var temperature);
            SignalSources[_signalIndices[TemperatureIndex]].AssignWith(temperature.DegreesCelsius, timestamp);
        }

        if (_signalIndices[TemperatureIndex] != -1)
        {
            _bmp280.TryReadPressure(out var pressure);
            SignalSources[_signalIndices[PressureIndex]].AssignWith(pressure.Pascals, timestamp);
        }
    }
}