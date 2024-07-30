using System.Management;
using SignalF.Datamodel.Hardware;
using Microsoft.Extensions.Logging;
using Scotec.Math.Units;
using SignalF.Controller.Hardware.Channels;
using SignalF.Controller.Hardware.Channels.I2c;
using SignalF.Controller.Signals;
using SignalF.Controller.Signals.Devices;

namespace SignalF.Devices.Bmxx80
{
    public class Bme280 : I2cDevice<IDeviceConfiguration>
    {
        private const int TemperatureIndex = 0;
        private const int PressureIndex = 1;
        private const int HumidityIndex = 2;

        private readonly int[] _signalIndices = new int[3];

        private Iot.Device.Bmxx80.Bme280? _bme280;

        public Bme280(ISignalHub signalHub, ILogger<I2cDevice<IDeviceConfiguration>> logger) : base(signalHub, logger)
        {
        }

        public override void AssignChannels(IList<IChannel> channels)
        {
            _bme280 = new Iot.Device.Bmxx80.Bme280(new I2cIotDevice(channels.OfType<II2cChannel>().ToList()));
        }

        protected override void OnConfigure(IDeviceConfiguration configuration)
        {
            base.OnConfigure(configuration);

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
                    default:
                    {
                        throw new Exception($"Configuration of sensor BME280 is wrong! Invalid signal definition name '{signalDefinition.Name}'.");
                    }
                }
            }
        }

        protected override void OnRead()
        {
            var timestamp = SignalHub.GetTimestamp();

            _bme280.TryReadTemperature(out var temperature);
            _bme280.TryReadPressure(out var pressure);
            _bme280.TryReadHumidity(out var humidity);

            SignalSources[_signalIndices[TemperatureIndex]].AssignWith(temperature.DegreesCelsius, timestamp);
            SignalSources[_signalIndices[PressureIndex]].AssignWith(pressure.Pascals, timestamp);
            SignalSources[_signalIndices[HumidityIndex]].AssignWith(humidity.Percent, timestamp);
        }
    }
}
