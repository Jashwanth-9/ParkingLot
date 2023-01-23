using System;
using System.Reflection;
using System.Collections.Generic;

namespace ParkingLot.Models
{
    public class ParkingSlots
    {
        public int noOfTwoWheelerSlots;
        public int noOfFourWheelerSlots;
        public int noOfHeavyVehicleSlots;
        public int entryExit;
        public int vehicleType;
        public List<bool> noOfTwoWheelerSlotsAvailable = new List<bool>();
        public List<bool> noOfFourWheelerSlotsAvailable = new List<bool>();
        public List<bool> noOfHeavyVehicleSlotsAvailable = new List<bool>();
        public List<Vehicle> vehicles = new List<Vehicle>();

    }
}