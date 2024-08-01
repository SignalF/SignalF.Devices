using Microsoft.Extensions.DependencyInjection;
using SignalF.Configuration;
using SignalF.Configuration.Devices;
using SignalF.Configuration.Devices.Gpio;
using SignalF.Controller.Configuration;
using SignalF.Controller.Signals.Devices;
using SignalF.Datamodel.Hardware;
using SignalF.Datamodel.Signals;
using SignalF.Extensions.Configuration;

namespace SignalF.Extensions.IotDevices.Bme280
{
    public static class Bme280Extensions
    {
        public static ISignalFConfiguration AddBme280Configuration(this ISignalFConfiguration configuration,
                                                                  Action<IDeviceConfigurationBuilder> builder)
        {
            return configuration.AddDeviceConfiguration<IDeviceConfigurationBuilder, Bme280Options>(builder);
        }

        public static ISignalFConfiguration AddBme280Configuration<TType>(this ISignalFConfiguration configuration
                                                                                 , Action<IDeviceConfigurationBuilder> builder)
            where TType : Bme280
        {
            return configuration.AddDeviceConfiguration<IDeviceConfigurationBuilder, Bme280Options, TType>(builder);
        }

        public static ISignalFConfiguration AddBme280Definition(this ISignalFConfiguration configuration, Action<IDeviceDefinitionBuilder> builder)
        {
            return configuration.AddDeviceDefinition<IDeviceDefinitionBuilder, Bme280Options>(builder);
        }

        public static ISignalFConfiguration AddBme280Definition<TType>(this ISignalFConfiguration configuration,
                                                                              Action<IDeviceDefinitionBuilder> builder)
            where TType : Bme280
        {
            return configuration.AddDeviceDefinition<IDeviceDefinitionBuilder, Bme280Options, TType>(builder);
        }

        public static ISignalFConfiguration AddBme280Template(this ISignalFConfiguration configuration, Action<IDeviceTemplateBuilder> builder)
        {
            return configuration.AddDeviceTemplate<IDeviceTemplateBuilder, Bme280Options, Bme280>(builder);
        }

        public static ISignalFConfiguration AddDeviceTemplate<TType>(this ISignalFConfiguration configuration, Action<IDeviceTemplateBuilder> builder)
            where TType : class, IDevice
        {
            return configuration.AddDeviceTemplate<IDeviceTemplateBuilder, Bme280Options, TType>(builder);
        }

        public static IServiceCollection AddBme280(this IServiceCollection services)
        {
            return services.AddTransient<Bme280>();
        }

        public static ISignalConfiguration AddBme280(this ISignalFConfiguration configuration, Action<IDeviceConfigurationBuilder> builder)
        {
            configuration.AddBme280Definition()
                         .AddDeviceConfiguration(builder);
        }
        public static ISignalFConfiguration AddBme280Definition(this ISignalFConfiguration configuration)
        {

            configuration.AddDeviceDefinition(builder =>
            {
                builder.SetName("Bme280Definition")
                    .UseTemplate("DefaultTemplate")
                    .AddSignalSourceDefinition("Temperature", EUnitType.Temperature)
                    .AddSignalSourceDefinition("Pressure", EUnitType.Pressure)
                    .AddSignalSourceDefinition("Humidity", EUnitType.None);
                    ;
            });

            return configuration;
        }
    }
}
