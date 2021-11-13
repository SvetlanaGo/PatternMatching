using ConsumerVehicleRegistration;
using LiveryRegistration;
using System;

namespace toll_calculator
{
    public class TollCalculatorByPassengers : ITollCalculator
    {
        private readonly ITollCalculator tollCalculator;
        private readonly decimal[] surchargeArray = { 0.5m, -0.5m, 1.0m };

        public TollCalculatorByPassengers(ITollCalculator tollCalculator) =>
            this.tollCalculator = tollCalculator ?? throw new ArgumentNullException(nameof(tollCalculator));

        public decimal CalculateToll(object vehicle) => vehicle switch
        {
            Car c => this.GetSurcharge(c.Passengers) + this.tollCalculator.CalculateToll(c),
            Taxi t => this.GetSurcharge(t.Fares) + this.tollCalculator.CalculateToll(t),
            { } => this.tollCalculator.CalculateToll(vehicle),
            null => throw new ArgumentNullException(nameof(vehicle))
        };

        private decimal GetSurcharge(int passengers) => passengers < this.surchargeArray.Length ?
            this.surchargeArray[passengers] : this.surchargeArray[this.surchargeArray.Length - 1];
    }
}
