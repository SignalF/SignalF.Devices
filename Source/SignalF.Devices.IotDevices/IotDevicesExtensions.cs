using Microsoft.Extensions.DependencyInjection;

namespace SignalF.Devices.IotDevices;

public static class IotDevicesExtensions
{
    public static IServiceCollection AddIotDevices(this IServiceCollection service)
    {
        return service;//.AddBme280();
            ;
    }
}
