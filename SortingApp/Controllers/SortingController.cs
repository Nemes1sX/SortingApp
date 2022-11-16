using Microsoft.AspNetCore.Mvc;
using SortingApp.Services;

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
        [Route("sort")]
        public async Task<IActionResult> Bubble(string inputArray)
        {
            int[] numberArray = inputArray.Split(",").Select(int.Parse).ToArray();
            var sortingList = await _sortingService.SortingListAsync(numberArray);

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
