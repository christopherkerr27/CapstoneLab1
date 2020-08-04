using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Week4Cap.Models;

namespace Week4Cap.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IHttpContextAccessor _accessor;

        public ShoppingCartController(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public IActionResult Index()
        {
            var model = new ShoppingCart
            {
                Quantity = GetQuantity()
            };

            return View(model);
        }

        public IActionResult Submit()
        {
            var newValue = GetQuantity() + 1;

            
            _accessor.HttpContext.Session.SetInt32("quantity", newValue);

            return RedirectToAction("Index");
        }

        private int GetQuantity()
        {
            
            var current = _accessor.HttpContext.Session.GetInt32("quantity");

            if (!int.TryParse(current, out int value))
            {
                value = 0;
            }

            return value;
        }
    }
}