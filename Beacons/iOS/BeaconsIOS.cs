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
using System.Collections.ObjectModel;
using CoreLocation;
using Foundation;
using CoreFoundation;
using CoreBluetooth;
using UIKit;
using System.Collections.Generic;
using MapKit;

[assembly: Xamarin.Forms.Dependency(typeof(Beacons.iOS.BeaconsIOS))]
namespace Beacons.iOS
{
	public class BeaconsIOS : UIViewController ,IBeacons, ICLLocationManagerDelegate
	{
		CLBeaconRegion region;
		ObservableCollection<Beacon> beacons = new ObservableCollection<Beacon>();
		CLLocationManager locatoinManager;
		CBCentralManager centralManager;
		string IdRegion = "LIT";

		public BeaconsIOS()
		{
			centralManager = new CBCentralManager(DispatchQueue.DefaultGlobalQueue);
			centralManager.DiscoveredPeripheral += ( sender,  e) => Console.WriteLine("Beacon encontrado");
			centralManager.ScanForPeripherals(serviceUuid: null );
		}

		public ObservableCollection<Beacon> getBeacons(string uuid)
		{
			var regionUUID = new NSUuid(uuid);
			region = new CLBeaconRegion(regionUUID,IdRegion);
			region.NotifyEntryStateOnDisplay = true;
			region.NotifyOnExit = true;
			region.NotifyOnEntry = true;

			locatoinManager = new CLLocationManager();
			locatoinManager.RequestWhenInUseAuthorization();

			locatoinManager.DidRangeBeacons += (sender, e) => {
				if (e.Beacons.Length > 0 ) {
					foreach (var beacon in e.Beacons) {
						if (beacon.Accuracy > 0){
							var MyBeacon = new Beacon((ushort)beacon.Minor, (ushort)beacon.Major, beacon.ProximityUuid.ToString());
							MyBeacon.setAccuracy(beacon.Accuracy);
							beacons.Clear();
							beacons.Add(MyBeacon);
						}
					}
				}			
			};
			locatoinManager.StartRangingBeacons(region);
			return this.beacons;
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			Console.WriteLine("pasou por aqui");
		}

		private void isScanning()
		{
			Console.WriteLine(centralManager.IsScanning ? "scaniando" : "parado");
		}

	}

}
