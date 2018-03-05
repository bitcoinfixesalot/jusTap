// ***********************************************************************
// Assembly         : XLabs.Platform.WP81
// Author           : XLabs Team
// Created          : 12-27-2015
// 
// Last Modified By : XLabs Team
// Last Modified On : 01-04-2016
// ***********************************************************************
// <copyright file="WindowsPhoneDevice.cs" company="XLabs Team">
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
using Windows.Security.ExchangeActiveSyncProvisioning;
using Windows.System;
using Windows.System.Profile;


namespace TapFast2.WP81.Device
{
    /// <summary>
    /// Windows phone device.
    /// </summary>
    public class WindowsPhoneDevice : IDevice
    {
        /// <summary>
        /// The current device.
        /// </summary>
        private static IDevice _currentDevice;

        /// <summary>
        /// The _hardware token
        /// </summary>
        private HardwareToken _hardwareToken;

        private EasClientDeviceInformation _clientDeviceInformation;

        /// <summary>
        /// Creates an instance of <see cref="WindowsPhoneDevice" />.
        /// </summary>
        public WindowsPhoneDevice()
        {			
            this.Display = new Display();


        
            //if (DeviceCapabilities.IsEnabled(DeviceCapabilities.Capability.IdCapMicrophone))
            //{
            //	if (XnaMicrophone.IsAvailable)
            //	{
            //		this.Microphone = new XnaMicrophone();
            //	}
            //}

            //if (DeviceCapabilities.IsEnabled(DeviceCapabilities.Capability.ID_CAP_MEDIALIB_PHOTO))
            //{
            //    MediaPicker = new MediaPicker();
            //}
        }

        /// <summary>
        /// Gets the current device.
        /// </summary>
        /// <value>The current device.</value>
        public static IDevice CurrentDevice
        {
            get
            {
                return _currentDevice ?? (_currentDevice = new WindowsPhoneDevice());
            }
            set
            {
                _currentDevice = value;
            }
        }

        #region IDevice Members

        /// <summary>
        /// Gets Unique Id for the device.
        /// </summary>
        /// <value>The id for the device.</value>
        /// <exception cref="UnauthorizedAccessException">Application has no access to device identity. To enable access consider enabling ID_CAP_IDENTITY_DEVICE on app manifest.</exception>
        /// <remarks>Requires the application to check ID_CAP_IDENTITY_DEVICE on application permissions.</remarks>
        public virtual string Id
        {
            get { return _hardwareToken != null ? _hardwareToken.Id.ToString() : (_hardwareToken = HardwareIdentification.GetPackageSpecificToken(null)).Id.ToString(); }
        }

        /// <summary>
        /// Gets the display.
        /// </summary>
        /// <value>The display.</value>
        public IDisplay Display { get; private set; }
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name of the device.</value>
        public string Name
        {
            get
            {
                return (_clientDeviceInformation ?? (_clientDeviceInformation = new EasClientDeviceInformation())).FriendlyName;
            }
        }

        /// <summary>
        /// Gets the firmware version.
        /// </summary>
        /// <value>The firmware version.</value>
        public string FirmwareVersion
        {
            get
            {
                return (_clientDeviceInformation ?? (_clientDeviceInformation = new EasClientDeviceInformation())).SystemFirmwareVersion;
            }
        }

        /// <summary>
        /// Gets the hardware version.
        /// </summary>
        /// <value>The hardware version.</value>
        public string HardwareVersion
        {
            get
            {
                return (_clientDeviceInformation ?? (_clientDeviceInformation = new EasClientDeviceInformation())).SystemHardwareVersion;
            }
        }

        /// <summary>
        /// Gets the manufacturer.
        /// </summary>
        /// <value>The manufacturer.</value>
        public string Manufacturer
        {
            get
            {
                return (_clientDeviceInformation ?? (_clientDeviceInformation = new EasClientDeviceInformation())).SystemManufacturer;
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
                //return DeviceStatus.DeviceTotalMemory;
                throw new System.NotImplementedException();
            }
        }

        /// <summary>
        /// Gets the time zone offset.
        /// </summary>
        /// <value>The time zone offset.</value>
        public double TimeZoneOffset
        {
            get
            {
                return TimeZoneInfo.Local.GetUtcOffset(DateTime.Now).TotalMinutes / 60;
            }
        }

        /// <summary>
        /// Gets the time zone.
        /// </summary>
        /// <value>The time zone.</value>
        public string TimeZone
        {
            get
            {
                return TimeZoneInfo.Local.DisplayName;
            }
        }

        /// <summary>
        /// Gets the language code.
        /// </summary>
        /// <value>The language code.</value>
        public string LanguageCode
        {
            get
            {
                return System.Globalization.CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            }
        }

        /// <summary>
        /// Gets the orientation.
        /// </summary>
        /// <value>The orientation.</value>
        public Orientation Orientation
        {
            get
            {
                switch (Windows.Graphics.Display.DisplayInformation.GetForCurrentView().CurrentOrientation)
                {

                    case Windows.Graphics.Display.DisplayOrientations.Landscape:
                        return Orientation.Landscape & Orientation.LandscapeLeft;
                    case Windows.Graphics.Display.DisplayOrientations.Portrait:
                        return Orientation.Portrait & Orientation.PortraitUp;
                    case Windows.Graphics.Display.DisplayOrientations.PortraitFlipped:
                        return Orientation.Portrait & Orientation.PortraitDown;
                    case Windows.Graphics.Display.DisplayOrientations.LandscapeFlipped:
                        return Orientation.Landscape & Orientation.LandscapeRight;
                    default:
                        return Orientation.None;
                }
            }
        }

        /// <summary>
        /// Starts the default app associated with the URI for the specified URI.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns>The launch operation.</returns>
        public async Task<bool> LaunchUriAsync(Uri uri)
        {
            return await Launcher.LaunchUriAsync(uri);
        }

        #endregion
    }
}

