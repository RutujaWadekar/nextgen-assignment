using PaySpace.Calculator.Services.Abstractions;
using PaySpace.Calculator.Services.Models;
using PaySpace.Calculator.Data.Models;

namespace PaySpace.Calculator.Services.Calculators
{
    public sealed class FlatRateCalculator : IFlatRateCalculator
    {
        private const decimal FlatRate = 0.175m;
        public async Task<CalculateResult> CalculateAsync(decimal income)
        {
            if (income < 0)
                throw new ArgumentException("Income cannot be negative.");

            decimal tax = income * FlatRate;

            return await Task.FromResult(new CalculateResult
            {
                Tax = tax,
                Calculator = CalculatorType.FlatRate
            });
        }
    }
}