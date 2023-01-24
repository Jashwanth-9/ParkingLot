using System;
using System.Reflection;
using System.Text.RegularExpressions;
using ParkingLot.Models;
using ParkingLot.Models.Enums;
using static System.Reflection.Metadata.BlobBuilder;

namespace ParkingLot.Services
{
    public class ParkingService : IParkingService
    {
        ParkingSlots parkingSlots;
        public ParkingService(ParkingSlots slots)
        {
            this.parkingSlots = slots;
        }
        public void SetSlots(int noOfTwoWheelerSlots,int noOfFourWheelerSlots,int noOfHeavyVehicleSlots)
        {
            Vehicle vehicle= new Vehicle();
            parkingSlots.noOfTwoWheelerSlots=noOfTwoWheelerSlots;
            parkingSlots.noOfFourWheelerSlots = noOfFourWheelerSlots;
            parkingSlots.noOfHeavyVehicleSlots = noOfHeavyVehicleSlots;
            for (int i = 0; i < noOfTwoWheelerSlots; i++)
            {
                parkingSlots.noOfTwoWheelerSlotsAvailable.Add(true);
            }
            for (int i = 0; i < noOfFourWheelerSlots; i++)
            {
                parkingSlots.noOfFourWheelerSlotsAvailable.Add(true);
            }
            for (int i = 0; i < noOfHeavyVehicleSlots; i++)
            {
                parkingSlots.noOfHeavyVehicleSlotsAvailable.Add(true);
            }
            int totalSlots=noOfTwoWheelerSlots +noOfFourWheelerSlots +noOfHeavyVehicleSlots;
            for(int i = 0; i < totalSlots; i++)
            {
                parkingSlots.vehicles.Add(vehicle);
            }
        }
        public void Initializer()
        {
            ParkingService setSlots=new ParkingService(parkingSlots);
            Console.WriteLine("Welcome to the parking");
            Console.WriteLine("Specify total number of Two wheeler slots");
            int noOfTwoWheelerSlots=Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Specify total number of Four wheeler slots");
            int noOfFourWheelerSlots = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Specify total number of Heavy Vehicle slots");
            int noOfHeavyVehicleSlots = Convert.ToInt32(Console.ReadLine());
            setSlots.SetSlots(noOfTwoWheelerSlots, noOfFourWheelerSlots,noOfHeavyVehicleSlots);
        }
        public int EntryExit()
        {
            Console.WriteLine("Choose one of the options : ");
            Console.WriteLine("1.Entry");
            Console.WriteLine("2.Exit");
            Console.WriteLine("3.Stop");
            int vehicleStatus = Convert.ToInt32(Console.ReadLine());
            if (vehicleStatus == ExtensionMethods.GetIndex(VehicleStatus.entry) || vehicleStatus == ExtensionMethods.GetIndex(VehicleStatus.exit) || vehicleStatus == ExtensionMethods.GetIndex(VehicleStatus.stop))
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
            string vehicleNumber = Console.ReadLine();
            if (!vehicleService.IsValidVehicle(vehicleNumber))
            {
                Exit();
                return;
            }
            Console.WriteLine("Enter the slot number");
            int slot = Convert.ToInt32(Console.ReadLine());
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
            string outTime = DateTime.Now.ToString("hh:mm:ss tt");
            parkingSlots.vehicles[slot - 1].outTime = outTime;
            Console.WriteLine("Exit Successful");
            Console.WriteLine("Vehicle Number :" + parkingSlots.vehicles[slot - 1].number);
            Console.WriteLine("Vehicle In-Time :" + parkingSlots.vehicles[slot - 1].inTime);
            Console.WriteLine("Vehicle Out-Time :" + parkingSlots.vehicles[slot - 1].outTime);
        }
    }
}