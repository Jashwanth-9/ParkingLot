using System;
using ParkingLot.Services;
using ParkingLot.Models;
using ParkingLot.Models.Enums;
using ParkingLot.Services.Extensions;

namespace ParkingLot
{
    class Parking
    {
        public static void Main(string[] args)
        {
            ParkingSlots parkingSlots = new ParkingSlots();
            IParkingService parkingService= new ParkingService(parkingSlots);   // registration 
            IVehicleService vehicleService= new VehicleService(parkingSlots);
            parkingService.Initializer();
            bool stop = false;
            while(!stop)
            {
                int vehicleStatus = parkingService.EntryExit();
                if (vehicleStatus == VehicleStatus.entry.GetIndex())
                {
                    vehicleService.GetVehicleType();
                    vehicleService.CanVehicleEnter();
                }
                else if(vehicleStatus == VehicleStatus.exit.GetIndex())
                {
                    parkingService.Exit();

                }
                else if(vehicleStatus == VehicleStatus.stop.GetIndex())
                {
                    stop = true;
                }
            }
        }
    }  
}
