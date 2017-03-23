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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Beacons.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(BeaconsDroid))]
namespace Beacons.Droid
{
	public class BeaconsDroid : IBeacons
	{
		//static readonly int NOTIFICATION_ID = 123321;
		//BeaconManager beaconManager;
		//Region region;
		ObservableCollection<Beacon> Beacons = new ObservableCollection<Beacon>();
		public BeaconsDroid()
		{
		}

		public ObservableCollection<Beacon> getBeacons(string uuid)
		{
			Beacons.Add(new Beacon(123, 9899, uuid));
			Beacons.Add(new Beacon(765, 529, uuid));
			Beacons.Add(new Beacon(871, 965, uuid));
			Beacons.Add(new Beacon(421, 8739, uuid));
			return this.Beacons;
		}
	}
}
