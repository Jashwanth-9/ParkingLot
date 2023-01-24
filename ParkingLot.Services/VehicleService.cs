using ParkingLot.Models.Enums;
using ParkingLot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ParkingLot.Services.Extensions;

namespace ParkingLot.Services
{
    public class VehicleService : IVehicleService
    {
        ParkingSlots parkingSlots;
        public VehicleService(ParkingSlots parkingSlots)
        {
            this.parkingSlots = parkingSlots;
        }
        public void GetVehicleType()
        {
            Console.WriteLine("Choose the Vehicle Type from below");
            Console.WriteLine("1. Two Wheeler");
            Console.WriteLine("2. Four Wheeler");
            Console.WriteLine("3. Heavy Vehicle");
            int vehicleType = Console.ReadLine()!.ToInt32();
            Console.WriteLine();
            parkingSlots.vehicleType = vehicleType;
        }
        public bool IsValidVehicle(string vehicleNumber)
        {
            string pattern = @"^[A-Z]{2} [0-9]{2} [A-Z]{2} [0-9]{4}$";
            Regex regex = new Regex(pattern);
            if (regex.IsMatch(vehicleNumber))
            {
                return true;
            }
            return false;
        }
        public bool CanVehicleEnter()
        {
            bool isAvailable = false;
            int availableSlot = 100;
            if (parkingSlots.vehicleType == VehicleType.twoWheeler.GetIndex())
            {
                int slot=parkingSlots.noOfTwoWheelerSlotsAvailable.IndexOf(true);
                if(slot >= 0)
                {
                    parkingSlots.noOfTwoWheelerSlotsAvailable[slot] = false;
                    isAvailable = true;
                    availableSlot = slot + 1;
                }
            }
            else if (parkingSlots.vehicleType == VehicleType.fourWheeler.GetIndex())
            {
                int slot = parkingSlots.noOfFourWheelerSlotsAvailable.IndexOf(true);
                if (slot >= 0)
                {
                    parkingSlots.noOfFourWheelerSlotsAvailable[slot] = false;
                    isAvailable = true;
                    availableSlot = slot + parkingSlots.noOfTwoWheelerSlots + 1;
                }    
            }
            else if (parkingSlots.vehicleType == VehicleType.heavyVehicle.GetIndex())
            {
                int slot=parkingSlots.noOfHeavyVehicleSlotsAvailable.IndexOf(true);
                if (slot >= 0)
                {
                    parkingSlots.noOfHeavyVehicleSlotsAvailable[slot] = false;
                    isAvailable = true;
                    availableSlot = slot + parkingSlots.noOfFourWheelerSlots + parkingSlots.noOfTwoWheelerSlots + 1;
                }
            }
            if (isAvailable)
            {
                GenerateTicket(availableSlot);
                return true;
            }
            Console.WriteLine("Slot is Unavialable");
            Console.WriteLine();
            return false;
        }
        private void GenerateTicket(int availableSlot)
        {
            Vehicle new_vehicle = new Vehicle();
            Console.WriteLine("Enter a valid Vehicle Number");
            string vehicleNumber = Console.ReadLine()!;
            if (IsValidVehicle(vehicleNumber))
            {
                new_vehicle.number = vehicleNumber;
            }
            else
            {
                GenerateTicket(availableSlot);
                return;
            }
            new_vehicle.type = parkingSlots.vehicleType;
            new_vehicle.slot = availableSlot;
            new_vehicle.inTime = DateTime.Now.Tohhmmsstt();
            parkingSlots.vehicles[availableSlot - 1] = new_vehicle;
            Console.WriteLine("Your Ticket is Confirmed.");
            Console.WriteLine("Your Slot Number is " + availableSlot);
            Console.WriteLine("Vehicle In Time :" + new_vehicle.inTime.ToString());
        }
    }
}
