using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASP.NET_Web_API.Models;

namespace ASP.NET_Web_API.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {

        private static List<Category> categories = new List<Category>();

        //get all categories
        [HttpGet]
        public IActionResult GetCategories(string searchValue)
        {
            Console.WriteLine($"Search Value: {searchValue}");

            if (string.IsNullOrEmpty(searchValue))
            {
                return Ok(new
                {
                    status = 200,
                    data = categories
                });
            }
            var result = categories.Where(c => !string.IsNullOrEmpty(c.Name) &&
                                                c.Name.Contains(searchValue, StringComparison.OrdinalIgnoreCase));

            return Ok(new
            {
                status = 200,
                data = result.ToList()
            });
        }

    }
}