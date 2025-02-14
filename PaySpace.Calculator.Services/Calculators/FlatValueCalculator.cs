using PaySpace.Calculator.Services.Abstractions;
using PaySpace.Calculator.Services.Models;
using PaySpace.Calculator.Data.Models;

namespace PaySpace.Calculator.Services.Calculators
{
    public sealed class FlatValueCalculator : IFlatValueCalculator
    {
        private const decimal Threshold = 200000m;
        private const decimal FlatTax = 10000m;
        private const decimal PercentageRate = 0.05m;
        public async Task<CalculateResult> CalculateAsync(decimal income)
        {
            if (income < 0)
                throw new ArgumentException("Income cannot be negative.");

            decimal tax = income < Threshold ? income * PercentageRate : FlatTax;

            return await Task.FromResult(new CalculateResult
            {
                Tax = tax,
                Calculator = CalculatorType.FlatValue
            });
        }
    }
}