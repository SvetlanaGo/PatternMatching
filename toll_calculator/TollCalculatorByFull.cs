using LiveryRegistration;
using System;

namespace toll_calculator
{
    public class TollCalculatorByFull : ITollCalculator
    {
        private const double lowerPercent = 50.0, upperPercent = 90.0;

        private readonly ITollCalculator tollCalculator;
        private readonly decimal[] surchargeArray = { 2.0m, -1.0m };

        public TollCalculatorByFull(ITollCalculator tollCalculator) =>
            this.tollCalculator = tollCalculator ?? throw new ArgumentNullException(nameof(tollCalculator));

        public decimal CalculateToll(object vehicle) => vehicle switch
        {
            Bus b => this.GetSurcharge(b.Capacity, b.Riders) + this.tollCalculator.CalculateToll(b),            
            { } => this.tollCalculator.CalculateToll(vehicle),
            null => throw new ArgumentNullException(nameof(vehicle))
        };

        private decimal GetSurcharge(double capacity, double riders) => (riders / capacity * 100.0) switch
        {
            < lowerPercent => surchargeArray[0],
            > upperPercent => surchargeArray[1],
            _ => 0.0m
        };
    }
}
