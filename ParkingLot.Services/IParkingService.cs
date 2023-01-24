using ParkingLot.Models.Enums;
using ParkingLot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace ParkingLot.Services
{
    public interface IParkingService
    {
        void SetSlots(int noOfTwoWheelerSlots, int noOfFourWheelerSlots, int noOfHeavyVehicleSlots);
        void Initializer();
        int EntryExit();
        void Exit();
    }
}
