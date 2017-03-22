//
//  BeaconsIOS.cs
//
//  Author:
//       ramiresmoreira <>
//
//  Copyright (c) 2017 
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Collections.Generic;
using CoreLocation;
using Foundation;
using CoreFoundation;
using CoreBluetooth;
[assembly: Xamarin.Forms.Dependency(typeof(Beacons.iOS.BeaconsIOS))]
namespace Beacons.iOS
{
	public class BeaconsIOS : IBeacons
	{
		//CLBeaconManager beaconManager;
		CLBeaconRegion region;
		List<Beacon> beacons = new List<Beacon>();
		MYCBPeripheralManagerDelegate peripheralDelegate;
		CBPeripheralManager peripheralManager;
		CLLocationManager locatoinManager;
		string uuid = "B9407F30-F5F8-466E-AFF9-25556B57FE6D"; //default estimote uuid
		string IdRegion = "LIT";


		public BeaconsIOS()
		{
			peripheralDelegate = new MYCBPeripheralManagerDelegate();
			peripheralManager = new CBPeripheralManager(peripheralDelegate,DispatchQueue.DefaultGlobalQueue);
		}

		public List<Beacon> carregaBeacons()
		{
			var regionUUID = new NSUuid(uuid);
			region = new CLBeaconRegion(regionUUID,IdRegion);
			region.NotifyEntryStateOnDisplay = true;
			region.NotifyOnExit = true;
			region.NotifyOnEntry = true;

			locatoinManager = new CLLocationManager();
			locatoinManager.RequestWhenInUseAuthorization();

			locatoinManager.RegionEntered += (sender, e) => {
				Console.WriteLine("existem beacons na região");
			};

			locatoinManager.DidRangeBeacons += (sender, e) => {
				if (e.Beacons.Length > 0) {
					foreach (var beacon in e.Beacons) {
						var MyBeacon = new Beacon(beacon.Minor.Int16Value, beacon.Major.Int16Value, beacon.ProximityUuid.ToString());
						beacons.Add(MyBeacon);
					}
				}			
			};

			return beacons;
		}
	}

	public class MYCBPeripheralManagerDelegate : CBPeripheralManagerDelegate
	{
		public override void StateUpdated(CBPeripheralManager peripheral)
		{
			if (peripheral.State == CBPeripheralManagerState.PoweredOn)
				Console.WriteLine("Power ON");
			if (peripheral.State == CBPeripheralManagerState.PoweredOff)
				Console.WriteLine("Power OFF");
		}
	}
}
