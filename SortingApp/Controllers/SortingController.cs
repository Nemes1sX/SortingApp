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
        public async Task<IActionResult> Bubble()
        {
            int[] numberArray = { 6, 8, 1, 5, 4, 12, 3, 34 };
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
