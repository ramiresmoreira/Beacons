using System.Collections.ObjectModel;


namespace Beacons
{
	public interface IBeacons
	{

		ObservableCollection<Beacon> getBeacons(string uuid);
	}


}