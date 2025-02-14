using PaySpace.Calculator.Services.Abstractions;
using PaySpace.Calculator.Services.Models;
using System;

namespace PaySpace.Calculator.Services.Calculators
{
    public class TaxCalculatorService : ITaxCalculatorService
    {
        private readonly FlatRateCalculator _flatRateCalculator;
        private readonly FlatValueCalculator _flatValueCalculator;
        private readonly ProgressiveCalculator _progressiveCalculator;

        public TaxCalculatorService(
            FlatRateCalculator flatRateCalculator, 
            FlatValueCalculator flatValueCalculator, 
            ProgressiveCalculator progressiveCalculator)
        {
            _flatRateCalculator = flatRateCalculator;
            _flatValueCalculator = flatValueCalculator;
            _progressiveCalculator = progressiveCalculator;
        }

        public async Task<CalculateResult> CalculateTaxAsync(decimal income, string postalCode)
        {
            return postalCode switch
            {
                "7441" or "1000" => await _progressiveCalculator.CalculateAsync(income),
                "A100" => await _flatValueCalculator.CalculateAsync(income),
                "7000" => await _flatRateCalculator.CalculateAsync(income),
                _ => throw new ArgumentException("Invalid postal code.")
            };
        }
    }
}
