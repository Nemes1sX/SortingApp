using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SortingApp.Models.Dtos;
using SortingApp.Services;
using System.Linq.Expressions;

namespace SortingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SortingController : ControllerBase
    {
        private readonly ISortingService _sortingService;
        public SortingController(ISortingService sortingService)
        {
            _sortingService = sortingService;
        }

        [HttpGet]
        [Route("bubble")]
        public IActionResult Bubble()
        {
           var sortedArrays = new List<SortingDto>();
           sortedArrays.Add(_sortingService.BubbleSort());
           sortedArrays.Add(_sortingService.LinqSort());
           sortedArrays.Add(_sortingService.QuickSort());

          if (!sortedArrays.Any())
            {
                return NotFound();
            }

            return Ok(new { sortedArrays });
        }
    }
}
