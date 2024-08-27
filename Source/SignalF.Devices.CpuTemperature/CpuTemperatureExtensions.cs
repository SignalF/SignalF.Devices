using System.Runtime.Versioning;
using Scotec.Math.Units;
using SignalF.Configuration;
using SignalF.Datamodel.Signals;

namespace SignalF.Devices.CpuTemperature;

[SupportedOSPlatform("linux")]
public static class CpuTemperatureExtensions
{
    private const string DeviceTemplateName = "CpuTemperatureTemplate";
    private const string DeviceDefinitionName = "CpuTemperatureDefinition";
    private const string DeviceDefaultName = "CpuTemperature";
    private const string SignalDefinitionName = "CpuTemperature";
    private const string SignalDefaultName = "CPU-Temperature";

    [SupportedOSPlatform("linux")]
    public static ISignalFConfiguration AddCpuTemperature(this ISignalFConfiguration configuration)
    {
        return configuration.AddCpuTemperatureConfiguration(DeviceDefaultName, SignalDefaultName);
    }

    public static ISignalFConfiguration AddCpuTemperature(this ISignalFConfiguration configuration, string deviceName,
        string signalName)
    {
        return configuration.AddCpuTemperatureTemplate()
            .AddCpuTemperatureDefinition()
            .AddCpuTemperatureConfiguration(deviceName, signalName);
    }

    private static ISignalFConfiguration AddCpuTemperatureTemplate(this ISignalFConfiguration configuration)
    {
        configuration.AddCpuTemperatureTemplate(builder =>
        {
            builder.SetName(DeviceTemplateName)
                .SetType<CpuTemperature>()
                .AddSignalSourceDefinition(SignalDefinitionName, EUnitType.Temperature);
        });

        return configuration;
    }

    private static ISignalFConfiguration AddCpuTemperatureConfiguration(this ISignalFConfiguration configuration,
        string deviceName, string signalName)
    {
        configuration.AddCpuTemperatureConfiguration(builder =>
        {
            builder.SetName(deviceName)
                .UseDefinition(DeviceDefinitionName)
                .AddSignalSourceConfiguration(SignalDefinitionName, signalName, Temperature.Units.DegreeCelsius);
        });

        return configuration;
    }

    private static ISignalFConfiguration AddCpuTemperatureDefinition(this ISignalFConfiguration configuration)
    {
        configuration.AddCpuTemperatureDefinition(builder =>
        {
            builder.SetName(DeviceDefinitionName)
                .UseTemplate(DeviceTemplateName);
        });

        return configuration;
    }
}