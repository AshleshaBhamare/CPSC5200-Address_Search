using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AddressSearchSolution.Models;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;

namespace AddressSearchSolution.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        [Route("api/validateAddress")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public string ValidateAddress([FromBody]Address address)
        {
            string messageToUser = "Please fill our required fields";
            Boolean valid = true;
            switch (address.country)
            {
                case "BR":
                    var _brazilZipRegEx = @"^\d{5}[-\s*]\d{3}$";
                    if (!Regex.Match(address.post_code, _brazilZipRegEx).Success) {
                        valid = false;
                        messageToUser += "Invalid post code. Post Code should bea  5 digit number follow by - and a 3 digit number.";
                    }
                    var _brazilCityRegex = @"[-][a-zA-Z][a-zA-Z]$";
                    if (!Regex.Match(address.city, _brazilCityRegex).Success)
                    {
                        valid = false;
                        messageToUser += "Invalid city-province code. province code should be two alphabet letters.";
                    }
                    if (valid) {
                        messageToUser = "The address is valid for brazil.";
                    } else {

                    }
                    break;

                case "CA":
                    var _canadaPostalRegEx = @"^(?!.*[DFIOQU])[A-VXY][0-9][A-Z] ?[0-9][A-Z][0-9]$";
                    if (!Regex.Match(address.post_code, _canadaPostalRegEx).Success)
                    {
                        valid = false;
                        messageToUser += "Invalid post code. Post Code should be alphanumeric where letter is followed bydigit with " +
                            "a space seperating third and fourth characters.";
                    }
                    var _canadaCityRegex = @"[a-zA-Z][a-zA-Z]$";
                    if (!Regex.Match(address.city, _canadaCityRegex).Success)
                    {
                        valid = false;
                        messageToUser += "Invalid city name. Please add a valid word for city";
                    }
                    if (valid)
                    {
                        messageToUser = "The address is valid for Canada.";
                    }

                    break;

                case "MX":
                    var _mexicoZipRegEx = @"^\d{5}$";
                    if (!Regex.Match(address.post_code, _mexicoZipRegEx).Success)
                    {
                        valid = false;
                        messageToUser += "Invalid post code. Post Code should bea  5 digit number.";
                    }
                    var _mexicoCityRegex = @"[a-zA-Z][a-zA-Z]$";
                    if (!Regex.Match(address.city, _mexicoCityRegex).Success)
                    {
                        valid = false;
                        messageToUser += "Invalid city or locality. Please provide a valid word for city or locality.";
                    }
                    if (valid)
                    {
                        messageToUser = "The address is valid for Mexico.";
                    }
                    
                    break;

                
            }

            return messageToUser;
        }
    }
}
