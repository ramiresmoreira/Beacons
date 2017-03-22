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
using Beacons.Droid;
using EstimoteSdk;

[assembly: Xamarin.Forms.Dependency(typeof(BeaconsDroid))]
namespace Beacons.Droid
{
	public class BeaconsDroid : IBeacons
	{
		//static readonly int NOTIFICATION_ID = 123321;
		//BeaconManager beaconManager;
		//Region region;

		public BeaconsDroid()
		{
		}

		public List<Beacon> carregaBeacons()
		{
			throw new NotImplementedException();
		}
	}
}
