using PaySpace.Calculator.Data.Models;
using PaySpace.Calculator.Services.Models;

namespace PaySpace.Calculator.Services.Abstractions
{
    public interface ITaxCalculatorService
    {
        Task<CalculateResult> CalculateTaxAsync(decimal income, string postalCode);
    }
}