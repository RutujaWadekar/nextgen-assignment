using PaySpace.Calculator.Services.Abstractions;
using PaySpace.Calculator.Services.Models;
using PaySpace.Calculator.Data.Models;

namespace PaySpace.Calculator.Services.Calculators
{
    public sealed class ProgressiveCalculator : IProgressiveCalculator
    {
        public async Task<CalculateResult> CalculateAsync(decimal income)
        {
            decimal tax = CalculateProgressiveTax(income);

            return await Task.FromResult(new CalculateResult
            {
                Tax = tax,
                Calculator = CalculatorType.Progressive
            });
        }
         private decimal CalculateProgressiveTax(decimal income)
        {
            decimal tax = 0;
            decimal previousBracket = 0;

            var brackets = new[]
            {
                (Upper: 8350m, Rate: 0.10m),
                (Upper: 33950m, Rate: 0.15m),
                (Upper: 82250m, Rate: 0.25m),
                (Upper: 171550m, Rate: 0.28m),
                (Upper: 372950m, Rate: 0.33m),
                (Upper: decimal.MaxValue, Rate: 0.35m)
            };

            foreach (var bracket in brackets)
            {
                if (income > bracket.Upper)
                {
                    tax += (bracket.Upper - previousBracket) * bracket.Rate;
                    previousBracket = bracket.Upper;
                }
                else
                {
                    tax += (income - previousBracket) * bracket.Rate;
                    break;
                }
            }

            return tax;
        }
    }
}