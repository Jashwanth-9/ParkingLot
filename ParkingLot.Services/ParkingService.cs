using System;
using System.Reflection;
using System.Text.RegularExpressions;
using ParkingLot.Models;
using ParkingLot.Models.Enums;
using ParkingLot.Services.Extensions;
using static System.Reflection.Metadata.BlobBuilder;

namespace ParkingLot.Services
{
    public class ParkingService : IParkingService
    {
        private ParkingSlots parkingSlots;
        public ParkingService(ParkingSlots slots)
        {
            this.parkingSlots = slots;
        }
        private void SetSlots(int noOfTwoWheelerSlots,int noOfFourWheelerSlots,int noOfHeavyVehicleSlots)
        {
            Vehicle vehicle= new Vehicle();
            parkingSlots.noOfTwoWheelerSlots=noOfTwoWheelerSlots;
            parkingSlots.noOfFourWheelerSlots = noOfFourWheelerSlots;
            parkingSlots.noOfHeavyVehicleSlots = noOfHeavyVehicleSlots;
            int totalSlots = noOfTwoWheelerSlots + noOfFourWheelerSlots + noOfHeavyVehicleSlots;
            parkingSlots.noOfTwoWheelerSlotsAvailable = Enumerable.Range(0, noOfTwoWheelerSlots).Select(n => true).ToList();
            parkingSlots.noOfFourWheelerSlotsAvailable = Enumerable.Range(0, noOfFourWheelerSlots).Select(n => true).ToList();
            parkingSlots.noOfHeavyVehicleSlotsAvailable = Enumerable.Range(0, noOfHeavyVehicleSlots).Select(n => true).ToList();
            parkingSlots.vehicles = Enumerable.Range(0, totalSlots).Select(n => vehicle).ToList();
        }
        public void Initializer()
        {
            ParkingService setSlots=new ParkingService(parkingSlots);
            Console.WriteLine("Welcome to the parking");
            Console.WriteLine("Specify total number of Two wheeler slots");
            int noOfTwoWheelerSlots=Console.ReadLine()!.ToInt32();
            Console.WriteLine("Specify total number of Four wheeler slots");
            int noOfFourWheelerSlots = Console.ReadLine()!.ToInt32();
            Console.WriteLine("Specify total number of Heavy Vehicle slots");
            int noOfHeavyVehicleSlots = Console.ReadLine()!.ToInt32();
            setSlots.SetSlots(noOfTwoWheelerSlots, noOfFourWheelerSlots,noOfHeavyVehicleSlots);
        }
        public int EntryExit()
        {
            Console.WriteLine("Choose one of the options : ");
            Console.WriteLine("1.Entry");
            Console.WriteLine("2.Exit");
            Console.WriteLine("3.Stop");
            int vehicleStatus = Console.ReadLine()!.ToInt32();
            if (vehicleStatus == VehicleStatus.entry.GetIndex() || vehicleStatus == VehicleStatus.exit.GetIndex() || vehicleStatus == VehicleStatus.stop.GetIndex())
            {
                return vehicleStatus;
            }
            else
            {
                Console.WriteLine("Choose either 1 or 2 or 3");
                EntryExit();
            }
            return 0;
        }
   
        public void Exit()
        {
            VehicleService vehicleService=new VehicleService(parkingSlots);
            Console.WriteLine("Enter a Valid Vehicle Number");
            string vehicleNumber = Console.ReadLine()!;
            if (!vehicleService.IsValidVehicle(vehicleNumber!))
            {
                Exit();
                return;
            }
            Console.WriteLine("Enter the slot number");
            int slot = Console.ReadLine()!.ToInt32();
            if (parkingSlots.vehicles[slot-1].number != vehicleNumber) 
            {
                Console.WriteLine("Enter the correct slot Number and Vehicle Number");
                Exit();
                return;
            }
            if (slot<parkingSlots.noOfTwoWheelerSlots)
            {
                parkingSlots.noOfTwoWheelerSlotsAvailable[slot-1] = true;
                Console.WriteLine(slot);
            }
            else if (slot < parkingSlots.noOfFourWheelerSlots)
            {
                parkingSlots.noOfFourWheelerSlotsAvailable[slot-parkingSlots.noOfTwoWheelerSlots -1] = true;
            }
            else if (slot < parkingSlots.noOfHeavyVehicleSlots)
            {
                parkingSlots.noOfHeavyVehicleSlotsAvailable[slot-parkingSlots.noOfFourWheelerSlots -parkingSlots.noOfTwoWheelerSlots - 1] = true;
            }
            string outTime = DateTime.Now.Tohhmmsstt();
            parkingSlots.vehicles[slot - 1].outTime = outTime;
            Console.WriteLine("Exit Successful");
            Console.WriteLine("Vehicle Number :" + parkingSlots.vehicles[slot - 1].number);
            Console.WriteLine("Vehicle In-Time :" + parkingSlots.vehicles[slot - 1].inTime);
            Console.WriteLine("Vehicle Out-Time :" + parkingSlots.vehicles[slot - 1].outTime);
        }
    }
}