using CommercialRegistration;
using ConsumerVehicleRegistration;
using LiveryRegistration;
using System;
using System.Collections.Generic;

namespace toll_calculator
{
    public class TollCalculatorBase : ITollCalculator
    {
        private readonly Dictionary<Vehicle, decimal> toll = new Dictionary<Vehicle, decimal>
        {
            [Vehicle.Bus] = 5.0m,
            [Vehicle.Car] = 2.0m,
            [Vehicle.DeliveryTruck] = 10.0m,
            [Vehicle.Taxi] = 3.5m
        };

        public decimal CalculateToll(object vehicle) => vehicle switch
        {
            Bus b => this.toll[Vehicle.Bus],
            Car c => this.toll[Vehicle.Car],
            DeliveryTruck d => this.toll[Vehicle.DeliveryTruck],
            Taxi t => this.toll[Vehicle.Taxi],
            { } => throw new ArgumentException(message: "Not a known vehicle type", paramName: nameof(vehicle)),
            null => throw new ArgumentNullException(nameof(vehicle))
        };
    }
}
