using System;
using ParkingLot.Services;
using ParkingLot.Models;
using ParkingLot.Models.Enums;

namespace ParkingLot
{
    class Parking
    {
        public static void Main(string[] args)
        {
            ParkingSlots parkingSlots = new ParkingSlots();
            ParkingService parkingService= new ParkingService(parkingSlots);
            VehicleService vehicleService= new VehicleService(parkingSlots);
            parkingService.Initializer();
            bool stop = false;
            while(!stop)
            {
                int vehicleStatus = parkingService.EntryExit();
                if (vehicleStatus == Convert.ToInt32(VehicleStatus.entry))
                {
                    vehicleService.GetVehicleType();
                    vehicleService.CanVehicleEnter();
                }
                else if(vehicleStatus == Convert.ToInt32(VehicleStatus.exit))
                {
                    parkingService.Exit();

                }
                else if(vehicleStatus == Convert.ToInt32(VehicleStatus.stop))
                {
                    stop = true;
                }
            }
        }
    }  
}
