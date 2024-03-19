using System.Runtime.Versioning;
using Microsoft.Extensions.Logging;
using Scotec.Math.Units;
using SignalF.Controller.Signals;
using SignalF.Controller.Signals.Devices;
using SignalF.Datamodel.Configuration;
using SignalF.Datamodel.Hardware;

namespace SignalF.Devices.CpuTemperature;

/// <summary>
///     CPU temperature.
/// </summary>
[SupportedOSPlatform("linux")]
public sealed class CpuTemperature : NullDevice<IDeviceConfiguration>, ICpuTemperature
{
    private int _channelIndex;

    /// <summary>
    ///     Creates an instance of the CpuTemperature class
    /// </summary>
    public CpuTemperature(ISignalHub signalHub, ILogger<NullDevice<IDeviceConfiguration>> logger) 
        : base(signalHub, logger)
    {
        Device = new Device.CpuTemperature();
    }

    private Device.CpuTemperature Device { get; }

    public override void Execute(ETaskType taskType)
    {
        switch (taskType)
        {
            case ETaskType.Init:
            {
                break;
            }
            case ETaskType.Write:
            {
                var timestamp = SignalHub.GetTimestamp();
                var temperature = Device.IsAvailable ? Device.Temperature[Temperature.Units.Kelvin] : double.NaN;
                SignalHub.SetSignal(new Signal(_channelIndex, temperature, timestamp));
                break;
            }
            case ETaskType.Exit:
            {
                break;
            }
            default:
                throw new ArgumentOutOfRangeException(nameof(taskType), taskType, "The task type is not supported.");
        }
    }

    public void Dispose()
    {
        Device.Dispose();
    }

    protected override void OnConfigure(IDeviceConfiguration configuration)
    {
        base.OnConfigure(configuration);

        foreach (var signalDefinition in configuration.Definition.Template.SignalSourceDefinitions)
        {
            var signalConfiguration = configuration.SignalSources.Single(s => s.Definition.Id == signalDefinition.Id);

            switch (signalDefinition.Name)
            {
                case "CpuTemperature":
                {
                    _channelIndex = SignalHub.GetSignalIndex(signalConfiguration);
                    break;
                }
                default:
                {
                    throw new Exception($"Configuration of CPU temperature sensor is wrong! Invalid signal definition name '{signalDefinition.Name}'.");
                }
            }
        }
    }
}
