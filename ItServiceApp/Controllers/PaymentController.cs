using ItServiceApp.Services;
using ItServiceApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItServiceApp.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult CheckInstallment(string binNumber)
        {
            if (binNumber.Length != 6)
                return BadRequest(new
            {
                Message = "Bad req."
            });
            var result = _paymentService.CheckInstallments(binNumber, 1000);
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        public IActionResult MakePayment(PaymentViewModel model)
        {
            return View();
        }

    }
}
