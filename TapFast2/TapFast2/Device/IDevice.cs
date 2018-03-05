// ***********************************************************************
// Assembly         : XLabs.Platform
// Author           : XLabs Team
// Created          : 12-27-2015
// 
// Last Modified By : XLabs Team
// Last Modified On : 01-04-2016
// ***********************************************************************
// <copyright file="IDevice.cs" company="XLabs Team">
//     Copyright (c) XLabs Team. All rights reserved.
// </copyright>
// <summary>
//       This project is licensed under the Apache 2.0 license
//       https://github.com/XLabs/Xamarin-Forms-Labs/blob/master/LICENSE
//       
//       XLabs is a open source project that aims to provide a powerfull and cross 
//       platform set of controls tailored to work with Xamarin Forms.
// </summary>
// ***********************************************************************
// 

using System;
using System.Threading.Tasks;
using TapFast2.Enums;

namespace TapFast2
{
    /// <summary>
    /// Abstracted device interface.
    /// </summary>
    public interface IDevice
    {
        /// <summary>
        /// Gets Unique Id for the device.
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Gets the display information for the device.
        /// </summary>
        IDisplay Display { get; }


        /// <summary>
        /// Gets the name of the device.
        /// </summary>
        /// <value>The name of the device.</value>
        string Name { get; }

        /// <summary>
        /// Gets the firmware version.
        /// </summary>
        string FirmwareVersion { get; }

        /// <summary>
        /// Gets the hardware version.
        /// </summary>
        string HardwareVersion { get; }

        /// <summary>
        /// Gets the manufacturer.
        /// </summary>
        string Manufacturer { get; }

        /// <summary>
        /// Gets the total memory in bytes.
        /// </summary>
        long TotalMemory { get; }

        /// <summary>
        /// Gets the ISO Language Code
        /// </summary>
        string LanguageCode { get; }

        /// <summary>
        /// Gets the UTC offset
        /// </summary>
        double TimeZoneOffset { get; }

        /// <summary>
        /// Gets the timezone name
        /// </summary>
        string TimeZone { get; }
        
        /// <summary>
        /// Gets the <see cref="Orientation"/> of the device.
        /// </summary>
        Orientation Orientation { get; }

    }
}

