﻿using Microsoft.Extensions.Logging;
using SignalF.Configuration.Integration;
using SignalF.Controller.Hardware.Channels;
using SignalF.Controller.Hardware.Channels.I2c;
using SignalF.Controller.Signals;
using SignalF.Datamodel.Hardware;

namespace SignalF.Devices.IotDevices.Bme280;

[Device]
public partial class Bme280 : IotI2cDevice
{
    private const int TemperatureIndex = 0;
    private const int PressureIndex = 1;
    private const int HumidityIndex = 2;

    private readonly int[] _signalIndices = new int[3];

    private Iot.Device.Bmxx80.Bme280? _bme280;
    private IotI2cDeviceConnector? _deviceConnector;

    public Bme280(ISignalHub signalHub, ILogger<IotI2cDevice> logger) : base(signalHub, logger)
    {
    }

    public override void AssignChannels(IList<IChannel> channels)
    {
        // The Bme280 implementation already attempts to access the I2cDevice for reading
        // and writing in the constructor. As this already happens in the configuration
        // phase of the SignalF controller, the channel is not yet open. For this reason,
        // the Bme280 must not be instantiated here. This can only be done in the Init method.
        _deviceConnector = new IotI2cDeviceConnector(channels.OfType<II2cChannel>().ToList());
    }

    protected override void OnConfigure(IDeviceConfiguration configuration)
    {
        base.OnConfigure(configuration);

        Array.Fill(_signalIndices, -1);
        var signalSourceDefinitions = configuration.Definition.SignalSourceDefinitions
                                                   .Concat(configuration.Definition.Template.SignalSourceDefinitions);

        foreach (var signalDefinition in signalSourceDefinitions)
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
                    throw new Exception(
                        $"Configuration of device BME280 is wrong! Invalid signal definition name '{signalDefinition.Name}'.");
                }
            }
        }
    }

    protected override void OnInitialize()
    {
        base.OnInitialize();

        if (_deviceConnector == null)
        {
            throw new NullReferenceException(nameof(_deviceConnector));
        }

        _bme280 = new Iot.Device.Bmxx80.Bme280(_deviceConnector);
        
        var options = GetOptions<Bme280Options>();
        if (options != null)
        {
            _bme280.SetPowerMode(options.PowerMode);
            _bme280.PressureSampling = options.PressureSampling;
            _bme280.TemperatureSampling = options.TemperatureSampling;
            _bme280.HumiditySampling = options.HumiditySampling;
            _bme280.StandbyTime = options.StandbyTime;
        }
    }

    protected override void OnExit()
    {
        _bme280?.Dispose();
        _bme280 = null;

        base.OnExit();
    }

    protected override void OnWrite()
    {
        var timestamp = SignalHub.GetTimestamp();

        if (_bme280 == null)
        {
            return;
        }

        if (_signalIndices[TemperatureIndex] != -1)
        {
            _bme280.TryReadTemperature(out var temperature);
            SignalSources[_signalIndices[TemperatureIndex]].AssignWith(temperature.Kelvins, timestamp);
        }

        if (_signalIndices[PressureIndex] != -1)
        {
            _bme280.TryReadPressure(out var pressure);
            SignalSources[_signalIndices[PressureIndex]].AssignWith(pressure.Pascals, timestamp);
        }

        if (_signalIndices[HumidityIndex] != -1)
        {
            _bme280.TryReadHumidity(out var humidity);
            SignalSources[_signalIndices[HumidityIndex]].AssignWith(humidity.Percent, timestamp);
        }
    }
}