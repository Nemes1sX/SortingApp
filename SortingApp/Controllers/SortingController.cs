using Microsoft.AspNetCore.Mvc;
using SortingApp.Models.FormRequest.CustomRules;
using SortingApp.Services;
using System.ComponentModel.DataAnnotations;

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

        [HttpPost]
        [Route("sort")]
        public async Task<IActionResult> SortArray([FromBody][Required][TwoInteger] int[] inputArray)
        {
            var sortingList = await _sortingService.SortingListAsync(inputArray);

            if (!sortingList.Any())
            {
                return NotFound();
            }

            return Ok(new { sortingList });
        }

        [HttpGet]
        [Route("load")]
        public async Task<IActionResult> Load()
        {
            var loadedArrayText = await _sortingService.LoadSortedArrayAsync();
            if(loadedArrayText.Length == 0 || loadedArrayText == null)
            {
                return NotFound(new {message = "Not found"});
            }

            return Ok(new { array = loadedArrayText });
        }
    }
}
