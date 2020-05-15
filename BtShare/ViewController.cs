using Foundation;
using System;
using UIKit;
using CoreBluetooth;

namespace BtShare
{
    public partial class ViewController : UIViewController, ICBCentralManagerDelegate
    {
        CBCentralManager cBCentralManager;
        CBPeripheralManager cbPeriphMang = new CBPeripheralManager();

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            cBCentralManager = new CBCentralManager(this, null);
            // Perform any additional setup after loading the view, typically from a nib.


            //A example of advertising
            //https://stackoverflow.com/questions/47716181/how-to-add-services-to-cbperipheralmanager-correctly-in-xamarin

            //Example of getting android service data
            //https://stackoverflow.com/questions/57805222/ble-send-advertise-data-to-ios-from-android
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        public void UpdatedState(CBCentralManager central)
        {
            
        }

        [Export("centralManager:didDiscoverPeripheral:advertisementData:RSSI:")]
        public void DiscoveredPeripheral(CBCentralManager central, CBPeripheral peripheral, NSDictionary advertisementData, NSNumber RSSI)
        {
            throw new System.NotImplementedException();
        }

        public void AdvertiseData()
        {
            var uui = new CBUUID[] { CBUUID.FromString("E20A39F4-73F5-4BC4-A12F-17D1AD07A961") };
            var nsArray = NSArray.FromObjects(uui);
            var nsObject = NSObject.FromObject(nsArray);

            var manufacturerDataBytes = new byte[6] { 5, 255, 76, 0, 25, 35 };

            var advertisementData = new NSDictionary(
                 CBAdvertisement.DataLocalNameKey, "id1",
                 CBAdvertisement.DataServiceUUIDsKey, nsObject,
                 CBAdvertisement.DataManufacturerDataKey, NSData.FromArray(manufacturerDataBytes),
                 CBAdvertisement.DataServiceDataKey, "Sent data");

            if (cbPeriphMang.Advertising) cbPeriphMang.StopAdvertising();

            cbPeriphMang.StartAdvertising(advertisementData);
        }
    }
}