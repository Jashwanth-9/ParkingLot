using ParkingLot.Models.Enums;
using ParkingLot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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
            int vehicleType = Convert.ToInt32(Console.ReadLine());
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
            if (parkingSlots.vehicleType == ExtensionMethods.GetIndex(VehicleType.twoWheeler))
            {
                for (int i = 0; i < parkingSlots.noOfTwoWheelerSlots; i++)
                {
                    if (parkingSlots.noOfTwoWheelerSlotsAvailable[i] == true)
                    {
                        parkingSlots.noOfTwoWheelerSlotsAvailable[i] = false;
                        isAvailable = true;
                        availableSlot = i + 1;
                        break;
                    }
                }
            }
            else if (parkingSlots.vehicleType == ExtensionMethods.GetIndex(VehicleType.fourWheeler))
            {
                for (int i = 0; i < parkingSlots.noOfFourWheelerSlots; i++)
                {
                    if (parkingSlots.noOfFourWheelerSlotsAvailable[i] == true)
                    {
                        parkingSlots.noOfFourWheelerSlotsAvailable[i] = false;
                        isAvailable = true;
                        availableSlot = i + parkingSlots.noOfTwoWheelerSlots + 1;
                        break;
                    }
                }
            }
            else if (parkingSlots.vehicleType == ExtensionMethods.GetIndex(VehicleType.heavyVehicle))
            {
                for (int i = 0; i < parkingSlots.noOfHeavyVehicleSlots; i++)
                {
                    if (parkingSlots.noOfHeavyVehicleSlotsAvailable[i] == true)
                    {
                        parkingSlots.noOfHeavyVehicleSlotsAvailable[i] = false;
                        isAvailable = true;
                        availableSlot = i + parkingSlots.noOfFourWheelerSlots + parkingSlots.noOfTwoWheelerSlots + 1;
                        break;
                    }
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
        public void GenerateTicket(int availableSlot)
        {
            Vehicle new_vehicle = new Vehicle();
            Console.WriteLine("Enter a valid Vehicle Number");
            string vehicleNumber = Console.ReadLine();
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
            new_vehicle.inTime = DateTime.Now.ToString("hh:mm:ss tt");
            parkingSlots.vehicles[availableSlot - 1] = new_vehicle;
            Console.WriteLine("Your Ticket is Confirmed.");
            Console.WriteLine("Your Slot Number is " + availableSlot);
            Console.WriteLine("Vehicle In Time :" + new_vehicle.inTime.ToString());
        }
    }
}
