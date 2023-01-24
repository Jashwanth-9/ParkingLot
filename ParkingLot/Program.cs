using System;
using ParkingLot.Services;
using ParkingLot.Models;
using ParkingLot.Models.Enums;
using ParkingLot.Services.Extensions;
using System.Runtime.CompilerServices;

namespace ParkingLot
{
    class Parking
    {
        public static void Main(string[] args)
        {
            ParkingSlots parkingSlots = new ParkingSlots();
            MainService parking = new MainService(new ParkingService(parkingSlots), new VehicleService(parkingSlots));
            
            parking.Initializer();
            
            bool stop = false;
            while(!stop)
            {
                
                int vehicleStatus = parking.EntryExit();
                if (vehicleStatus == VehicleStatus.entry.GetIndex())
                {
                    
                    parking.GetVehicleType();
                    parking.CanVehicleEnter();
                }
                else if(vehicleStatus == VehicleStatus.exit.GetIndex())
                {
                    
                    parking.Exit();

                }
                else if(vehicleStatus == VehicleStatus.stop.GetIndex())
                {
                    stop = true;
                }
            }
        }
    }  
}
