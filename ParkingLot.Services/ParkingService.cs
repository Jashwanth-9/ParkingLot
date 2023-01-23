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
        ParkingSlots slots;
        public ParkingService(ParkingSlots slots)
        {
            this.slots = slots;
        }
        public void SetSlots(int noOfTwoWheelerSlots,int noOfFourWheelerSlots,int noOfHeavyVehicleSlots)
        {
            Vehicle vehicle= new Vehicle();
            slots.noOfTwoWheelerSlots = noOfTwoWheelerSlots;
            slots.noOfFourWheelerSlots = noOfFourWheelerSlots;
            slots.noOfHeavyVehicleSlots = noOfHeavyVehicleSlots;
            for (int i = 0; i < noOfTwoWheelerSlots; i++)
            {
                slots.noOfTwoWheelerSlotsAvailable.Add(true);
            }
            for (int i = 0; i < noOfFourWheelerSlots; i++)
            {
                slots.noOfFourWheelerSlotsAvailable.Add(true);
            }
            for (int i = 0; i < noOfHeavyVehicleSlots; i++)
            {
                slots.noOfHeavyVehicleSlotsAvailable.Add(true);
            }
            int totalSlots=noOfTwoWheelerSlots +noOfFourWheelerSlots +noOfHeavyVehicleSlots;
            for(int i = 0; i < totalSlots; i++)
            {
                slots.vehicles.Add(vehicle);
            }
        }
        public void Initializer()
        {
            ParkingService setSlots=new ParkingService(slots);
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
            if(vehicleStatus == Convert.ToInt32(VehicleStatus.entry) || vehicleStatus == Convert.ToInt32(VehicleStatus.exit) || vehicleStatus == Convert.ToInt32(VehicleStatus.stop)) {
                return vehicleStatus;
            }
            else
            {
                Console.WriteLine("Choose either 1 or 2 or 3");
                EntryExit();
            }
            return 0;
        }
        public void GetVehicleType() {
            Console.WriteLine("Choose the Vehicle Type from below");
            Console.WriteLine("1. Two Wheeler");
            Console.WriteLine("2. Four Wheeler");
            Console.WriteLine("3. Heavy Vehicle");
            int vehicleType = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            slots.vehicleType = vehicleType;
        }
        public bool IsValidVehicle(string str)
        {
            string pattern = @"^[A-Z]{2} [0-9]{2} [A-Z]{2} [0-9]{4}$";
            Regex regex = new Regex(pattern);
            if (regex.IsMatch(str))
            {
                return true;
            }
            return false;
        }
        public bool CanVehicleEnter()
        {
            bool isAvailable=false;
            int availableSlot=100;
            if (slots.vehicleType == Convert.ToInt32(VehicleType.twoWheeler))
            {
                for (int i = 0; i < slots.noOfTwoWheelerSlots; i++)
                {
                    if (slots.noOfTwoWheelerSlotsAvailable[i] == true)
                    {
                        slots.noOfTwoWheelerSlotsAvailable[i] = false;
                        isAvailable = true;
                        availableSlot= i+1;
                        break;
                    }
                }
            }
            else if (slots.vehicleType == Convert.ToInt32(VehicleType.fourWheeler))
            {
                for (int i = 0; i < slots.noOfFourWheelerSlots; i++)
                {
                    if (slots.noOfFourWheelerSlotsAvailable[i] == true)
                    {
                        slots.noOfFourWheelerSlotsAvailable[i] = false;
                        isAvailable = true;
                        availableSlot = i + slots.noOfTwoWheelerSlots + 1;
                        break;
                    }
                }
            }
            else if (slots.vehicleType == Convert.ToInt32(VehicleType.heavyVehicle))
            {
                for (int i = 0; i < slots.noOfHeavyVehicleSlots; i++)
                {
                    if (slots.noOfHeavyVehicleSlotsAvailable[i] == true)
                    {
                        slots.noOfHeavyVehicleSlotsAvailable[i] = false;
                        isAvailable = true;
                        availableSlot = i + slots.noOfFourWheelerSlots + slots.noOfTwoWheelerSlots + 1;
                        break;
                    }
                }
            }
            if(isAvailable)
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
            new_vehicle.type = slots.vehicleType;
            new_vehicle.slot = availableSlot;
            new_vehicle.inTime = DateTime.Now.ToString("hh:mm:ss tt");
            slots.vehicles[availableSlot-1] = new_vehicle;
            Console.WriteLine("Your Ticket is Confirmed.");
            Console.WriteLine("Your Slot Number is " + availableSlot);
            Console.WriteLine("Vehicle In Time :" + new_vehicle.inTime.ToString());
        }
        public void Exit()
        {
            Console.WriteLine("Enter a Valid Vehicle Number");
            string vehicleNumber = Console.ReadLine();
            if (!IsValidVehicle(vehicleNumber))
            {
                Exit();
                return;
            }
            Console.WriteLine("Enter the slot number");
            int slot = Convert.ToInt32(Console.ReadLine());
            if (slots.vehicles[slot-1].number != vehicleNumber) 
            {
                Console.WriteLine("Enter the correct slot Number and Vehicle Number");
                Exit();
                return;
            }
            if (slot<slots.noOfTwoWheelerSlots)
            {
                slots.noOfTwoWheelerSlotsAvailable[slot-1] = true;
                Console.WriteLine(slot);
            }
            else if (slot < slots.noOfFourWheelerSlots)
            {
                slots.noOfFourWheelerSlotsAvailable[slot-slots.noOfTwoWheelerSlots -1] = true;
            }
            else if (slot < slots.noOfHeavyVehicleSlots)
            {
                slots.noOfHeavyVehicleSlotsAvailable[slot-slots.noOfFourWheelerSlots -slots.noOfTwoWheelerSlots - 1] = true;
            }
            string outTime = DateTime.Now.ToString("hh:mm:ss tt");
            slots.vehicles[slot - 1].outTime = outTime;
            Console.WriteLine("Exit Successful");
            Console.WriteLine("Vehicle Number :" + slots.vehicles[slot - 1].number);
            Console.WriteLine("Vehicle In-Time :" + slots.vehicles[slot - 1].inTime);
            Console.WriteLine("Vehicle Out-Time :" + slots.vehicles[slot - 1].outTime);
        }
    }
}