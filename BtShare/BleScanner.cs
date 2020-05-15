using System;
using CoreBluetooth;
using Foundation;

namespace BtShare
{
    public class BleScanner : NSObject
    {
        static CBUUID serviceUUID = CBUUID.FromString("UUID");
        //static let log = OSLog(subsystem: Bundle.main.bundleIdentifier!, category: "Scanner")

        private CBCentralManager centralManager;
    //private Timer scanningTimer

    override Init()
        {
            super.init()
        centralManager = CBCentralManager(delegate: self, queue: nil)
    }

        func startScanning()
        {
            scanningTimer = Timer.scheduledTimer(withTimeInterval: TimeInterval(20), repeats: false, block: {
                (_) in
            self.stopScanning()
            })
        centralManager.scanForPeripherals(withServices: [BleScanner.serviceUUID], options: nil)
        }

        func stopScanning()
        {
            centralManager.stopScan()
        }
    }

    extension BleScanner : CBCentralManagerDelegate {

    func centralManagerDidUpdateState(_ central: CBCentralManager)
    {
        if centralManager.state == .poweredOn {
            startScanning()
        }
    }

    func centralManager(_ central: CBCentralManager, didDiscover peripheral: CBPeripheral, advertisementData: [String: Any], rssi RSSI: NSNumber)
    {
        for (key, value) in advertisementData {
            if key == CBAdvertisementDataServiceDataKey {
                let serviceData = value as! [CBUUID: NSData]
                for (uuid, data) in serviceData {
                    os_log("Advertisement data: %{public}s: %{public}s", log: BleScanner.log, type: .info, uuid.uuidString, data.debugDescription)
                }
            }
        }
    }
}
