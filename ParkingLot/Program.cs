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
            ParkingSlots slots = new ParkingSlots();
            ParkingService parkingService= new ParkingService(slots);
            parkingService.Initializer();
            bool stop = false;
            while(!stop)
            {
                int vehicleStatus = parkingService.EntryExit();
                if (vehicleStatus == Convert.ToInt32(VehicleStatus.entry))
                {
                    parkingService.GetVehicleType();
                    parkingService.CanVehicleEnter();
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
