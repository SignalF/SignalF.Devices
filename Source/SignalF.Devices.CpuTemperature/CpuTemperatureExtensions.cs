using System.Runtime.Versioning;
using Microsoft.Extensions.DependencyInjection;
using Scotec.Math.Units;
using SignalF.Configuration;
using SignalF.Datamodel.Signals;
using SignalF.Extensions.Configuration;
namespace SignalF.Devices.CpuTemperature;

[SupportedOSPlatform("linux")]

public static class CpuTemperatureExtensions
{
    private const string DeviceName = "CpuTemperature";
    private const string SignalName = "CpuTemperature";

    [SupportedOSPlatform("linux")]
    public static IServiceCollection AddCpuTemperature(this IServiceCollection services)
    {
        return services.AddTransient<CpuTemperature>();
    }

    public static ICoreConfiguration AddCpuTemperatureTemplate(this ICoreConfiguration configuration)
    {
        configuration.AddDeviceTemplate(builder =>
        {
            builder.SetName($"{DeviceName}Template")
                   .SetType<CpuTemperature>()
                   .AddSignalSourceDefinition(SignalName, EUnitType.Temperature);
        });

        return configuration;
    }

    public static ICoreConfiguration AddCpuTemperatureDefinition(this ICoreConfiguration configuration)
    {
        configuration.AddDeviceDefinition(builder =>
        {
            builder.SetName($"{DeviceName}Definition")
                   .UseTemplate($"{DeviceName}Template");
        });

        return configuration;
    }

    public static ICoreConfiguration AddCpuTemperatureConfiguration(this ICoreConfiguration configuration)
    {
        return configuration.AddCpuTemperatureConfiguration(DeviceName, SignalName);
    }

    public static ICoreConfiguration AddCpuTemperatureConfiguration(this ICoreConfiguration configuration, string deviceName, string signalName)
    {
        configuration.AddDeviceConfiguration(builder =>
        {
            builder.SetName(deviceName)
                   .UseDefinition($"{deviceName}Definition")
                   .AddSignalSourceConfiguration(signalName, "CPUTemperature", Temperature.Units.DegreeCelsius);
        });

        return configuration;
    }

    public static ICoreConfiguration AddCpuTemperature(this ICoreConfiguration configuration, string deviceName, string signalName)
    {
        return configuration.AddCpuTemperatureTemplate()
                            .AddCpuTemperatureDefinition()
                            .AddCpuTemperatureConfiguration(deviceName, signalName);
    }
}
