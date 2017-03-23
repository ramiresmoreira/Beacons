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
using System.Timers;
using System.Collections.Generic;
[assembly: Xamarin.Forms.Dependency(typeof(Beacons.iOS.BeaconsIOS))]
namespace Beacons.iOS
{
	public class BeaconsIOS : UIViewController ,IBeacons
	{
		//CLBeaconManager beaconManager;
		CLBeaconRegion region;
		ObservableCollection<Beacon> beacons = new ObservableCollection<Beacon>();
		//private List<MyBeacon> MyprivateBeacons = new List<MyBeacon>();

		MYCBPeripheralManagerDelegate peripheralDelegate;
		CBPeripheralManager peripheralManager;
		CLLocationManager locatoinManager;
		string IdRegion = "LIT";
		string uuid = string.Empty;

		public BeaconsIOS()
		{
			peripheralDelegate = new MYCBPeripheralManagerDelegate();
			peripheralManager = new CBPeripheralManager(peripheralDelegate,DispatchQueue.DefaultGlobalQueue);
			var options = new NSDictionary();
			peripheralManager.StartAdvertising(options);
		}

		public ObservableCollection<Beacon> getBeacons(string uuid)
		{
			this.uuid = uuid;
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
				if (e.Beacons.Length > 0 ) {
					beacons.Clear();
					foreach (var beacon in e.Beacons) {
						ushort minor = (ushort) beacon.Minor;
						ushort major = (ushort) beacon.Major;
						var MyBeacon = new Beacon(minor, major, beacon.ProximityUuid.ToString());
						MyBeacon.setAccuracy(beacon.Accuracy);
						beacons.Add(MyBeacon);
					}
				}			
			};

			locatoinManager.RegionEntered += (sender, e) => SendEnteredNotfication();
			locatoinManager.RegionLeft += (sender, e) => SendExitedNotfication();
			locatoinManager.StartRangingBeacons(region);
			locatoinManager.StartMonitoring(region);

			return this.beacons;
		}


		private void SendEnteredNotfication() 
		{
			Console.WriteLine("Beacons na regiao");
		} 

		private void SendExitedNotfication()
		{
			Console.WriteLine("Sem Beacons na regiao");
		}


		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			//Timer timer = new Timer(2*1000);
			//timer.Elapsed += (sender, e) =>
			//{
			//	beacons.Clear();
			//	getBeacons(uuid);
			//};
			getBeacons(uuid);
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


	public class MyBeacon : CLBeacon {

		public override bool Equals(object obj)
		{
			CLBeacon other = (CLBeacon) obj;
			return (this.Minor == other.Minor) && 
					(this.Major == other.Major) && 
					(this.ProximityUuid.Equals(other.Proximity));
		}

		public override int GetHashCode()
		{
			return Minor.GetHashCode() + Major.GetHashCode() + Proximity.GetHashCode();
		}
	}

}
