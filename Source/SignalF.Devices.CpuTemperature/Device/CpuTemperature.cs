// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

// Ported from https://github.com/dotnet/iot/blob/main/src/devices/CpuTemperature/CpuTemperature.cs

using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using Scotec.Math.Units;

namespace SignalF.Devices.CpuTemperature.Device;

/// <summary>
///     CPU temperature.
/// </summary>
[SupportedOSPlatform("linux")]
public sealed class CpuTemperature
{
    private bool _checkedIfAvailable;

    /// <summary>
    ///     Creates an instance of the CpuTemperature class
    /// </summary>
    public CpuTemperature()
    {
        IsAvailable = false;
        _checkedIfAvailable = false;

        CheckAvailable();
    }

    /// <summary>
    ///     Gets CPU temperature
    /// </summary>
    public Temperature Temperature => new(Temperature.Units.DegreeCelsius, ReadTemperatureUnix());

    /// <summary>
    ///     Is CPU temperature available
    /// </summary>
    public bool IsAvailable { get; private set; }

    public void Dispose()
    {
        // Any further calls will fail
        IsAvailable = false;
    }

    private bool CheckAvailable()
    {
        if (!_checkedIfAvailable)
        {
            _checkedIfAvailable = true;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) &&
                File.Exists("/sys/class/thermal/thermal_zone0/temp"))
            {
                IsAvailable = true;
            }
        }

        return IsAvailable;
    }

    /// <summary>
    ///     Returns all known temperature sensor values.
    /// </summary>
    /// <returns>A list of name/value pairs for temperature sensors</returns>
    public List<(string Sensor, Temperature Temperature)> ReadTemperatures()
    {
        var ret = new List<(string, Temperature)>
            { ("CPU", new Temperature(Temperature.Units.DegreeCelsius, ReadTemperatureUnix())) };
        return ret;
    }

    private double ReadTemperatureUnix()
    {
        var temperature = double.NaN;

        if (CheckAvailable())
        {
            using var fileStream =
                new FileStream("/sys/class/thermal/thermal_zone0/temp", FileMode.Open, FileAccess.Read);
            if (fileStream is null)
            {
                throw new Exception("Cannot read CPU temperature");
            }

            using var reader = new StreamReader(fileStream);
            var data = reader.ReadLine();
            if (data is { Length: > 0 } &&
                int.TryParse(data, out var temp))
            {
                temperature = temp / 1000F;
            }
        }

        return temperature;
    }
}