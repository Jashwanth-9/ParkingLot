using ParkingLot.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLot.Services.Extensions
{
    public static class IntExtension
    {
        public static int ToInt32(this string text)
        {
            return Convert.ToInt32(text);
        }
    }
}
