using System;
using System.Reflection;
using System.Text.RegularExpressions;
using ParkingLot.Models;
using ParkingLot.Models.Enums;
using static System.Reflection.Metadata.BlobBuilder;

namespace ParkingLot.Services
{
    public class ParkingService
    {
        ParkingSlots slots;
        public ParkingService(ParkingSlots slots)
        {
            this.slots = slots;
        }
        public void SetSlots(int two,int four,int heavy)
        {
            Vehicle vehicle= new Vehicle();
            slots.twoWheelerSlots = two;
            slots.fourWheelerSlots = four;
            slots.heavyVehicleSlots = heavy;
            for (int i = 0; i < two; i++)
            {
                slots.twoWheelerSlotsAvailable.Add(true);
            }
            for (int i = 0; i < four; i++)
            {
                slots.fourWheelerSlotsAvailable.Add(true);
            }
            for (int i = 0; i < heavy; i++)
            {
                slots.heavyVehicleSlotsAvailable.Add(true);
            }
            int totalSlots=two+four+heavy;
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
            int twoSlots=Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Specify total number of Four wheeler slots");
            int fourSlots=Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Specify total number of Heavy Vehicle slots");
            int heavySlots=Convert.ToInt32(Console.ReadLine());
            setSlots.SetSlots(twoSlots, fourSlots,heavySlots);
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
        public void VehicleType() {
            Console.WriteLine("Choose the Vehicle Type from below");
            Console.WriteLine("1. Two Wheeler");
            Console.WriteLine("2. Four Wheeler");
            Console.WriteLine("3. Heavy Vehicle");
            int vehicleType = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            slots.vehicleType = vehicleType;
        }
        public bool ValidVehicle(string str)
        {
            string pattern = @"^[A-Z]{2} [0-9]{2} [A-Z]{2} [0-9]{4}$";
            Regex regex = new Regex(pattern);
            if (regex.IsMatch(str))
            {
                return true;
            }
            return false;
        }
        public bool GetEntry()
        {
            bool isAvailable=false;
            int availableSlot=100;
            if (slots.vehicleType == Convert.ToInt32(VehicleTypes.twoWheeler))
            {
                for (int i = 0; i < slots.twoWheelerSlots; i++)
                {
                    if (slots.twoWheelerSlotsAvailable[i] == true)
                    {
                        slots.twoWheelerSlotsAvailable[i] = false;
                        isAvailable = true;
                        availableSlot= i+1;
                        break;
                    }
                }
            }
            else if (slots.vehicleType == Convert.ToInt32(VehicleTypes.fourWheeler))
            {
                for (int i = 0; i < slots.fourWheelerSlots; i++)
                {
                    if (slots.fourWheelerSlotsAvailable[i] == true)
                    {
                        slots.fourWheelerSlotsAvailable[i] = false;
                        isAvailable = true;
                        availableSlot = i + slots.twoWheelerSlots + 1;
                        break;
                    }
                }
            }
            else if (slots.vehicleType == Convert.ToInt32(VehicleTypes.heavyVehicle))
            {
                for (int i = 0; i < slots.heavyVehicleSlots; i++)
                {
                    if (slots.heavyVehicleSlotsAvailable[i] == true)
                    {
                        slots.heavyVehicleSlotsAvailable[i] = false;
                        isAvailable = true;
                        availableSlot = i + slots.fourWheelerSlots + slots.twoWheelerSlots + 1;
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
            if (ValidVehicle(vehicleNumber))
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
            Console.WriteLine();
            Console.WriteLine("Your Ticket is Confirmed.");
            Console.WriteLine();
            Console.WriteLine("Your Slot Number is " + availableSlot);
            Console.WriteLine("Vehicle In Time :" + new_vehicle.inTime.ToString());
            Console.WriteLine();
        }
        public void Exit()
        {
            Console.WriteLine("Enter a Valid Vehicle Number");
            string vehicleNumber = Console.ReadLine();
            if (!ValidVehicle(vehicleNumber))
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
            if (slot<slots.twoWheelerSlots)
            {
                slots.twoWheelerSlotsAvailable[slot-1] = true;
                Console.WriteLine(slot);
            }
            else if (slot < slots.fourWheelerSlots)
            {
                slots.fourWheelerSlotsAvailable[slot-slots.twoWheelerSlots -1] = true;
            }
            else if (slot < slots.heavyVehicleSlots)
            {
                slots.heavyVehicleSlotsAvailable[slot-slots.fourWheelerSlots -slots.twoWheelerSlots - 1] = true;
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