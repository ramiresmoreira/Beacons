//
//  BeaconsDroid.cs
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
using Beacons.Droid;
using EstimoteSdk;
using Java.Util.Concurrent;
using Android.App;

[assembly: Xamarin.Forms.Dependency(typeof(BeaconsDroid))]

namespace Beacons.Droid
                 
{
	public class BeaconsDroid : Activity, IBeacons, BeaconManager.IServiceReadyCallback
	{
		static readonly int NOTIFICATION_ID = 123321;
		BeaconManager beaconManager;
		Region region;
		new 
		ObservableCollection<Beacon> Beacons = new ObservableCollection<Beacon>();
		//List<Beacon> Beacons = new List<Beacon>();

		public BeaconsDroid()
		{
		}

		public ObservableCollection<Beacon> getBeacons(string uuid)
		{
			var beacon = new Beacon(123, 9899, uuid);
			beacon.setAccuracy(1.2);
			var beacon2 = new Beacon(33, 785, uuid);
			beacon2.setAccuracy(0.6);
			Beacons.Add(beacon);
			Beacons.Add(beacon2);
			return this.Beacons;
		}


		//private void googleBeacons() {

		//	BluetoothAdapter adapter;
		//	bool scanning;
		//	Handler mHandler = new Handler();
		//	long SCAN_PERIOD = 10000;
		//	mHandler.PostDelayed(()=> {
		//		scanning = false;
		//		adapter.StopLeScan();
		//	} , SCAN_PERIOD);
		//}


		protected override void OnCreate(Android.OS.Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.notification_media_action);

			//region = 
			beaconManager = new BeaconManager(this);
			beaconManager.SetBackgroundScanPeriod(TimeUnit.Seconds.ToMillis(1), 0);
			beaconManager.EnteredRegion += (sender, e) =>
			{
				Console.WriteLine(e.Beacons);
			};
			beaconManager.ExitedRegion += (sender, e) =>
			{
				Console.WriteLine("saiu da regiao");
			};
		}

		void BeaconManager.IServiceReadyCallback.OnServiceReady()
		{
			//throw new NotImplementedException();
			beaconManager.StartMonitoring(region);
		}

		protected override void OnResume()
		{
			base.OnResume();
			beaconManager.Connect(this);
		}

		protected override void OnDestroy()
		{
			// Make sure we disconnect from the Estimote.
			beaconManager.Disconnect();
			base.OnDestroy();
		}

	}
}
