using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using PaySpace.Calculator.Web.Models;
using PaySpace.Calculator.Web.Services.Abstractions;
using PaySpace.Calculator.Web.Services.Models;

namespace PaySpace.Calculator.Web.Controllers
{
    public class CalculatorController : Controller
    {
       private readonly ICalculatorHttpService _calculatorHttpService;

        public CalculatorController(ICalculatorHttpService calculatorHttpService)
        {
            _calculatorHttpService = calculatorHttpService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var vm = await GetCalculatorViewModelAsync();
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CalculateRequestViewModel request)
        {
            if (!ModelState.IsValid)
            {
                var vm = await GetCalculatorViewModelAsync(request);
                return View(vm);
            }

            try
            {
                await _calculatorHttpService.CalculateTaxAsync(new CalculateRequest
                {
                    PostalCode = request.PostalCode,
                    Income = request.Income
                });

                return RedirectToAction(nameof(History));
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
            }

            var viewModel = await GetCalculatorViewModelAsync(request);
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> History()
        {
            var history = await _calculatorHttpService.GetHistoryAsync();
            return View(new CalculatorHistoryViewModel { CalculatorHistory = history });
        }

        private async Task<CalculatorViewModel> GetCalculatorViewModelAsync(CalculateRequestViewModel? request = null)
        {
            var postalCodes = await _calculatorHttpService.GetPostalCodesAsync();
            return new CalculatorViewModel
            {
                PostalCodes = new SelectList(postalCodes, "Code", "Code"),
                Income = request?.Income ?? 0,
                PostalCode = request?.PostalCode ?? string.Empty
            };
        }
    }
}