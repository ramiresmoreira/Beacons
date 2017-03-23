//
//  BeaconsPage.xaml.cs
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
using System.Collections.ObjectModel;


namespace Beacons
{
	public partial class BeaconsPage : ContentPage
	{
		public ObservableCollection<Beacon> Beacons {get;set;}

		string uuid = "B9407F30-F5F8-466E-AFF9-25556B57FE6D";

		public BeaconsPage()
		{
			InitializeComponent();
			Beacons = DependencyService.Get<IBeacons>().getBeacons(uuid);

			BindingContext = this;

		}
	}
}
