using System;

namespace DataPusherWorker
{
    public static class MockDeviceDataService
    {
      internal static DeviceData GetMockData()
        {
            var deviceData = new DeviceData();
            int p = new Random().Next(1, 3);
            deviceData.CommunicatedTime = DateTime.UtcNow;
            deviceData.InkPressure = ((double)1 / p) + ((double)new Random().Next(1, 9) / 100); //new Random().NextDouble();
            deviceData.InkResidualQuantityLevel = new Random().Next(0, 7);
            deviceData.MakeupResidualQuantityLevel = new Random().Next(0, 7);
            deviceData.InkViscosity = new Random().Next(80, 110);
            deviceData.MakeupConsumption = p + 2;
            deviceData.Name = "MockDevice-" + p;
            deviceData.PrintCount = DateTime.Now.Ticks;
            deviceData.SerialNumber = deviceData.Name.GetHashCode(); //06321804 + p;
            deviceData.TypeName = "Model-D160";
            deviceData.AmbientTemperature = new Random().Next(15, 20);
            deviceData.InkOperatingTime = 5687;
            deviceData.DeflectionVoltage = 3.8;
            deviceData.InkConsumption = 8753;
            deviceData.InkFilterUseTime = 5999;
            deviceData.RecoveryFilterUseTime = 5100;
            deviceData.MgvFilterUseTime = 4246;
            deviceData.PumpUseTime = 12100;
            deviceData.HeatingUnitUseTime = 12100;
            deviceData.Mv1UseTime = 5799;

            deviceData.OperationStatus = "Stop";
            deviceData.InkOperatingTime = 308;

            deviceData.ExcitationFrequency = 68.9;
            deviceData.InkMakeupInkType = 18;
            deviceData.CommunicationConnectionStatus = 0;
            deviceData.CirculationFilterUseTime = 311;
            deviceData.CumulativeOperationTime = 310;
            deviceData.WarningStatus = "Stop";
            deviceData.ExcitationVRef = 8;
            deviceData.MakeupFilterUseTime = 311;
            deviceData.ReceptionStatus = 0;
            deviceData.InkAlarmTime = 0;
            deviceData.RAirFilterUseTime = 311;
            deviceData.AirFilterUseTime = 311;

            deviceData.Mv1UseTime = 5799 + 10 * p;
            deviceData.Mv2UseTime = 5799 + 10 * p;
            deviceData.Mv3UseTime = 5799 + 10 * p;
            deviceData.Mv4UseTime = 5799 + 10 * p;
            deviceData.Mv5UseTime = 5799 + 10 * p;
            deviceData.Mv6UseTime = 5799 + 10 * p;
            deviceData.Mv7UseTime = 5799 + 10 * p;
            deviceData.Mv8UseTime = 5799 + 10 * p;
            deviceData.Mv9UseTime = 5799 + 10 * p;

            deviceData.InkName = "1072K";

            return deviceData;
        }
    }
}
