using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Iot.Device.Bmxx80;
using Microsoft.Extensions.Logging;
using SignalF.Controller.Hardware.Channels;
using SignalF.Controller.Hardware.Channels.I2c;
using SignalF.Controller.Signals;
using SignalF.Datamodel.Hardware;

namespace SignalF.Devices.Bmxx80
{
    public class I2cIotDeviceBinding<TDevice> : I2cDevice<IDeviceConfiguration> where TDevice : Bmxx80Base
    {
        public I2cIotDeviceBinding(ISignalHub signalHub, ILogger<I2cDevice<IDeviceConfiguration>> logger) : base(signalHub, logger)
        {
        }

        public override void AssignChannels(IList<IChannel> channels)
        {
            throw new NotImplementedException();
        }
    }
}
