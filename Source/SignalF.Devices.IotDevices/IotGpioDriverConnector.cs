using System.Device.Gpio;
using Microsoft.Win32;
using System.Device.Gpio.Drivers;
using SignalF.Controller.Hardware.Channels.Gpio;

namespace SignalF.Devices.IotDevices;

public class IotGpioDriverConnector : GpioDriver
{
    private readonly IList<IGpioChannel> _channels;

    public IotGpioDriverConnector(IList<IGpioChannel> channels)
    {
        _channels = channels;
    }

    protected override int PinCount => 0;

    protected override int ConvertPinNumberToLogicalNumberingScheme(int pinNumber)
    {
        throw new NotImplementedException();
    }

    protected override void OpenPin(int pinNumber)
    {
        throw new NotImplementedException();
    }

    protected override void ClosePin(int pinNumber)
    {
        throw new NotImplementedException();
    }

    protected override void SetPinMode(int pinNumber, PinMode mode)
    {
        throw new NotImplementedException();
    }

    protected override PinMode GetPinMode(int pinNumber)
    {
        throw new NotImplementedException();
    }

    protected override bool IsPinModeSupported(int pinNumber, PinMode mode)
    {
        throw new NotImplementedException();
    }

    protected override PinValue Read(int pinNumber)
    {
        throw new NotImplementedException();
    }

    protected override void Write(int pinNumber, PinValue value)
    {
        throw new NotImplementedException();
    }

    protected override WaitForEventResult WaitForEvent(int pinNumber, PinEventTypes eventTypes, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    protected override void AddCallbackForPinValueChangedEvent(int pinNumber, PinEventTypes eventTypes, PinChangeEventHandler callback)
    {
        throw new NotImplementedException();
    }

    protected override void RemoveCallbackForPinValueChangedEvent(int pinNumber, PinChangeEventHandler callback)
    {
        throw new NotImplementedException();
    }
}
