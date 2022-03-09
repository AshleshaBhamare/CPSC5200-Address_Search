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

                case "DE":
                    var _germanyPostalRegEx = @"^\d{5}$";
                    if (!Regex.Match(address.post_code, _germanyPostalRegEx).Success)
                    {
                        valid = false;
                        messageToUser += "Invalid post code. Post Code be 5 digit number.";
                    }
                    var _germanyCityRegex = @"[a-zA-Z][a-zA-Z]$";
                    if (!Regex.Match(address.city, _germanyCityRegex).Success)
                    {
                        valid = false;
                        messageToUser += "Invalid city name. Please add a valid word for city";
                    }
                    if (valid)
                    {
                        messageToUser = "The address is valid for Germany.";
                    }
                    break;

                case "IN":
                    var _indiaPostalRegEx = @"^[1-9][0-9]{5}$";
                    if (!Regex.Match(address.post_code, _indiaPostalRegEx).Success)
                    {
                        valid = false;
                        messageToUser += "Invalid post code. Post Code be 6 digit number.";
                    }
                    var _indiaCityRegex = @"[a-zA-Z][a-zA-Z]$";
                    if (!Regex.Match(address.city, _indiaCityRegex).Success)
                    {
                        valid = false;
                        messageToUser += "Invalid city name. Please add a valid word for city";
                    }
                    if (valid)
                    {
                        messageToUser = "The address is valid for India.";
                    }
                    break;

                case "JP":
                    var _japanPostalRegEx = @"^\d{3}-\d{4}$";
                    if (!Regex.Match(address.post_code, _japanPostalRegEx).Success)
                    {
                        valid = false;
                        messageToUser += "Invalid post code. Post Code be 6 digit number.";
                    }
                    var _japanCityRegex = @"[a-zA-Z][a-zA-Z]$";
                    if (!Regex.Match(address.city, _japanCityRegex).Success)
                    {
                        valid = false;
                        messageToUser += "Invalid city name. Please add a valid word for city";
                    }
                    if (valid)
                    {
                        messageToUser = "The address is valid for Japan.";
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
                case "ES":
                    var _spainZipRegEx = @"^\d{5}$";
                    if (!Regex.Match(address.post_code, _spainZipRegEx).Success)
                    {
                        valid = false;
                        messageToUser += "Invalid post code. Post Code should bea  5 digit number.";
                    }
                    var _spainCityRegex = @"[a-zA-Z][a-zA-Z]$";
                    if (!Regex.Match(address.city, _spainCityRegex).Success)
                    {
                        valid = false;
                        messageToUser += "Invalid city or locality. Please provide a valid word for city or locality.";
                    }
                    if (valid)
                    {
                        messageToUser = "The address is valid for Spain.";
                    }

                    break;


                case "US":
                    var _usZipRegEx = @"^\d{5}$";
                    if (!Regex.Match(address.post_code, _usZipRegEx).Success)
                    {
                        valid = false;
                        messageToUser += "Invalid post code. Post Code should bea  5 digit number.";
                    }
                    var _usCityRegEx = @"[a-zA-Z][a-zA-Z]$";
                    if (!Regex.Match(address.city, _usCityRegEx).Success)
                    {
                        valid = false;
                        messageToUser += "Invalid city.Please provide a valid word for city.";
                    }
                    if (valid)
                    {
                        messageToUser = "The address is valid for US.";
                    }

                    break;


                case "GB":
                    var _ukZipRegEx = @"^([A-PR-UWYZ0-9][A-HK-Y0-9][AEHMNPRTVXY0-9]?[ABEHMNPRVWXY0-9]? {1,2}[0-9][ABD-HJLN-UW-Z]{2}|GIR 0AA)$";
                    if (!Regex.Match(address.post_code, _ukZipRegEx).Success)
                    {
                        valid = false;
                        messageToUser += "Invalid post code. Post Code should have two parts separated by a space. ";
                    }
                    var _ukCityRegEx = @"[a-zA-Z][a-zA-Z]$";
                    if (!Regex.Match(address.city, _ukCityRegEx).Success)
                    {
                        valid = false;
                        messageToUser += "Invalid city.Please provide a valid word for postal town.";
                    }
                    if (valid)
                    {
                        messageToUser = "The address is valid for UK.";
                    }

                    break;
            }

            return messageToUser;
        }
    }
}
