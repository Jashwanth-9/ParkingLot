using ParkingLot.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLot.Services.Extensions
{
    public static class EnumExtension
    {
        public static int GetIndex(this VehicleType vehicle)
        {
            return Convert.ToInt32(vehicle);
        }
        public static int GetIndex(this VehicleStatus vehicle)
        {
            return Convert.ToInt32(vehicle);
        }
    }
}
