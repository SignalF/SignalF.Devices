using Microsoft.Extensions.DependencyInjection;
using SignalF.Extensions.IotDevices.Bme280;

namespace SignalF.Extensions.IotDevices;

public static class IotDevicesExtensions
{
    public static IServiceCollection AddIotDevices(this IServiceCollection service)
    {
        return service.AddBme280();
    }
}
