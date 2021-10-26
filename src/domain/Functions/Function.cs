using System;

namespace Domain.Functions
{
    public static class Function
    {
        public static double Calculate(double input, double oldCalculationResult)
        {
            return Math.Cbrt(Math.Log(input)) / oldCalculationResult;
        }
    } 
}