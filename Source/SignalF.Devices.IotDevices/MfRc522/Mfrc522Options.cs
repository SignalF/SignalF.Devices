using SignalF.Configuration.Devices;

namespace SignalF.Devices.IotDevices.MfRc522;

public class Mfrc522Options : DeviceOptions
{
    public BusType BusType { get; set; }
}
