using System;

namespace toll_calculator
{
    public static class TollCalculatorExtension
    {
        private const int endOvernight = 6, startOvernight = 19, morningRush = 10, daytime = 16;

        private static readonly decimal[] surcharges = { 0.75m, 1.0m, 1.5m, 2.0m };
        private enum TimeBand
        {
            MorningRush,
            Daytime,
            EveningRush,
            Overnight
        }

        public static decimal CalculateToll(this ITollCalculator tollCalculator, object vehicle, DateTime timeOfToll, bool inbound) =>
            (IsWeekDay(timeOfToll), GetTimeBand(timeOfToll), inbound) switch
            {
                (true, TimeBand.Overnight, _) => tollCalculator.CalculateToll(vehicle) * surcharges[0],
                (true, TimeBand.Daytime, _) => tollCalculator.CalculateToll(vehicle) * surcharges[2],
                (true, TimeBand.MorningRush, true) => tollCalculator.CalculateToll(vehicle) * surcharges[3],
                (true, TimeBand.EveningRush, false) => tollCalculator.CalculateToll(vehicle) * surcharges[3],
                (_, _, _) => tollCalculator.CalculateToll(vehicle) * surcharges[1],
            };

        private static bool IsWeekDay(DateTime timeOfToll) =>
            timeOfToll.DayOfWeek switch
            {
                DayOfWeek.Saturday => false,
                DayOfWeek.Sunday => false,
                _ => true
            };

        private static TimeBand GetTimeBand(DateTime timeOfToll) =>
            timeOfToll.Hour switch
            {
                < endOvernight or > startOvernight => TimeBand.Overnight,
                < morningRush => TimeBand.MorningRush,
                < daytime => TimeBand.Daytime,
                _ => TimeBand.EveningRush,
            };
    }
}
