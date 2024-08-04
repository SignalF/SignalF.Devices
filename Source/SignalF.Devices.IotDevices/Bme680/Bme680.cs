using Microsoft.Extensions.Logging;
using SignalF.Configuration;
using SignalF.Configuration.Integration;
using SignalF.Controller.Hardware.Channels;
using SignalF.Controller.Hardware.Channels.I2c;
using SignalF.Controller.Signals;
using SignalF.Datamodel.Hardware;

namespace SignalF.Devices.IotDevices.Bme680
{
    [Device]
    public class Bme680 : I2cIotDevice
    {
        private const int TemperatureIndex = 0;
        private const int PressureIndex = 1;
        private const int HumidityIndex = 2;
        private const int GasResistanceIndex = 3;

        private readonly int[] _signalIndices = new int[4];

        private Iot.Device.Bmxx80.Bme680? _bme680;

        public Bme680(ISignalHub signalHub, ILogger<I2cIotDevice> logger) : base(signalHub, logger)
        {
        }

        public override void AssignChannels(IList<IChannel> channels)
        {
            _bme680 = new Iot.Device.Bmxx80.Bme680(new I2cIotDeviceConnector(channels.OfType<II2cChannel>().ToList()));
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
                    case "Humidity":
                        {
                            _signalIndices[HumidityIndex] = GetSignalIndex(signalConfiguration);
                            break;
                        }
                    case "GasResistance":
                        {
                            _signalIndices[GasResistanceIndex] = GetSignalIndex(signalConfiguration);
                            break;
                        }
                    default:
                        {
                            throw new Exception($"Configuration of device BME680 is wrong! Invalid signal definition name '{signalDefinition.Name}'.");
                        }
                }
            }
        }

        protected override void OnRead()
        {
            var timestamp = SignalHub.GetTimestamp();

            if (_bme680 == null)
            {
                return;
            }

            if (_signalIndices[TemperatureIndex] != -1)
            {
                _bme680.TryReadTemperature(out var temperature);
                SignalSources[_signalIndices[TemperatureIndex]].AssignWith(temperature.DegreesCelsius, timestamp);
            }
            if (_signalIndices[TemperatureIndex] != -1)
            {
                _bme680.TryReadPressure(out var pressure);
                SignalSources[_signalIndices[PressureIndex]].AssignWith(pressure.Pascals, timestamp);
            }
            if (_signalIndices[TemperatureIndex] != -1)
            {
                _bme680.TryReadHumidity(out var humidity);
                SignalSources[_signalIndices[HumidityIndex]].AssignWith(humidity.Percent, timestamp);
            }
            if (_signalIndices[TemperatureIndex] != -1)
            {
                _bme680.TryReadGasResistance(out var gasResistance);
                SignalSources[_signalIndices[GasResistanceIndex]].AssignWith(gasResistance.Ohms, timestamp);
            }
        }
    }
}
