using System;
using System.Reflection;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace ParkingLot.Models
{
    public class ParkingSlots
    {
        public int noOfTwoWheelerSlots{ get; set; }
        public int noOfFourWheelerSlots { get; set; }
        public int noOfHeavyVehicleSlots { get; set; }
        public int vehicleType { get; set; }

        public List<bool> noOfTwoWheelerSlotsAvailable = new List<bool>();
        public List<bool> noOfFourWheelerSlotsAvailable = new List<bool>();
        public List<bool> noOfHeavyVehicleSlotsAvailable = new List<bool>();
        public List<Vehicle> vehicles = new List<Vehicle>();

    }
}