using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreBluetooth;
using MonoTouch.CoreLocation;
using MonoTouch.CoreFoundation;

namespace iOSBeacon
{
	public partial class iOSBeaconViewController : UIViewController
	{
		CBPeripheralManager peripheralMgr;
		BTPeripheralDelegate peripheralDelegate;

		public iOSBeaconViewController () : base ("iOSBeaconViewController", null)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			var uuid = new NSUuid ("85A622A1-C5FE-4E75-ACF7-013656D418A7");
			var beaconId = "iOSBeacon";
			var beaconRegion = new CLBeaconRegion (uuid, beaconId) {
				NotifyEntryStateOnDisplay = true,
				NotifyOnEntry = true,
				NotifyOnExit = true
			};

			var peripheralData = beaconRegion.GetPeripheralData (new NSNumber (-59));

			peripheralDelegate = new BTPeripheralDelegate ();
			peripheralMgr = new CBPeripheralManager (peripheralDelegate, DispatchQueue.DefaultGlobalQueue);
			peripheralMgr.StartAdvertising (peripheralData);
		}

		class BTPeripheralDelegate : CBPeripheralManagerDelegate
		{
			public override void StateUpdated (CBPeripheralManager peripheral)
			{
				if (peripheral.State == CBPeripheralManagerState.PoweredOn) {
					Console.WriteLine ("iBeacon powered on");
				}
			}
		}
	}
}