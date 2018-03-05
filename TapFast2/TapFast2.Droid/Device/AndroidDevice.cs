// ***********************************************************************
// Assembly         : XLabs.Platform.Droid
// Author           : XLabs Team
// Created          : 12-27-2015
//
// Last Modified By : XLabs Team
// Last Modified On : 01-04-2016
// ***********************************************************************
// <copyright file="AndroidDevice.cs" company="XLabs Team">
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
using System.IO;
using System.IO.IsolatedStorage;
using System.Threading.Tasks;
using Android.App;
using Android.Bluetooth;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Telephony;
using Android.Util;
using Android.Views;
using Java.IO;
using Java.Util;
using Java.Util.Concurrent;
using TapFast2;
using TapFast2.Enums;

namespace TapFast2.Droid.Device
{
    /// <summary>
    /// Android device implements <see cref="IDevice"/>.
    /// </summary>
    public class AndroidDevice : IDevice
    {
        private static readonly long DeviceTotalMemory = GetTotalMemory();
        private static IDevice currentDevice;


        /// <summary>
        /// Creates a default instance of <see cref="AndroidDevice"/>.
        /// </summary>
        public AndroidDevice()
        {

            this.Display = new Display();

            this.Manufacturer = Build.Manufacturer;
            this.Name = Build.Model;
            this.HardwareVersion = Build.Hardware;
            this.FirmwareVersion = Build.VERSION.Release;

        }

        /// <summary>
        /// Gets the current device.
        /// </summary>
        /// <value>
        /// The current device.
        /// </value>
        //public static IDevice CurrentDevice
        //{
        //    get { return currentDevice ?? (currentDevice = new AndroidDevice()); }
        //    set { currentDevice = value; }
        //}

        /// <summary>
        /// Gets Unique Id for the device.
        /// </summary>
        /// <value>
        /// The id for the device.
        /// </value>
        public virtual string Id
        {
            // TODO: Verify what is the best combination of Unique Id for Android
            get
            {
                return Build.Serial;
            }
        }

        /// <summary>
        /// Gets the display information for the device.
        /// </summary>
        /// <value>
        /// The display.
        /// </value>
        public IDisplay Display { get; private set; }



        /// <summary>
        /// Gets the name of the device.
        /// </summary>
        /// <value>
        /// The name of the device.
        /// </value>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the firmware version.
        /// </summary>
        /// <value>
        /// The firmware version.
        /// </value>
        public string FirmwareVersion { get; private set; }

        /// <summary>
        /// Gets the hardware version.
        /// </summary>
        /// <value>
        /// The hardware version.
        /// </value>
        public string HardwareVersion { get; private set; }

        /// <summary>
        /// Gets the manufacturer.
        /// </summary>
        /// <value>
        /// The manufacturer.
        /// </value>
        public string Manufacturer { get; private set; }

        /// <summary>
        /// Gets the language code.
        /// </summary>
        /// <value>The language code.</value>
        public string LanguageCode
        {
            get { return Locale.Default.Language; }
        }

        /// <summary>
        /// Gets the time zone offset.
        /// </summary>
        /// <value>The time zone offset.</value>
        public double TimeZoneOffset
        {
            get
            {
                using (var calendar = new GregorianCalendar())
                {
                    return TimeUnit.Hours.Convert(calendar.TimeZone.RawOffset, TimeUnit.Milliseconds) / 3600;
                }
            }
        }

        /// <summary>
        /// Gets the time zone.
        /// </summary>
        /// <value>The time zone.</value>
        public string TimeZone
        {
            get { return Java.Util.TimeZone.Default.ID; }
        }

        /// <summary>
        /// Gets the orientation.
        /// </summary>
        /// <value>The orientation.</value>
        public Orientation Orientation
        {
            get
            {
                using (var wm = Android.App.Application.Context.GetSystemService(Context.WindowService).JavaCast<IWindowManager>())
                using (var dm = new DisplayMetrics())
                {
                    var rotation = wm.DefaultDisplay.Rotation;
                    wm.DefaultDisplay.GetMetrics(dm);

                    var width = dm.WidthPixels;
                    var height = dm.HeightPixels;

                    if (height > width && (rotation == SurfaceOrientation.Rotation0 || rotation == SurfaceOrientation.Rotation180)  ||
                        width > height && (rotation == SurfaceOrientation.Rotation90 || rotation == SurfaceOrientation.Rotation270))
                    {
                        switch (rotation)
                        {
                            case SurfaceOrientation.Rotation0:
                                return Orientation.Portrait & Orientation.PortraitUp;
                            case SurfaceOrientation.Rotation90:
                                return Orientation.Landscape & Orientation.LandscapeLeft;
                            case SurfaceOrientation.Rotation180:
                                return Orientation.Portrait & Orientation.PortraitDown;
                            case SurfaceOrientation.Rotation270:
                                return Orientation.Landscape & Orientation.LandscapeRight;
                            default:
                                return Orientation.None;
                        }
                    }

                    switch (rotation)
                    {
                        case SurfaceOrientation.Rotation0:
                            return Orientation.Landscape & Orientation.LandscapeLeft;
                        case SurfaceOrientation.Rotation90:
                            return Orientation.Portrait & Orientation.PortraitUp;
                        case SurfaceOrientation.Rotation180:
                            return Orientation.Landscape & Orientation.LandscapeRight;
                        case SurfaceOrientation.Rotation270:
                            return Orientation.Portrait & Orientation.PortraitDown;
                        default:
                            return Orientation.None;
                    }
                }
            }
        }

         /// <summary>
        /// Gets the total memory in bytes.
        /// </summary>
        /// <value>The total memory in bytes.</value>
        public long TotalMemory
        {
            get
            {
                return DeviceTotalMemory;
            }
        }

        private static long GetTotalMemory()
        {

            using (var reader = new RandomAccessFile("/proc/meminfo", "r"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Log.Debug("Memory", line);
                }
            }

            using (var reader = new RandomAccessFile("/proc/meminfo", "r"))
            {
                var line = reader.ReadLine(); // first line --> MemTotal: xxxxxx kB
                var split = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                return Convert.ToInt64(split[1]) * 1024;
            }
        }
    }
}