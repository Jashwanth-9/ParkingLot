using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLot.Services.Extensions
{
    public static class DateTimeExtention
    {
        public static string Tohhmmsstt(this DateTime dateTime)
        {
            return dateTime.ToString("hh:mm:ss tt");
        }
    }
}
