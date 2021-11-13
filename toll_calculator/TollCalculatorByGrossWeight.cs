using CommercialRegistration;
using System;

namespace toll_calculator
{
    public class TollCalculatorByGrossWeight : ITollCalculator
    {
        private const int lowerWeight = 3000, upperWeight = 5000;

        private readonly ITollCalculator tollCalculator;
        private readonly decimal[] surchargeArray = { -2.0m, 5.0m };

        public TollCalculatorByGrossWeight(ITollCalculator tollCalculator) =>
            this.tollCalculator = tollCalculator ?? throw new ArgumentNullException(nameof(tollCalculator));

        public decimal CalculateToll(object vehicle) => vehicle switch
        {
            DeliveryTruck d => this.GetSurcharge(d.GrossWeightClass) + this.tollCalculator.CalculateToll(d),
            { } => this.tollCalculator.CalculateToll(vehicle),
            null => throw new ArgumentNullException(nameof(vehicle))
        };

        private decimal GetSurcharge(int grossWeight) => grossWeight switch
        {
            < lowerWeight => surchargeArray[0],
            > upperWeight => surchargeArray[1],
            _ => 0.0m
        };
    }
}
