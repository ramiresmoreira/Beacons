//
//  Beacon.cs
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
using Xamarin.Forms;
namespace Beacons
{
	public class Beacon : ViewCell
	{
		

		public ushort Minor { get; set; }
		public ushort Major { get; set; }
		public string uuid { get; set; }
		public double Accuracy {get;set;}

		public string precisao { 
			get { return $"{Accuracy:N1}m"; }
		}


		public Beacon(ushort minor, ushort major, string uuid)
		{
			
			this.Minor = minor;
			this.Major = major;
			this.uuid = uuid;
		}

		public override bool Equals(object obj)
		{
			Beacon other = (Beacon) obj;
			return (Minor == other.Minor) && (Major == other.Major);
		}

		public void setAccuracy(double accuracy)
		{
			this.Accuracy = accuracy;
		}

		public override int GetHashCode()
		{
			return Minor.GetHashCode() + Major.GetHashCode() + uuid.GetHashCode() + Accuracy.GetHashCode();
		}
}
}
