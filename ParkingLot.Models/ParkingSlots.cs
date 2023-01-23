using System;
using System.Reflection;
using System.Collections.Generic;

namespace ParkingLot.Models
{
    public class ParkingSlots
    {
        public int twoWheelerSlots;
        public int fourWheelerSlots;
        public int heavyVehicleSlots;
        public int entryExit;
        public int vehicleType;
        public List<bool> twoWheelerSlotsAvailable = new List<bool>();
        public List<bool> fourWheelerSlotsAvailable = new List<bool>();
        public List<bool> heavyVehicleSlotsAvailable = new List<bool>();
        public List<Vehicle> vehicles = new List<Vehicle>();

    }
}