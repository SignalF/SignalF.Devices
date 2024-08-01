using Microsoft.Extensions.DependencyInjection;
using SignalF.Configuration;
using SignalF.Configuration.Devices;
using SignalF.Controller.Configuration;
using SignalF.Datamodel.Hardware;

namespace SignalF.Extensions.IotDevices.Bme280;

//public partial class Bme280Options : SignalFConfigurationOptions
//{
//}

//public interface
//    IBme280ConfigurationBuilder : IBme280ConfigurationBuilder<IBme280ConfigurationBuilder,
//        IDeviceConfiguration, Bme280Options>
//{
//}

//public interface
//    IBme280ConfigurationBuilder<out TBuilder, in TConfiguration, in TOptions> : IDeviceConfigurationBuilder<TBuilder, TConfiguration,
//        TOptions>
//    where TBuilder : IDeviceConfigurationBuilder<TBuilder, TConfiguration, TOptions>
//    where TConfiguration : IDeviceConfiguration
//    where TOptions : Bme280Options
//{
//}

//public class Bme280ConfigurationBuilder
//    : Bme280ConfigurationBuilder<Bme280ConfigurationBuilder, IBme280ConfigurationBuilder,
//          IDeviceConfiguration, Bme280Options>, IBme280ConfigurationBuilder
//{
//    protected override IBme280ConfigurationBuilder This => this;
//}

//public abstract class Bme280ConfigurationBuilder<TImpl, TBuilder, TConfiguration, TOptions>
//    : DeviceConfigurationBuilder<TImpl, TBuilder, TConfiguration, TOptions>
//      , IBme280ConfigurationBuilder<TBuilder, TConfiguration, TOptions>
//    where TBuilder : IBme280ConfigurationBuilder<TBuilder, TConfiguration, TOptions>
//    where TImpl : Bme280ConfigurationBuilder<TImpl, TBuilder, TConfiguration, TOptions>
//    where TConfiguration : IDeviceConfiguration
//    where TOptions : Bme280Options
//{
//}

//public static class Bme280Extensions
//{
//    public static ISignalFConfiguration AddBme280Configuration(this ISignalFConfiguration configuration,
//                                                               Action<IBme280ConfigurationBuilder> builder)
//    {
//        return configuration.AddDeviceConfiguration<IBme280ConfigurationBuilder, Bme280Options>(builder);
//    }

//    public static ISignalFConfiguration AddBme280Configuration<TType>(this ISignalFConfiguration configuration
//                                                                      , Action<IBme280ConfigurationBuilder> builder)
//        where TType : Bme280
//    {
//        return configuration.AddDeviceConfiguration<IBme280ConfigurationBuilder, Bme280Options, TType>(builder);
//    }

//    public static IServiceCollection AddBme280(this IServiceCollection services)
//    {
//        return services.AddTransient<Bme280>();
//    }

//    //public static ISignalConfiguration AddBme280(this ISignalFConfiguration configuration, Action<IBme280ConfigurationBuilder> builder)
//    //{
//    //    return configuration.AddDeviceConfiguration(builder);
//    //}

//    //public static ISignalFConfiguration AddBme280Definition(this ISignalFConfiguration configuration)
//    //{

//    //    configuration.AddDeviceDefinition(builder =>
//    //    {
//    //        builder.SetName("Bme280Definition")
//    //            .UseTemplate("DefaultTemplate")
//    //            .AddSignalSourceDefinition("Temperature", EUnitType.Temperature)
//    //            .AddSignalSourceDefinition("Pressure", EUnitType.Pressure)
//    //            .AddSignalSourceDefinition("Humidity", EUnitType.None);
//    //            ;
//    //    });

//    //    return configuration;
//    //}
//}
