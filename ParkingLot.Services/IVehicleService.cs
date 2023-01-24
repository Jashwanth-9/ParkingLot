using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLot.Services
{
    public interface IVehicleService
    {
        void GetVehicleType();
        bool IsValidVehicle(string vehicleNumber);
        bool CanVehicleEnter();
    }
}
