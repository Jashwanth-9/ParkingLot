using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLot.Services
{
    public class MainService
    {
        private IParkingService parkingService;
        private IVehicleService vehicleService;
        public MainService(IParkingService parkingService, IVehicleService vehicleService)
        {
            this.parkingService = parkingService;
            this.vehicleService = vehicleService;
        }
        public void Initializer()
        {
            parkingService.Initializer();
        }
        public int EntryExit()
        {
            return parkingService.EntryExit();
        }
        public void Exit()
        {
            parkingService.Exit();
        }
        public void GetVehicleType()
        {
            this.vehicleService.GetVehicleType();
        }
        public bool CanVehicleEnter()
        {
            return this.vehicleService.CanVehicleEnter();
        }
    }
}
