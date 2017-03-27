using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace Beacons
{
	public interface IBeacons
	{

		ObservableCollection<Beacon> getBeacons(string uuid);
	}


}